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

			string[] strings = new string[]
			{
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles",
				"AAAAAA\ttrue\thello\tyes\tno\tleles\taaaaaaa",
			};

			ui.ShowMenu();
			ui.ShowSearchResult(strings);

			Console.ReadKey();
			//Database db = new Database();

			//ui.ShowSearchResult(db.SearchName("The Force Awa"));
		}
	}
}
