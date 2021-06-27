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
        public FindMyMP(ReadingContext context)
        {
            InitializeComponent();
            BindingContext = context;
        }

        // TODO at the moment this is copy-pasted from RegisterPage2 - organise better.
        void OnStatePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
                    
            int selectedIndex = picker.SelectedIndex;
         
            if (selectedIndex != -1)
            {
                Tag selectedState = (Tag) picker.ItemsSource[selectedIndex];
                selectedState.Selected = true;
                ((ReadingContext) BindingContext).SelectedStateOrTerritory = selectedState.TagLabel;
            }
        }
        async void OnAddressEntered(object sender, EventArgs e)
        {
            ReadingContext context = (ReadingContext) BindingContext;
            var random = new Random();
            // Choose a random state and federal electorate for now.
            int stateElectorateCount = context.StateElectorates.Count;
            int federalElectorateCount = context.FederalElectorates.Count;
            // int index = random.Next(stateElectorateCount);

            context.SelectedStateElectorate = context.StateElectorates[random.Next(stateElectorateCount)].TagLabel;
            context.SelectedFederalElectorate= context.FederalElectorates[random.Next(federalElectorateCount)].TagLabel;

            // ((ReadingContext) BindingContext).MPsSelected = true;
            // var mpExploringPage = new ExploringPage(((ReadingContext) BindingContext).MyMPs);
            // mpExploringPage.BindingContext = BindingContext;
            // await Navigation.PushAsync (mpExploringPage);
            // addressAcknowledgement.Text = "Thankyou for selecting your MPs. RightToAsk will not retain your address.";
            // await Task.Delay(2000);


        }

        async void OnSubmitAddressButton_Clicked(object sender, EventArgs e)
        {
            OnAddressEntered(sender, e);
        }
    }
}