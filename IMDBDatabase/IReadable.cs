namespace IMDBDatabase
{
	public interface IReadable
	{
		string GetBasicInfo();
		string GetDetailedInfo();
        IReadable GetParentInfo();
        IReadable[] GetCoupled();
        IReadable[] GetCrew();
	}
}
