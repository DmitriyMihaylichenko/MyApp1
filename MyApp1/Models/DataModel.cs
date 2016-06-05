using System;
using System.Linq;
using System.Collections.Generic;

namespace MyApp1
{
	public class DataModel<T> : IDataModel<T>
	{
		public Dictionary<string, List<T>> data { get; set; }

		public DataModel ()
		{
			data = new Dictionary<string, List<T>> ();
		}

		// Get all sections from Model
		public string[] getAllSections() =>
			(from sect in data select sect.Key).ToArray<string>();

		public int getSectionsCount () =>
			getAllSections().Length;

		// Get all items count
		public int getItemsCount () {
			var sectCont = (from val in data 
							select val.Value);

			int counter = 0;

			foreach (List<T> itList in sectCont) {
				foreach(T it in itList) {
					counter++;
				}
			}

			return counter;
		}

		// Get all sections from Model
		public string getSectionByIndex(nint index) =>
			getAllSections()[index];

		// Get all items from current section
		public List<T> getItemsInSectionByName(string name) =>
			(from sect in data where sect.Key == name select sect.Value).First().ToList<T>();

		// Get items from section using section index
		public List<T> getItemsInSectionByIndex(nint index) {
			nint ind = 0;

			string sectName = string.Empty;

			foreach (KeyValuePair<string, List<T>> pair in data) {
				if(ind == index) {
					sectName = pair.Key;

					break;
				}

				ind++;
			}

			return getItemsInSectionByName (sectName);
		}

		// Check is section in Model
		public bool isSectionPresent(string name) =>
			(from sect in data where sect.Key == name select sect.Key).ToList().Count > 0 ? true : false;

		// Create a new section and add items into
		public void sectionWithItemsCreate(string name, List<T> items) =>
			data.Add(name, items);

		// Remove section by name
		public void sectionRemove(string name) =>
			data.Remove(name);
	}
}

