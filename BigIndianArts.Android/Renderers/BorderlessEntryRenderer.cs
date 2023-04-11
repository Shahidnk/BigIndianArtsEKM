using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BigIndianArts.CustomControls;
using BigIndianArts.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace BigIndianArts.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null) return;

            var element = this.Element as BorderlessEntry;

            if (element == null) return;
            {
                //if (element.ShowWhiteCursor)
                //{
                //    IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                //    IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");

                // white_cursor is the xml file name which we defined above
                ///     JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.white_cursor);
            }

            if (element.IsHideLine == false) return;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
                Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Transparent);
            else
                Control.Background.SetColorFilter(Android.Graphics.Color.Transparent, PorterDuff.Mode.SrcAtop);
        }
    }
}