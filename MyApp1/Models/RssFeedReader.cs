using System;

using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

namespace MyApp1
{
	public class RssFeedReader
	{
		public List<RssFeedPost> ReadFeed(string url)
		{
			List<RssFeedPost> postsList = new List<RssFeedPost>();

			try {
				var rssFeed = XDocument.Load(url);

				var posts = from item in rssFeed.Descendants("item") select new RssFeedPost
				{
					Title = item.Element("title").Value,
					Link = item.Element("link").Value,
					Category = item.Element("category").Value,
					Description = item.Element("description").Value,
					FullText = item.Element("fulltext").Value,
					PubDate = item.Element("pubDate").Value,
					//ContentEncoded = item.Element("content:encoded").Value,
					GUID = item.Element("guid").Value,
					Image = item.Element("enclosure")?.Attribute("url")?.Value
				};

				postsList = posts.ToList();
			} catch {
				Console.WriteLine (Const.errorReadingRss);
			}

			return postsList;
		}
	}
}

