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
		 * - Return the headers
		 * - Split stuff using the extracted headers
		 * - Store everything
		 * ####################################################################
		*/

		// tconst	titleType	primaryTitle	originalTitle	isAdult	startYear	endYear		runtimeMinutes	genres

		private const string _NAME_BASICS_FILENAME =	"name.basics.tsv.gz";
		private const string _TITLE_AKAS_FILENAME =		"title.akas.tsv.gz";
		private const string _TITLE_BASICS_FILENAME =	"title.basics.tsv.gz";
		private const string _TITLE_CREW_FILENAME =		"title.crew.tsv.gz";
		private const string _TITLE_EPISODE_FILENAME =	"title.episode.tsv.gz";
		private const string _TITLE_PRINCIPALS_FILENAME = "title.principals.tsv.gz";
		private const string _TITLE_RATINGS_FILENAME =	"title.ratings.tsv.gz";

		private readonly string _path;
		private string[] _titleRatings;


		public DataReader()
		{
			Console.OutputEncoding = Encoding.UTF8;

			_path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData) +
				"\\MyIMDBSearcher\\";

			_path += _TITLE_BASICS_FILENAME;

		}

		public void ReadData()
		{
			ProcessTitleBasicInfo();
		}

		private void GetRatingInfo()
		{

		}

		private void ProcessTitleBasicInfo()
		{
			string titleLine;

			WriteFakeLoadingDots("Searching for firewall breach");
			using (FileStream fs = new FileStream(
				_path, FileMode.Open, FileAccess.Read))
			{
				Console.WriteLine("Firewall breached.");
				Thread.Sleep(400);
				Console.Clear();
				WriteFakeLoadingDots("Decrypting passwords");
				using (GZipStream gzs = new GZipStream(
					fs, CompressionMode.Decompress))
				{
					Console.WriteLine("Passwords decrypted.\n");
					Thread.Sleep(400);
					Console.Clear();
					WriteFakeLoadingDots("Hacking IMDB database");
					using (StreamReader sr = new StreamReader(gzs, Encoding.UTF8))
					{
						while ((titleLine = sr.ReadLine()) != null)
						{
							// Parse line
							ParseTitleBasicsLine(titleLine);
							//Console.ReadKey(true);
						}
						Console.WriteLine("Access Granted.\n");
						Thread.Sleep(400);
					}
				}
			}
		}

		private void ParseTitleBasicsLine(string line)
		{
			string[] words = line.Split('\t');
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
						id = int.Parse(words[i].Substring(2));
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

		private Tuple<int, float> GetScoresFromID(int id)
		{

		}

		private void WriteFakeLoadingDots(string fakeProcess)
		{
			Console.Write(fakeProcess);
			Thread.Sleep(431);
			Console.Write(".");
			Thread.Sleep(327);
			Console.Write(".");
			Thread.Sleep(398);
			Console.WriteLine(".");
			Thread.Sleep(401);
		}
	}
}
