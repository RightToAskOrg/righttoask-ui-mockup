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
            addressSavingStack.IsVisible = false;
            findMPsButton.IsVisible = false;
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
                ((ReadingContext) BindingContext).SelectedStateOrTerritory = selectedState.TagEntity.EntityName;
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
                ((ReadingContext) BindingContext).SelectedStateElectorate = selectedStateElectorate.TagEntity.EntityName;
                
                if (((ReadingContext) BindingContext).SelectedFederalElectorate != null)
                {
                    findMPsButton.IsVisible = true;
                    ((ReadingContext) BindingContext).MPsKnown = true;
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
                ((ReadingContext) BindingContext).SelectedFederalElectorate = selectedFederalElectorate.TagEntity.EntityName;

                if (((ReadingContext) BindingContext).SelectedStateElectorate != null)
                {
                    findMPsButton.IsVisible = true;
                    ((ReadingContext) BindingContext).MPsKnown = true;
                }
            }
        }

        async private void OnFindMPsButtonClicked(object sender, EventArgs e)
        {
            // string message = "These are your MPs.  Select the one(s) you want to answer your question";
			// var selectMyMPsPage = new ExploringPage(((ReadingContext) BindingContext).MyMPs, message);
			// selectMyMPsPage.BindingContext = BindingContext;
			// await Navigation.PushAsync(selectMyMPsPage);
            // Navigation.RemovePage(this);
            await Navigation.PopAsync();
        }

        async void OnAddressEntered(object sender, EventArgs e)
        {
            address = ((Editor) sender).Text;
            // OnSubmitAddressButton_Clicked();
        }

        // At the moment this just chooses random electorates. 
        async void OnSubmitAddressButton_Clicked(object sender, EventArgs e)
        {
            ReadingContext context = (ReadingContext) BindingContext;
            
            var random = new Random();
            int stateElectorateCount = context.StateElectorates.Count;
            int federalElectorateCount = context.FederalElectorates.Count;

            context.SelectedStateElectorate = context.StateElectorates[random.Next(stateElectorateCount)].TagEntity.EntityName;
            context.SelectedFederalElectorate= context.FederalElectorates[random.Next(federalElectorateCount)].TagEntity.EntityName;
            context.MPsKnown = true;

            ((Button) sender).Text = "Electorates found! See above";
            federalElectoratePicker.TextColor = Color.Black;
            stateElectoratePicker.TextColor = Color.Black;
            ((Button) sender).IsEnabled = false;
            addressSavingStack.IsVisible = true;
        }
        
        private void OnSaveAddressButtonClicked(object sender, EventArgs e)
        {
            ((ReadingContext) BindingContext).Address = address;
            saveAddressButton.Text = "Address saved";
            noSaveAddressButton.IsVisible = false;
            findMPsButton.IsVisible = true;
            // offerRegistrationCompletion();
        }

        private void OnNoSaveAddressButtonClicked(object sender, EventArgs e)
        {
            noSaveAddressButton.Text = "Address not saved";
            saveAddressButton.IsVisible = false;
            findMPsButton.IsVisible = true;
            // offerRegistrationCompletion();
        }

        // At the moment there is no distinction between registering and not registering,
        // except the flag set differently.
        private void OnNoRegisterButtonClicked(object sender, EventArgs e)
        {
            ((ReadingContext) BindingContext).Is_Registered = false;
            completeRegistration();
        }

        // Register both a name and electorates. At the moment, since there is no
        // public registration, this is identical to the case in which you register
        // a name and electorates.
        private void OnRegisterElectoratesButtonClicked(object sender, EventArgs e)
        {
            ((ReadingContext) BindingContext).Is_Registered = true;
            completeRegistration();
        }

        private void OnRegisterNameButtonClicked(object sender, EventArgs e)
        {
            ((ReadingContext) BindingContext).Is_Registered = true;
            throw new NotImplementedException();
        }

        async private void completeRegistration()
        {
            // Remove page before this, which should be RegisterPage1 
            // TODO should check that this is the page we expect it to be before removing it
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            // This PopAsync will now go to wherever the user started registration from 
            // this.Navigation.PopAsync ();
            await Navigation.PopAsync();
        }
    }
}