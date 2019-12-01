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
			DataReader dr = new DataReader();

			dr.ReadData();
		}
	}
}
