using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
	public struct Rating
	{
		public int Votes { get; }
		public float Score { get; }

		public Rating(int votes, float score)
		{
			Votes = votes;
			Score = score;
		}
	}
}
