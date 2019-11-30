using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
	static class DataParser
	{
		/* 
		 * ####################################################################
		 *							###	TO-DO ###
		 * - Return the headers
		 * - Split stuff using the extracted headers
		 * - Store everything
		 * ####################################################################
		*/

		public static string[] Parse(string data)
		{
			Console.WriteLine("Parsing...");

			string[] lines = data.Split('\n');

			string[] headers = GetHeaders(data);

			Console.WriteLine("Parsing complete!");
			return lines;
		}

		private static string[] GetHeaders(string data)
		{
			string words = "";
			string singleWord = "";

			string[] headers = null;

			foreach (char c in data)
			{
				singleWord += c;

				if (c == '\t')
				{
					words += singleWord;
					singleWord = "";
				}

				if (c == '\n')
				{
					words += singleWord;
					singleWord = "";
					headers = words.Split('\n', '\t');
					break;
				}
			}

			return headers;
		}
	}
}
