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
        private static HttpClient httpClient;
        private static CookieContainer cookieContainer;
        private static readonly string baseUrl = "http://10.0.2.2:8080/api{0}";

        private static void GetClient()
        {
            if(httpClient is null)
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

        public async static Task<(Account?, string, bool)> RegisterUser(Account account)
        {
            try
            {
                GetClient();

                Uri uri = new Uri(string.Format(baseUrl, "/register"));
                var json = JsonSerializer.Serialize(account);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(uri, content);

                if(response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync();
                    var a = JsonSerializer.Deserialize<Account>(body);
                    a.accountCreationDate = DateTime.Now.ToString("yyyy-MM-dd");
                    return (a,"Success", true);
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }


            }
            catch(Exception ex) 
            {
                return (null,ex.Message, false);
            }

        }

        public async static Task<bool> Login(string email, string password)
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
                    return true;
                }
                throw new Exception(await response.Content.ReadAsStringAsync());

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
