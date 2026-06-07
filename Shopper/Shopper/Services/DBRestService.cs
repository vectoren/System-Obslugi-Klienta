using Shopper.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Shopper.Services
{
    public static class DBRestService
    {
        public static HttpClient httpClient;
        private static CookieContainer cookieContainer;
        private static readonly string baseUrl = "http://10.0.2.2:8080/api{0}";

        private static void GetClient()
        {
            if (httpClient is null)
            {
                cookieContainer = new CookieContainer();
                var handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true
                };
                httpClient = new HttpClient(handler);

            }
            return;
        }

        public async static Task<(string, bool)> RegisterUser(Account account)
        {
            try
            {
                GetClient();
                account.userId = null;
                Uri uri = new Uri(string.Format(baseUrl, "/register"));
                var json = JsonSerializer.Serialize(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    return ("SUCCESS", true);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }


            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }

        }

        public async static Task<(string, bool)> Login(string email, string password)
        {
            try
            {
                GetClient();
                Uri uri = new Uri(string.Format(baseUrl, "/login"));

                var keyValuePairs = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("username",email),
                    new KeyValuePair<string, string>("password", password)
                };

                var content = new FormUrlEncodedContent(keyValuePairs);

                var response = await httpClient.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    var account = JsonSerializer.Deserialize<Account>(await response.Content.ReadAsStringAsync());
                    await SecureStorage.Default.SetAsync("currentUser", JsonSerializer.Serialize(account));
                    return ("SUCCESS", true);
                }
                throw new Exception(await response.Content.ReadAsStringAsync());    

            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
        public static async Task Logout()
        {
            try
            {
                httpClient = null!;
                cookieContainer = null!; // Wydaje mi sie ze jest to nie potrzebne, ale na wypadek zostawie
                SecureStorage.RemoveAll();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during logout: {ex.Message}");
            }
        }

        //Dorobic na backendzie
        public static async Task<(string, bool)> DeleteUser(int? id)
        {
            try
            {
                GetClient();
                Uri uri = new Uri(string.Format(baseUrl, $"/{id}/delete"));
                var response = await httpClient.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    await Logout();
                    return ("SUCCESS", true);
                }
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
        public static async Task<(string, bool)> UpdateUser(int? id, Account user)
        {
            //naprawić hasło
            try
            {
                GetClient();
                Uri uri = new Uri(string.Format(baseUrl, $"/{id}/update"));
                var json = JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var account = JsonSerializer.Deserialize<Account>(await response.Content.ReadAsStringAsync());
                    await SecureStorage.Default.SetAsync("currentUser", JsonSerializer.Serialize(account));
                    return ("SUCCESS", true);
                }
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
