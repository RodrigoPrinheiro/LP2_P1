using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace IMDBDatabase
{
	class DataReader
	{
		/* 
		 * ####################################################################
		 *							###	TO-DO ###
		 * - Try parse for when file not found
		 * - Try parse for when file could not be uncompressed
		 * - Return the headers
		 * - Split stuff using the extracted headers
		 * - Store everything
		 * ####################################################################
		*/

		// tconst	titleType	primaryTitle	originalTitle	isAdult	startYear	endYear	runtimeMinutes	genres

		private const string _NAME_BASICS_FILENAME =	"name.basics.tsv.gz";
		private const string _TITLE_AKAS_FILENAME =		"title.akas.tsv.gz";
		private const string _TITLE_BASICS_FILENAME =	"title.basics.tsv.gz";
		private const string _TITLE_CREW_FILENAME =		"title.crew.tsv.gz";
		private const string _TITLE_EPISODE_FILENAME =	"title.episode.tsv.gz";
		private const string _TITLE_PRINCIPALS_FILENAME = "title.principals.tsv.gz";
		private const string _TITLE_RATINGS_FILENAME =	"title.ratings.tsv.gz";

		private Dictionary<int, string[]> titles;
		private readonly string _path;


		public DataReader()
		{
			Console.OutputEncoding = Encoding.UTF8;

			titles = new Dictionary<int, string[]>();

			_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData) +
				"\\MyIMDBSearcher\\";

			_path += _TITLE_BASICS_FILENAME;

		}

		public void ReadData()
		{
			string titleLine;

			Console.WriteLine($"Opening {_path}...");
			using (FileStream fs = new FileStream(
				_path, FileMode.Open, FileAccess.Read))
			{
				Console.WriteLine("Successfully opened!");
				Console.WriteLine("Decompressing file...");
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					Console.WriteLine("Successfully decompressed!\n");
					Console.WriteLine("Parsing file...");
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Parse line
							ParseTitleBasicsLine(titleLine);
							Console.ReadKey(true);
						}
						Console.WriteLine("Successfully parsed!\n");
					}
				}
			}
		}

		private void ParseTitleBasicsLine(string line)
		{
			string[] words = line.Split('\t');

			int titleID = default;

			for (byte i = 0; i < words.Length; i++)
			{
				// Check if line has ID
				if (words[i].Substring(0, 2) == "tt")
					switch (i)
					{
						case 0:
							titleID = int.Parse(words[i].Substring(2));
							break;
						case 1:
							titleID = int.Parse(words[i].Substring(2));
							break;
					}
			}

			Console.WriteLine();
		}
	}
}
