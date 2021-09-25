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
       private string address;
       private IndividualParticipant thisParticipant;
        public RegisterPage2(ReadingContext context)
        {
            InitializeComponent();
            BindingContext = context;
            IndividualParticipant thisParticipant = new IndividualParticipant();
            addressSavingStack.IsVisible = false;
            findMPsButton.IsVisible = false;
            stateOrTerritoryPicker.ItemsSource = BackgroundElectorateAndMPData.StatesAndTerritories;
        }
        
        // TODO Refactor this nicely so it isn't copy-pasted in FindMyMP
        void OnStatePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
                    
            int selectedIndex = picker.SelectedIndex;
         
            if (selectedIndex != -1)
            {
                thisParticipant.StateOrTerritory = (string) picker.SelectedItem;
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
                thisParticipant.SelectedStateElectorate = selectedStateElectorate.TagEntity.EntityName;
                
                if (thisParticipant.SelectedFederalElectorate != null)
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
                thisParticipant.SelectedFederalElectorate = selectedFederalElectorate.TagEntity.EntityName;

                if (thisParticipant.SelectedStateElectorate != null)
                {
                    findMPsButton.IsVisible = true;
                    ((ReadingContext) BindingContext).MPsKnown = true;
                }
            }
        }

        async private void OnFindMPsButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        void OnAddressEntered(object sender, EventArgs e)
        {
            address = ((Editor) sender).Text;
        }

        // At the moment this just chooses random electorates. 
        // TODO: We probably want this to give the person a chance to go back and fix it if wrong.
        async void OnSubmitAddressButton_Clicked(object sender, EventArgs e)
        {
            ReadingContext context = (ReadingContext) BindingContext;
            
            var random = new Random();
            int stateElectorateCount = context.StateElectorates.Count;
            int federalElectorateCount = context.FederalElectorates.Count;

            thisParticipant.SelectedStateElectorate = context.StateElectorates[random.Next(stateElectorateCount)].TagEntity.EntityName;
            thisParticipant.SelectedFederalElectorate= context.FederalElectorates[random.Next(federalElectorateCount)].TagEntity.EntityName;
            context.MPsKnown = true;

            await DisplayAlert("Electorates found!", 
                "State Electorate: "+thisParticipant.SelectedStateElectorate+"\nFederal Electorate: "
                +thisParticipant.SelectedFederalElectorate, "OK");
            ((Button) sender).IsVisible = false; 
            federalElectoratePicker.TextColor = Color.Black;
            stateElectoratePicker.TextColor = Color.Black;
            ((Button) sender).IsEnabled = false;
            addressSavingStack.IsVisible = true;
        }
        
        private void OnSaveAddressButtonClicked(object sender, EventArgs e)
        {
            thisParticipant.Address = address;
            saveAddressButton.Text = "Address saved";
            noSaveAddressButton.IsVisible = false;
            findMPsButton.IsVisible = true;
        }

        private void OnNoSaveAddressButtonClicked(object sender, EventArgs e)
        {
            noSaveAddressButton.Text = "Address not saved";
            saveAddressButton.IsVisible = false;
            findMPsButton.IsVisible = true;
        }

        // At the moment there is no distinction between registering and not registering,
        // except the flag set differently.
        private void OnNoRegisterButtonClicked(object sender, EventArgs e)
        {
            thisParticipant.Is_Registered = false;
            completeRegistration();
        }

        // Register both a name and electorates. At the moment, since there is no
        // public registration, this is identical to the case in which you register
        // a name and electorates.
        private void OnRegisterElectoratesButtonClicked(object sender, EventArgs e)
        {
            thisParticipant.Is_Registered = true;
            completeRegistration();
        }

        private void OnRegisterNameButtonClicked(object sender, EventArgs e)
        {
            thisParticipant.Is_Registered = true;
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