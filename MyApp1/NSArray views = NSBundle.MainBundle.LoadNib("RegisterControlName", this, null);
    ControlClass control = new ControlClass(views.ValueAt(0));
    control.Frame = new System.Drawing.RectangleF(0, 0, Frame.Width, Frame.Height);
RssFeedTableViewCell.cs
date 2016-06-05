using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace MyApp1
{
	partial class NSArray views = NSBundle.MainBundle.LoadNib("RegisterControlName", this, null);
    ControlClass control = new ControlClass(views.ValueAt(0));
    control.Frame = new System.Drawing.RectangleF(0, 0, Frame.Width, Frame.Height);
RssFeedTableViewCell : UIView
	{
		public NSArray views = NSBundle.MainBundle.LoadNib("RegisterControlName", this, null);
    ControlClass control = new ControlClass(views.ValueAt(0));
    control.Frame = new System.Drawing.RectangleF(0, 0, Frame.Width, Frame.Height);
RssFeedTableViewCell (IntPtr handle) : base (handle)
		{
		}
	}
}
