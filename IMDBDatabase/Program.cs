/// @file
/// @brief This is the main file that runs the program.
/// 
/// @author Tomás Franco e Rodrigo Pinheiro
/// @date 2019

namespace IMDBDatabase
{
	/// <summary>
	/// Connects everything and runs the program
	/// </summary>
	class Program
	{
		/// <summary>
		/// Main method
		/// </summary>
		/// <param name="args">User passed arguments</param>
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
			// Initialize variables
			int userMenuDecision = 0;
			string userSearch = "";
			IInterface ui = new ConsoleInterface();
			Database db = new Database();

			// Selection loop
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
					// Detailed Search
					case 2:
						ui.RenderAdvancedSearch(db);
						break;
				}
			}
		}
	}
}
