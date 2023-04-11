using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using BigIndianArts.CustomControls;
using BigIndianArts.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;
using Color = Android.Graphics.Color;
[assembly: ExportRenderer(typeof(BorderlessEditor), typeof(BorderlessEditorRenderer))]
namespace BigIndianArts.Droid.Renderers
{
    public class BorderlessEditorRenderer : EditorRenderer
    {
        #region Constructor

        public BorderlessEditorRenderer() : base(Application.Context)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Used to set the transparent color for editor control background property.
        /// </summary>
        /// <param name="e">The editor</param>
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null) Control.Background = new ColorDrawable(Color.Transparent);
            // if (Control != null) Control.SetMaxHeight(150);

        }

        #endregion
    }
}