using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
    /// <summary>
    /// Basic set of information to identify a person in a IMDBDatabase.Title. 
    /// </summary>
    public class Person : IHasID
    {
        private HashSet<IHasID> _filmography;

        public int ID { get; }
        public string Name { get; }
        public string[] Professions { get; }
        public Tuple<ushort?, ushort?> Year { get; }

        public Person(int id, string name, string[] year, string[] professions)
        {
            ushort i;
            // Nullable integers so we can have null years in case of a movie etc...
            ushort? startYear = null;
            ushort? endYear = null;

            // Parse the years
            startYear = UInt16.TryParse(year[0], out i) ? i : (ushort?)null;
            endYear = UInt16.TryParse(year[1], out i) ? i : (ushort?)null;

            ID = id;
            Name = name;
            Professions = professions;
            Year = new Tuple<ushort?, ushort?>(startYear, endYear);

            _filmography = new HashSet<IHasID>();
        }

        public IEnumerable<IHasID> Filmography
        {
            get
            {
                foreach (IHasID id in _filmography)
                    yield return id;
            }
        }

        public void AddTitle(IHasID id)
        {
            _filmography.Add(id);
        }
    }
}
