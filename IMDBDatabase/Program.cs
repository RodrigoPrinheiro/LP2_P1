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
			int userMenuDecision = 0;
			string userSearch = "";

			IInterface ui = new ConsoleInterface();
            IReadable info = new Title(202, new Rating(2, 4.2f)
                , "Fromage",
                "tvSeries",
                "Action, Drama, Fantasy",
                false,
                "2102", "");
			//Database db = new Database();
			//ui.ShowTitleSearchResult(db.SearchName("Star wars"));
			//ui.ShowDetailedTitleInfo(info);

			while (true)
			{
				ui.RenderStartMenu(out userMenuDecision);

				if (userMenuDecision < 0) break;

				// Check user choice
				switch (userMenuDecision)
				{
					// Name
					case 0:
						userSearch = ui.RenderSearchBar("Name");
						// if (userMenuDecision != null)
						//		db.search(userMenuDecision)
						break;
					// Type
					case 1:
						userSearch = ui.RenderSearchBar("Type");
						// db.search(userMenuDecision)
						break;
					// Type of content
					case 2:
						userSearch = ui.RenderSearchBar("Content");
						// db.search(userMenuDecision)
						break;
					// Score
					case 3:
						userSearch = ui.RenderSearchBar("Score");
						// db.search(userMenuDecision)
						break;
					// Votes
					case 4:
						userSearch = ui.RenderSearchBar("Votes");
						// db.search(userMenuDecision)
						break;
					// Year
					case 5:
						userSearch = ui.RenderSearchBar("Year");
						// db.search(userMenuDecision)
						break;
					// Genres
					case 6:
						userSearch = ui.RenderSearchBar("Genres");
						// db.search(userMenuDecision)
						break;
					// People
					case 7:
						userSearch = ui.RenderSearchBar("People");
						// db.search(userMenuDecision)
						break;
				}
			}

			Console.Clear();
		}
	}
}
