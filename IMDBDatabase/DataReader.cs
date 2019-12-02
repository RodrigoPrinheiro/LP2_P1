/// @file
/// @brief This file contains class IMDBDatabase.DataReader, 
/// this will read the compressed data files and store them for later use.
/// 
/// @author Tomás Franco
/// @date 2019

using System;
using System.IO;
using System.IO.Compression;
using System.Collections.Generic;
using System.Threading;
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
		/// Ammount of titles
		/// </summary>
		int _titleAmmount;

		/// <summary>
		/// Ui rendering
		/// </summary>
		private IInterface _ui;

		/// <summary>
		/// Ratings dictionary using title ID as key
		/// </summary>
		private Dictionary<int, Rating> _ratingDict;

		/// <summary>
		/// List containing titles
		/// </summary>
		private List<Title> _titleInfo;

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
			_titleAmmount = 0;
		}

		/// <summary>
		/// Begin reading, decrypting and assigning data.
		/// </summary>
		public ICollection<Title> ReadData()
		{
			// Start data process
			try
			{
				// Get ratings
				ReadFromFile(_TITLE_RATINGS_FILENAME, AddRatingTodictionary);

				// Get the ammount of titles
				ReadFromFile(
					_TITLE_BASICS_FILENAME, IncrementAmmountOfTitleLines);
				_titleAmmount++;

				// Initialize title list with predetermined ammount of titles
				_titleInfo = new List<Title>(_titleAmmount);

				// Get titles into list
				ReadFromFile(_TITLE_BASICS_FILENAME, AddTitleBasicsLineToList);

				// Return fill title info
				return _titleInfo;
			} 
			// If a file is not found
			catch (FileNotFoundException e)
			{
				_ui.RenderError("\nOops, we couldn't find your file...");
				throw e;
			}
			// If a file is not found
			catch (OutOfMemoryException e)
			{
				_ui.RenderError("\nOops, memory run out... Thats our fault. :)");
				throw e;
			}

			finally
			{
				_ui.ShowMsg($"\nList size is: {_titleAmmount}", true);
				_ui.ShowMsg("\nFile reading successfull.\n", true);
			}
		}
		
		/// <summary>
		/// Opens, uncompresses and reads a file, then make an action for every
		/// string line on the read file.
		/// </summary>
		/// <param name="fileName">Name of the file to be read.</param>
		/// <param name="lineAction">Action to be made per line.</param>
		private void ReadFromFile(string fileName, Action<string> lineAction)
		{
			string titleLine = default;
			string path = _path + fileName;

			// Open file
			_ui.ShowFakeLoadingProcess("Opening file: " + fileName);
			using (FileStream fs = new FileStream(
				path, FileMode.Open, FileAccess.Read))
			{
				_ui.ShowMsg("File found and opened.", true);
				Thread.Sleep(200);
				Console.Clear();
				// Uncompress file
				_ui.ShowFakeLoadingProcess("Uncompressing");
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					_ui.ShowMsg("Uncompressing successfull.\n\n", true);
					Thread.Sleep(200);
					Console.Clear();
					// Read file
					_ui.ShowFakeLoadingProcess("Reading data");
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						// Run through all of the raw text data
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Invoke action
							lineAction.Invoke(titleLine);
						}
						_ui.ShowMsg("Data read.\n", true);
						Thread.Sleep(200);
						Console.Clear();
					}
				}
			}
		}

		/// <summary>
		/// Increment ammount of title lines if the line isn't a header. 
		/// </summary>
		/// <param name="rawLine">Raw line.</param>
		private void IncrementAmmountOfTitleLines(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("tt")) return;

			_titleAmmount++;
		}

		/// <summary>
		/// Parse raw title basics line data into the Title Info collection.
		/// </summary>
		/// <param name="rawLine">Raw line info.</param>
		/// <returns>The parsed title.</returns>
		private void AddTitleBasicsLineToList(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("tt")) return;

			int id =			default;
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
					// Title Genres
					case 8:
						genres = words[i];
						break;
				}
			}

			Rating rating = GetRatingFromID(id);

			// Pass the info
			_titleInfo.Add(new Title(
				id, rating, name, type, genres,
					isAdult, startYear, endYear));
		}

		/// <summary>
		/// Process raw data line into the ratings dictionary.
		/// </summary>
		/// <param name="rawLine">Raw data line.</param>
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
		/// Find the rating data of the requested ID on the dictionary.
		/// </summary>
		/// <param name="id">Target title ID</param>
		/// <returns></returns>
		private Rating GetRatingFromID(int id)
		{
			// If title doesn't have a rating give a new default rating
			if (!_ratingDict.ContainsKey(id)) return new Rating(0,0.0f);

			// Return respective rating
			return _ratingDict[id];

		}

		/// <summary>
		/// Extracts int ID from raw data .
		/// </summary>
		/// <param name="rawTitleId">raw title ID data </param>
		/// <returns>ID as an int</returns>
		private int ExtractID(string rawTitleId) =>
			int.Parse(rawTitleId.Substring(2));

		
	}
}
