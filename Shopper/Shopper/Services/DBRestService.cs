using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Shopper.Services
{
    public static class DBRestService
    {
        private static HttpClient httpClient;
        private static CookieContainer cookieContainer;

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
            }
        }
    }
}
