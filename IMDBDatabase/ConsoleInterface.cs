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

		private const byte _MIN_RANDOM_SLOW_WRITE_TIME = 10;
		private const byte _MAX_RANDOM_SLOW_WRITE_TIME = 60;
		private const byte _MIN_RANDOM_DOT_WRITE_TIME = 200;
		private const int  _MAX_RANDOM_DOT_WRITE_TIME = 380;

		private const ConsoleColor  _DEFAULT_FG_COLOR = ConsoleColor.White;
		private const ConsoleColor  _DEFAULT_BG_COLOR = ConsoleColor.Black;

		private const char  _BLOCK_CHAR = '█';

		private Random _rand;

		public ConsoleInterface()
		{
			OutputEncoding = Encoding.UTF8;

			_rand = new Random();

			ForegroundColor = _DEFAULT_FG_COLOR;

			Console.SetWindowSize(_CONSOLE_WINDOW_WIDTH, _CONSOLE_WINDOW_HEIGHT);
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

		//public void ShowSearchResult(IEnumerable<Title> results)
		//{
		//	RenderSolidBackgroundBlock(
		//		ConsoleColor.DarkYellow,
		//		new int[] { 40, 25 },
		//		new int[2] { 45, 55 });

		//	foreach (Title t in results)
		//	{
		//		SlowWrite(t.Name);
		//		//t.ToString();
		//	}
		//}

		// DEBUG VVV
		public void ShowSearchResult(string[] strings)
		{
			int headerTopPosition = 11;
			int headerLeftPosition = 6;
			int yIndex = 1;

			string header = "" +
				"ID	NAME				AVERAGE_RATING	VOTES	TYPE	GENRES		ADULT";

			// Draw yellow
			RenderSolidBackgroundBlock(
				ConsoleColor.DarkYellow,
				new int[2] { headerTopPosition - 1, headerLeftPosition - 1 },
				new int[2] { 35, 105 });
			// Make hole
			RenderSolidBackgroundBlock(
				ConsoleColor.Black,
				new int[2] { headerTopPosition + 2, headerLeftPosition},
				new int[2] { 33, 104 });
			SetCursorPosition(headerLeftPosition, headerTopPosition);
			Write(header);


			foreach (string title in strings)
			{
				SetCursorPosition(
					headerLeftPosition, headerTopPosition + ++yIndex);

				Write("696969	Wild Mr. Mozby Dreams		5.0           	180	TVSeries action/adventure X");
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

		public char WaitForAnyUserKeyPress() => ReadKey().KeyChar;

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

		private void SetCursorPosition(int? left = null, int? top = null)
		{
			if (left != null) CursorLeft = (int)left;
			if (top != null) CursorTop = (int)top;
		}
	}
}
