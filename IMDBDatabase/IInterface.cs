using System;

namespace IMDBDatabase
{
	public interface IInterface
	{
		void RenderStartMenu(out int userDecision);
		string RenderSearchBar(string searchingBy);
		bool? RenderContentChoice();
		void ShowTitleSearchResult(IReadable[] results);
		void RenderError(string error);
		void ShowMsg(string msg, bool slowWrite = false);
		void ShowFakeLoadingProcess(string fakeProcess);
		void WaitForMilliseconds(int millisecs);
	}
}
