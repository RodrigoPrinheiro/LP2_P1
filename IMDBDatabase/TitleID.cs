using System;

namespace IMDBDatabase
{
    public struct TitleID
    {
        public int ID { get; }
        public int Votes { get; }
        public float AverageScore { get; }
        public TitleType Type { get; }
        public string[] Genres { get; }

        public TitleID(int id, int votes, float score, 
            string type, string genres)
        {
            ID = id;
            Votes = votes;
            AverageScore = score;
            Type = ParseEnum<TitleType>(type);
            Genres = genres.Split(',',' ', StringSplitOptions.RemoveEmptyEntries);
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }
    }
}
