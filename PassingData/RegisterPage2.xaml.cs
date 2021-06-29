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
       private string address;
        public RegisterPage2(ReadingContext context)
        {
            InitializeComponent();
            BindingContext = context;

            completeRegistrationButton.IsVisible = false;
        }
        
        // TODO Refactor this nicely so it isn't copy-pasted in FindMyMP
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

                // If both state and federal electorates are chosen, registration can be completed.
                if (((ReadingContext) BindingContext).SelectedFederalElectorate != null)
                {
                    offerRegistrationCompletion();
                }
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
                ((ReadingContext) BindingContext).SelectedFederalElectorate = selectedFederalElectorate.TagLabel;
                
                // If both state and federal electorates are chosen, registration can be completed.
                if (((ReadingContext) BindingContext).SelectedStateElectorate != null)
                {
                    offerRegistrationCompletion();
                }
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
            offerRegistrationCompletion();
        }
        async void OnAddressEntered(object sender, EventArgs e)
        {
            address = ((Entry) sender).Text;
            // OnSubmitAddressButton_Clicked();
        }

        async void OnSubmitAddressButton_Clicked(object sender, EventArgs e)
        {
            ReadingContext context = (ReadingContext) BindingContext;
            
            var random = new Random();
            int stateElectorateCount = context.StateElectorates.Count;
            int federalElectorateCount = context.FederalElectorates.Count;

            context.SelectedStateElectorate = context.StateElectorates[random.Next(stateElectorateCount)].TagLabel;
            context.SelectedFederalElectorate= context.FederalElectorates[random.Next(federalElectorateCount)].TagLabel;
        }
        private void offerRegistrationCompletion()
        {
            skipThisStepButton.IsVisible = false;
            completeRegistrationButton.IsVisible = true;
        }    
        
        // When registration is complete, remove (pop) both registration pages
        // to go back to wherever you were.
        async void OnCompleteRegistrationButtonClicked(object sender, EventArgs e)
        {
            // Remove page before this, which should be RegisterPage1 
            ((ReadingContext) BindingContext).Is_Registered = true;
            this.Navigation.RemovePage (this.Navigation.NavigationStack [this.Navigation.NavigationStack.Count - 2]);
            // This PopAsync will now go to wherever the user started registration from 
            // this.Navigation.PopAsync ();
            await Navigation.PopAsync(); 
        }

        private void OnSaveAddressButtonClicked(object sender, EventArgs e)
        {
            ((ReadingContext) BindingContext).Address = address;
            offerRegistrationCompletion();
        }

        private void OnNoSaveAddressButtonClicked(object sender, EventArgs e)
        {
            offerRegistrationCompletion();
        }
    }
}