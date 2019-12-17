/// @file
/// @brief This file contains class IMDBDatabase.Title, this the basic data set that is used 
/// for each title in the IMDB database.
/// 
/// @author Rodrigo Pinheiro & Tomás Franco.
/// @date 2019

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
    /// <summary>
    /// Class representing a dataset for a Title of the IMDB database.
    /// Movies, series, shorts, video games etc...
    /// </summary>
    public class Title : IComparable<Title>, IReadable
    {
        /// <summary>
        /// Collection for all the episodes assign to this Title
        /// </summary>
        private ICollection<Title> _episodes;

        /// <summary>
        /// Collection for the crew that worked in this Title
        /// </summary>
        private ICollection<Person> _crew;

        /// <summary>
        /// Parent Title for when this title is an episode;
        /// </summary>
        private Title _parentTitle;

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
        public TitleGenre Genres { get; }

        /// <summary>
        /// If the title is classified as adult content or not
        /// </summary>
        public bool AdultContent { get; }

        /// <summary>
        /// Get only property for the parent Title for this Title
        /// </summary>
        public Title ParentTitle { get => _parentTitle; }

        /// <summary>
        /// Season number this title is assign to (if it's an episode).
        /// </summary>
        public byte? Season { get; set; }

        /// <summary>
        /// Episode number of this title (if it's an episode).
        /// </summary>
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
        public Title(Rating rating, string name,
            string type, string genres, bool content, string[] year)
        {
            // Temporary variable to parse years
            ushort i;
            // Null-able integers so we can have null years in case of a movie etc...
            ushort? startYear = null;
            ushort? endYear = null;
            string[] cleanGenres;

            // Parse the years
            startYear = UInt16.TryParse(year[0], out i) ? i : (ushort?)null;
            endYear = UInt16.TryParse(year[1], out i) ? i : (ushort?)null;
            cleanGenres = genres.Split(',', ' ', 
                StringSplitOptions.RemoveEmptyEntries);

            // Initialize Variables
            _crew = new HashSet<Person>();
            _rating = rating;
            Name = name;
            Type = (TitleType)Enum.Parse(typeof(TitleType), type, true);
            AdultContent = content;
            Year = new Tuple<ushort?, ushort?>(startYear, endYear);

            // Set genres
            foreach (string g in cleanGenres)
            {
                // Remove weird characters from string
                string gWithoutChars = g.Replace("-", "");

                if (gWithoutChars.Equals(@"\N"))
                    gWithoutChars = "None";

                Genres |= (TitleGenre)Enum.Parse
                    (typeof(TitleGenre), gWithoutChars, true);
            }
        }

        /// <summary>
        /// Adds an episode Title to this Title and adds this Title to the
        /// episode parent Title reference.
        /// </summary>
        /// <param name="episode">Episode to be added to this Title</param>
        public void AddEpisode(Title episode, byte? season, short? number)
        {
            // Only creates a list if this title needs an episode list.
            if (_episodes == null) _episodes = new HashSet<Title>();
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

        /// <summary>
        /// Adds a new crew member to the movie's crew
        /// </summary>
        /// <param name="person"> Crew member to be added</param>
        public void AddCrewMember(Person person)
        {
            _crew.Add(person);
        }

        /// <summary>
        /// Compares movies by the overall rating
        /// </summary>
        /// <param name="other"> The movie to compare this instance to</param>
        /// <returns> returns a negative number if it's below in order
        /// of the movie it's being compared to</returns>
        public int CompareTo(Title other)
        {
            int i = Comparer<float>.Default.
                Compare(other?.AverageScore ?? 0f, AverageScore);
            if (i != 0) return i;

            i = Comparer<float>.Default.
                Compare(other?.Votes ?? 0f, Votes);
            return i;
        }

		/// <summary>
		/// Get associated titles (episodes)
		/// </summary>
		/// <returns>Array with all titles</returns>
        public IReadable[] GetCoupled()
        {
            return _episodes.ToArray<IReadable>();
        }

        /// <summary>
        /// Gets the basic information to write a title on the screen
        /// </summary>
        /// <returns> String containing the Name and type 
        /// divided by tabs</returns>
		public string GetBasicInfo()
		{
			return $"{Name}\t{Type}";
		}

        /// <summary>
        /// Gets the complete information to write a title on the screen
        /// </summary>
        /// <returns> String containing the Name, type, content, 
        /// rating, years and genres of this title divided by tabs</returns>
		public string GetDetailedInfo()
		{
            string adultContent = AdultContent ? "Yes" : "No";
            string yearOne;
            string yearTwo;
            StringBuilder info = new StringBuilder();

            // Append name, type, content and ratings
            info.Append(Name + "\t");
            info.Append(Type + "\t");
            info.Append(adultContent + "\t");
            info.Append(_rating.Score + "\t");
            info.Append(_rating.Votes + "\t");

            // Sort year by null
            yearOne = Year.Item1 == null ? @"\N" : Year.Item1.ToString();
            yearTwo = Year.Item2 == null ? @"\N" : Year.Item2.ToString();
            // Append Year
            info.Append(yearOne + "\t");
            info.Append(yearTwo + "\t");
            info.Append(Genres.ToString());

            // Return created string
            return info.ToString();
		}

		/// <summary>
		/// Get parent title.
		/// </summary>
		/// <returns>Parent title</returns>
        public IReadable GetParentInfo()
        {
            return _parentTitle as IReadable;
        }

		/// <summary>
		/// Get everyone who participated in the title.
		/// </summary>
		/// <returns>Array with every person.</returns>
        public IReadable[] GetCrew()
        {
            return _crew.ToArray<IReadable>();
        }
    }
}
