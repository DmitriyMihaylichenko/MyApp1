using System;
using System.Collections.Generic;

using Foundation;
using CoreGraphics;
using UIKit;
using ObjCRuntime;

namespace MyApp1
{
	public partial class RssFeedController : UIViewController
	{
		public const string cellId = "RssTableViewCell";

		public static event ScreenOrientChanged OrientChanged;

		public List<RssFeedPost> posts { get; set; }

		public RssFeedController () : base ("RssFeedController", null)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.RegisterClassForCellReuse (typeof(RssFeedTableViewCell), cellId);

			this.TableView.SeparatorColor = UIColor.Blue;
			this.TableView.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLineEtched;
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			posts = new RssFeedReader().ReadFeed(Const.rssFeedUrl);

			TableView.Source = new RssFeedTableViewSource (posts, this);;

			TableView.ReloadData ();
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			OrientChanged?.Invoke (UIScreen.MainScreen.Bounds.Size);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}
	}
}


