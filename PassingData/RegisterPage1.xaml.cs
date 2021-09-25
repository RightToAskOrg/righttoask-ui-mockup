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
        private IndividualParticipant thisParticipant;
        private ReadingContext readingContext;
        public RegisterPage1(IndividualParticipant thisParticipant, ReadingContext readingContext)
        {
            InitializeComponent();
            this.thisParticipant = thisParticipant;
            BindingContext = readingContext;
            if (!thisParticipant.Is_Registered)
            {
                registerCitizenButton.IsVisible = true;
                findElectoratesButton.IsVisible = false;
            }
            else
            {
                if (thisParticipant.MPsKnown)
                {
                    
                    DisplayAlert("Electorates already selected",
                        "You have already selected your electorates - you can change them if you like",
                        "OK");
                }
                registerCitizenButton.IsVisible = false;
                findElectoratesButton.IsVisible = true;
            }
        }

        async void OnRegisterNameFieldCompleted(object sender, EventArgs e)
        {
	        thisParticipant.Username = ((Editor) sender).Text;
        }
        
        // If MPs are not known, show page that allows finding electorates.
        // Whether or not they choose some, let them finish registering.
        // Make sure they've entered a name.
        void OnRegisterCitizenButtonClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(thisParticipant.Username))
            {
                DisplayAlert("Enter username",
                    "You need to choose a username in order to make an account",
                    "OK");
            }
            else
            {
                // thisParticipant.Is_Registered = true;
                Navigation.PopAsync();
            }
        }

        async void OnFindElectoratesButtonClicked(object sender, EventArgs e)
        {
                registerCitizenButton.IsVisible = true;
                findElectoratesButton.IsVisible = false; 
                var secondRegisterPage = new RegisterPage2(readingContext);
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
	        thisParticipant.UserEmail = ((Editor) sender).Text;
        }
    }
}