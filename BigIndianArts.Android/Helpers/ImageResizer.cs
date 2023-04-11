using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BigIndianArts.Droid.Helpers;
using BigIndianArts.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ImageResizer))]
namespace BigIndianArts.Droid.Helpers
{
    public class ImageResizer : IImageResizer
    {
        static ImageResizer()
        {
        }

        public async Task<byte[]> ResizeImage(byte[] imageData)
        {
            return ResizeImageAndroid(imageData);
        }

        public byte[] ResizeImageAndroid(byte[] imageData)
        {
            // Load the bitmap
            Bitmap originalImage = BitmapFactory.DecodeByteArray(imageData, 0, imageData.Length);
            // Bitmap resizedImage = Bitmap.CreateScaledBitmap(originalImage, (int)width, (int)height, false);
            using (MemoryStream ms = new MemoryStream())
            {
                originalImage.Compress(Bitmap.CompressFormat.Jpeg, 33, ms);
                return ms.ToArray();
            }
        }
    }
}