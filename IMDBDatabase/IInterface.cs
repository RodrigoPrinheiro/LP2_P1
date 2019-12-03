using System.Collections.Generic;

namespace IMDBDatabase
{
	public interface IInterface
	{
		void ShowMenu();
		// Debug VVV
		void ShowSearchResult(string[] strings);
		//void ShowSearchResult(IEnumerable<Title> results);
		void RenderError(string error);
		void ShowMsg(string msg, bool slowWrite = false);
		void ShowFakeLoadingProcess(string fakeProcess);
		void WaitForMilliseconds(int millisecs);
		char WaitForAnyUserKeyPress();
		string WaitForUserTextInput();
	}
}
