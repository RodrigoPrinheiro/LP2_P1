using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
	/// <summary>
	/// Basic set of information to identify a person in a IMDBDatabase.Title. 
	/// </summary>
	public class Person
	{
		private ICollection<Title> _filmography;

		public string Name { get; }
		public string Professions { get; }
		public Tuple<ushort?, ushort?> Year { get; }

		public Person(string name, ushort? birthYear, ushort? deathYear, 
            string professions, ICollection<Title> knownForTitles)
		{
			Name = name;
			Professions = professions;
			Year = new Tuple<ushort?, ushort?>(birthYear, deathYear);
			_filmography = knownForTitles;
		}

		public IEnumerable<Title> Filmography
		{
			get
			{
				foreach (Title id in _filmography)
					yield return id;
			}
		}
	}
}
