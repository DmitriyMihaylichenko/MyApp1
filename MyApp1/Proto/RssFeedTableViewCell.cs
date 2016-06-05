using System;
using System.Threading.Tasks;
using System.Net.Http;

using Foundation;
using CoreGraphics;
using UIKit;

namespace MyApp1
{
	public class RssFeedTableViewCell : UITableViewCell
	{
		public new UIImageView ImageView { get; set; }
		public new UITextView TextLabel { get; set; }

		private UIView cellView;

		private float imgWidth = 80f;
		private float imgHeight = 80f;

		private float imgMargin = 10f;

		private float textMarginTop = 10f;
		private float textMarginFromImage = 10f;
		private float textMarginRight = 20f;
		private float textMarginLeft;		

		private float textWidth;
		private float textHeight = 80f;

		public RssFeedTableViewCell (IntPtr handle) : base (handle)
		{
			CGPoint cellPos = new CGPoint (0, 0);
			CGSize cellSize = new CGSize (UIScreen.MainScreen.Bounds.Width, Config.rssTableViewRowHeight);

			cellView = new UIView (new CGRect(cellPos, cellSize));

			addImageView ();
			addTextLabel ();

			this.BackgroundView = cellView;

			RssFeedController.OrientChanged += (newTableViewSize) => {
				updateCell(newTableViewSize);
			};

			//cellView.AddConstraint (NSLayoutConstraint.Create (ImageView, NSLayoutAttribute.Right, NSLayoutRelation.Equal, TextLabel, NSLayoutAttribute.Left, 1, 10));
		}

		// Add image component to cell
		public void addImageView() {
			CGPoint imgPos = new CGPoint (imgMargin, imgMargin);
			CGSize imgSize = new CGSize (imgWidth, imgHeight);

			ImageView = new UIImageView (new CGRect(imgPos, imgSize));
			ImageView.ContentMode = UIViewContentMode.ScaleAspectFit;

			cellView.AddSubview (ImageView);
		}

		// Add text component to cell
		public void addTextLabel() {
			textMarginLeft = imgMargin + imgHeight + textMarginFromImage;

			textWidth = (float)UIScreen.MainScreen.Bounds.Width - textMarginLeft - textMarginRight;

			CGPoint textPos = new CGPoint (textMarginLeft, textMarginTop);
			CGSize textSize = new CGSize (textWidth, textHeight);

			TextLabel = new UITextView (new CGRect(textPos, textSize));
			TextLabel.Font = UIFont.FromName(Config.rssTableViewCellFont, Config.rssTableViewCellFontSize);

			cellView.AddSubview (TextLabel);
		}

		// Update Cell position
		public void updateCell(CGSize newSize) {
			textWidth = (float)newSize.Width - textMarginLeft - textMarginRight;

			CGPoint textPos = new CGPoint (textMarginLeft, textMarginTop);
			CGSize textSize = new CGSize (textWidth, textHeight);

			TextLabel.Frame = new CGRect (textPos, textSize);
		}

		public override void AwakeFromNib ()
		{
			base.AwakeFromNib ();

			/*NSArray views = NSBundle.MainBundle.LoadNib("RssFeedTableViewCell", this, null);
			ControlClass control = new ControlClass(views.ValueAt(0));

			control.Frame = new System.Drawing.RectangleF(0, 0, Frame.Width, Frame.Height);

			AddSubview(control);*/
		}
	}
}

