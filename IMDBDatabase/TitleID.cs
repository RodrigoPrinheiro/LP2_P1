using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
    public struct TitleID
    {
        public int ID { get; }
        public int Votes { get; }
        public float AverageScore { get; }
        public TitleType Type { get; }
        public TitleGenre Genre { get; private set; }

        public TitleID(int id, int votes, float score, string type, string genres)
        {
            ID = id;
            Votes = votes;
            AverageScore = score;
            Type = ParseEnum<TitleType>(type);
            Genre = ParseEnum<TitleGenre>(genres);
        }

        public static T ParseEnum<T>(string value)
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

    }
}
