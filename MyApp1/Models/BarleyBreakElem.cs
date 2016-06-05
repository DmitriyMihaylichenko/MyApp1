using System;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MyApp1
{
	public delegate void TouchElemHandler (BarleyBreakElem target);

	public class BarleyBreakElem : UIView
	{
		private int _number;
		public int number { 
			get { return _number; }
			set { 
				_number = value;

				string val = value.ToString();

				imageView.Image = UIImage.FromFile ("Images/Barley/" + val + ".png"); 
			}
		}

		public event TouchElemHandler OnElemTouch;

		public CGPoint posIndex;

		public UIImageView frameImage;

		public int index;

		public UIImageView imageView;

		public BarleyBreakElem (CGRect frame) : base (frame)
		{
			imageView = new UIImageView (new CGRect(0, 0, this.Bounds.Width, this.Bounds.Height));

			this.AddSubview (imageView);
		}

		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			base.TouchesBegan (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;
			if (touch != null)
			{
				if (touch.TapCount == 1)
				{
					OnElemTouch?.Invoke (this);
				}
			}
		}

		public void addSelectorMark() {
			frameImage = new UIImageView (new CGRect(0, 0, this.Bounds.Width, this.Bounds.Height));

			frameImage.Image = UIImage.FromFile ("Images/Barley/frameIcon.png");

			this.AddSubview (frameImage);
		}

		public void remSelectorMark() {
			frameImage.RemoveFromSuperview ();

			frameImage = null;
		}
	}
}

