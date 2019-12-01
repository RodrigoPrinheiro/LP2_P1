using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading;

namespace IMDBDatabase
{
	class DataReader
	{
		/* 
		 * ####################################################################
		 *							###	TO-DO ###
		 * - Try parse for when file not found
		 * - Try parse for when file could not be uncompressed
		 * ####################################################################
		*/

		private const string _NAME_BASICS_FILENAME =	"name.basics.tsv.gz";
		private const string _TITLE_AKAS_FILENAME =		"title.akas.tsv.gz";
		private const string _TITLE_BASICS_FILENAME =	"title.basics.tsv.gz";
		private const string _TITLE_CREW_FILENAME =		"title.crew.tsv.gz";
		private const string _TITLE_EPISODE_FILENAME =	"title.episode.tsv.gz";
		private const string _TITLE_PRINCIPALS_FILENAME ="title.principals.tsv.gz";
		private const string _TITLE_RATINGS_FILENAME =	"title.ratings.tsv.gz";

		private readonly string _path;
		private IInterface _ui;
		private Dictionary<int, Rating> _ratingDict;


		public DataReader()
		{
			Console.OutputEncoding = Encoding.UTF8;

			_ui = new ConsoleInterface();
			_ratingDict = new Dictionary<int, Rating>();

			_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData) +
				"\\MyIMDBSearcher\\";
		}

		public void ReadData()
		{
			GetRatingInfo();
			ProcessTitleBasicInfo();
		}

		private void GetRatingInfo()
		{
			string pathToFile = _path + _TITLE_RATINGS_FILENAME;
			string titleLine;

			_ui.ShowFakeLoadingProcess("Booting bootleg_unity-v.2021.TfRp");
			using (FileStream fs = new FileStream(
				pathToFile, FileMode.Open, FileAccess.Read))
			{
				_ui.ShowMsg("definetly-unity-v.2021.TfRp booted.", true);
				Thread.Sleep(400);
				Console.Clear();
				_ui.ShowFakeLoadingProcess("Spoofing");
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					_ui.ShowMsg("Spoofing complete.\n", true);
					Thread.Sleep(400);
					Console.Clear();
					_ui.ShowFakeLoadingProcess("Searching for Honeypot");
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Parse line
							AddRatingTodictionary(titleLine);
							//Console.ReadKey(true);
						}
						_ui.ShowMsg("Honeypot successfully found and avoided.\n",
							true);
						Thread.Sleep(400);
						Console.Clear();
					}
				}
			}
		}

		private void ProcessTitleBasicInfo()
		{
			string pathToFile = _path + _TITLE_BASICS_FILENAME;
			string titleLine;

			_ui.ShowFakeLoadingProcess("Searching for firewall breach");
			using (FileStream fs = new FileStream(
				pathToFile, FileMode.Open, FileAccess.Read))
			{
				_ui.ShowMsg("Firewall breached.", true);
				Thread.Sleep(400);
				Console.Clear();
				_ui.ShowFakeLoadingProcess("Decrypting passwords");
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					_ui.ShowMsg("Passwords decrypted.\n", true);
					Thread.Sleep(400);
					Console.Clear();
					_ui.ShowFakeLoadingProcess("Breaching IMDB database");
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Parse line
							ParseTitleBasicsLine(titleLine);
							//Console.ReadKey(true);
						}
						_ui.ShowMsg("Access Granted.\n", true);
						Thread.Sleep(400);
					}
				}
			}
		}

		private void ParseTitleBasicsLine(string rawLine)
		{
			string[] words = rawLine.Split('\t');
			if (!words[0].StartsWith("tt")) return;

			int    id =			default;
			bool   isAdult =	default;
			string type =		default;
			string name =		default;
			string startYear =	default;
			string endYear =	default;
			string genres =		default;

			for (byte i = 0; i < words.Length; i++)
			{
				switch (i) 
				{ 
					// Title ID
					case 0:
						id = ExtractID(words[i]);
						break;
					// Title Type
					case 1:
						type = words[i];
						break;
					// Title Primary Title
					case 2:
						name = words[i];
						break;
					// Title Original Title
					case 3:
						//titleName = words[i];
						break;
					// Title Is Adult
					case 4:
						isAdult = words[i] == "1" ? true : false;
						break;
					// Title Start Year
					case 5:
						startYear = words[i];
						break;
					// Title End Year
					case 6:
						endYear = words[i];
						break;
					// Title Runtime (min)
					case 7:
						break;
					// Title Genres
					case 8:
						genres = words[i];
						break;
				}
			}

			//titles.Add(new Title(
			//	id, 0, 0, name, type, genres, isAdult, startYear, endYear));
		}

		private void AddRatingTodictionary(string rawLine)
		{
			string[] words = rawLine.Split('\t');
			if (!words[0].StartsWith("tt")) return;

			int id =		default;
			int votes =		default;
			float average = default;

			for (byte i = 0; i < words.Length; i++)
			{
				switch (i)
				{
					// Title ID
					case 0:
						id = ExtractID(words[i]);
						break;
					// Title Average Rating
					case 1:
						average = float.Parse(words[i]);
						break;
					// Title Votes
					case 2:
						votes = int.Parse(words[i]);
						break;
				}
			}

			_ratingDict.Add(id, new Rating(votes, average));
		}

		private Rating GetRatingFromID(int id)
		{
			return _ratingDict[id];
		}

		private int ExtractID(string rawTitleId) =>
			int.Parse(rawTitleId.Substring(2));

		
	}
}
