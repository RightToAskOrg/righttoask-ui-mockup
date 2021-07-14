using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace PassingData
{
    public partial class App : Application
    {
        public App()
        {
            setTheStyles();
            MainPage = new NavigationPage(new MainPage(DateTime.Now.ToString("u")));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void setTheStyles()
        {
            
            var redButton = new Style(typeof(Xamarin.Forms.Button))
            {
                Class = "RedColouredButton",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.BackgroundColorProperty,
                        Value = "Red"
                    }
                }
            };
            Resources.Add(redButton);
            
            var doneButton = new Style(typeof(Xamarin.Forms.Button))
            {
                Class = "DoneButton",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.BackgroundColorProperty,
                        Value = "DarkGreen",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.TextProperty,
                        Value = "Done",
                    }
                    
                }
            };
            Resources.Add(doneButton);
        }
    }
}

