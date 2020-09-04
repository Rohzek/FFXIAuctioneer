using Auctioneer.Database;
using System;
using System.Collections.Generic;

namespace Auctioneer.Auction
{
	public class Auctioner
	{
		DatabaseConnection conn;
		Random random;

		public Auctioner()
		{
			conn = new DatabaseConnection();
			random = new Random();
		}

		public void Buy()
		{
			Console.WriteLine("Running Auctioner.Buy");
			conn.Query();
			CompareBuy(conn.GetUnsold(), conn.GetSold());
		}

		public void Sell() 
		{
			// Get data from https://www.ffxiah.com/browse
			Console.WriteLine("Running Auctioner.Sell");
		}

		public void CompareBuy(List<AuctionItem> unsold, List<AuctionItem> sold)
		{
			foreach(var item in unsold) // Checks items for sale
			{
				foreach(var nitem in sold) // Check against the previously sold items prices, so that you can't game the system for millions of gil
				{
					if(item.ItemID.Equals(nitem.ItemID) && item.Price <= nitem.Sale)
					{
						var r = random.Next(100) + 1; // Random number between 1 and 100

						if(r > 49 && r < 75) // 25% chance of the item selling this time.
						{
							conn.BuyItem(item);
						}
					}
				}
			}
		}
	}
}