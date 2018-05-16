using Auctioneer.Util;

namespace Auctioneer.Database
{
    /**
     * As I don't know why Date in the database was stored as an Int,
     * I don't know how the int is meant to be calculated, but
     * I went with something that seems to give a similar
     * number to items added buy the game.
     * 
     * See Util/TimeObject.cs to see how it works.
     */
    public class AuctionItem
    {
        public string Table { get; } = "auction_house";
        public uint Id { get; set; }
        public ushort ItemID { get; set; }
        public int Stack { get; set; }
        public uint Seller { get; set; }
        public string SellerName { get; set; }
        public uint Date { get; set; }
        public uint Price { get; set; }
        public string BuyerName { get; set; }
        public uint Sale { get; set; }
        public uint SaleDate { get; set; }

        public AuctionItem()
        {
            ItemID = 0;
            Stack = 0;
            Seller = 0;
            SellerName = "AuctionMoogle";
            Date = TimeObject.Convert();
            Price = 0;
            Sale = 0;
            SaleDate = TimeObject.Convert();
        }
    }
}
