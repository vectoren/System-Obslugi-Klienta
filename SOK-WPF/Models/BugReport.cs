using System;
using System.Collections.Generic;
using System.Text;

namespace SOK_WPF.Models
{
    public class BugReport
    {
        public int bugId {  get; set; }
        public string bugName { get; set; }
        public string bugDescription { get; set; }
        public DateOnly reportDate { get; set; }
    }
}
