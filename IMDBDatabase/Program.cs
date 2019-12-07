using System;
using System.Threading;
using System.Linq;

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

            ui.ShowMsg("Number of TvSeries: " 
                + db.SearchType("tvseries").Count().ToString());
		}
	}
}
