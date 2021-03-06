using System;
using PassingData.Controls;
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
                        Value = "Teal",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.TextColorProperty,
                        Value = "White",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.TextProperty,
                        Value = "Next",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.HorizontalOptionsProperty,
                        Value = "End",
                    }
                    
                }
            };
            Resources.Add(doneButton);
            
            // Currently identical to the 'done' button, but
            // separated in case we want to make them different later.
            var saveButton = new Style(typeof(Xamarin.Forms.Button))
            {
                Class = "SaveButton",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.BackgroundColorProperty,
                        Value = "Teal",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.TextColorProperty,
                        Value = "White",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.TextProperty,
                        Value = "Save",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.HorizontalOptionsProperty,
                        Value = "End",
                    }
                    
                }
            };
            Resources.Add(saveButton);
            
            var normalButton = new Style(typeof(Xamarin.Forms.Button))
            {
                Class = "NormalButton",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.BackgroundColorProperty,
                        Value = "Turquoise"
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.CornerRadiusProperty,
                        Value = "20",
                    }
                }
            };
            Resources.Add(normalButton);
            
            var upVoteButton = new Style(typeof(Xamarin.Forms.Button))
            {
                Class = "UpVoteButton",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.BackgroundColorProperty,
                        Value = "Turquoise"
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.CornerRadiusProperty,
                        Value = "20",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.HorizontalOptionsProperty,
                        Value = "Center",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.PaddingProperty,
                        Value = "0",
                    },
                    new Setter
                    {
                        Property = Xamarin.Forms.Button.TextProperty,
                        Value = "+1",
                    },
                }
            };
            Resources.Add(upVoteButton);
            
            var normalEditor = new Style(typeof(Xamarin.Forms.Editor))
            {
                Class = "NormalEditor",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Editor.BackgroundColorProperty,
                        Value = "Turquoise"
                    },
                }
            };
            Resources.Add(normalEditor);
            
            var normalEntry = new Style(typeof(Xamarin.Forms.Entry))
            {
                Class = "NormalEntry",
                ApplyToDerivedTypes = true,
                Setters =
                {
                    new Setter
                    {
                        Property = Xamarin.Forms.Entry.BackgroundColorProperty,
                        Value = "Turquoise"
                    },
                }
            };
            Resources.Add(normalEntry);
            
            var selectableDataTemplate = new DataTemplate(() =>
            {
                var grid = new Grid();
                var nameLabel = new Label { FontAttributes = FontAttributes.Bold };
                var selectedToggle = new Switch();

                // nameLabel.SetBinding(Label.TextProperty, "TagEntity.NickName");
                nameLabel.SetBinding(Label.TextProperty, "TagEntity");
                selectedToggle.SetBinding(Switch.IsToggledProperty, "Selected");

                grid.Children.Add(nameLabel);
                grid.Children.Add(selectedToggle, 1, 0);
                
                return new Xamarin.Forms.ViewCell { View = grid };
            });
            Resources.Add("SelectableDataTemplate",selectableDataTemplate);
        }
    }
}

