using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.Models
{
    public class ClientIssue
    {
        public int warningId { get; set; }

        public string issueTopic { get; set; }

        public string issueStatus { get; set; }

        public DateOnly recivedDate { get; set; }
        public string? affectedProducts { get; set; }
        public string? description { get; set; }
        public string? expectations { get; set; }


    }
}
