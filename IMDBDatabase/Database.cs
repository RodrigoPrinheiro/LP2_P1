/// @file
/// @brief This file has the class IMDBDatabase.Database, this class is reserved
/// to store the information gathered and the entry point for questioning the
/// database.
/// 
/// @author Rodrigo Pinheiro & Tomás Franco.
/// @date 2019

using System.Linq;
using System.Collections.Generic;
using System;

namespace IMDBDatabase
{
    /// <summary>
    /// This class stores the information and contains several methods to question
    /// the database.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// The main collection that contains every title in the IMDB database.
        /// </summary>
        private ICollection<Title> _titles;

        /// <summary>
        /// The main collection that contains every person in the IMDB database.
        /// </summary>
        private ICollection<Person> _people;

        /// <summary>
        /// Database constructor, only has to initialize an IMDBDatabase.DataReader
        /// and reads the data to the main collection.
        /// </summary>
        public Database()
        {
			DataReader dr = new DataReader();
            _titles = dr.ReadData();
            _people = dr.GetPeople();
        }

        /// <summary>
        /// Advanced research in the database, can search for every information
        /// category that a Title can have.
        /// </summary>
        /// <param name="name"> Name in the Title name.</param>
        /// <param name="type"> TitleType of the Title.</param>
        /// <param name="genre"> TitleGenre of the Title.</param>
        /// <param name="content"> Content of the Title (adult, not or both).
        /// </param>
        /// <param name="startYear"> Year of release of the Title.</param>
        /// <param name="endYear"> End year of the Title.</param>
        /// <returns> Returns an IReadable array with all the information
        /// of each title requested.</returns>
        public IReadable[] AdvancedSearch(
            string name = null, 
            TitleType type = 0, TitleGenre genre = 0, 
            bool? content = null, 
            ushort? startYear = null, ushort? endYear = null)
        {
            // Create a list for the results (list so we can use Sort())
            IEnumerable<Title> query = new List<Title>(_titles);

            if (name != null)
                query = SearchName(name, query);
            if (type != 0)
                query = SearchType(type, query);
            if (genre != 0)
                query = SearchGenre(genre, query);
            if (content != null)
                query = SearchContent((bool)content, query);
            if (startYear != null)
                query = SearchStartYear((ushort)startYear, query);
            if (endYear != null)
                query = SearchEndYear((ushort)endYear, query);

            // Sort by average rating (popularity)
            //(query as List<Title>).Sort();
            // Return IReadable Array
            return query.ToArray() as IReadable[];
        }

        /// <summary>
        /// Asks the database for a given type and returns an array of IReadable
        /// with all the valid entries.
        /// </summary>
        /// <param name="type"> TitleType to be searched for</param>
        /// <returns> IReadable array containing all the readable information
        /// from a title</returns>
        private IEnumerable<Title> SearchType(TitleType type,
            IEnumerable<Title> source)
        {
            return source.Where(x => x.Type == type);
        }

        /// <summary>
        /// Asks the database for a given name, returns an array of IReadable
        /// with all the valid entries.
        /// </summary>
        /// <param name="nameContent"> Name to be searched for</param>
        /// <returns> IReadable array containing all the readable information
        /// from a title</returns>
        private IEnumerable<Title> SearchName(string nameContent,
            IEnumerable<Title> source)
        {
            return source.Where(x => x.Name.Contains
                (nameContent, 
				StringComparison.CurrentCultureIgnoreCase));
        }

        /// <summary>
        /// Asks the database for a given content, true for adult false for not,
        /// and returns an array with all of the valid entries.
        /// </summary>
        /// <param name="content"> True for adult content, false to exclude
        /// adult content</param>
        /// <returns> IReadable array containing all the readable information
        /// from a title</returns>
        private IEnumerable<Title> SearchContent(bool content,
            IEnumerable<Title> source)
        {
            return source.Where(x => x.AdultContent == content);
        }

        /// <summary>
        /// Asks the database for a given start year,
        /// and returns an array with all of the valid entries.
        /// </summary>
        /// <param name="year"> The start year to search for</param>
        /// <returns> IReadable array containing all the readable information
        /// from a title</returns>
        private IEnumerable<Title> SearchStartYear(ushort year,
            IEnumerable<Title> source)
        {
            return source.Where(x => x.Year.Item1 == year);
        }

        /// <summary>
        /// Asks the database for a given end year,
        /// and returns an array with all of the valid entries.
        /// </summary>
        /// <param name="year"> The end year to search for</param>
        /// <returns> IReadable array containing all the readable information
        /// from a title</returns>
        private IEnumerable<Title> SearchEndYear(ushort year,
            IEnumerable<Title> source)
        {
            return source.Where(x => x.Year.Item2 == year);
        }

        /// <summary>
        /// Asks the database for a given Genre (can search for multiple),
        /// and returns an array with all of the valid entries.
        /// </summary>
        /// <param name="genres"> TitleGenre Flags enumeration for what
        /// Genres to search for</param>
        /// <returns> IReadable array containing all the readable information
        /// from a title</returns>
        private IEnumerable<Title> SearchGenre(TitleGenre genres,
            IEnumerable<Title> source)
        {
            return source.Where(x => x.Genres == genres);
        }
    }
}
