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
using System.Net;
using System.IO;
using ODataClientApp.DataModel;
using Newtonsoft.Json;
using AndroidClientApp.DataModel;

namespace ODataClientApp
{
    [Activity(Label = "RESO Client Application - Listing Id Result")]
    public class APICallResult : ListActivity
    {
        PropertyTemplate propertyTemplate;

        public string[] items;
        string appURL = AppPreferences.applicationURL.ToString();

        protected override void OnCreate(Bundle bundle)
        {
            string appToken;

            string filterQuery;
            string sortingQuery;
            string topQuery;
            string skipQuery;

            List<string> propertyList = new List<string>();

            base.OnCreate(bundle);

            //SetContentView(Resource.Layout.CallResult);

            //listProperties = FindViewById<ListView>(Resource.Id.listProperties);

            // Create your application here
            appToken = Intent.GetStringExtra("applicationToken");

            filterQuery = Intent.GetStringExtra("filterQuery");
            sortingQuery = Intent.GetStringExtra("sortingQuery");
            topQuery = Intent.GetStringExtra("topQuery");
            skipQuery = Intent.GetStringExtra("skipQuery");


            try
            {
                string url = appURL + "/RESO/Properties.svc/Property?$filter=";

                url = string.Concat(url, filterQuery);

                if (!string.IsNullOrEmpty(sortingQuery))
                {
                    url = string.Concat(url, "&$orderyby=", sortingQuery);
                }

                if (!string.IsNullOrEmpty(topQuery))
                {
                    url = string.Concat(url, "&$top=", topQuery);
                }

                if (!string.IsNullOrEmpty(skipQuery))
                {
                    url = string.Concat(url, "&$skip=", skipQuery);
                }

                url = string.Concat(url, "&$format=json");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add("ApplicationToken", appToken);

                string responseText;

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseText = reader.ReadToEnd();
                        responseText = responseText.Replace("odata.metadata", "metadata");

                        propertyTemplate = new PropertyTemplate();
                        propertyTemplate = JsonConvert.DeserializeObject<PropertyTemplate>(responseText);

                        for (int x = 0; x < propertyTemplate.value.Count(); x++)
                        {
                            propertyList.Add(propertyTemplate.value[x].ListingId.ToString());
                        }

                        ListAdapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleListItem1, propertyList.ToArray());
                        items = propertyList.ToArray();

                    }
                }

            }
            catch (WebException exception)
            {
                string responseText;
                using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    responseText = reader.ReadToEnd();
                    Console.WriteLine(responseText);
                }
            }
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            //var t = items[position];
            //Android.Widget.Toast.MakeText(this, t, Android.Widget.ToastLength.Short).Show();
            var apiCallDetailResultActivity = new Intent(this, typeof(CallDetailResult));

            apiCallDetailResultActivity.PutExtra("xml_result", JsonConvert.SerializeObject(propertyTemplate.value[position]).ToString());
            StartActivity(apiCallDetailResultActivity);

        }

    }
}