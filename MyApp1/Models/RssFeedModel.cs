using System;
using System.Collections.Generic;

namespace MyApp1
{
	public class RssFeedModel : DataModel<RssFeedPost>
	{	

		public RssFeedModel (List<RssFeedPost> postsList)
		{
			this.data = parsePostsByCats(postsList);
		}

		// Parse input posts by categories
		public Dictionary<string, List<RssFeedPost>> parsePostsByCats(List<RssFeedPost> postsList) {
			var resDict = new Dictionary<string, List<RssFeedPost>> ();

			foreach(RssFeedPost post in postsList) {
				string postCat = post.Category;

				if (!resDict.ContainsKey (postCat)) {
					resDict.Add (postCat, new List<RssFeedPost> ());
				}

				resDict [postCat].Add(post);
			}

			return resDict;
		}
	}
}

