﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Text;
using static System.Console;

namespace IMDBDatabase
{
	public class ConsoleInterface : IInterface
	{
		private const byte _MIN_RANDOM_SLOW_WRITE_TIME = 10;
		private const byte _MAX_RANDOM_SLOW_WRITE_TIME = 60;
		private const byte _MIN_RANDOM_DOT_WRITE_TIME = 200;
		private const int  _MAX_RANDOM_DOT_WRITE_TIME = 380;

		private Random _rand;

		public ConsoleInterface()
		{
			OutputEncoding = Encoding.UTF8;

			_rand = new Random();

		}

		public void RenderError(string error)
		{
			ForegroundColor = ConsoleColor.Red;
			ShowMsg(error);
			ForegroundColor = ConsoleColor.White;
		}

		public void ShowMenu()
		{
			throw new NotImplementedException();
		}

		public void ShowMsg(string msg, bool slowWrite = false)
		{
			if (slowWrite)
				SlowWrite(msg);
			else
				Write(msg);
		}

		public void ShowSearchResult(IEnumerable<Title> results)
		{
			foreach(Title t in results)
			{
				SlowWrite(t.Name);
				//t.ToString();
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
	}
}
