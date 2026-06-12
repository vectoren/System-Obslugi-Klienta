using SOK_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace SOK_WPF.Services
{
    public static class RestService
    {
        public static HttpClient httpClient;
        private static CookieContainer cookieContainer;
        public static Account? account;
        private static readonly string baseUrl = "http://localhost:8080/api{0}";
        public static readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static async Task InitializeAsync()
        {
            if (httpClient == null)
            {
                cookieContainer = new CookieContainer();
                var handler = new HttpClientHandler
                {
                    CookieContainer = cookieContainer,
                    UseCookies = true,
                };

                httpClient = new HttpClient(handler);
            }
        }

        #region HTTP Methods

        public async static Task<(string, bool)> Login(string email, string password)
        {
            try
            {
                await InitializeAsync();
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
                    var responseAccount = JsonSerializer.Deserialize<Account>(await response.Content.ReadAsStringAsync(), jsonOptions);
                    account = responseAccount;
                    return ("SUCCESS", true);
                }
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                return (ex.Message, true);

            }
        }



        public async static Task<List<Account>> GetActiveAdmins()
        {
            try
            {
                await InitializeAsync();
                Uri uri = new Uri(string.Format(baseUrl, "/active-admins"));
                var response = await httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var responseAccounts = JsonSerializer.Deserialize<List<Account>>(await response.Content.ReadAsStringAsync(), jsonOptions);
                    return new List<Account>(responseAccounts);
                }
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
            catch
            {
                return new List<Account>()
                {
                    new Account { userId = 1, firstName = "Jan", lastName = "Kowalski", email = "jan@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 2, firstName = "Anna", lastName = "Nowak", email = "anna@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 3, firstName = "Piotr", lastName = "Zieliński", email = "piotr@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 4, firstName = "Maria", lastName = "Wójcik", email = "maria@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 5, firstName = "Krzysztof", lastName = "Wiśniewski", email = "krzysztof@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 6, firstName = "Agnieszka", lastName = "Kaczmarek", email = "agnieszka@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 7, firstName = "Tomasz", lastName = "Mazur", email = "tomasz@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") },
                    new Account { userId = 8, firstName = "Ewa", lastName = "Michalska", email = "ewa@test.pl", password = "password", accountCreationDate = DateTime.UtcNow.ToString("o") }
                };
            }
        }


        public async static Task<List<Dictionary<string, string>>> GetChatHistory(Account? Acc)
        {

            if (Acc != null)
            {
                return new()
                {
                    new(){
                        { "user", $"{Acc.fullName}"} ,
                        { "content", "Cześć, masz już gotowy ten projekt?"} ,
                        { "user1", "Tak, jeśli o to chodzi..."}
                    },
                    new(){
                        { "user", "Current User"} ,
                        { "content", "Tak, jeśli o to chodzi..."} ,
                    },
                    new(){
                        { "user", $"{Acc.fullName}"} ,
                        { "content", "Nevermind, wszystko rozumiem..."}
                    }
                };
            }
            else
                return new();
        }




        #endregion

    }
}
