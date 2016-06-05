using System;
using System.Collections.Generic;

using Foundation;
using UIKit;
using CoreGraphics;

namespace MyApp1
{
	public class BarleyBreakGame : UIView
	{
		public UIViewController ownerController;

		public List<BarleyBreakElem> barleyElems;

		public BarleyBreakElem selectedElem;
		public BarleyBreakElem emptyElem;

		public bool canSelect = true;

		private bool moveOK;

		public int[] numbArray = new int[16];

		private const int maxElemsCount = 16;
		private const float topFieldOffset = 44f;
		private const int maxElemsInRow = 4;
		private nfloat elemSize = 64f;
		private const float elemMargin = 4f;
		private nfloat fieldPosOffsetY = 20f;

		public BarleyBreakGame (UIView Stage, UIViewController owner)
		{
			this.ownerController = owner;

			BarleyBreakController.OnOrientChange += (s) => {
				updateFieldPos();
			};

			updateFieldPos ();

			Stage.AddSubview (this);

			barleyElems = new List<BarleyBreakElem> ();

			startGame ();
		}

		public void updateFieldPos() {
			CGSize fieldSize = getFieldSize ();
			CGPoint fieldPos = getFieldPos (fieldSize);

			this.Frame = new CGRect(fieldPos, fieldSize);
		}

		// Set or update game field position
		public CGPoint getFieldPos(CGSize fieldSize) {
			nfloat xPos = UIScreen.MainScreen.Bounds.Width / 2 - fieldSize.Width / 2; 
			nfloat yPos = UIScreen.MainScreen.Bounds.Height / 2 - fieldSize.Height / 2;

			return new CGPoint (xPos, yPos + fieldPosOffsetY);
		}

		// Get size of game field
		public CGSize getFieldSize () {
			nfloat size = maxElemsInRow * elemSize + (maxElemsInRow + 1) * elemMargin;

			return new CGSize (size, size);
		}

		public override void TouchesEnded (NSSet touches, UIEvent evt)
		{
			base.TouchesEnded (touches, evt);

			if (selectedElem != null) {
				tryToMove ();

				moveOK = false;

				selectedElem.remSelectorMark ();

				selectedElem = null;
			}
		}

		public override void TouchesMoved (NSSet touches, UIEvent evt)
		{
			base.TouchesMoved (touches, evt);

			UITouch touch = touches.AnyObject as UITouch;

			if (touch != null) {
				if (emptyElem.Frame.Contains (touch.LocationInView (this))) {
					moveOK = true;
				}
			}
		}

		// Game enter point
		public void startGame() {
			for(int i = 0; i < maxElemsCount; i ++){
				numbArray[i] = -1;
			}

			for (int i = 0; i < maxElemsCount; i++) {
				genNumbValues (i);
			}

			genElements ();
		}

		// End game
		public void endGame() {
			foreach(BarleyBreakElem elem in barleyElems) {
				elem.RemoveFromSuperview ();
			}

			barleyElems.Clear ();

			startGame ();
		}

		// Generate numbers for elements
		public void genNumbValues(int index) {			
			int randVal = randValue (1, maxElemsCount);

			bool present = false;

			foreach (int currElem in numbArray) {
				if(currElem == randVal) {
					present = true;

					break;
				}
			}

			if (present) {
				genNumbValues (index);
			} else {
				numbArray [index] = randVal;
			}
		}

		// Random default value for element
		public int randValue(int fromVal, int toVal) {
			Random r = new Random ();

			int res = r.Next(fromVal, toVal + 1);

			return res;
		}

		// Create all elements
		public void genElements() {
			int elemsRowCounter = 1;
			int elemsColCounter = 1;

			CGSize elementSize = new CGSize (elemSize, elemSize);

			for(int i = 0; i < maxElemsCount; i++) {				
				nfloat xPos = (elemsColCounter - 1) * elemSize + elemMargin * elemsColCounter;
				nfloat yPos = (elemsRowCounter - 1) * elemSize + elemMargin * elemsRowCounter;

				CGPoint elementPos = new CGPoint (xPos, yPos);

				BarleyBreakElem el = new BarleyBreakElem (new CGRect(elementPos, elementSize));
				el.posIndex = new CGPoint (elemsRowCounter, elemsColCounter);
				el.number = numbArray [i];
				el.index = i;

				el.OnElemTouch += OnElemTouch;

				barleyElems.Add (el);

				this.AddSubview (el);

				if(el.number == maxElemsCount) {
					emptyElem = el;
				}

				if (elemsColCounter == maxElemsInRow) {
					elemsColCounter = 1;

					elemsRowCounter++;
				} else {
					elemsColCounter++;
				}
			}
		}

		// Check is all elements are combined
		public bool isAllElemsCombined() {
			bool result = true;

			foreach(BarleyBreakElem elem in barleyElems){
				if (elem.index != elem.number - 1) {
					result = false;
				}
			}

			return result;
		}

		// Recombinate selected elements
		public bool tryToMove() {
			if(canRecombine(selectedElem.posIndex, emptyElem.posIndex)) {
				canSelect = false;

				CGRect elemRect1 = selectedElem.Frame;
				CGRect elemRect2 = emptyElem.Frame;

				CGPoint tmpPos = selectedElem.posIndex;
				selectedElem.posIndex = emptyElem.posIndex;
				emptyElem.posIndex = tmpPos;

				int indexTemp = selectedElem.index;
				selectedElem.index = emptyElem.index;
				emptyElem.index = indexTemp;

				UIView.Animate (0.5f, () => {
					selectedElem.Frame = elemRect2;
					emptyElem.Frame = elemRect1;
				}, () => {
					if(isAllElemsCombined()){
						UIAlertController okAlertController = UIAlertController.Create ("Game Completed", 
																						"You WIN!",
																						UIAlertControllerStyle.Alert);

						okAlertController.AddAction(UIAlertAction.Create("OK", UIAlertActionStyle.Default, null));

						ownerController.PresentViewController (okAlertController, true, null);

						endGame();
					}

					canSelect = true;
				});

				return true;
			}

			return false;
		}

		// Check is objects can be recombined
		public bool canRecombine(CGPoint posInd1, CGPoint posInd2) {
			nfloat posIndX1 = posInd1.X;
			nfloat posIndY1 = posInd1.Y;

			nfloat posIndX2 = posInd2.X;
			nfloat posIndY2 = posInd2.Y;

			if (moveOK) {
				if (posIndX1 == posIndX2) {
					if (posIndY1 == posIndY2 + 1 || posIndY1 == posIndY2 - 1) {
						return true;
					}
				}

				if (posIndY1 == posIndY2) {
					if (posIndX1 == posIndX2 + 1 || posIndX1 == posIndX2 - 1) {
						return true;
					}
				}
			}

			return false;
		}

		// On element click handler
		public void OnElemTouch(BarleyBreakElem target) {
			//if (canSelect) {
				if (target.number != maxElemsCount) {
					selectedElem = target;

					selectedElem.addSelectorMark ();
				}
			//}
		}

		public void OnTouchMove(BarleyBreakElem target) {
			if(target == emptyElem){
				moveOK = true;
			}
		}
	}
}

