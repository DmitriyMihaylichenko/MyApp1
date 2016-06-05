using System;

using UIKit;

namespace MyApp1
{
	public partial class BarleyBreakController : UIViewController
	{
		public BarleyBreakGame barleyGame;

		public static event ScreenOrientChanged OnOrientChange;

		public BarleyBreakController () : base ("BarleyBreakController", null)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			barleyGame = new BarleyBreakGame (Stage, this);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();

		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();

			OnOrientChange?.Invoke (UIScreen.MainScreen.Bounds.Size);
		}
	}
}


