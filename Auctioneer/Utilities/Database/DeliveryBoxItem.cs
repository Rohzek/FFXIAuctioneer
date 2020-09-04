namespace Auctioneer.Database
{
    public class DeliveryBoxItem
    {
        public string Table { get; } = "delivery_box";
        public uint CharID { get; set; }
        public string CharName { get; set; }
        public int Box { get; set; }
        public ushort Slot { get; set; }
        public ushort ItemID { get; set; }
        public ushort ItemSubID { get; set; }
        public uint Quantity { get; set; }
        public uint SenderID { get; set; }
        public string Sender { get; set; }
        public int Recieved { get; set; }
        public int Sent { get; set; }

        public DeliveryBoxItem()
        {
            CharID = 0;
            CharName = "";
            Box = 1;
            Slot = 0;
            ItemID = 65535; // Gil
            ItemSubID = 656; // ? Is Gil
            Quantity = 0;
            SenderID = 0;
            Sender = "AuctionMoogle";
            Recieved = 0;
            Sent = 0;
        }

        override public string ToString()
        {
            return $"{CharID}, {CharName}, {Box}, {Slot}, {ItemID}, {ItemSubID}, {Quantity}, {SenderID}, {Sender}, {Recieved}, {Sent}";
        }
    }
}
