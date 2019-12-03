using System;
using System.Collections.Generic;
using System.Threading;

namespace IMDBDatabase
{
	class Program
	{
		static void Main(string[] args)
		{
			ThomasDebug(args);
		}

		private static void ThomasDebug(string[] args)
		{
			IInterface ui = new ConsoleInterface();

			//Database db = new Database();


			IReadable[] readable= new IReadable[10];

			for (int i = 0; i < 10; i++)
				readable[i] = (new Title(999999, new Rating(430, 4.2f),
					"This is a debug movie titleeafeeeffawfefjkhgfjhwgjwgefjhksdbvhjgfbgv;ljfdvndfbsghkfdbgka;dgdbfs;dfgdfjb"
					, "TvMiniSeries", "Action, Adventure", true, "1970", "1970"));

			ui.ShowMenu();
			ui.ShowSearchResult(readable);
			//ui.ShowSearchResult(db.SearchName("Alien"));

			Console.ReadKey();
		}
	}
}
