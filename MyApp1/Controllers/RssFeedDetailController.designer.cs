// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MyApp1
{
	[Register ("RssFeedDetailController")]
	partial class RssFeedDetailController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIImageView ImageView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIScrollView ScrollView { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel TextLabel { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIView ViewContainer { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIWebView WebView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}
			if (ScrollView != null) {
				ScrollView.Dispose ();
				ScrollView = null;
			}
			if (TextLabel != null) {
				TextLabel.Dispose ();
				TextLabel = null;
			}
			if (ViewContainer != null) {
				ViewContainer.Dispose ();
				ViewContainer = null;
			}
			if (WebView != null) {
				WebView.Dispose ();
				WebView = null;
			}
		}
	}
}
