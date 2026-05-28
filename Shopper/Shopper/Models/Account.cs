using System;
using System.Collections.Generic;
using System.Text;

namespace Shopper.Models
{
    public class Account
    {
        //fnaem, lname, email, password, registerdate
        //public int id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public string email { get; set; }
        public string password { get; set; }

        public Account() { }
        public Account(string fname, string lname, string email, string password)
        {
            //this.id = id;
            this.fname = fname;
            this.lname = lname;
            this.email = email;
            this.password = password;
        }


    }
}
