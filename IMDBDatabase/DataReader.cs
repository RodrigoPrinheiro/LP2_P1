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
using System.Text;
using System.Linq;
using System.Threading;

namespace IMDBDatabase
{
	/// <summary>
	/// Class reads, decrypts and assigns the parsed data from files.
	/// </summary>
	class DataReader
	{
		// Constants
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
		/// Amount of titles
		/// </summary>
		private int _titleAmmount;

		/// <summary>
		/// 
		/// </summary>
		private int _peopleAmmount;

		/// <summary>
		/// 
		/// </summary>
		private int _ratingAmmount;

		/// <summary>
		/// 
		/// </summary>
		private int _episodeAmmount;

		/// <summary>
		/// Ui rendering
		/// </summary>
		private IInterface _ui;

		/// <summary>
		/// Ratings dictionary using title ID as key
		/// </summary>
		private Dictionary<int, Rating> _ratingDict;

        /// <summary>
        /// Episode dictionary using parent title ID as key
        /// </summary>
        private Dictionary<int, ICollection<Title>> _episodeDict;

		/// <summary>
		/// Dictionary containing titles
		/// </summary>
		private Dictionary<int, Title> _titleInfo;

		/// <summary>
		/// People dictionary containing ID as key
		/// </summary>
		private ICollection<Person> _peopleInfo;

		/// <summary>
		/// Constructor that initializes instance variables.
		/// </summary>
		public DataReader()
		{
			_ui = new ConsoleInterface();

			_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData) +
				"\\MyIMDBSearcher\\";

			_peopleAmmount = 0;
			_titleAmmount = 0;
			_ratingAmmount = 0;
			_episodeAmmount = 0;
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
				_ui.ShowMsg("PROCESS 1/4: Ratings\n");
				ReadFromFile(_TITLE_RATINGS_FILENAME, 
					IncrementAmmountOfRatingLines,
					CreateRatingDict,
					AddRatingTodictionary);

				// Get titles
				_ui.ShowMsg("PROCESS 2/4: Titles\n");
				ReadFromFile(_TITLE_BASICS_FILENAME,
					IncrementAmmountOfTitleLines,
					CreateTitleDict,
					AddTitleBasicsLineToDict);

				// Get title episodes
				_ui.ShowMsg("PROCESS 3/4: Episodes\n");
				ReadFromFile(_TITLE_EPISODE_FILENAME, 
					IncrementAmmountOfEpisodeLines,
					CreateEpisodeDict,
					AddEpisodeToTitle);

				// Get People
				_ui.ShowMsg("PROCESS 4/4: People\n");
				ReadFromFile(_NAME_BASICS_FILENAME,
					IncrementAmmountOfPeopleLines,
					CreatePeopleDict,
					AddPeopleLineToDict);

				Console.Clear();

				// Final feedback
				_ui.ShowMsg("COMPLETE.\n\n");
				_ui.ShowMsg($"\nTitle amount is: {_titleAmmount}", true);
				_ui.ShowMsg($"\nEpisodes amount is: {_episodeAmmount}", true);
				_ui.ShowMsg($"\nPeople amount is: {_peopleAmmount}", true);
				_ui.ShowMsg("\nFile reading successful.\n", true);

				_ui.ShowMsg("\n\nPress any key to continue...", true);

				// Return fill title info
				return _titleInfo.Values.ToList();
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
		}

		/// <summary> 
		/// Opens, uncompresses and reads a file, then make an action for every
		/// string line on the read file.
		/// </summary>
		/// <param name="fileName">File to open name</param>
		/// <param name="firstReadLineAction">Action to invoke per line on first 
		/// file read</param>
		/// <param name="onEndFirstRead">Action to invoke when first read is 
		/// over</param>
		/// <param name="secondReadLineAction">Action to invoke per line on
		/// second read</param>
		private void ReadFromFile(string fileName, 
			Action<string> firstReadLineAction,
			Action onEndFirstRead,
			Action<string> secondReadLineAction)
		{
			string titleLine = default;
			string path = _path + fileName;

			// Open file
			_ui.ShowFakeLoadingProcess("Opening file: " + fileName);
			using (FileStream fs = new FileStream(
				path, FileMode.Open, FileAccess.Read))
			{
				_ui.ShowMsg("File found and opened.", true);
				_ui.WaitForMilliseconds(200);
				// Uncompress file
				_ui.ShowFakeLoadingProcess("\n\nUncompressing");
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					_ui.ShowMsg("Uncompressing successful.\n\n", true);
					Thread.Sleep(200);
                    // Read file
                    using (BufferedStream bs = new BufferedStream(gzs))
                    {
                        _ui.ShowFakeLoadingProcess("Reading data");
                        using (StreamReader sr = new StreamReader(bs, 
							Encoding.UTF8))
                        {
                            // Run through all of the raw text data
                            while ((titleLine = sr.ReadLine()) != null)
                            {
                                // Invoke first read line action
                                firstReadLineAction.Invoke(titleLine);
							}

							// Invoke file read action
							onEndFirstRead.Invoke();
							
							// Second read
							fs.Position = 0;
							sr.DiscardBufferedData();
							while((titleLine = sr.ReadLine()) != null)
							{
								// Invoke second read line action
								secondReadLineAction.Invoke(titleLine);
							}
                            
							_ui.ShowMsg("Data read.\n", true);

                            Thread.Sleep(200);
                            Console.Clear();
                        }
                    }
				}
			}
		}

		/// <summary>
		/// Increment amount of title lines if the line isn't a header. 
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
		/// Increment amount of Rating lines if the line isn't a header. 
		/// </summary>
		/// <param name="rawLine">Raw line.</param>
		private void IncrementAmmountOfRatingLines(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("tt")) return;

			_ratingAmmount++;
		}

		/// <summary>
		/// Increment amount of Episode lines if the line isn't a header. 
		/// </summary>
		/// <param name="rawLine">Raw line.</param>
		private void IncrementAmmountOfEpisodeLines(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("tt")) return;

			_episodeAmmount++;
		}

		/// <summary>
		/// Increment amount of People lines if the line isn't a header. 
		/// </summary>
		/// <param name="rawLine">Raw line.</param>
		private void IncrementAmmountOfPeopleLines(string rawLine)
		{
			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("nm")) return;

			_peopleAmmount++;
		}

		private void CreateTitleDict()
		{
			// Initialize title list with predetermined amount of titles
			_titleInfo = new Dictionary<int, Title>(_titleAmmount + 1);
		}

		private void CreateRatingDict()
		{
			// Initialize title list with predetermined amount of titles
			_ratingDict = new Dictionary<int, Rating>(_ratingAmmount + 1);
		}

		private void CreateEpisodeDict()
		{
			// Initialize title list with predetermined amount of titles
			_episodeDict = new Dictionary<int, ICollection<Title>>
				( _episodeAmmount + 1);
		}

		private void CreatePeopleDict()
		{
			// Initialize title list with predetermined amount of titles
			_peopleInfo = new HashSet<Person>(_peopleAmmount + 1);
		}

		/// <summary>
		/// Parse raw title basics line data into the Title Info collection.
		/// </summary>
		/// <param name="rawLine">Raw line info.</param>
		/// <returns>The parsed title.</returns>
		private void AddTitleBasicsLineToDict(string rawLine)
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

			// Pass the info, ID is the key to the dictionary
			_titleInfo.Add(id ,new Title(
                rating, name, type, genres, isAdult, startYear, endYear));
		}

        /// <summary>
        /// Parse a raw line and add an episode to its proper parent series
        /// </summary>
        /// <param name="rawLine">Raw line information from a line</param>
        private void AddEpisodeToTitle(string rawLine)
        {
            // episode tconst   parent tconst     season    number

            string[] words = rawLine.Split('\t');

            if (!words[0].StartsWith("tt")) return;
            
            // Variables to save each required part for the episodes
            int id = default;
            int parentID = default;
            byte? seasonNumber = default;
            short? episodeNumber = default;

            // save episode ID
            id = ExtractID(words[0]);
            parentID = ExtractID(words[1]);
            // Get season number in a null-able byte
            seasonNumber = byte.TryParse(words[2], out byte bResult)
                ? (byte?)bResult : null;

            episodeNumber = short.TryParse(words[3], out short sResult)
                ? (short?)sResult : null;

            // If it contains the parent then add it to the parent's episode list
            if (_episodeDict.ContainsKey(parentID))
            {
                _episodeDict[parentID].Add(_titleInfo[id]);
                _titleInfo[parentID].AddEpisode(
                    _titleInfo[id], 
                    seasonNumber, 
                    episodeNumber);
            }
            else if (_titleInfo.ContainsKey(parentID) && !_episodeDict.ContainsKey(parentID))
            {
                // Create a new list in key value
                _episodeDict.Add(parentID, new List<Title>());
                _episodeDict[parentID].Add(_titleInfo[id]);

                // Add title to corresponding parent as an episode
                _episodeDict[parentID].Add(_titleInfo[id]);
                _titleInfo[parentID].AddEpisode(
                    _titleInfo[id],
                    seasonNumber,
                    episodeNumber);
            }
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

            int id = default;
            int votes = default;
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
		/// Process raw data line into the People dictionary.
		/// </summary>
		/// <param name="rawLine">Raw line info</param>
		private void AddPeopleLineToDict(string rawLine)
		{
			// nconst			primaryName			birthYear			deathYear       primaryProfession					knownForTitles
			// nm0000001        Fred Astaire		1899				1987			soundtrack,actor,miscellaneous		tt0043044,tt0072308,tt0053137,tt0050419

			// Split into words
			string[] words = rawLine.Split('\t');
			// Check if it is not a header
			if (!words[0].StartsWith("nm")) return;

			string name = "";
            ushort parseVar;
            ushort? birthYear = 0;
			ushort? deathYear = 0;
			string professions = default;
			ICollection<Title> knownForTitles = new HashSet<Title>();

            // Extract info
            name = words[1];
            birthYear = UInt16.TryParse(words[2], out parseVar) ?
                parseVar : (ushort?)null;

            deathYear = UInt16.TryParse(words[3], out parseVar) ?
                parseVar : (ushort?)null;
            professions = words[4];

            // Get Titles known for
            foreach (string titleID in words[5].Split(','))
            {
                int id = ExtractID(titleID);
                if (_titleInfo.ContainsKey(id))
                    knownForTitles.Add(_titleInfo[id]);
            }

            // Now that we have the information for 1 person we create it.
            Person newCrewMember = new Person
                (name, birthYear, deathYear, professions, knownForTitles);

            // For each title that the person is known for, add it to it's crew
            // collection.
            foreach (Title title in knownForTitles)
            {
                title.AddCrewMember(newCrewMember);
            }

            _peopleInfo.Add(newCrewMember);
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
		private int ExtractID(string rawId) =>
			int.Parse(rawId.Substring(2));
	}
}
