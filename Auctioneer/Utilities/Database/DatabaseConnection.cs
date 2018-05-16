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

        public void AddItem(AuctionItem item)
        {
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO {item.Table} (itemid, stack, seller, seller_name, date, buyer_name, sale, sell_date) " +
                                  $"values('{item.ItemID}','{item.Stack}','{item.Seller}','{item.SellerName}','{item.Date}','{item.BuyerName}','{item.Sale}','{item.SaleDate}');";
            command.ExecuteNonQueryAsync();
        }

        public void BuyItem(AuctionItem item)
        {
            // Set item to sold
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"UPDATE {item.Table} SET buyer_name=\"AuctionMoogle\", sale={item.Price}, sell_date={TimeObject.Convert()};";
            command.ExecuteNonQueryAsync();

            // Send money
            var pay = new DeliveryBoxItem();
            pay.CharID = item.Seller;
            pay.Quantity = item.Price;
            Payment(pay);
        }

        public void Payment(DeliveryBoxItem item)
        {
            MySqlCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO {item.Table} (charid, box, itemid, itemsubid, quantity, sender) " +
                                  $"values('{item.CharID}','{item.Box}','{item.ItemID}','{item.ItemSubID}','{item.Quantity}','{item.Sender}');";
            command.ExecuteNonQueryAsync();
        }
    }
}
