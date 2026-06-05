using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Models
{
    public class Account
    {
        public int userId { get; set; }
        public string fullName { get => $"{firstName} {lastName}"; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string accountCreationDate { get; set; }

        public Account() { }
        public Account(string fname, string lname, string email, string password)
        {
            this.firstName = fname;
            this.lastName = lname;
            this.email = email;
            this.password = password;
        }

    }
}
