using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage1 : ContentPage
    {
        public RegisterPage1(ReadingContext context)
        {
            InitializeComponent();
            BindingContext = context;
        }

        async void OnRegisterNameFieldCompleted(object sender, EventArgs e)
        {
	        ((ReadingContext) BindingContext).Username = ((Entry) sender).Text;

	        // var readingPage = new ReadingPage(true);
	        // readingPage.BindingContext = readingContext;
	        // await Navigation.PushAsync(readingPage);
        }
        
        void OnRegisterCitizenButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Registering not implemented yet";
        }
        void OnRegisterMPButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Registering not implemented yet";
        }
        void OnRegisterOrgButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Registering not implemented yet";
        }
        
    }
}