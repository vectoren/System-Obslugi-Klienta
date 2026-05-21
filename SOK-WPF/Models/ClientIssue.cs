using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.Models
{
    public class ClientIssue
    {
        public int Id { get; set; }

        public int TopicId { get; set; }

        public int ClientId { get; set; }

        public int OrderId { get; set; }

        public DateOnly IssuedOn { get; set; }
        public int[]? ProductsIds { get; set; }
        public string? Description { get; set; }
        public string? Expectations { get; set; }
        
    }
}
