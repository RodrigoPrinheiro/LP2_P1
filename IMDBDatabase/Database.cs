using System.Linq;
using System.Collections.Generic;
using System;

namespace IMDBDatabase
{
    public class Database
    {
        private ICollection<Title> _titles;

        public Database()
        {
			DataReader dr = new DataReader();

            _titles = dr.ReadData();
        }

        public void AddTitle(Title title)
        {
            _titles.Add(title);
        }

        public IReadable[] SearchType(string type)
        {
            TitleType search = (TitleType)Enum.Parse
                (typeof(TitleType), type, true);

            return _titles.Where(x => x.Type == search).ToArray()
                as IReadable[];
        }

        public IReadable[] SearchName(string nameContent)
        {
            return _titles.Where(x => x.Name.Contains
                (nameContent, 
				StringComparison.CurrentCultureIgnoreCase)).ToArray<IReadable>();
        }

        public IReadable[] SearchContent(bool content)
        {
            return _titles.Where(x => x.AdultContent == content).ToArray()
				as IReadable[];
        }

        public IReadable[] SearchStartYear(ushort year)
        {
            return _titles.Where(x => x.Year.Item1 == year).ToArray()
				as IReadable[];
        }

        public IReadable[] SearchEndYear(ushort year)
        {
            return _titles.Where(x => x.Year.Item2 == year).ToArray()
				as IReadable[];
        }

        public IReadable[] SearchGenre(TitleGenre genres)
        {
            return _titles.Where(x => x.Genres == genres).ToArray()
                as IReadable[];
        }
    }
}
