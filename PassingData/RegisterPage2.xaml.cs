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
    public partial class RegisterPage2 : ContentPage
    {
       // private ReadingContext BindingContext;
        
        public RegisterPage2(ReadingContext context)
        {
            InitializeComponent();
            BindingContext = context;

            completeRegistrationButton.IsVisible = false;
        }
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

        void OnStateElectoratePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;

            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Tag selectedStateElectorate = (Tag) picker.ItemsSource[selectedIndex];
                selectedStateElectorate.Selected = true;
                ((ReadingContext) BindingContext).SelectedStateElectorate = selectedStateElectorate.TagLabel;
            }
        }

        void OnFederalElectoratePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;

            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Tag selectedFederalElectorate = (Tag) picker.ItemsSource[selectedIndex];
                selectedFederalElectorate.Selected = true;
                ((ReadingContext) BindingContext).SelectedStateOrTerritory = selectedFederalElectorate.TagLabel;
            }
        }

        async private void OnFindMPsButtonClicked(object sender, EventArgs e)
        {
			var findMyMPPage = new FindMyMP((ReadingContext) BindingContext);
			// findMyMPPage.BindingContext = BindingContext;
			await Navigation.PushAsync (findMyMPPage);
        }

        private void OnSkipButtonClicked(object sender, EventArgs e)
        {
            skipThisStepButton.IsVisible = false;
            completeRegistrationButton.IsVisible = true;
        }

        private void OnCompleteRegistrationButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Registering not implemented yet";
        }
    }
}