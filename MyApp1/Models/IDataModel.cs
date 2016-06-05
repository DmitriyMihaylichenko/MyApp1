using System;

using System.Collections.Generic;

namespace MyApp1
{
	public interface IDataModel<T>
	{
		string[] getAllSections();
		string getSectionByIndex(nint index);
		List<T> getItemsInSectionByName(string name);
	}
}

