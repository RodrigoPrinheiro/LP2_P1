/// @file
/// @brief This Struct stores title ratings.
/// 
/// @author Tomás Franco e Rodrigo Pinheiro
/// @date 2019

namespace IMDBDatabase
{
	/// <summary>
	/// Struct used to store title ratings.
	/// </summary>
	public struct Rating
	{
		/// <summary>
		/// Title Votes.
		/// </summary>
		public int Votes { get; }
		/// <summary>
		/// Title Score.
		/// </summary>
		public float Score { get; }

		/// <summary>
		/// Constructor initializes variables.
		/// </summary>
		/// <param name="votes">Amount of Votes</param>
		/// <param name="score">Average Score</param>
		public Rating(int votes, float score)
		{
			Votes = votes;
			Score = score;
		}
	}
}
