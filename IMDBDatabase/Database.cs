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
            IEnumerable<Title> intersectedGenreTitles;
            intersectedGenreTitles = _titles.
                Where(x => x?.Genres[0].ToLower().Contains(genres[0]) ?? false);

            if(genres.Length > 1)
                for (int i = 1; i < genres.Length; i++)
                {
                    intersectedGenreTitles.Intersect(
                        _titles.Where(x => x?.Genres[i].Contains
                            (genres[i], StringComparison.CurrentCultureIgnoreCase)
                            ?? false));
                }

            return intersectedGenreTitles;
        }
    }
}
