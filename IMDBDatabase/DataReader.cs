/// @file
/// @brief This file contains class IMDBDatabase.DataReader, 
/// this will read the compressed data files and store them for later use.
/// 
/// @author Tomás Franco
/// @date 2019

using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace IMDBDatabase
{
	/// <summary>
	/// Class reads, decrypts and assigns the parsed data from files.
	/// </summary>
	class DataReader
	{
		// Consts
		/// <summary>
		/// Name of the name_basics file
		/// </summary>
		private const string _NAME_BASICS_FILENAME =	"name.basics.tsv.gz";
		/// <summary>
		/// Name of the title_akas file
		/// </summary>
		private const string _TITLE_AKAS_FILENAME =		"title.akas.tsv.gz";
		/// <summary>
		/// Name of the title_basics file
		/// </summary>
		private const string _TITLE_BASICS_FILENAME =	"title.basics.tsv.gz";
		/// <summary>
		/// Name of the title_crew file
		/// </summary>
		private const string _TITLE_CREW_FILENAME =		"title.crew.tsv.gz";
		/// <summary>
		/// Name of the title_episode file
		/// </summary>
		private const string _TITLE_EPISODE_FILENAME =	"title.episode.tsv.gz";
		/// <summary>
		/// Name of the title_principals file
		/// </summary>
		private const string _TITLE_PRINCIPALS_FILENAME ="title.principals.tsv.gz";
		/// <summary>
		/// Name of the title_ratings file
		/// </summary>
		private const string _TITLE_RATINGS_FILENAME =	"title.ratings.tsv.gz";

		/// <summary>
		/// Path to local appdata
		/// </summary>
		private readonly string _path;
		/// <summary>
		/// Ui rendering
		/// </summary>
		private IInterface _ui;
		/// <summary>
		/// Ratings dictionary using title ID as key
		/// </summary>
		private Dictionary<int, Rating> _ratingDict;

		/// <summary>
		/// Constructor that initializes instance variables.
		/// </summary>
		public DataReader()
		{
			_ui = new ConsoleInterface();
			_ratingDict = new Dictionary<int, Rating>();

			_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData) +
				"\\MyIMDBSearcher\\";
		}

		/// <summary>
		/// Begin reading, decrypting and assigning data.
		/// </summary>
		public void ReadData()
		{
			// Start data process
			try
			{
				GetRatingInfo();
				ProcessTitleBasicInfo();
			} 
			// If a file is not found
			catch (FileNotFoundException e)
			{
				_ui.RenderError("\nOops, we couldn't find your file...");
				throw e;
			}
		}

		/// <summary>
		/// Gets the raw lines of text of the rating info
		/// from _TITLE_RATINGS_FILENAME and adds them to a Dictionary.
		/// </summary>
		private void GetRatingInfo()
		{
			string pathToFile = _path + _TITLE_RATINGS_FILENAME;
			string titleLine;

			_ui.ShowFakeLoadingProcess("Booting bootleg_unity-v.2021.TfRp");
			// Open file
			using (FileStream fs = new FileStream(
				pathToFile, FileMode.Open, FileAccess.Read))
			{
				_ui.ShowMsg("definetly-unity-v.2021.TfRp booted.", true);
				_ui.WaitForMilliseconds(400);
				Console.Clear();
				_ui.ShowFakeLoadingProcess("Spoofing");
				// Uncompress file
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					_ui.ShowMsg("Spoofing complete.\n", true);
					_ui.WaitForMilliseconds(400);
					Console.Clear();
					_ui.ShowFakeLoadingProcess("Searching for Honeypot");
					// Read file
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						// Run through all of the raw text data
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Parse line
							AddRatingTodictionary(titleLine);
						}
						_ui.ShowMsg("Honeypot successfully found and avoided.\n",
							true);
						_ui.WaitForMilliseconds(400);
						Console.Clear();
					}
				}
			}
		}

		/// <summary>
		/// Processes raw text data from TITLE_BASICS_FILENAME
		/// </summary>
		private void ProcessTitleBasicInfo()
		{
			string pathToFile = _path + _TITLE_BASICS_FILENAME;
			string titleLine;

			_ui.ShowFakeLoadingProcess("Searching for firewall breach");
			// Open file
			using (FileStream fs = new FileStream(
				pathToFile, FileMode.Open, FileAccess.Read))
			{
				_ui.ShowMsg("Firewall breached.", true);
				_ui.WaitForMilliseconds(400);
				Console.Clear();
				_ui.ShowFakeLoadingProcess("Decrypting passwords");
				// Uncompress file
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					_ui.ShowMsg("Passwords decrypted.\n", true);
					_ui.WaitForMilliseconds(400);
					Console.Clear();
					_ui.ShowFakeLoadingProcess("Breaching IMDB database");
					// Read file
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						// Run through all of the raw text data
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Parse line
							ParseTitleBasicsLine(titleLine);
						}
						_ui.ShowMsg("Access Granted.\n", true);
						_ui.WaitForMilliseconds(400);
					}
				}
			}
		}

		/// <summary>
		/// Parse raw title basics line data into a Title class.
		/// </summary>
		/// <param name="rawLine">Raw line data</param>
		private void ParseTitleBasicsLine(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("tt")) return;

			int    id =			default;
			bool   isAdult =	default;
			string type =		default;
			string name =		default;
			string startYear =	default;
			string endYear =	default;
			string genres =		default;

			// Run through all of the words, respectively parsing info
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

			// Pass the info

			//titles.Add(new Title(
			//	id, GetRatingFromId(id), name, type, genres, 
			//		isAdult, startYear, endYear));
		}

		/// <summary>
		/// Process raw data line into the ratings dictionary
		/// </summary>
		/// <param name="rawLine">Raw data line</param>
		private void AddRatingTodictionary(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("tt")) return;

			int id =		default;
			int votes =		default;
			float average = default;

			// Run through all of the words, respectively parsing info
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

			// Add to dictionary
			_ratingDict.Add(id, new Rating(votes, average));
		}
		
		/// <summary>
		/// Find the rating data of the requested ID on the dictionary
		/// </summary>
		/// <param name="id">Target title ID</param>
		/// <returns></returns>
		private Rating GetRatingFromID(int id)
		{
			return _ratingDict[id];
		}

		/// <summary>
		/// Extracts int ID from raw data 
		/// </summary>
		/// <param name="rawTitleId">raw title ID data </param>
		/// <returns>ID as an int</returns>
		private int ExtractID(string rawTitleId) =>
			int.Parse(rawTitleId.Substring(2));

		
	}
}
