using System;

namespace IMDBDatabase
{
    class Program
    {
        private static void PineTreeDebug()
        {
            TitleGenre genre = default;
            TitleID id = new TitleID(2, 3, 12.2f, "SHORT", null);

            Console.WriteLine(genre);
            Console.WriteLine(id);
        }

        static void Main(string[] args)
        {
            PineTreeDebug();
        }

    }
}
