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
	}
}
