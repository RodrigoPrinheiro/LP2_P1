using System;

namespace IMDBDatabase
{
	public interface IInterface
	{
		void ShowMenu();
		void ShowSearchResult(IReadable[] results);
		void RenderError(string error);
		void ShowMsg(string msg, bool slowWrite = false);
		void ShowFakeLoadingProcess(string fakeProcess);
		void WaitForMilliseconds(int millisecs);
		ConsoleKey WaitForAnyUserKeyPress();
		string WaitForUserTextInput();
	}
}
