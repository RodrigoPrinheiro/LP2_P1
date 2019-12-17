namespace IMDBDatabase
{
	public interface IReadable
	{
		/// <summary>
		/// Provides basic info.
		/// </summary>
		/// <returns>Basic info separated by tabs</returns>
		string GetBasicInfo();
		/// <summary>
		/// Provides detailed info.
		/// </summary>
		/// <returns>Detailed info separated by tabs</returns>
		string GetDetailedInfo();
		/// <summary>
		/// Provides parent info (used by episodes)
		/// </summary>
		/// <returns>Parent info</returns>
        IReadable GetParentInfo();
		/// <summary>
		/// Provides all associated titles.
		/// </summary>
		/// <returns>All coupled titles</returns>
        IReadable[] GetCoupled();
		/// <summary>
		/// Provides all persons involved in a title.
		/// </summary>
		/// <returns>All involved persons</returns>
        IReadable[] GetCrew();
	}
}
