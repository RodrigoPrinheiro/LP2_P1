﻿/// @file
/// @brief This file will handle all the visuals and user selections.
/// 
/// @author Tomás Franco e Rodrigo Pinheiro
/// @date 2019

using System;
using System.Threading;
using System.Text;
using static System.Console;

namespace IMDBDatabase
{
    public class ConsoleInterface : IInterface
    {
        /// <summary>
        /// Console window width.
        /// </summary>
        private const byte _CONSOLE_WINDOW_WIDTH = 110;
        /// <summary>
        /// Console window height.
        /// </summary>
        private const byte _CONSOLE_WINDOW_HEIGHT = 40;

        /// <summary>
        /// Minimum slow write letter delay.
        /// </summary>
        private const byte _MIN_RANDOM_SLOW_WRITE_TIME = 4;
        /// <summary>
        /// Maximum slow write letter delay.
        /// </summary>
        private const byte _MAX_RANDOM_SLOW_WRITE_TIME = 10;
        /// <summary>
        /// Minimum dot write delay.
        /// </summary>
        private const byte _MIN_RANDOM_DOT_WRITE_TIME = 15;
        /// <summary>
        /// Maximum dot write delay.
        /// </summary>
        private const int _MAX_RANDOM_DOT_WRITE_TIME = 30;

        /// <summary>
        /// Max number of chars of the title name to display on screen.
        /// </summary>
        private const byte _MAX_TITLE_NAME_DISPLAY_CHARS = 82;
        /// <summary>
        /// Title results to display on screen.
        /// </summary>
        private const byte _MAX_SEARCH_RESULT_DISPLAY_TITLES = 10;

        /// <summary>
        /// Tile name header.
        /// </summary>
        private const string _NAME_HEADER = "     TITLE NAME     ";
        /// <summary>
        /// Type header.
        /// </summary>
        private const string _TYPE_HEADER = "    TYPE    ";
        /// <summary>
        /// Possible start menu choices
        /// </summary>
        private static readonly string[] _START_MENU_CHOICES =
            {"Title Name", "People", "Advanced Search"};
        /// <summary>
        /// Search menu possible options.
        /// </summary>
        private const string _SEARCH_MENU_SUBTITLE_ =
            " \t↑ : Move Selection Up\t\t\tENTER : Select Search\n" +
            " \t↓ : Move Selection Down\t\t\tESC : Exit";
        /// <summary>
        /// Search menu title options.
        /// </summary>
        private const string _SEARCH_RESULT_MENU_TXT =
            " → : Next Page\t\t↑ : Move Selection Up\t\tENTER : Select Title\n" +
            " ← : Previous Page\t↓ : Move Selection Down\t\tESC : Exit Search";
        /// <summary>
        /// Search menu detailed possible options.
        /// </summary>
        private const string _TITLE_DETAILED_MENU_TXT =
            " C : Show Title Crew\t\t\t\tE: Show Coupled Title\n" +
            " P : Go to Parent Title\t\t\t\tESC : Exit Search";
        /// <summary>
        /// Title info sub-info.
        /// </summary>
        private static readonly string[] _TITLE_INFO_HEADERS =
            {"Type", "Adult Content", "Score", "Votes",
            "Start Year", "End Year", "Genres"};
        /// <summary>
        /// Person header info to use in detailed person info.
        /// </summary>
        private static readonly string[] _PERSON_DETAILED_INFO =
            {"Birth Year", "Death Year", "Professions"};
        /// <summary>
        /// Search bar possible menu options.
        /// </summary>
        private static readonly string _SEARCH_BAR_MENU_TXT =
            "\t\t\t\t\tEnter: Search\n" +
            "\t\t\t\t\t  Esc: Exit";

		/// <summary>
		/// Advanced search menu options.
		/// </summary>
        private static readonly string[] _ADVANCED_SEARCH_OPTIONS =
            {"Name", "Type", "Genre", "Adult Content",
            "Start Year", "End Year", "##### SEARCH FOR TITLES! ####"};

		/// <summary>
		/// Title Genres.
		/// </summary>
        private static readonly string[] _GENRES =
        {
            "Action", "Adventure", "Animation", "Biography", "Comedy", "Crime",
            "Documentary", "Drama", "Family", "Fantasy", "FilmNoir", "GameShow",
            "History", "Horror", "Music", "Musical", "Mystery", "News",
            "RealityTv", "Romance", "SciFi", "Sport", "TalkShow", "Thriller",
            "War", "Western", "Short", "Adult"
        };

        /// <summary>
        /// IMDB logo.
        /// </summary>
        private static readonly string[] _TITLE_ =
            { "██╗███╗   ███╗██████╗ ██████╗", "██║████╗ ████║██╔══██╗██╔══██╗",
            "██║██╔████╔██║██║  ██║██████╔╝", "██║██║╚██╔╝██║██║  ██║██╔══██╗",
            "██║██║ ╚═╝ ██║██████╔╝██████╔╝", "╚═╝╚═╝     ╚═╝╚═════╝ ╚═════╝",
            "\t\t  The Database"};

        /// <summary>
        /// Default foreground color.
        /// </summary>
        private const ConsoleColor _DEFAULT_FG_COLOR = ConsoleColor.White;
        /// <summary>
        /// Default background color.
        /// </summary>
        private const ConsoleColor _DEFAULT_BG_COLOR = ConsoleColor.Black;
        /// <summary>
        /// Default logo color.
        /// </summary>
        private const ConsoleColor _DEFAULT_TITLE_COLOR = ConsoleColor.Magenta;

        /// <summary>
        /// Selection arrow char
        /// </summary>
        private const char _ARROW_CHAR = '►';

        /// <summary>
        /// Current arrow selection index.
        /// </summary>
        private int _selectionArrowIndex;
        /// <summary>
        /// Left and Top position of the arrow
        /// </summary>
        private int[] _selectionArrowPos;
        /// <summary>
        /// Random
        /// </summary>
        private Random _rand;

        /// <summary>
        /// Constructor
        /// </summary>
        public ConsoleInterface()
        {
            // Get the console ready
            OutputEncoding = Encoding.UTF8;

            // Initialize variables
            _rand = new Random();
            _selectionArrowIndex = 0;
            _selectionArrowPos = new int[2];

            ForegroundColor = _DEFAULT_FG_COLOR;

            // Set the console size
            SetWindowSize(_CONSOLE_WINDOW_WIDTH, _CONSOLE_WINDOW_HEIGHT);

            // Hide cursor
            CursorVisible = false;
        }

        /// <summary>
        /// Render msg with red background.
        /// </summary>
        /// <param name="error">msg to display.</param>
        public void RenderError(string error)
        {
            BackgroundColor = ConsoleColor.Red;
            ForegroundColor = ConsoleColor.Black;
            ShowMsg(error);
            ForegroundColor = _DEFAULT_FG_COLOR;
            BackgroundColor = _DEFAULT_BG_COLOR;
        }

		/// <summary>
		/// Render the Start Menu
		/// </summary>
		/// <param name="playerDecision"></param>
        public void RenderStartMenu(out int playerDecision)
        {
            Clear();

			// Initialize variables
            byte leftPos = 39;
            byte topPos = 21;
            byte yIndex = 1;

            bool leaveSwitch = false;
            int finalDecision = 0;

            _selectionArrowIndex = 0;

			// Render visuals
            RenderIMDB();
            RenderMenu(_SEARCH_MENU_SUBTITLE_);
            RenderSolidBackgroundBlock(
                ConsoleColor.Yellow,
                new int[] { topPos, leftPos },
                new int[] { 40, 72 });

            SetCursorPosition(leftPos + 4, topPos + 1);
            BackgroundColor = _DEFAULT_TITLE_COLOR;
            Write($"{CenterString("Search for", 25),25}");
            ResetColor();

            for (int i = 0; i < _START_MENU_CHOICES.Length * 2; i++)
            {
                if (_START_MENU_CHOICES.Length > i)
                {
                    string paddedHeader = null;
                    SetCursorPosition(leftPos + 1, topPos + 2 + yIndex++ + i);
                    paddedHeader = CenterString(_START_MENU_CHOICES[i], 31);
                    Write($"{paddedHeader,31}");
                }
            }

            _selectionArrowPos[0] = leftPos - 2;
            _selectionArrowPos[1] = topPos + 3;
            RenderVerticalSelectionArrow(false);

			// Selection loop
            while (!leaveSwitch)
                switch (WaitForAnyUserKeyPress().Key)
                {
                    // Selection Up
                    case ConsoleKey.UpArrow:
                        RenderVerticalSelectionArrow(false, 3);
                        break;
                    // Selection Down
                    case ConsoleKey.DownArrow:
                        RenderVerticalSelectionArrow(true, 3);
                        break;
                    // User choice
                    case ConsoleKey.Enter:
                        finalDecision = _selectionArrowIndex;
                        leaveSwitch = true;
                        break;
                    // Exit search
                    case ConsoleKey.Escape:
                        finalDecision = -1;
                        leaveSwitch = true;
                        break;
                };

            playerDecision = finalDecision;
        }

		/// <summary>
		/// Render Advanced search menu
		/// </summary>
		/// <param name="db">Main Database</param>
        public void RenderAdvancedSearch(Database db)
        {
			// Initialize loop
            string name = null;
            TitleType type = 0;
            TitleGenre genre = 0;
            bool? content = null;
            ushort? startYear = null;
            ushort? endYear = null;

            bool leaveDetailedSearch = false;

			// Menu loop
            while (!leaveDetailedSearch)
            {
                Clear();

                byte leftPos = 39;
                byte topPos = 17;
                byte yIndex = 1;

                bool leaveSwitch = false;

                RenderMenu(_SEARCH_MENU_SUBTITLE_);
                RenderSolidBackgroundBlock(
                    ConsoleColor.Yellow,
                    new int[] { topPos, leftPos },
                    new int[] { topPos + 17, 72 });

                SetCursorPosition(leftPos + 4, topPos + 1);
                BackgroundColor = _DEFAULT_TITLE_COLOR;
                Write($"{CenterString("Set parameters:", 25),25}");
                ResetColor();

                for (int i = 0; i < _ADVANCED_SEARCH_OPTIONS.Length * 2; i++)
                {
                    if (_ADVANCED_SEARCH_OPTIONS.Length > i)
                    {
                        string paddedHeader = null;
                        SetCursorPosition(leftPos + 1, topPos + 2 + yIndex++ + i);
                        paddedHeader = CenterString(_ADVANCED_SEARCH_OPTIONS[i], 31);
                        Write($"{paddedHeader,31}");
                    }
                }

                _selectionArrowIndex = 0;
                _selectionArrowPos[0] = leftPos - 2;
                _selectionArrowPos[1] = topPos + 3;
                RenderVerticalSelectionArrow(false);

				// Selection loop
                while (!leaveSwitch)
                {
                    switch (WaitForAnyUserKeyPress().Key)
                    {
                        // Selection Up
                        case ConsoleKey.UpArrow:
                            RenderVerticalSelectionArrow(false,
                                (byte)_ADVANCED_SEARCH_OPTIONS.Length);
                            break;
                        // Selection Down
                        case ConsoleKey.DownArrow:
                            RenderVerticalSelectionArrow(true,
                                (byte)_ADVANCED_SEARCH_OPTIONS.Length);
                            break;
                        // User choice
                        case ConsoleKey.Enter:
                            switch (_selectionArrowIndex)
                            {
                                // Name
                                case 0:
                                    name = RenderSearchBar("Insert Name:");
                                    Console.WriteLine(name);
                                    break;
                                // Type
                                case 1:
                                    type = (TitleType)RenderTypeChoice();
                                    Console.WriteLine(type.ToString());
                                    break;
                                // Genre
                                case 2:
                                    genre = (TitleGenre)RenderGenreChoice();
                                    Console.WriteLine(genre.ToString());
                                    break;
                                // Content
                                case 3:
                                    content = RenderContentChoice();
                                    Console.WriteLine(content);
                                    break;
                                // Start year
                                case 4:
                                    ushort start = 0;
                                    UInt16.TryParse(
                                        RenderSearchBar("Insert Start year"),
                                        out start);
                                    if (start != 0)
                                        startYear = start;
                                    Console.WriteLine(start);
                                    break;
                                // End year
                                case 5:
                                    ushort end = 0;
                                    UInt16.TryParse(
                                        RenderSearchBar("Insert End year"),
                                        out end);
                                    if (end != 0)
                                        endYear = end;
                                    Console.WriteLine(end);
                                    break;
                                // Search
                                case 6:
                                    leaveDetailedSearch = true;
                                    break;
                            }
                            leaveSwitch = true;
                            break;
                        // Exit search
                        case ConsoleKey.Escape:
                            leaveSwitch = true;
                            leaveDetailedSearch = true;
                            break;
                    };
                }
            }

			// Show results
            ShowTitleSearchResult(
                db.AdvancedSearch(name, type, genre, content, startYear, endYear));
        }

		/// <summary>
		/// Render type choice menu
		/// </summary>
		/// <returns>Final type</returns>
        private int RenderTypeChoice()
        {
			// Variables
            bool leaveSwitch = false;

            Clear();
            byte enumLength = GetTypeSize();

            byte leftPos = 39;
            byte topPos = (byte)(21 - enumLength);
            byte yIndex = 1;

			// Render Visuals
            RenderMenu(_SEARCH_MENU_SUBTITLE_);
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[] { topPos, leftPos },
                new int[] { topPos + enumLength * 2 + 1, 72 });

            SetCursorPosition(leftPos + 4, topPos + 1);
            BackgroundColor = _DEFAULT_TITLE_COLOR;
            Write($"{CenterString("Choose Type", 25),25}");
            ResetColor();

            for (int i = 0; i < enumLength * 2; i++)
            {
                if (enumLength > i && i > 0)
                {
                    string paddedHeader = GetTypeNameByIndex(i);
                    SetCursorPosition(leftPos + 1, topPos + 1 + yIndex++ + i);
                    if (paddedHeader != null)
                        paddedHeader = CenterString(paddedHeader, 31);
                    Write($"{paddedHeader,31}");
                }
            }

            _selectionArrowIndex = 0;
            _selectionArrowPos[0] = leftPos - 2;
            _selectionArrowPos[1] = topPos + 3;
            RenderVerticalSelectionArrow(false);

			// Selection loop
            while (!leaveSwitch)
                switch (WaitForAnyUserKeyPress().Key)
                {
                    // Selection Up
                    case ConsoleKey.UpArrow:
                        RenderVerticalSelectionArrow(false,
                            (byte)(enumLength - 1));
                        break;
                    // Selection Down
                    case ConsoleKey.DownArrow:
                        RenderVerticalSelectionArrow(true,
                            (byte)(enumLength - 1));
                        break;
                    // User choice
                    case ConsoleKey.Enter:
                        leaveSwitch = true;
                        break;
                    // Exit search
                    case ConsoleKey.Escape:
                        leaveSwitch = true;
                        break;
                };

            return _selectionArrowIndex + 1;
        }

		/// <summary>
		/// Renders genre choice menu
		/// </summary>
		/// <returns>Final genre</returns>
        private TitleGenre RenderGenreChoice()
        {
			// Variables
            string userChoice = "";

            Clear();
            byte enumLength = (byte)_GENRES.Length;
            byte leftPos = 9;
            byte topPos = 20;

            TitleGenre finalGenre = TitleGenre.Ignore;

			// Visuals
            SetCursorPosition(leftPos + 4, topPos + 1);
            BackgroundColor = _DEFAULT_TITLE_COLOR;
            ResetColor();

            Write("\n");
            for (byte i = 0; i < _GENRES.Length; i++)
            {
                Write("|" + i + " - " + _GENRES[i]);
                if (i % 7 == 0 && i != 0)
                    Write('\n');
            }
            RenderMenu(_SEARCH_MENU_SUBTITLE_);
            userChoice = RenderSearchBar
                ("Genres (split by spaces using index)", false);

            foreach (string index in userChoice.Split(" "))
            {
                int parsedIndex = 0;
                WriteLine(index);
                if (Int32.TryParse(index, out parsedIndex))
                {
                    finalGenre |= (TitleGenre)Enum.Parse
                        (typeof(TitleGenre), _GENRES[parsedIndex], true);
                }
            }

            return finalGenre;
        }
		
		/// <summary>
		/// Get the size of Types enum
		/// </summary>
		/// <returns>Size of enum</returns>
        private byte GetTypeSize() =>
            (byte)Enum.GetNames(typeof(TitleType)).Length;

		/// <summary>
		/// Get Type name on Enum by index
		/// </summary>
		/// <param name="index">Enum Index</param>
		/// <returns>Type Name</returns>
		private string GetTypeNameByIndex(int index) =>
            ((TitleType)index).ToString();

		/// <summary>
		/// Render IMDB logo
		/// </summary>
        private void RenderIMDB()
        {
            byte headerLeftPosition = (byte)
                (WindowWidth / 2 - _TITLE_[0].Length / 2);

            ForegroundColor = _DEFAULT_TITLE_COLOR;
            for (int i = 0; i < _TITLE_.Length; i++)
            {
                SetCursorPosition(headerLeftPosition, 10 + i);
                Write(_TITLE_[i]);
            }
            ResetColor();
        }

		/// <summary>
		/// Render info menu
		/// </summary>
		/// <param name="text">Info</param>
        public void RenderMenu(string text)
        {
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { 43, 15 },
                new int[2] { 45, 95 });

            // Write Text
            ForegroundColor = ConsoleColor.Black;
            BackgroundColor = ConsoleColor.DarkYellow;
            SetCursorPosition(15, 43);
            string[] lines = text.Split('\n');
            foreach (string line in lines)
            {
                Write(line);
                SetCursorPosition(15, 44);
            }
            ForegroundColor = _DEFAULT_FG_COLOR;
            BackgroundColor = _DEFAULT_BG_COLOR;
        }

		/// <summary>
		/// Display a message
		/// </summary>
		/// <param name="msg">Msg to display</param>
		/// <param name="slowWrite">Slow write enabled</param>
        public void ShowMsg(string msg, bool slowWrite = false)
        {
            if (slowWrite)
                SlowWrite(msg);
            else
                Write(msg);
        }

		/// <summary>
		/// Display all given results
		/// </summary>
		/// <param name="results">Results to display</param>
        public void ShowTitleSearchResult(IReadable[] results)
        {
            if (results == null) return;
            _selectionArrowIndex = 0;
            bool exitSearchResult = false;

            byte headerTopPosition = 11;
            byte headerLeftPosition = 6;
            byte typeXPos = (byte)(headerLeftPosition + 85);

            int yIndex = 1;
            int resultLengh = results.Length;

            RenderMenu(_SEARCH_RESULT_MENU_TXT);

            RenderSearchResultTable(
                headerTopPosition, headerLeftPosition, typeXPos,
                resultLengh, 1, 9);

            // Set arrow
            _selectionArrowPos[1] = headerTopPosition + 2;
            _selectionArrowPos[0] = headerLeftPosition - 3;
            RenderVerticalSelectionArrow(false);

            // Write results
            string resultBasicInfo = null;
            byte resultAmmount = 0;
            for (int i = 0; i < results.Length + 10; i++)
            {
                if (results.Length > i)
                {
                    // Get info
                    resultBasicInfo = results[i].GetBasicInfo();
                    string[] splitBasicInfo = resultBasicInfo.Split('\t');

                    // Write title name
                    int currentCharNum = 0;

                    SetCursorPosition(
                        headerLeftPosition, headerTopPosition + ++yIndex);

                    foreach (char letter in splitBasicInfo[0])
                    {
                        if (currentCharNum >= _MAX_TITLE_NAME_DISPLAY_CHARS)
                            break;

                        Write(letter);
                        currentCharNum++;
                    }

                    // Write type info
                    SetCursorPosition(left: typeXPos);
                    Write(splitBasicInfo[1]);
                }
                yIndex++;
                resultAmmount++;

                // Max result amount reached
                if (resultAmmount % _MAX_SEARCH_RESULT_DISPLAY_TITLES == 0)
                {
                    bool leaveSwitch = false;
                    while (!leaveSwitch)
                        switch (WaitForAnyUserKeyPress().Key)
                        {
                            // Previous page
                            case ConsoleKey.LeftArrow:
                                yIndex = 1;
                                resultAmmount = 0;
                                if (i - _MAX_SEARCH_RESULT_DISPLAY_TITLES > -1)
                                    i -= _MAX_SEARCH_RESULT_DISPLAY_TITLES * 2;
                                else
                                    i = -1;
                                leaveSwitch = true;
                                break;
                            // Next Page
                            case ConsoleKey.RightArrow:
                                resultAmmount = 0;
                                yIndex = 1;
                                if (i + _MAX_SEARCH_RESULT_DISPLAY_TITLES >
                                    results.Length + 10)
                                    i -= _MAX_SEARCH_RESULT_DISPLAY_TITLES;
                                leaveSwitch = true;
                                break;

                            case ConsoleKey.UpArrow:
                                RenderVerticalSelectionArrow(false);
                                break;
                            // Selection down
                            case ConsoleKey.DownArrow:
                                RenderVerticalSelectionArrow(true);
                                break;
                            // Show detailed info of selected title
                            case ConsoleKey.Enter:
                                ShowDetailedTitleInfo(
                                    results?[i -
                                    ((_MAX_SEARCH_RESULT_DISPLAY_TITLES - 1) -
                                    _selectionArrowIndex)]);
                                break;
                            // Exit search
                            case ConsoleKey.Escape:
                                exitSearchResult = true;
                                leaveSwitch = true;
                                break;
                        };

                    UpdateResultViewport(
                        headerTopPosition, headerLeftPosition, typeXPos,
                    resultLengh, i < 0 ? 1 : i,
                    i + _MAX_SEARCH_RESULT_DISPLAY_TITLES);

                    // Exit if user ended search
                    if (exitSearchResult) break;
                }
            }

            Clear();
        }

		/// <summary>
		/// Show detailed info
		/// </summary>
		/// <param name="titleInfo">title to display info</param>
        public void ShowDetailedTitleInfo(IReadable titleInfo = null)
        {
            if (titleInfo == null) return;

            string[] titleDetailedInfo = null;
            string titleHeader = null;

            byte headerTopPosition = 10;
            byte headerLeftPosition = 10;
            byte typeXPos = (byte)(headerLeftPosition + 85);
            byte yIndex = 1;
            bool leaveSwitch = false;

            Beep();
            Clear();

            // Name, Type, AdultContent, Score, Votes, Year, Genres
            // Get the information from the title
            titleDetailedInfo = titleInfo?.GetDetailedInfo().Split('\t') ?? null;
            titleHeader = titleDetailedInfo?[0] ?? "None";

            RenderMenu(_TITLE_DETAILED_MENU_TXT);
            RenderTitleInfoTable(headerTopPosition, headerLeftPosition,
                typeXPos, 1, 9, titleHeader);

            // Write info
            for (int i = 1; i < titleDetailedInfo.Length; i++)
            {
                SetCursorPosition(
                    headerLeftPosition + 4,
                    headerTopPosition + yIndex++ + i);
                if (titleDetailedInfo.Length <= 4)
                    Write($"{_PERSON_DETAILED_INFO[i - 1],15}");
                else
                    Write($"{_TITLE_INFO_HEADERS[i - 1],15}");

                SetCursorPosition(
                    headerLeftPosition + 21,
                    headerTopPosition + yIndex - 1 + i);

                Write(titleDetailedInfo[i]);
            }

            while (!leaveSwitch)
            {
                switch (WaitForAnyUserKeyPress().Key)
                {
                    case ConsoleKey.E:
                        ShowTitleSearchResult(titleInfo.GetCoupled());
                        break;
                    case ConsoleKey.P:
                        ShowDetailedTitleInfo(titleInfo.GetParentInfo());
                        break;
                    case ConsoleKey.C:
                        ShowTitleSearchResult(titleInfo.GetCrew());
                        break;
                    case ConsoleKey.Escape:
                        leaveSwitch = true;
                        break;
                }
            }
        }

		/// <summary>
		/// Write fake loading process
		/// </summary>
		/// <param name="fakeProcess">Text to fake</param>
        public void ShowFakeLoadingProcess(string fakeProcess)
        {
            SlowWrite(fakeProcess);

            for (byte i = 0; i < 3; i++)
            {
                Write('.');
                Thread.Sleep(_rand.Next(
                    _MIN_RANDOM_DOT_WRITE_TIME,
                    _MAX_RANDOM_DOT_WRITE_TIME));
            }

            WriteLine();
            Thread.Sleep(_rand.Next(
                _MIN_RANDOM_DOT_WRITE_TIME,
                _MAX_RANDOM_DOT_WRITE_TIME));
        }

		/// <summary>
		/// Render search bar
		/// </summary>
		/// <param name="searchingBy">Header</param>
		/// <param name="clear">Toggle clear before render</param>
		/// <returns></returns>
        public string RenderSearchBar(string searchingBy, bool clear = true)
        {
            if (clear)
                Clear();

            string userTxt = "";
            bool exitSearch = false;

            RenderMenu(_SEARCH_BAR_MENU_TXT);

            // Render header
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { 13, 30 },
                new int[2] { 15, 80 });

            SetCursorPosition(35, 14);
            Write($"{CenterString(searchingBy, 40),40}");

            // Render outer Search bar
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { 15, 10 },
                new int[2] { 18, 100 });

            // Render inner Search bar
            RenderSolidBackgroundBlock(
                ConsoleColor.Black,
                new int[2] { 16, 11 },
                new int[2] { 17, 99 });
            SetCursorPosition(11, 16);

            CursorVisible = true;

            while (!exitSearch)
            {
                ConsoleKeyInfo userKey = WaitForAnyUserKeyPress();

                switch (userKey.Key)
                {
                    case ConsoleKey.Escape:
                        userTxt = null;
                        exitSearch = true;
                        break;
                    case ConsoleKey.Enter:
                        exitSearch = true;
                        break;
                    case ConsoleKey.Backspace:
                        if (userTxt.Length - 1 >= 0)
                        {
                            SetCursorPosition(CursorLeft - 1);
                            Write(" ");
                            SetCursorPosition(CursorLeft - 1);

                            userTxt = userTxt.Remove(userTxt.Length - 1);
                        }
                        break;
                    default:
                        Write(userKey.KeyChar);
                        userTxt += userKey.KeyChar;
                        break;
                }
            }

            CursorVisible = false;
            return userTxt;
        }

		/// <summary>
		/// Render content choice menu
		/// </summary>
		/// <returns>Choice</returns>
        public bool? RenderContentChoice()
        {
            Clear();

            bool? searchForAdult = null;

            RenderMenu(_SEARCH_MENU_SUBTITLE_);

            // Header txt
            SetCursorPosition(35, 20);
            Write($"{CenterString("Adult content?", 40),40}");

            // Render outer bar
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { 22, 47 },
                new int[2] { 29, 63 });

            // Render inner bar
            RenderSolidBackgroundBlock(
                ConsoleColor.Black,
                new int[2] { 22, 48 },
                new int[2] { 29, 62 });

            SetCursorPosition(54, 24);
            Write("Yes");
            SetCursorPosition(54, 26);
            Write("No");

            _selectionArrowPos[0] = 51;
            RenderVerticalSelectionArrow(false, 2);

            bool leaveSwitch = false;
            while (!leaveSwitch)
                switch (WaitForAnyUserKeyPress().Key)
                {
                    case ConsoleKey.UpArrow:
                        RenderVerticalSelectionArrow(false, 2);
                        break;
                    // Selection down
                    case ConsoleKey.DownArrow:
                        RenderVerticalSelectionArrow(true, 2);
                        break;
                    // Show detailed info of selected title
                    case ConsoleKey.Enter:
                        searchForAdult = _selectionArrowIndex == 0;
                        leaveSwitch = true;
                        break;
                    // Exit search
                    case ConsoleKey.Escape:
                        leaveSwitch = true;
                        break;
                }


            return searchForAdult;
        }

		/// <summary>
		/// Wait for any user input
		/// </summary>
		/// <returns>User input key</returns>
        private ConsoleKeyInfo WaitForAnyUserKeyPress() => ReadKey(true);

		/// <summary>
		/// Wait for milliseconds
		/// </summary>
		/// <param name="milliseconds">time to wait</param>
        public void WaitForMilliseconds(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

		/// <summary>
		/// Slow write text
		/// </summary>
		/// <param name="text">text to slow write</param>
        private void SlowWrite(string text)
        {
            foreach (char letter in text)
            {
                Write(letter);
                Thread.Sleep(_rand.Next(
                    _MIN_RANDOM_SLOW_WRITE_TIME,
                    _MAX_RANDOM_SLOW_WRITE_TIME));
            }
        }

		/// <summary>
		/// Render solid background color on given position
		/// </summary>
		/// <param name="color">Block color</param>
		/// <param name="topLeft">top left position</param>
		/// <param name="bottomRight">bottom right position</param>
        private void RenderSolidBackgroundBlock(
            ConsoleColor color, int[] topLeft, int[] bottomRight)
        {
            BackgroundColor = color;

            // Y
            for (int i = topLeft[0]; i < bottomRight[0]; i++)
            {
                SetCursorPosition(top: i);
                SetCursorPosition(topLeft[1]);

                // X
                for (int j = topLeft[1]; j < bottomRight[1]; j++)
                {
                    Write(" ");
                }
            }
            BackgroundColor = _DEFAULT_BG_COLOR;

            WriteLine();
        }

		/// <summary>
		/// Render title info table
		/// </summary>
		/// <param name="headerTopPosition">top position of header</param>
		/// <param name="headerLeftPosition">left position of header</param>
		/// <param name="typeXpos">type left position</param>
		/// <param name="currentMinI">Current minimum I</param>
		/// <param name="currentMaxI">Current maximum I</param>
		/// <param name="titleName">Target Title name</param>
        private void RenderTitleInfoTable(
            byte headerTopPosition, byte headerLeftPosition, byte typeXpos,
            int currentMinI, int currentMaxI, string titleName)
        {
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkCyan,
                new int[] { 9, 5 },
                new int[] { 30, 109 });

            SetCursorPosition(headerLeftPosition + 5, headerTopPosition);
            titleName = CenterString(titleName, _MAX_TITLE_NAME_DISPLAY_CHARS);
            // Writes the truncated/padded title
            Write($"{titleName,_MAX_TITLE_NAME_DISPLAY_CHARS}");

            // Make hole
            RenderSolidBackgroundBlock(
                ConsoleColor.Black,
                new int[2] { headerTopPosition + 2, headerLeftPosition + 20 },
                new int[2] { 25, 100 });
        }

		/// <summary>
		/// Render search result table
		/// </summary>
		/// <param name="headerTopPosition">top position of header</param>
		/// <param name="headerLeftPosition">left position of header</param>
		/// <param name="typeXPos">type left position</param>
		/// <param name="length">length</param>
		/// <param name="currentMinI">Current minimum I</param>
		/// <param name="maxCurrentI">Current maximum I</param>
		private void RenderSearchResultTable(
            byte headerTopPosition, byte headerLeftPosition, byte typeXPos,
            int length, int currentMinI, int maxCurrentI)
        {
            // Draw yellow
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { headerTopPosition - 1, headerLeftPosition - 1 },
                new int[2] { 35, 105 });

            // Write headers
            SetCursorPosition(headerLeftPosition, headerTopPosition);
            Write(_NAME_HEADER);
            SetCursorPosition(typeXPos, headerTopPosition);
            Write(_TYPE_HEADER);

            // Make hole
            UpdateResultViewport(headerTopPosition, headerLeftPosition, typeXPos,
				length, currentMinI, maxCurrentI);
        }

		/// <summary>
		/// Update Result viewport
		/// </summary>
		/// <param name="headerTopPosition">Header top position</param>
		/// <param name="headerLeftPosition">Header left position</param>
		/// <param name="typeXPos"></param>
		/// <param name="lengh"></param>
		/// <param name="currentMinI"></param>
		/// <param name="maxCurrentI"></param>
        private void UpdateResultViewport(
            byte headerTopPosition, byte headerLeftPosition, byte typeXPos,
            int lengh, int currentMinI, int maxCurrentI)
        {
            // Make hole
            RenderSolidBackgroundBlock(
                ConsoleColor.Black,
                new int[2] { headerTopPosition + 2, headerLeftPosition },
                new int[2] { 33, 104 });

            // Make Bar
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { headerTopPosition + 2, typeXPos - 2 },
                new int[2] { 35, typeXPos - 1 });

            // Make page section
            RenderSolidBackgroundBlock(
                ConsoleColor.DarkYellow,
                new int[2] { headerTopPosition - 2, headerLeftPosition },
                new int[2] { headerTopPosition, headerLeftPosition + 98 });
            SetCursorPosition(headerLeftPosition + 1, headerTopPosition - 2);
            BackgroundColor = ConsoleColor.DarkYellow;
            ForegroundColor = ConsoleColor.Black;
            Write($"Showing: {currentMinI}-{maxCurrentI} " +
                $"of {lengh} search results");

            BackgroundColor = _DEFAULT_BG_COLOR;
            ForegroundColor = _DEFAULT_FG_COLOR;
        }

		/// <summary>
		/// Change cursor position
		/// </summary>
		/// <param name="left">Left position</param>
		/// <param name="top">Top position</param>
        private void SetCursorPosition(int? left = null, int? top = null)
        {
			// Change left position
            if (left != null) CursorLeft = (int)left;
			// Change top position
            if (top != null) CursorTop = (int)top;
        }

		/// <summary>
		/// Center given string
		/// </summary>
		/// <param name="text">Text to center</param>
		/// <param name="max">max size</param>
		/// <returns></returns>
        private string CenterString(string text, byte max)
        {
            if (text.Length < max)
            {
                // Calculates the minimal padding that this title can have
                byte minPadding = (byte)
                    (max / 2 - text.Length / 2);

                // Pads the title
                text += new string(' ', Math.Clamp((max / 2) - text.Length,
                    minPadding, max));
                return text;
            }
            return text.Substring(0, _MAX_TITLE_NAME_DISPLAY_CHARS);

        }

		/// <summary>
		/// Render selection arrow
		/// </summary>
		/// <param name="incrementIndex">should increment index</param>
		/// <param name="maxIndex">Max index</param>
        private void RenderVerticalSelectionArrow(bool incrementIndex,
            byte maxIndex = _MAX_SEARCH_RESULT_DISPLAY_TITLES)
        {
            // Clear Arrow
            SetCursorPosition(_selectionArrowPos[0], _selectionArrowPos[1]);
            Write(" ");

            // Increment
            if (incrementIndex &&
                _selectionArrowIndex < maxIndex - 1)
            {
                _selectionArrowIndex++;
                _selectionArrowPos[1] += 2;
            }
            // Decrement
            else if (!incrementIndex && _selectionArrowIndex > 0)
            {
                _selectionArrowIndex--;
                _selectionArrowPos[1] -= 2;
            }

            // New Arrow
            SetCursorPosition(_selectionArrowPos[0], _selectionArrowPos[1]);
            Write(_ARROW_CHAR);
        }
    }
}