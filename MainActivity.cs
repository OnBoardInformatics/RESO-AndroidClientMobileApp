using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using ODataClientApp.DataModel;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using AndroidClientApp;
using AndroidClientApp.DataModel;

namespace ODataClientApp
{
    [Activity(Label = "RESO Client Application", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        SecurityTemplate securityTemplate;

        string ApplicationToken;
        string appURL = AppPreferences.applicationURL.ToString();

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            EditText clientIdText = FindViewById<EditText>(Resource.Id.ClientIdText);
            EditText clientSecretText = FindViewById<EditText>(Resource.Id.ClientSecretText);

            Button authenticateButton = FindViewById<Button>(Resource.Id.AuthenticateButton);


            EditText filterQueryText = FindViewById<EditText>(Resource.Id.FilterQueryText);
            EditText sortingQueryText = FindViewById<EditText>(Resource.Id.SortQueryText);
            EditText topQueryText = FindViewById<EditText>(Resource.Id.TopQueryText);
            EditText skipQueryText = FindViewById<EditText>(Resource.Id.SkipQueryText);

            Button callAPIButton = FindViewById<Button>(Resource.Id.CallAPIButton);
            Button callAppSettings = FindViewById<Button>(Resource.Id.CallAppSettings);

            filterQueryText.Enabled = false;
            sortingQueryText.Enabled = false;
            topQueryText.Enabled = false;
            skipQueryText.Enabled = false;
            callAPIButton.Enabled = false;

            authenticateButton.Click += (object sender, EventArgs e) =>
            {
                securityTemplate = new SecurityTemplate();

                if (clientIdText.Text != "" && clientSecretText.Text != "")
                {
                    try
                    {
                        string url = appURL + "/RESO/Security.svc/ApplicationToken?$filter=";

                        url = string.Concat(url, "clientId eq ", clientIdText.Text);
                        url = string.Concat(url, "and clientSecret eq ", clientSecretText.Text);
                        url = string.Concat(url, "&$format=json");

                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
                        request.Method = "GET";
                        request.ContentType = "application/json";
                        string responseText;

                        using (var response = request.GetResponse())
                        {
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                responseText = reader.ReadToEnd();
                                responseText = responseText.Replace("odata.metadata", "metadata");

                                securityTemplate = new SecurityTemplate();
                                securityTemplate = JsonConvert.DeserializeObject<SecurityTemplate>(responseText);

                                if (securityTemplate != null)
                                {
                                    if (securityTemplate.value[0].TotalFound != "0")
                                    {
                                        ApplicationToken = securityTemplate.value[0].Details;
                                        var messDialog = new AlertDialog.Builder(this);
                                        messDialog.SetMessage("Authenticated..");
                                        messDialog.SetPositiveButton("Ok", delegate { });
                                        messDialog.Show();

                                        filterQueryText.Enabled = true;
                                        sortingQueryText.Enabled = true;
                                        topQueryText.Enabled = true;
                                        skipQueryText.Enabled = true;
                                        callAPIButton.Enabled = true;

                                        filterQueryText.FindFocus();

                                    }
                                    else
                                    {
                                        var messDialog = new AlertDialog.Builder(this);
                                        messDialog.SetMessage(securityTemplate.value[0].Details);
                                        messDialog.SetPositiveButton("Ok", delegate { });
                                        messDialog.Show();
                                    }
                                }

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
                else
                {
                    var messDialog = new AlertDialog.Builder(this);
                    messDialog.SetMessage("Please Provide Client Id and Client Secret Values");
                    messDialog.SetPositiveButton("Ok", delegate { });
                    messDialog.Show();
                }
            };


            callAPIButton.Click += (object sender, EventArgs e) =>
            {
                var apiCallActivity = new Intent(this, typeof(APICallResult));

                apiCallActivity.PutExtra("applicationToken", ApplicationToken);
                apiCallActivity.PutExtra("filterQuery", filterQueryText.Text);
                apiCallActivity.PutExtra("sortingQuery", sortingQueryText.Text);
                apiCallActivity.PutExtra("topQuery", topQueryText.Text);
                apiCallActivity.PutExtra("skipQuery", skipQueryText.Text);

                StartActivity(apiCallActivity);
            };

            callAppSettings.Click += (object sender, EventArgs e) =>
            {
                var appSettings = new Intent(this, typeof(AppSettings));

                StartActivity(appSettings);
            };
        }
    }
}

