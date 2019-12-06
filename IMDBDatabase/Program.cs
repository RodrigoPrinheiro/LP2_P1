using System;
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

			Database db = new Database();

			ui.ShowSearchResult(db.SearchName("baguette"));
		}
	}
}
