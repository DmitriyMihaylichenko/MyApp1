using System;
using System.Linq;
using System.IO;
using System.Json;
using System.Collections;
using System.Collections.Generic;

namespace MyApp1
{
	public class ApplicationsModel : DataModel<AppInfo>
	{
		public ApplicationsModel (string fileName)
		{
			if (fileName != string.Empty) {
				this.data = parseJSON (fileName);
			} else {
				throw new Exception (Const.appJSONFileNotSet);
			}
		}

		// Parse items from JSON
		public Dictionary<string, List<AppInfo>> parseJSON(string fileName) {
			var resDict = new Dictionary<string, List<AppInfo>> ();

			string text = File.ReadAllText(fileName);

			var json = JsonValue.Parse(text);
			var data = json["applications"];

			foreach (JsonValue sectionEntry in data) {
				string sectionName = sectionEntry["sectionName"];

				if(!resDict.ContainsKey(sectionName)){
					resDict.Add (sectionName, new List<AppInfo>());
				}

				List<AppInfo> appsListInfo = new List<AppInfo> ();

				foreach(JsonValue sectionContent in sectionEntry["sectionContent"]) {
					string appName = sectionContent["appName"];
					string appIcon = sectionContent["appIcon"];
					string appContr = sectionContent["appContr"];

					appsListInfo.Add (new AppInfo(){ Name = appName,
													 Icon = appIcon,
													 Contr = appContr });
				}

				resDict[sectionName] = appsListInfo;
			}

			return resDict;
		}
	}
}

