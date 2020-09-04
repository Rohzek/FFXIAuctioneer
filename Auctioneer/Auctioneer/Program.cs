using System;
using System.Timers;
using System.Threading.Tasks;
using Auctioneer.Util;
using Auctioneer.Auction;

namespace Auctioneer
{
	class Program
	{
		static Timer timer;
		static Auctioner ac = new Auctioner();

		public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

		public async Task MainAsync()
		{
			Console.WriteLine("\nThe auctioneer bot will loop to do a check every one minute, forever.");
			Console.WriteLine("Type \'q\' to quit program.\n\n");

			Settings.Init();

			ac.Buy(); // Run once immediatly, before we schedule it.

			SetupTimer();

			if(Console.Read() == 'q')
			{
				Environment.Exit(0);
			}

			await Task.Delay(-1); // This will "loop" main forever
		}

		static void SetupTimer()
		{
			timer = new Timer();
			timer.Elapsed += new ElapsedEventHandler(OnTimerEvent);
			// Interval is in miliseconds, but we asked for seconds in settings.json
			timer.Interval = Settings.GetRunSettings().RunInterval * 1000;
			timer.Enabled = true;
		}

		/**
		 * Should watch for items to buy and sell
		 */
		public static void OnTimerEvent(object source, ElapsedEventArgs e)
		{
			//Console.WriteLine("It's been a minute.");
			ac.Buy();
			ac.Sell();
		}
	}
}