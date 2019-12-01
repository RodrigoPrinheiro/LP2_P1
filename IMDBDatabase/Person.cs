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
        public int Name { get; }

        public IEnumerable<IHasID> Filmography
        {
            get
            {
                foreach (IHasID id in _filmography)
                    yield return id;
            }
        }

        private HashSet<IHasID> _filmography;
        public void AddTitle(IHasID id)
        {
            _filmography.Add(id);
        }
    }
}
