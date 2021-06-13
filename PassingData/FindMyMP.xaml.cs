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
            addressAcknowledgement.Text = "Thankyou. RightToAsk will not retain your address.";
            await Task.Delay(2000);
            
            
        }
    }
}