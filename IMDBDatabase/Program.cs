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
			string path = Environment.GetFolderPath(
				Environment.SpecialFolder.LocalApplicationData) +
				"\\MyIMDBSearcher\\";

			string fileName = "name.basics.tsv.gz";

			string finalPath = path + fileName;

			string dataNames = "";

			DataReader dr = new DataReader();

			dr.ReadData(finalPath, out dataNames);

			DataParser.Parse(dataNames);
		}
	}
}
