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
        private IndividualParticipant _thisParticipant;
        ReadingContext BindingContext ;
        public RegisterPage1(IndividualParticipant thisParticipant, ReadingContext context)
        {
            InitializeComponent();
            _thisParticipant = thisParticipant;
            BindingContext = context;
            if (context.MPsKnown)
            {
                registerCitizenButton.IsVisible = true;
                findElectoratesButton.IsVisible = false;
            }
            else
            {
                registerCitizenButton.IsVisible = false;
                findElectoratesButton.IsVisible = true;
            }
        }

        async void OnRegisterNameFieldCompleted(object sender, EventArgs e)
        {
	        _thisParticipant.Username = ((Editor) sender).Text;
        }
        
        // If MPs are not known, show page that allows finding electorates.
        // Whether or not they choose some, let them finish registering.
        // Make sure they've entered a name.
        async void OnRegisterCitizenButtonClicked(object sender, EventArgs e)
        {
            // TODO  TODONOW This doesn't seem to be doing the right thing.
            // Make a nice popup alert instead.
            if (_thisParticipant.Username == "Anonymous user")
            {
                registerNameInstructions.BackgroundColor = Color.Red;
            }
            else
            {
                _thisParticipant.Is_Registered = true;
                Navigation.PopAsync();
            }
        }

        async void OnFindElectoratesButtonClicked(object sender, EventArgs e)
        {
                registerCitizenButton.IsVisible = true;
                findElectoratesButton.IsVisible = false; 
                var secondRegisterPage = new RegisterPage2(BindingContext);
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
	        _thisParticipant.UserEmail = ((Editor) sender).Text;
        }
    }
}