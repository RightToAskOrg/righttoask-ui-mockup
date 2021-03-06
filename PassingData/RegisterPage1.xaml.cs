using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage1 : ContentPage
    {
        private ReadingContext readingContext;
        public RegisterPage1(ReadingContext readingContext)
        {
            InitializeComponent();
            this.readingContext = readingContext;
            BindingContext = readingContext;
            if (!readingContext.ThisParticipant.MPsKnown)
            {
                registerCitizenButton.Text = "Next: Find your electorates";
            }
            
            if (!readingContext.ThisParticipant.Is_Registered)
            {
                registerCitizenButton.IsVisible = true;
                // findElectoratesButton.IsVisible = false;
            }
            else
            {
                if (readingContext.ThisParticipant.MPsKnown)
                {
                    
                    DisplayAlert("Electorates already selected",
                        "You have already selected your electorates - you can change them if you like",
                        "OK");
                }
                registerCitizenButton.IsVisible = false;
                // findElectoratesButton.IsVisible = true;
            }
        }

        async void OnRegisterNameFieldCompleted(object sender, EventArgs e)
        {
	        readingContext.ThisParticipant.UserName = ((Editor) sender).Text;
        }
        
        // If MPs are not known, show page that allows finding electorates.
        // Whether or not they choose some, let them finish registering.
        // Make sure they've entered a name.
        async void OnRegisterCitizenButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(readingContext.ThisParticipant.UserName))
            {
                DisplayAlert("Enter username",
                    "You need to choose a username in order to make an account",
                    "OK");
            }
            else
            {
                readingContext.ThisParticipant.Is_Registered = true;
                
                var currentPage = Navigation.NavigationStack.LastOrDefault();
                
                if (!readingContext.ThisParticipant.MPsKnown)
                {
                    var findElectoratesPage = new RegisterPage2(readingContext.ThisParticipant, true);
                    await Navigation.PushAsync(findElectoratesPage);
                }
                
                Navigation.RemovePage(currentPage);
            }
        }

        async void OnFindElectoratesButtonClicked(object sender, EventArgs e)
        {
                registerCitizenButton.IsVisible = true;
                var secondRegisterPage = new RegisterPage2(readingContext.ThisParticipant, true);
			    await Navigation.PushAsync (secondRegisterPage);
        }
        void OnRegisterMPButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Registering not implemented yet";
        }
        void OnRegisterOrgButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Registering not implemented yet";
        }

        private void OnRegisterEmailFieldCompleted(object sender, EventArgs e)
        {
	        readingContext.ThisParticipant.UserEmail = ((Editor) sender).Text;
        }
    }
}