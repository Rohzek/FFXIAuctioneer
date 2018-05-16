namespace Auctioneer.Database
{
    public class DeliveryBoxItem
    {
        public uint CharID { get; set; }
        public string CharName { get; set; }
        public bool Box { get; set; }
        public ushort Slot { get; set; }
        public ushort ItemID { get; set; }
        public ushort ItemSubID { get; set; }
        public uint Quantity { get; set; }
        public uint SenderID { get; set; }
        public string Sender { get; set; }
        public bool Recieved { get; set; }
        public bool Sent { get; set; }

        public DeliveryBoxItem()
        {
            CharID = 0;
            CharName = "";
            Box = true;
            Slot = 0;
            ItemID = 65535; // Gil
            ItemSubID = 656; // ? Is Gil
            Quantity = 0;
            SenderID = 0;
            Sender = "AuctionMoogle";
            Recieved = false;
            Sent = false;
        }
    }
}
