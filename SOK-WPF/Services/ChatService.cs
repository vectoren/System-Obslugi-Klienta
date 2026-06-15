using SOK_WPF.Models;
using SOK_WPF.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WebSocket4Net;

public static class ChatService
{
    private static WebSocket _webSocket;
    private static readonly string _serverUrl = "ws://localhost:8080/chat/websocket";

    public static event Action<ChatMessage> OnMessageReceived;

    // TaskCompletionSource pozwoli nam zaczekać z kodem dopóki Spring nie powie "CONNECTED"
    private static TaskCompletionSource<bool> _stompConnectionTask;

    public static async Task<bool> ConnectWithCookiesAsync()
    {
        // Jeśli już jesteśmy połączeni, nie rób nic
        if (_webSocket != null && _webSocket.State == WebSocketState.Open) return true;

        _stompConnectionTask = new TaskCompletionSource<bool>();

        try
        {
            var webSocketCookies = new List<KeyValuePair<string, string>>();

            if (RestService.cookieContainer != null)
            {
                var cookies = RestService.cookieContainer.GetCookies(new Uri("http://localhost:8080/api/login"));
                foreach (Cookie cookie in cookies)
                {
                    webSocketCookies.Add(new KeyValuePair<string, string>(cookie.Name, cookie.Value));
                }
            }

            _webSocket = new WebSocket(_serverUrl, subProtocol: "", cookies: webSocketCookies);

            _webSocket.Opened += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("[STOMP] Handshake WebSocket OK. Wysyłam CONNECT...");
                // Krok 1: Tylko CONNECT
                string connectFrame = "CONNECT\naccept-version:1.1,1.2\nheart-beat:10000,10000\n\n\u0000";
                _webSocket.Send(connectFrame);
            };

            _webSocket.MessageReceived += (sender, e) =>
            {
                string text = e.Message;
                if (string.IsNullOrEmpty(text)) return;

                // Krok 2: Serwer odpowiedział CONNECTED -> teraz możemy subskrybować
                if (text.StartsWith("CONNECTED"))
                {
                    System.Diagnostics.Debug.WriteLine("[STOMP] Odebrano CONNECTED. Rejestruję subskrypcję...");
                    string subscribeFrame = "SUBSCRIBE\nid:sub-0\ndestination:/topic/public\n\n\u0000";
                    _webSocket.Send(subscribeFrame);

                    _stompConnectionTask.TrySetResult(true); // Odblokowujemy oczekujący wątek
                    return;
                }

                // Krok 3: Obsługa standardowych wiadomości czatu
                if (text.StartsWith("MESSAGE"))
                {
                    int bodyStartIndex = text.IndexOf("\n\n");
                    if (bodyStartIndex != -1)
                    {
                        string jsonBody = text.Substring(bodyStartIndex + 2).TrimEnd('\0');
                        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                        var chatMessage = JsonSerializer.Deserialize<ChatMessage>(jsonBody, options);

                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            OnMessageReceived?.Invoke(chatMessage);
                        });
                    }
                }
            };

            _webSocket.Error += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine($"Błąd WebSocket: {e.Exception?.Message}");
                _stompConnectionTask.TrySetResult(false);
            };

            _webSocket.Closed += (sender, e) =>
            {
                System.Diagnostics.Debug.WriteLine("Połączenie WebSocket zamknięte.");
            };

            _webSocket.Open();

            // Czekamy asynchronicznie max 5 sekund na ramkę CONNECTED ze Springa
            var delayTask = Task.Delay(5000);
            var completedTask = await Task.WhenAny(_stompConnectionTask.Task, delayTask);

            if (completedTask == delayTask)
            {
                System.Diagnostics.Debug.WriteLine("Timeout: Serwer nie odpowiedział CONNECTED w ciągu 5s.");
                return false;
            }

            return await _stompConnectionTask.Task;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Błąd podczas nawiązywania połączenia: {ex.Message}");
            return false;
        }
    }

    public static void SendMessage(ChatMessage message)
    {
        if (_webSocket != null && _webSocket.State == WebSocketState.Open)
        {
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            string jsonBody = JsonSerializer.Serialize(message, options);
            string destination = "/app/chat.sendMessage";

            string sendFrame = $"SEND\ndestination:{destination}\ncontent-type:application/json\n\n{jsonBody}\u0000";
            _webSocket.Send(sendFrame);
        }
        else
        {
            MessageBox.Show("Brak aktywnego połączenia z serwerem czatu!");
        }
    }
}