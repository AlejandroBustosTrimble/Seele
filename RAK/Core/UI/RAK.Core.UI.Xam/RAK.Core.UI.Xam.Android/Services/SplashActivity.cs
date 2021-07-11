using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace RAK.Core.UI.Xam.Droid.Services
{
	[Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
	public abstract class SplashActivity : AppCompatActivity
	{
		protected abstract Type MainActivityType { get; }

		static readonly string TAG = "X:" + typeof(SplashActivity).Name;

		public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
		{
			base.OnCreate(savedInstanceState, persistentState);

			//base.OnCreate(savedInstanceState);
			//SetContentView(Resource.Layout.Main);

		}

		// Launches the startup task
		protected override void OnResume()
		{
			base.OnResume();
			Task startupWork = new Task(() => { SimulateStartup(); });
			startupWork.Start();
		}

		// Simulates background work that happens behind the splash screen
		async void SimulateStartup()
		{
			//await Task.Delay(8000); // Simulate a bit of startup work.
			StartActivity(new Intent(Application.Context, MainActivityType));
		}
	}
}