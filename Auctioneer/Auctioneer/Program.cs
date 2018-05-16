using System;
using System.Timers;
using System.Threading.Tasks;
using Auctioneer.Util;

namespace Auctioneer
{
    class Program
    {
        static Timer timer;

        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        public async Task MainAsync()
        {
            Settings.Init();
            SetupTimer();

            Console.WriteLine("Type \'q\' to quit");

            if (Console.Read() == 'q')
            {
                Environment.Exit(0);
            }

            await Task.Delay(-1); // This will "loop" main forever
        }

        static void SetupTimer()
        {
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(OnTimerEvent);
            timer.Interval = Settings.GetRunSettings().RunInterval * 1000; // Interval is in miliseconds
            timer.Enabled = true;
        }

        /**
         * Should watch for items to buy and sell
         */
        public static void OnTimerEvent(object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Hello, World!");
        }
    }
}
