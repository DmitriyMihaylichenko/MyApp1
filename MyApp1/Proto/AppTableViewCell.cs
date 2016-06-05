using System;

using UIKit;
using CoreGraphics;
using Foundation;

namespace MyApp1
{
	public class AppTableViewCell : UITableViewCell
	{
		public UIImageView ArrowImage { get; set; }

		private float imageRightMarg = 60f;

		public AppTableViewCell (IntPtr handle) : base (handle)
		{
			setupCell ();

			addArrowIcon ();

			AppViewController.OrientChanged += (newTableViewSize) => {
				updateCell(newTableViewSize);
			};
		}

		// Setup tableview custom cell
		private void setupCell() {
			this.TextLabel.TextColor = UIColor.DarkGray;
			this.TextLabel.Font = UIFont.FromName("Helvetica", 20f);

			UIImage tableCellBackImage = UIImage.FromFile ("Images/" + Config.tableViewCellBGPattern);
			this.BackgroundColor = UIColor.FromPatternImage(tableCellBackImage);
		}

		// Update Cell position
		public void updateCell(CGSize newSize) {
			CGPoint imagePos = new CGPoint (newSize.Width - imageRightMarg, 0f);
			CGSize imageSize = new CGSize (Config.tableViewHeaderHeight, Config.tableViewHeaderHeight);

			ArrowImage.Frame = new CGRect (imagePos, imageSize);
		}

		// Add right arrow image to the cell
		private void addArrowIcon() {
			CGPoint imagePos = new CGPoint (this.Bounds.Width - imageRightMarg, 0f);
			CGSize imageSize = new CGSize (Config.tableViewHeaderHeight, Config.tableViewHeaderHeight);

			ArrowImage = new UIImageView (new CGRect (imagePos, imageSize));
			ArrowImage.Image = UIImage.FromFile ("Images/" + Config.tableViewCellArrowRight);

			this.AddSubview (ArrowImage);
		}
	}
}

