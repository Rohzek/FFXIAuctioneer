using System;
using System.Collections.Generic;
using System.Text;

namespace Auctioneer.Util
{
    public static class TimeObject
    {
        public static DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0);

        public static uint Convert()
        {
            var output = checked((uint)Math.Round((DateTime.Now - Epoch).TotalSeconds));
            //Console.WriteLine($"Should be setting date as: {output}");
            return output;
        }

        public static uint Convert(DateTime date)
        {
            return checked((uint)Math.Round((date - Epoch).TotalSeconds));
        }
    }
}
