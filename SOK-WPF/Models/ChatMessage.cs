using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace SOK_WPF.Models
{
    public class ChatMessage
    {
        public string message { get; set; }
        public string sendDate { get; set; }
        public int sender { get; set; }
        public int recipient { get; set; }
        [JsonIgnore]
        public Account? user { get; set; }
        public ChatMessage() { }

    }
}
