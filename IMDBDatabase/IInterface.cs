using System;

namespace IMDBDatabase
{
	public interface IInterface
	{
		/// <summary>
		/// Render start menu
		/// </summary>
		/// <param name="userDecision">final user decision index</param>
		void RenderStartMenu(out int userDecision);
		/// <summary>
		/// Render interactive search bar.
		/// </summary>
		/// <param name="searchingBy">Searching header</param>
		/// <param name="clear">Should it clear the console?</param>
		/// <returns>Inputted string</returns>
		string RenderSearchBar(string searchingBy, bool clear = true);
		/// <summary>
		/// Render content selection
		/// </summary>
		/// <returns>Is for adult?</returns>
		bool? RenderContentChoice();
		/// <summary>
		/// Show all passed search results in a window
		/// </summary>
		/// <param name="results">Search results to show</param>
		void ShowTitleSearchResult(IReadable[] results);
		/// <summary>
		/// Render an custom error on screen.
		/// </summary>
		/// <param name="error">Error description</param>
		void RenderError(string error);
		/// <summary>
		/// A custom writeline with the choice to make it a slow write.
		/// </summary>
		/// <param name="msg">Message to write</param>
		/// <param name="slowWrite">Should it be slow written?</param>
		void ShowMsg(string msg, bool slowWrite = false);
		/// <summary>
		/// Show a fake loading process.
		/// </summary>
		/// <param name="fakeProcess">Fake process description</param>
		void ShowFakeLoadingProcess(string fakeProcess);
		/// <summary>
		/// Wait for milliseconds (Thread Sleep equivalent)
		/// </summary>
		/// <param name="millisecs">Milliseconds to wait for</param>
		void WaitForMilliseconds(int millisecs);
		/// <summary>
		/// Render advanced search menu.
		/// </summary>
		/// <param name="database">Main database to use</param>
        void RenderAdvancedSearch(Database database);
	}
}
