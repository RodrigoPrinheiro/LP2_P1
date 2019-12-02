/// @file
/// @brief This file contains class IMDBDatabase.Title, this the basic data set that is used 
/// for each title in the IMDB database.
/// 
///@author Rodrigo Pinheiro
///@date 2019

using System;
using System.Collections;
using System.Collections.Generic;

namespace IMDBDatabase
{
    /// <summary>
    /// Class representing a dataset for a Title of the IMDB database.
    /// Movies, series, shorts, video games etc...
    /// </summary>
    public class Title : IEnumerable<Title>, IComparable<Title>, IHasID
    {
        /// <summary>
        /// Collection for all the episodes assign to this Title
        /// </summary>
        private ICollection<Title> _episodes;

        /// <summary>
        /// Parent Title for when this title is an episode;
        /// </summary>
        private Title _parentTitle;

        /// <summary>
        /// Integer ID of this Title
        /// </summary>
        public int ID { get; }

        /// <summary>
        /// Struct for the rating of this Title
        /// </summary>
        private Rating _rating;
        
        /// <summary>
        /// Number of over all votes on this Title
        /// </summary>
        public int Votes { get => _rating.Votes; }

        /// <summary>
        /// Average score from all votes in this Title
        /// </summary>
        public float AverageScore { get => _rating.Score; }

        /// <summary>
        /// Year of start and the year of end of the Title
        /// </summary>
        public Tuple<ushort?, ushort?> Year { get; }

        /// <summary>
        /// Main name of the Title, the name that is used the most
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Type of this Title. Movie, series, short etc...
        /// </summary>
        public TitleType Type { get; }

        /// <summary>
        /// Genres of the Title
        /// </summary>
        public string[] Genres { get; }

        /// <summary>
        /// If the title is classified as adult content or not
        /// </summary>
        public bool AdultContent { get; }

        /// <summary>
        /// Get only property for the parent Title for this Title
        /// </summary>
        public Title ParentTitle { get => _parentTitle; }

        public byte? Season { get; set; }
        public short? EpisodeNumber { get; set; }


        /// <summary>
        /// Constructor that creates an instance of Title
        /// </summary>
        /// <param name="id">Integer ID that tags the Title.</param>
        /// <param name="rating">Overall Rating of this title.</param>
        /// <param name="name">Name of the Title.</param>
        /// <param name="type">Type of this Title, movie, series etc...</param>
        /// <param name="genres">Genres of the Title, Drama, Action etc...</param>
        /// <param name="content">Is the content adult or not</param>
        public Title(int id, Rating rating, string name,
            string type, string genres, bool content, params string[] year)
        {
            // Temporary variable to parse years
            ushort i;
            // Null-able integers so we can have null years in case of a movie etc...
            ushort? startYear = null;
            ushort? endYear = null;

            // Parse the years
            startYear = UInt16.TryParse(year[0], out i) ? i : (ushort?)null;
            endYear = UInt16.TryParse(year[1], out i) ? i : (ushort?)null;

            // Initialize Variables
            ID = id;
            _rating = rating;
            Name = name;
            Type = (TitleType)Enum.Parse(typeof(TitleType), type, true);
            Genres = genres.Split(',', ' ', StringSplitOptions.RemoveEmptyEntries);
            AdultContent = content;
            Year = new Tuple<ushort?, ushort?>(startYear, endYear);
        }

        /// <summary>
        /// Adds an episode Title to this Title and adds this Title to the
        /// episode parent Title reference.
        /// </summary>
        /// <param name="episode">Episode to be added to this Title</param>
        public void AddEpisode(Title episode, byte? season, short? number)
        {
            // Only creates a list if this title needs an episode list.
            if (_episodes == null) _episodes = new List<Title>();
            // Ads the title
            _episodes?.Add(episode);
            // Sets parent
            episode.SetParent(this);
            episode.Season = season;
            episode.EpisodeNumber = number;
        }

        /// <summary>
        /// Sets current Title parent Title.
        /// </summary>
        /// <param name="parent">Parent Title</param>
        public void SetParent(Title parent) => _parentTitle = parent;

        public int CompareTo(Title other)
        {
            return Comparer<float>.Default.
                Compare(other?.AverageScore ?? 0f, AverageScore);
        }

        public IEnumerator<Title> GetEnumerator() => GetEnumerator();

        // Return all children from this title
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (Title ep in _episodes)
            {
                yield return ep;
            }
        }
    }
}
