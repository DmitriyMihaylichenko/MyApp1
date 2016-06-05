using System;

using UIKit;
using CoreGraphics;
using Foundation;

namespace MyApp1
{
	public class AppTableViewSource : UITableViewSource
	{
		public ApplicationsModel appsModel { get; set; }

		public const string appNameSpace = "MyApp1.";

		public UIViewController ownerController;

		public AppTableViewSource (ApplicationsModel appsModel, UIViewController owner)
		{
			this.appsModel = appsModel;

			this.ownerController = owner;
		}

		public override nint NumberOfSections (UITableView tableView)
		{
			return appsModel.getAllSections().Length;
		}

		public override nint RowsInSection (UITableView tableview, nint section)
		{
			return appsModel.getItemsInSectionByIndex(section).Count;
		}

		public override nfloat GetHeightForHeader (UITableView tableView, nint section)
		{
			return Config.tableViewHeaderHeight;
		}

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			/*UIAlertController okAlertController = UIAlertController.Create ("Row Selected", data[indexPath.Row], UIAlertControllerStyle.Alert);
			okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

			ownerController.PresentViewController (okAlertController, true, null);*/

			goToController (appsModel.getItemsInSectionByIndex (indexPath.Section) [indexPath.Row].Contr);

			tableView.DeselectRow (indexPath, true);
		}

		// Go to controller by name
		public void goToController(string name) {
			Type contrType = Type.GetType (appNameSpace + name);

			UIViewController contr = Activator.CreateInstance(contrType) as UIViewController;

			ownerController.NavigationController.PushViewController (contr, true);
		}

		public override UIView GetViewForHeader (UITableView tableView, nint section)
		{
			UIView header = new UIView(new CGRect(0, 0, tableView.Bounds.Width, Config.tableViewHeaderHeight));

			if (header != null)
			{
				UIImage tableCellBackImage = UIImage.FromFile ("Images/" + Config.tableViewHeaderBGPattern);
				header.BackgroundColor = UIColor.FromPatternImage(tableCellBackImage);

				float imageLeftMarg = 0f;

				string sectionIcon = section % 2 == 0 ? Config.tableViewAppsHeaderImage : Config.tableViewGamesHeaderImage;

				CGPoint imagePos = new CGPoint (imageLeftMarg, 0f);
				CGSize imageSize = new CGSize (Config.tableViewHeaderHeight, Config.tableViewHeaderHeight);

				UIImageView Image = new UIImageView (new CGRect (imagePos, imageSize));
				Image.Image = UIImage.FromFile ("Images/" + sectionIcon);

				header.AddSubview (Image);

				float labelLeftMarg = 50f;

				CGPoint labelPos = new CGPoint (labelLeftMarg, 0f);
				CGSize labelSize = new CGSize (tableView.Bounds.Width, Config.tableViewHeaderHeight);

				UILabel TextLabel = new UILabel (new CGRect(labelPos, labelSize));
				TextLabel.TextColor = UIColor.DarkTextColor;
				TextLabel.Font = UIFont.FromName(Config.tableViewHeaderFont, Config.tableViewHeaderFontSize);
				TextLabel.Text = appsModel.getSectionByIndex(section);

				header.AddSubview (TextLabel);
			}

			return header;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			string cellId = AppViewController.cellId;

			var cell = (AppTableViewCell)tableView.DequeueReusableCell(cellId, indexPath);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellId) as AppTableViewCell;
			}

			cell.TextLabel.Text = appsModel.getItemsInSectionByIndex(indexPath.Section)[indexPath.Row].Name;
			cell.ImageView.Image = UIImage.FromFile("Images/" + appsModel.getItemsInSectionByIndex(indexPath.Section)[indexPath.Row].Icon);

			return cell;
		}
	}
}

