using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using static System.Console;

namespace IMDBDatabase
{
	public class ConsoleInterface : IInterface
	{
		private const byte _CONSOLE_WINDOW_WIDTH = 110;
		private const byte _CONSOLE_WINDOW_HEIGHT = 40;

		private const byte _MIN_RANDOM_SLOW_WRITE_TIME = 4;
		private const byte _MAX_RANDOM_SLOW_WRITE_TIME = 15;
		private const byte _MIN_RANDOM_DOT_WRITE_TIME = 200;
		private const int  _MAX_RANDOM_DOT_WRITE_TIME = 380;

		private const byte _MAX_TITLE_NAME_DISPLAY_CHARS = 82;
		private const byte _MAX_SEARCH_RESULT_DISPLAY_TITLES = 10;

		private const string _NAME_HEADER = "     TITLE_NAME     ";
		private const string _TYPE_HEADER = "    TYPE    ";

		private const ConsoleColor  _DEFAULT_FG_COLOR = ConsoleColor.White;
		private const ConsoleColor  _DEFAULT_BG_COLOR = ConsoleColor.Black;

		private const char  _BLOCK_CHAR = '█';

		private Random _rand;

		public ConsoleInterface()
		{
			OutputEncoding = Encoding.UTF8;

			_rand = new Random();

			ForegroundColor = _DEFAULT_FG_COLOR;

			SetWindowSize(_CONSOLE_WINDOW_WIDTH, _CONSOLE_WINDOW_HEIGHT);
		}

		public void RenderError(string error)
		{
			ForegroundColor = ConsoleColor.Red;
			ShowMsg(error);
			ForegroundColor = _DEFAULT_FG_COLOR;
		}

		public void ShowMenu()
		{
			RenderSolidBackgroundBlock(
				ConsoleColor.DarkYellow, 
				new int[2] { 40, 25 }, 
				new int[2] { 45, 85 });
		}

		public void ShowMsg(string msg, bool slowWrite = false)
		{
			if (slowWrite)
				SlowWrite(msg);
			else
				Write(msg);
		}

		public void ShowSearchResult(IReadable[] results)
		{
			byte headerTopPosition = 11;
			byte headerLeftPosition = 6;
			byte typeXPos = (byte)(headerLeftPosition + 85);

			int yIndex = 1;
			int resultLengh = results.Length;

			RenderSearchResultTable(
				headerTopPosition, headerLeftPosition, typeXPos,
				resultLengh, 1, 10);

			// Write results
			string resultBasicInfo = null;
			byte resultAmmount = 0;
			for (int i = 0; i < results.Length; i++)
			{
				// Get info
				resultBasicInfo = results[i].GetBasicInfo();
				string[] splitBasicInfo = resultBasicInfo.Split('\t');

				// Write title name
				int currentCharNum = 0;

				SetCursorPosition(headerLeftPosition, headerTopPosition + ++yIndex);

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

				yIndex++;
				resultAmmount++;

				// Max result ammount reached
				if (resultAmmount % _MAX_SEARCH_RESULT_DISPLAY_TITLES == 0)
				{
					// WAIT FOR USER INPUT METHOD
					// 
					switch (WaitForAnyUserKeyPress())
					{
						case ConsoleKey.LeftArrow:
							yIndex = 1;
							resultAmmount = 0;
							if (i - _MAX_SEARCH_RESULT_DISPLAY_TITLES > -1)
								i -= _MAX_SEARCH_RESULT_DISPLAY_TITLES * 2;
							else
								i = -1;

							UpdateResultViewport(
								headerTopPosition, headerLeftPosition, typeXPos,
							resultLengh, i, i + _MAX_SEARCH_RESULT_DISPLAY_TITLES);

							break;
						case ConsoleKey.RightArrow:
							resultAmmount = 0;
							yIndex = 1;

							if (i + _MAX_SEARCH_RESULT_DISPLAY_TITLES > results.Length)
								i -= _MAX_SEARCH_RESULT_DISPLAY_TITLES;

								UpdateResultViewport(
									headerTopPosition, headerLeftPosition, typeXPos,
								resultLengh, i, i + _MAX_SEARCH_RESULT_DISPLAY_TITLES);

							break;
						case ConsoleKey.UpArrow:
							break;
						case ConsoleKey.DownArrow:
							break;
						case ConsoleKey.Backspace:
							break;
					};
				}
			}

		}

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

		public ConsoleKey WaitForAnyUserKeyPress() => ReadKey().Key;

		public string WaitForUserTextInput()=> ReadLine();

		public void WaitForMilliseconds(int milliseconds)
		{
			Thread.Sleep(milliseconds);
		}

		private void SlowWrite(string text)
		{
			CursorVisible = false;
			foreach (char letter in text)
			{
				Write(letter);
				Thread.Sleep(_rand.Next(
					_MIN_RANDOM_SLOW_WRITE_TIME, 
					_MAX_RANDOM_SLOW_WRITE_TIME));
			}
			CursorVisible = true;
		}

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

		private void RenderSearchResultTable(
			byte headerTopPosition, byte headerLeftPosition, byte typeXPos,
			int lengh, int currentMinI, int maxCurrentI)
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
				lengh,currentMinI, maxCurrentI);
		}

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
				new int[2] { headerTopPosition - 2, headerLeftPosition},
				new int[2] { headerTopPosition, headerLeftPosition + 98 });

			SetCursorPosition(headerLeftPosition + 1, headerTopPosition - 2);
			BackgroundColor = ConsoleColor.DarkYellow;
			ForegroundColor = ConsoleColor.Black;
			Write($"Seeing: {currentMinI}-{maxCurrentI} of {lengh} search results");
			BackgroundColor = _DEFAULT_BG_COLOR;
			ForegroundColor = _DEFAULT_FG_COLOR;

		}

		private void SetCursorPosition(int? left = null, int? top = null)
		{
			if (left != null) CursorLeft = (int)left;
			if (top != null) CursorTop = (int)top;
		}
	}
}
