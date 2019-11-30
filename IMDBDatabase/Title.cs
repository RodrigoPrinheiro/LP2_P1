using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
    public class Title : IEnumerable<Title>, IComparable<Title>
    {
        private List<Title> _episodes;

        public int ID { get; }
        public int Votes { get; }
        public float AverageScore { get; }
        public Tuple<int, int> Year { get; }
        public string Name { get; }
        public TitleType Type { get; }
        public string[] Genres { get; }
        public bool AdultContent { get; }

        public Title(int id, int votes, float score, string name,
            string type, string genres, string[] year, bool content)
        {
            _episodes = new List<Title>();

            ID = id;
            Votes = votes;
            AverageScore = score;
            Name = name;
            Type = (TitleType)Enum.Parse(typeof(TitleType), type, true);
            Genres = genres.Split(',', ' ', StringSplitOptions.RemoveEmptyEntries);
            AdultContent = content;
            Year = new Tuple<int, int>
                (Convert.ToInt32(year[0]),
                Convert.ToInt32(year[1]));
        }

        public void AddEpisode(Title episode)
        {
            _episodes.Add(episode);
        }

        public IEnumerator<Title> GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Title ep in _episodes)
            {
                yield return ep;
            }
        }

        public int CompareTo(Title other)
        {
            return Comparer<float>.Default.
                Compare(other?.AverageScore ?? 0f, AverageScore);
        }
    }
}
