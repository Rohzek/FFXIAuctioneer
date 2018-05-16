using System;
using System.Collections.Generic;
using System.Text;

namespace Auctioneer.Util
{
    public class DatabaseSettings
    {
        public string Ip { get; set; }
        public string Port { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }

        public DatabaseSettings()
        {
            Ip = "127.0.0.1";
            Port = "3306";
            User = "root";
            Password = "password";
            Database = "dspdb";
        }
    }
}
