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
    public partial class FindMyMP : ContentPage
    {
        public FindMyMP()
        {
            InitializeComponent();
        }

        async void OnAddressEntered(object sender, EventArgs e)
        {
            ((ReadingContext) BindingContext).MPsSelected = true;
           	var mpExploringPage = new ExploringPage(((ReadingContext) BindingContext).MyMPs);
            mpExploringPage.BindingContext = BindingContext;
           	await Navigation.PushAsync (mpExploringPage);
            addressAcknowledgement.Text = "Thankyou for selecting your MPs. RightToAsk will not retain your address.";
            // await Task.Delay(2000);
            
            
        }
    }
}