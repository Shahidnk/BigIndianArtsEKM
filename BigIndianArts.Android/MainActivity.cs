using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Content.Res;
using Rg.Plugins.Popup.Services;
using Acr.UserDialogs;
using Plugin.FirebasePushNotification;
using Android.Content;

namespace BigIndianArts.Droid
{
    [Activity(Label = "BigIndianArts", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this);
            UserDialogs.Init(this);
            LoadApplication(new App());
            FirebasePushNotificationManager.ProcessIntent(this, Intent);

            FirebasePushNotificationManager.Initialize(this, true);

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override Resources Resources
        {
            get
            {
                Resources resource = base.Resources;
                Configuration configuration = new Configuration();
                configuration.SetToDefaults();
                if (Build.VERSION.SdkInt >= BuildVersionCodes.NMr1)
                {
                    return CreateConfigurationContext(configuration).Resources;
                }
                else
                {
                    resource.UpdateConfiguration(configuration, resource.DisplayMetrics);
                    return resource;
                }
            }
        }
        async public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do something if there are some pages in the `PopupStack`
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            //   FirebaseApp.InitializeApp(this);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
    }
}