using Auctioneer.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Auctioneer.Database
{
    public class DatabaseConnection
    {
        MySqlConnection conn;

        public void Connect()
        {
            try
            {
                conn = new MySqlConnection(Settings.Connection);
                conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
        }

        public void Disconnect()
        {
            conn.Close();
        }

        public List<string> Query(string text)
        {
            List<string> results = new List<string>();
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = text;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    results.Add(reader.GetName(i));
                }
            }

            return results;
        }

        public void Insert()
        {

        }

        public void Update()
        {

        }

        
    }
}
