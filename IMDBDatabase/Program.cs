using System;

namespace IMDBDatabase
{
	class Program
	{

		static void Main(string[] args)
		{
			Program prog = new Program();

			prog.Start();
		}

		/// <summary>
		/// Main program loop
		/// </summary>
		private void Start()
		{
			int userMenuDecision = 0;
			string userSearch = "";
			IInterface ui = new ConsoleInterface();
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
						userSearch = ui.RenderSearchBar("Search by: Name");
						if (userSearch == null) break;
						ui.ShowTitleSearchResult(db.SearchName(userSearch));
						break;
					// Person
					case 1:
						userSearch = ui.RenderSearchBar("Search for: Persons");
						if (userSearch == null) break;
                        ui.ShowTitleSearchResult(
                            db.SearchPersonName(userSearch));
						break;
					// Detailed
					case 2:
						ui.RenderAdvancedSearch(db);
						break;
				}
			}
		}
	}
}
