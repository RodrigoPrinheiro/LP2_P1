/// @file
/// @brief This file has the class IMDBDatabase.Person, this class is the basic
/// data-structure for a person in the database. It's coupled to the corresponding
/// IMDBDatabase.Title
/// 
/// @author Rodrigo Pinheiro & Tomás Franco
/// @data 2019

using System;
using System.Collections.Generic;
using System.Linq;

namespace IMDBDatabase
{
	/// <summary>
	/// Basic set of information to identify a person in a IMDBDatabase.Title. 
	/// </summary>
	public class Person : IReadable
	{
        /// <summary>
        /// Collection with all the movies this person has worked one.
        /// </summary>
		private ICollection<Title> _movies;
        /// <summary>
        /// Public read-only property with the name of the person.
        /// </summary>
		public string Name { get; }
        /// <summary>
        /// Public read-only property with the professions of this person.
        /// </summary>
        public string Professions { get; }
        /// <summary>
        /// Birth year and Death year of the person
        /// </summary>
		public Tuple<ushort?, ushort?> Year { get; }
        /// <summary>
        /// Basic constructor for a Person instance, initializes every field with
        /// the given information.
        /// </summary>
        /// <param name="name"> Name of the person</param>
        /// <param name="birthYear"> Birth year of the person</param>
        /// <param name="deathYear"> Death year of the person</param>
        /// <param name="professions"> Professions the person is known for</param>
        /// <param name="knownForTitles"> Titles this person is known for</param>
		public Person(string name, ushort? birthYear, ushort? deathYear, 
            string professions, ICollection<Title> knownForTitles)
		{
			Name = name;
			Professions = professions;
			Year = new Tuple<ushort?, ushort?>(birthYear, deathYear);
            _movies = knownForTitles;
		}

        /// <summary>
        /// Method that returns an Enumerable of the Movies the person
        /// is known for
        /// </summary>
        /// <return> IReadable array with the information of each movie</return>
		public IReadable[] GetCoupled()
        {
            return _movies.ToArray<IReadable>();
		}

        /// <summary>
        /// Gets the basic information to write a person on the screen.
        /// </summary>
        /// <returns> String containing the Name of the person</returns>
        public string GetBasicInfo()
        {
            return $"{Name}\t{Professions.Split(',', ' ')[0]}";
        }

        /// <summary>
        /// Gets the complete information to write a person on the screen.
        /// </summary>
        /// <returns> String containing the Name, Birth year, 
        /// Death year and Professions of the person</returns>
        public string GetDetailedInfo()
        {
            string birthYear;
            string deathYear;

            // Parse the year
            birthYear = Year.Item1 == null ? @"\N" : Year.Item1.ToString();
            deathYear = Year.Item2 == null ? @"\N" : Year.Item1.ToString();

            return $"{Name}\t{birthYear}\t{deathYear}\t{Professions}";
        }

		/// <summary>
		/// Unused method.
		/// </summary>
		/// <returns>null</returns>
        public IReadable GetParentInfo() => null;
		/// <summary>
		/// Unused method.
		/// </summary>
		/// <returns>null</returns>
        public IReadable[] GetCrew() => null;
    }
}
