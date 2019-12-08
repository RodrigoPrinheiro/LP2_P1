using System;

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
			ConsoleInterface ui = new ConsoleInterface();
            IReadable info = new Title(202, new Rating(2, 4.2f)
                , "Fromage",
                "tvSeries",
                "Action, Drama, Fantasy",
                false,
                "2102", "");
            //Database db = new Database();
            //ui.ShowTitleSearchResult(db.SearchName("Star wars"));
            //ui.ShowDetailedTitleInfo(info);
            ui.RenderStartMenu();
            Console.ReadKey();
		}
	}
}
