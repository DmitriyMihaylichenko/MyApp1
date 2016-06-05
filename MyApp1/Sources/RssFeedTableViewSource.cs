using System;
using System.Collections.Generic;

using UIKit;
using Foundation;
using CoreGraphics;

namespace MyApp1
{
	public class RssFeedTableViewSource : UITableViewSource
	{
		public RssFeedModel rssFeedModel { get; set; }

		public UIViewController ownerController;

		public RssFeedTableViewSource (List<RssFeedPost> postsList, UIViewController owner)
		{
			this.rssFeedModel = new RssFeedModel (postsList);

			this.ownerController = owner;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return rssFeedModel.getAllSections().Length;;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return rssFeedModel.getItemsInSectionByIndex(section).Count;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return Config.rssTableViewHeaderHeight;
		}

		public override nfloat GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return Config.rssTableViewRowHeight;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			RssFeedDetailController contr = new RssFeedDetailController ();
			contr.post = rssFeedModel.getItemsInSectionByIndex (indexPath.Section) [indexPath.Row];

			ownerController.NavigationController.PushViewController (contr, true);

			tableView.DeselectRow (indexPath, true);
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UIView header = new UIView(new CGRect(0, 0, tableView.Bounds.Width, Config.rssTableViewHeaderHeight));

			if (header != null)
			{
				UIImage tableCellBackImage = UIImage.FromFile ("Images/" + Config.rssTableViewHeaderBGPattern);
				header.BackgroundColor = UIColor.FromPatternImage(tableCellBackImage);

				float imageLeftMarg = 0f;

				CGPoint imagePos = new CGPoint (imageLeftMarg, 0f);
				CGSize imageSize = new CGSize (Config.tableViewHeaderHeight, Config.rssTableViewHeaderHeight);

				UIImageView Image = new UIImageView (new CGRect (imagePos, imageSize));
				Image.Image = UIImage.FromFile ("Images/" + Config.rssNewsHeaderIcon);

				header.AddSubview (Image);

				float labelLeftMarg = 50f;

				CGPoint labelPos = new CGPoint (labelLeftMarg, 0f);
				CGSize labelSize = new CGSize (tableView.Bounds.Width, Config.rssTableViewHeaderHeight);

				UILabel TextLabel = new UILabel (new CGRect(labelPos, labelSize));
				TextLabel.TextColor = UIColor.White;
				TextLabel.Font = UIFont.FromName(Config.rssTableViewHeaderFont, Config.rssTableViewHeaderFontSize);
				TextLabel.Text = rssFeedModel.getSectionByIndex(section);

				header.AddSubview (TextLabel);
			}

			return header;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			string cellId = RssFeedController.cellId;

			var cell = (RssFeedTableViewCell)tableView.DequeueReusableCell(cellId, indexPath);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellId) as RssFeedTableViewCell;
			}

			cell.TextLabel.Text = rssFeedModel.getItemsInSectionByIndex(indexPath.Section)[indexPath.Row].Title;
			cell.ImageView.Image = UIImage.FromFile ("Images/" + Config.imageLoadingPNG);

			cell.ImageView.LoadImageAsyncFromUrl(rssFeedModel.getItemsInSectionByIndex(indexPath.Section)[indexPath.Row].Image);

			return cell;
		}
	}
}

