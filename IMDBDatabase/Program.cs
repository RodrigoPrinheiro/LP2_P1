using System;

namespace IMDBDatabase
{
    class Program
    {
        private static void PineTreeDebug()
        {
            string test = "Sci-Fi, Drama";
            TitleID id = new TitleID(2, 3, 12.2f, "SHORT", "Sci-Fi, Drama");

            char[] seperators = { ',', ' ' };
            Console.WriteLine(test.Split(seperators, StringSplitOptions.RemoveEmptyEntries));
            //Console.WriteLine(test);
            Console.WriteLine(id.ID);
            Console.WriteLine(id.Votes);
            Console.WriteLine(id.Type);
            Console.WriteLine(id.Genres[0]);
            Console.WriteLine(id.Genres[1]);
        }

        static void Main(string[] args)
        {
            PineTreeDebug();
        }

    }
}
