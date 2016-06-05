using System;

using UIKit;

namespace MyApp1
{
	public partial class RssFeedDetailController : UIViewController
	{
		public RssFeedPost post { get; set; } 

		public RssFeedDetailController () : base ("RssFeedDetailController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			ImageView.LoadImageAsyncFromUrl (post.Image);

			TextLabel.Text = post.Title;

			WebView.LoadHtmlString (post.FullText, null);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			ScrollView.LayoutIfNeeded ();

			ViewContainer.Frame = new CoreGraphics.CGRect(0,0, UIScreen.MainScreen.Bounds.Width, Config.rssDetailContainerHeight);

			ScrollView.ContentSize = ViewContainer.Bounds.Size;
		}
	}
}


