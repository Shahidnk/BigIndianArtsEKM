using Android.App;
using Android.Content;
using Android.Net;
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
using Xamarin.Forms;

[assembly: Dependency(typeof(AlertPlayer))]
namespace BigIndianArts.Droid.Helpers
{
    public class AlertPlayer : IAlertPlayer
    {
        public void AlertMessege(string messege)
        {
            Toast toast = Toast.MakeText(Android.App.Application.Context, messege, ToastLength.Short);
            toast.SetGravity(GravityFlags.Bottom, 0, 100);
            //toast.View.SetBackgroundColor(Android.Graphics.Color.LawnGreen);
            toast.Show();
        }

        public void CloseApp()
        {
            Process.KillProcess(Process.MyPid());
        }

        public string VideoSave(string filepath)
        {
            var imageData = File.ReadAllBytes(filepath);

            var pathToNewFolder = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath + "/HomeNeeds/VideoTestimonial/";
            Directory.CreateDirectory(pathToNewFolder);

            var filename = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".mp4";
            var newFilepath = Path.Combine(pathToNewFolder, filename);

            File.WriteAllBytes(newFilepath, imageData);
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            mediaScanIntent.SetData(Android.Net.Uri.FromFile(new Java.IO.File(newFilepath)));
            Forms.Context.SendBroadcast(mediaScanIntent);

            return newFilepath;
        }

        public bool IsNetworkAvailable()
        {
            try
            {
                ConnectivityManager _connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(Context.ConnectivityService);

                NetworkInfo _activeNetworkInfo = _connectivityManager.ActiveNetworkInfo;

                if (_activeNetworkInfo != null && _activeNetworkInfo.IsConnectedOrConnecting)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //public string VideoOpen()
        //{
        //    Intent intent = new Intent(Intent.ActionGetContent);
        //    intent.SetType("/HomeNeeds/VideoTestimonial/");
        //  //  startActivityForResult(intent);
        //}
    }
}