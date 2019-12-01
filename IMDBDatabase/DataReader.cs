using System;
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

		public void ReadData(string path, out string outDataString)
		{
			Console.WriteLine($"Opening {path}...");
			using (Stream file = File.OpenRead(path))
			{
				Console.WriteLine("Successfully opened!");
				outDataString = UncompressGZip(file);
			}
		}

		private string UncompressGZip (Stream fileStream)
		{
			Console.WriteLine("Decompressing file...");

			string finalOutput = "";

			using (Stream gz =
				new GZipStream(fileStream, CompressionMode.Decompress))
			{
				using (MemoryStream ms = new MemoryStream())
				{
					gz.CopyTo(ms);
					finalOutput = Encoding.UTF8.GetString(ms.ToArray());

					ms.Close();
					gz.Close();

					Console.WriteLine("Successfully decompressed!\n");
					return finalOutput;
				}
			}
		}

		private string[] Parse(string data)
		{
			Console.WriteLine("Parsing...");

			string[] lines = data.Split('\n');

			string[] headers = GetHeaders(data);

			Console.WriteLine("Parsing complete!");
			return lines;
		}

		private string[] GetHeaders(string data)
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
