/// @file
/// @brief This file contains the enumeration IMDBDatabase.TitleType, which
/// defines the type of each Title.
/// 
/// @author Rodrigo Pinheiro
/// @date 2019

namespace IMDBDatabase
{
    /// <summary>
    /// Type of a Title
    /// </summary>
    public enum TitleType
    {
        /// <summary> defines Title as Movie</summary>
        Movie,
        /// <summary> defines Title as TvMovie</summary>
        TvMovie,
        /// <summary> defines Title as TvSeries</summary>
        TvSeries,
        /// <summary> defines Title as TvEpisode</summary>
        TvEpisode,
        /// <summary> defines Title as TvShort</summary>
        TvShort,
        /// <summary> defines Title as TvMiniSeries</summary>
        TvMiniSeries,
        /// <summary> defines Title as VideoGame</summary>
        VideoGame,
        /// <summary> defines Title as Video</summary>
        Video,
        /// <summary> defines Title as Short</summary>
        Short,
    };
}
