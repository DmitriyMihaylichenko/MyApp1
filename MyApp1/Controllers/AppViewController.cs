using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;

using Foundation;
using CoreGraphics;
using UIKit;

namespace MyApp1
{
	partial class AppViewController : UIViewController
	{
		public const string cellId = "AppTableViewCell";

		public static event ScreenOrientChanged OrientChanged;

		public ApplicationsModel appsModel;

		public AppViewController (IntPtr handle) : base (handle)
		{
			appsModel = new ApplicationsModel(Config.jsonApplicationsData);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.AutomaticallyAdjustsScrollViewInsets = false;

			this.TableView.SeparatorColor = UIColor.Clear;

			TableView.RegisterClassForCellReuse (typeof(AppTableViewCell), cellId);

			UIImage tableViewBGPattern = UIImage.FromFile ("Images/" + Config.tableViewBGPattern);

			TableView.BackgroundColor = UIColor.FromPatternImage(tableViewBGPattern);
			TableView.SectionHeaderHeight = Config.tableViewHeaderHeight;

			TableView.Source = new AppTableViewSource (appsModel, this);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
		}

		public override void ViewDidLayoutSubviews()
		{
			base.ViewDidLayoutSubviews();

			OrientChanged?.Invoke (UIScreen.MainScreen.Bounds.Size);
		}
	}
}
