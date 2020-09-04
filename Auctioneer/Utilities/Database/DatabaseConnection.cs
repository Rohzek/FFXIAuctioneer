using Auctioneer.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Auctioneer.Database
{
	public class DatabaseConnection
	{
		List<AuctionItem> results, sold;

		public MySqlConnection Connect()
		{
			MySqlConnection conn = null;

			try
			{
				return conn = new MySqlConnection(Settings.Connection);
			}
			catch(MySqlException ex)
			{
				Console.WriteLine(ex.StackTrace);
			}

			return conn;
		}

		public void Query()
		{
			var conn = Connect();
			conn.Open();

			results = new List<AuctionItem>();
			sold = new List<AuctionItem>();

			MySqlCommand command = conn.CreateCommand();
			command.CommandText = $"SELECT * FROM auction_house;";

			MySqlDataReader reader = command.ExecuteReader();

			while(reader.Read())
			{
				// 10 fields, we know that.
				var item = new AuctionItem();

				item.Id = (uint)reader.GetValue(0);
				item.ItemID = (ushort)reader.GetValue(1);
				item.Stack = ((bool)reader.GetValue(2) == false) ? 0 : 1;
				item.Seller = (uint)reader.GetValue(3);
				item.SellerName = (string)reader.GetValue(4);
				item.Date = (uint)reader.GetValue(5);
				item.Price = (uint)reader.GetValue(6);
				item.BuyerName = (reader.GetValue(7) == DBNull.Value ? "" : (string)reader.GetValue(7));
				item.Sale = (uint)reader.GetValue(8);
				item.SaleDate = (uint)reader.GetValue(9);

				if(item.SaleDate > 0)
				{
					sold.Add(item);
				}
				else
				{
					results.Add(item);
				}
			}

			reader.Close();
			conn.Close();
		}

		public List<AuctionItem> GetUnsold()
		{
			return results;
		}

		public List<AuctionItem> GetSold()
		{
			return sold;
		}

		public void AddItem(AuctionItem item)
		{
			var conn = Connect();
			conn.Open();
			MySqlCommand command = conn.CreateCommand();
			command.CommandText = $"INSERT INTO {item.Table} (itemid, stack, seller, seller_name, date, buyer_name, sale, sell_date) " +
					      $"values('{item.ItemID}','{item.Stack}','{item.Seller}','{item.SellerName}','{item.Date}','{item.BuyerName}','{item.Sale}','{item.SaleDate}');";
			command.ExecuteNonQueryAsync();
			conn.Close();
		}

		public void BuyItem(AuctionItem item)
		{
			var conn = Connect();
			conn.Open();

			// Set item to sold
			MySqlCommand buy = conn.CreateCommand();
			buy.CommandText = $"UPDATE {item.Table} SET buyer_name=\"AuctionMoogle\", sale={item.Price}, sell_date={TimeObject.Convert()} WHERE id={item.Id};";
			buy.ExecuteNonQueryAsync();
			buy.Dispose();

			conn.Close();

			// Alert console
			Console.WriteLine($"Buying {item.ItemID} from {item.SellerName} for {item.Price}");

			// Send money
			var itemToBuy = new DeliveryBoxItem();
			itemToBuy.CharID = item.Seller;
			itemToBuy.Quantity = item.Price;

			var con = Connect();
			con.Open();

			MySqlCommand pay = con.CreateCommand();
			pay.CommandText = $"INSERT INTO {itemToBuy.Table} (charid, box, itemid, itemsubid, quantity, sender) " +
					      $"values('{itemToBuy.CharID}','{itemToBuy.Box}','{itemToBuy.ItemID}','{itemToBuy.ItemSubID}','{itemToBuy.Quantity}','{itemToBuy.Sender}');";
			Console.WriteLine($"Giving {itemToBuy.Quantity} Gil to {itemToBuy.CharID}");
			pay.ExecuteNonQueryAsync();
			pay.Dispose();

			con.Close();
		}
	}
}
