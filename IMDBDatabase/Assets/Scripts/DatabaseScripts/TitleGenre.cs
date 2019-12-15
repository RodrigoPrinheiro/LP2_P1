/// @file
/// @brief This file contains the enumeration IMDBDatabase.TitleGenre, which
/// defines the genres of a given title
///
/// @author Rodrigo Pinheiro & Tomás Franco.
/// @date 2019

using System;

namespace IMDBDatabase
{
    /// <summary>
    /// Genres of a title, uses the attribute [Flags] so 1 variable of type
    /// IMDBDatabase.TitleGenre can contain multiple genres.
    /// </summary>
    [Flags]
    public enum TitleGenre
    {
        /// <summary> defines Genre as Ignore </summary>
        Ignore = 0b_0000_0000_0000_0000_0000_0000_0000_0000,
        /// <summary> defines Genre as Action </summary>
        Action = 0b_0000_0000_0000_0000_0000_0000_0000_0001,        // 1
        /// <summary> defines Genre as Adventure </summary>
        Adventure = 0b_0000_0000_0000_0000_0000_0000_0000_0010,     // 2
        /// <summary> defines Genre as Animation </summary>
        Animation = 0b_0000_0000_0000_0000_0000_0000_0000_0100,     // 3
        /// <summary> defines Genre as Biography </summary>
        Biography = 0b_0000_0000_0000_0000_0000_0000_0000_1000,     // 4
        /// <summary> defines Genre as Comedy </summary>
        Comedy = 0b_0000_0000_0000_0000_0000_0000_0001_0000,        // 5
        /// <summary> defines Genre as Crime </summary>
        Crime = 0b_0000_0000_0000_0000_0000_0000_0010_0000,         // 6
        /// <summary> defines Genre as Documentary </summary>
        Documentary = 0b_0000_0000_0000_0000_0000_0000_0100_0000,   // 7
        /// <summary> defines Genre as Drama </summary>
        Drama = 0b_0000_0000_0000_0000_0000_0000_1000_0000,         // 8
        /// <summary> defines Genre as Family </summary>
        Family = 0b_0000_0000_0000_0000_0000_0001_0000_0000,        // 9
        /// <summary> defines Genre as Fantasy </summary>
        Fantasy = 0b_0000_0000_0000_0000_0000_0010_0000_0000,       // 10
        /// <summary> defines Genre as FilmNoir </summary>
        FilmNoir = 0b_0000_0000_0000_0000_0000_0100_0000_0000,      // 11
        /// <summary> defines Genre as GameShow </summary>
        GameShow = 0b_0000_0000_0000_0000_0000_1000_0000_0000,      // 12
        /// <summary> defines Genre as History </summary>
        History = 0b_0000_0000_0000_0000_0001_0000_0000_0000,       // 13
        /// <summary> defines Genre as Horror </summary>
        Horror = 0b_0000_0000_0000_0000_0010_0000_0000_0000,        // 14
        /// <summary> defines Genre as Music </summary>
        Music = 0b_0000_0000_0000_0000_0100_0000_0000_0000,         // 15
        /// <summary> defines Genre as Musical </summary>
        Musical = 0b_0000_0000_0000_0000_1000_0000_0000_0000,       // 16
        /// <summary> defines Genre as Mystery </summary>
        Mystery = 0b_0000_0000_0000_0001_0000_0000_0000_0000,       // 17
        /// <summary> defines Genre as News </summary>
        News = 0b_0000_0000_0000_0010_0000_0000_0000_0000,          // 18
        /// <summary> defines Genre as RealityTv </summary>
        RealityTv = 0b_0000_0000_0000_0100_0000_0000_0000_0000,     // 19
        /// <summary> defines Genre as Romance </summary>
        Romance = 0b_0000_0000_0000_1000_0000_0000_0000_0000,       // 20
        /// <summary> defines Genre as SciFi </summary>
        SciFi = 0b_0000_0000_0001_0000_0000_0000_0000_0000,         // 21
        /// <summary> defines Genre as Sport </summary>
        Sport = 0b_0000_0000_0010_0000_0000_0000_0000_0000,         // 22
        /// <summary> defines Genre as TalkShow </summary>
        TalkShow = 0b_0000_0000_0100_0000_0000_0000_0000_0000,      // 23
        /// <summary> defines Genre as Thriller </summary>
        Thriller = 0b_0000_0000_1000_0000_0000_0000_0000_0000,      // 24
        /// <summary> defines Genre as War </summary>
        War = 0b_0000_0001_0000_0000_0000_0000_0000_0000,           // 25
        /// <summary> defines Genre as Western </summary>
        Western = 0b_0000_0010_0000_0000_0000_0000_0000_0000,       // 26
        /// <summary> defines Genre as Short </summary>
        Short = 0b_0000_0100_0000_0000_0000_0000_0000_0000,         // 27
        /// <summary> defines Genre as Adult </summary>
        Adult = 0b_0000_1000_0000_0000_0000_0000_0000_0000,         // 28
        /// <summary> defines Genre as None </summary>
        None = 0b_0001_0000_0000_0000_0000_0000_0000_0000           // 29
    }
}
