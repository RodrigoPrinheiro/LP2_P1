using System.Linq;
using System.Collections.Generic;
using System;
using System.Text;

namespace IMDBDatabase
{
    public class Database
    {
        private ICollection<Title> _titles;

        public Database()
        {
            _titles = new List<Title>();
        }

        public void AddTitle(Title title)
        {
            _titles.Add(title);
        }

        public IEnumerable<Title> SearchType(string type)
        {
            TitleType search = (TitleType)Enum.Parse
                (typeof(TitleType), type, true);

            return _titles.Where(x => x.Type == search);
        }

        public IEnumerable<Title> SearchName(string nameContent)
        {
            return _titles.Where(x => x.Name.Contains
                (nameContent, StringComparison.CurrentCultureIgnoreCase));
        }

        public IEnumerable<Title> SearchContent(bool content)
        {
            return _titles.Where(x => x.AdultContent == content);
        }

        public IEnumerable<Title> SearchStartYear(ushort year)
        {
            return _titles.Where(x => x.Year.Item1 == year);
        }

        public IEnumerable<Title> SearchEndYear(ushort year)
        {
            return _titles.Where(x => x.Year.Item2 == year);
        }
        
        public IEnumerable<Title> SearchGenres(params string[] genres)
        {

        }
    }
}
