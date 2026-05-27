using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.Models
{
    public class ClientIssue
    {
        public int Id { get; set; }

        public string IssueTopic { get; set; }

        public int ClientId { get; set; }
        public string ClientMail { get; set; }

        public int OrderId { get; set; }
        public string IssueStatus { get; set; }

        public DateOnly IssuedOn { get; set; }
        public List<Product> AffectedProducts { get; set; }
        public string? Description { get; set; }
        public string? Expectations { get; set; }
        
    }
}
