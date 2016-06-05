using System;
using System.Threading.Tasks;
using System.Net.Http;

using Foundation;
using CoreGraphics;
using UIKit;


namespace MyApp1
{
	public static class PublicExtens
	{
		public static async void LoadImageAsyncFromUrl (this UIImageView image, string imageUrl)
		{
			if (imageUrl != null && imageUrl != string.Empty) {
				var httpClient = new HttpClient ();

				Task<byte[]> contentsTask = httpClient.GetByteArrayAsync (imageUrl);

				var contents = await contentsTask;

				image.Image = UIImage.LoadFromData (NSData.FromArray (contents));
			}
		}
	}
}

