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
        ReadingContext BindingContext ;
        public RegisterPage1(ReadingContext context)
        {
            InitializeComponent();
            BindingContext = context;
            if (context.MPsKnown)
            {
                registerCitizenButton.IsVisible = true;
                findMPsButton.IsVisible = false;
            }
            else
            {
                registerCitizenButton.IsVisible = false;
                findMPsButton.IsVisible = true;
            }
        }

        async void OnRegisterNameFieldCompleted(object sender, EventArgs e)
        {
	        BindingContext.Username = ((Editor) sender).Text;
        }
        
        // If MPs are not known, show page that allows finding electorates.
        // Whether or not they choose some, let them finish registering.
        // Make sure they've entered a name.
        async void OnRegisterCitizenButtonClicked(object sender, EventArgs e)
        {
            // TODO This doesn't seem to be doing the right thing.
            if (BindingContext.Username == "Anonymous user")
            {
                registerNameInstructions.BackgroundColor = Color.Red;
            }
            else
            {
                BindingContext.Is_Registered = true;
                Navigation.PopAsync();
            }
        }

        async void OnFindMPsButtonClicked(object sender, EventArgs e)
        {
                registerCitizenButton.IsVisible = true;
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
	        BindingContext.UserEmail = ((Editor) sender).Text;
        }
    }
}