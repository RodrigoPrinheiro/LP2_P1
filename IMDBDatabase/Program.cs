using System;

namespace IMDBDatabase
{
	class Program
	{
		static void Main(string[] args)
		{
            //StringTest();
			ThomasDebug(args);
		}

        private static void StringTest()
        {
            string s = "Sci-Fi";
            string e = "Film-Noir";

            string f = s.Replace("-", "");

            Title t = new Title(new Rating(21, 213)
                , "no", "movie", $"{s}, {e}", true, "2023", "2324");


            Console.WriteLine(t.GetDetailedInfo());
        }

		private static void ThomasDebug(string[] args)
		{
			int userMenuDecision = 0;
			string userSearch = "";

			IInterface ui = new ConsoleInterface();
            IReadable info = new Title(new Rating(2, 4.2f)
                , "Fromage",
                "tvSeries",
                "Action, Drama, Fantasy",
                false,
                "2102", "");

			Database db = new Database();

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
						if (userSearch == null) break;
						//ui.ShowTitleSearchResult(db.SearchName(userSearch));
						break;
					// Type
					case 1:
						userSearch = ui.RenderSearchBar("Type");
						if (userSearch == null) break;
						// db.search(userMenuDecision)
						break;
					// Type of content
					case 2:
						bool? adult;
						adult = ui.RenderContentChoice();
						if (userSearch == null) break;
						//ui.ShowTitleSearchResult(db.SearchContent((bool)adult));
						break;
					// Score
					case 3:
						userSearch = ui.RenderSearchBar("Score");
						if (userSearch == null) break;
						// db.search(userMenuDecision)
						break;
					// Votes
					case 4:
						userSearch = ui.RenderSearchBar("Votes");
						if (userSearch == null) break;
						// db.search(userMenuDecision)
						break;
					// Year
					case 5:
						userSearch = ui.RenderSearchBar("Year");
						if (userSearch == null) break;
						// db.search(userMenuDecision)
						break;
					// Genres
					case 6:
						userSearch = ui.RenderSearchBar(
							"Genres - Split by spaces");
						if (userSearch == null) break;
						//ui.ShowTitleSearchResult(db.SearchGenres(userSearch.Split(' ')));
						break;
					// People
					case 7:
						userSearch = ui.RenderSearchBar("People");
						if (userSearch == null) break;
						// db.search(userMenuDecision)
						break;
				}
			}
		}
	}
}
