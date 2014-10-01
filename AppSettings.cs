using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using AndroidClientApp.DataModel;
using ODataClientApp;

namespace AndroidClientApp
{
    [Activity(Label = "RESO Client Application - Preferences")]
    public class AppSettings : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Preferences);
            // Create your application here

            EditText appURL = FindViewById<EditText>(Resource.Id.AppURLText);
            Button updatePref = FindViewById<Button>(Resource.Id.UpdatePreferences);

            appURL.SetText(AppPreferences.applicationURL.ToString(), TextView.BufferType.Normal);

            updatePref.Click += (object sender, EventArgs e) =>
            {
                AppPreferences.applicationURL = appURL.Text;

                var goMainActivity = new Intent(this, typeof(MainActivity));

                StartActivity(goMainActivity);
            };
        }
    }
}