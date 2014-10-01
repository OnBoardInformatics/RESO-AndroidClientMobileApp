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
using AndroidClientApp;

namespace ODataClientApp
{
    [Activity(Label = "RESO Client Application - Listing Detail Result")]
    public class CallDetailResult : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            string xmlResult = Intent.GetStringExtra("xml_result");

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CallDetailResult);
            //FindViewById<EditText>(Resource.Id.FilterQueryText);

            TextView detailResultText = FindViewById<TextView>(Resource.Id.DetailCallResultText);

            detailResultText.SetText(xmlResult, TextView.BufferType.Normal);

            // Create your application here

        }
    }
}