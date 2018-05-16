using System;
using System.Collections.Generic;
using System.Text;

namespace Auctioneer.Util
{
    public class RunSettings
    {
        public string RunIntervalDescription { get; set; }
        public long RunInterval { get; set; }

        public RunSettings()
        {
            RunIntervalDescription = "RunInterval is the time (in seconds) to wait between runs";
            // 1 minute, in ms
            RunInterval = 60;
        }
    }
}
