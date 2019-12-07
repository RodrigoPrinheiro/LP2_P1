using System;
using System.Collections.Generic;
using System.Text;

namespace IMDBDatabase
{
	public interface IReadable
	{
		string GetBasicInfo();
		string GetDetailedInfo();
	}
}
