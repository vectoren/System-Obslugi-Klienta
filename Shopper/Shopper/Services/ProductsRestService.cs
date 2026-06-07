using Shopper.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Shopper.Services
{
    public static class ProductsRestService
    {
        private static HttpClient httpClient = DBRestService.httpClient;
        private static readonly string baseUrl = "http://10.0.2.2:8080/api/{0}";

        public static async Task<int> AddNewOrder(Orders order)
        {
            try
            {
                Uri uri = new Uri(string.Format(baseUrl, "orders"));
                var json = JsonSerializer.Serialize(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var orderId = JsonSerializer.Deserialize<int>(jsonResponse);
                    return orderId;
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    throw new Exception(errorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching products: {ex.Message}");
                return 0;
            }
        }

        public static async Task<(string, bool)> AddNewDeliveryDetails(DeliveryDetails dd)
        {
            try
            {
                Uri uri = new Uri(string.Format(baseUrl, "delivery-details"));
                var json = JsonSerializer.Serialize(dd);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return ("Delivery details added successfully", true);
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }

        }
        public static async Task<(string, bool)> AddNewPaymentDetails(PaymentDetails pd)
        {
            try
            {
                Uri uri = new Uri(string.Format(baseUrl, "payment-details"));
                var json = JsonSerializer.Serialize(pd);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    return ("Payment details added successfully", true);
                }
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(errorMessage);
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
