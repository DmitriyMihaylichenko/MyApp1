using System;

namespace MyApp1
{
	public abstract class Config
	{
		// JSON file with all applications data
		public const string jsonApplicationsData = "JSON.txt";

		public const string imageLoadingPNG = "imageLoading.png";

		// Height of table header in AppViewController
		public const float tableViewHeaderHeight = 44f;
		public const string tableViewHeaderFont = "Helvetica-Bold";
		public const float tableViewHeaderFontSize = 20f;
		public const string tableViewCellArrowRight = "arrowRight.png";
		public const string tableViewAppsHeaderImage = "appsHeaderImage.png";
		public const string tableViewGamesHeaderImage = "gamesHeaderImage.png";
		public const string tableViewBGPattern = "bgPattern0.png";
		public const string tableViewCellBGPattern = "bgPattern1.png";
		public const string tableViewHeaderBGPattern = "bgPattern2.png";

		// Height of table header in RssFeedViewController
		public const float rssTableViewHeaderHeight = 44f;
		public const float rssTableViewRowHeight = 100f;
		public const string rssTableViewHeaderFont = "Helvetica";
		public const float rssTableViewHeaderFontSize = 18f;
		public const string rssTableViewCellFont = "Arial";
		public const float rssTableViewCellFontSize = 14f;
		public const float rssDetailContainerHeight = 770f;
		public const string rssNewsHeaderIcon = "newsHeaderIcon.png";
		public const string rssTableViewHeaderBGPattern = "bgPattern3.png";
	}
}

