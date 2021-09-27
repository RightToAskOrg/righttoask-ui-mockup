using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * This page allows a person to find which electorates they live in,
 * and hence which MPs represent them.
 *
 * This is used in two possible places: if the person clicks on 'My MP'
 * when setting question metadata, or if the person tries to vote or post
 * a question.  In the latter case, they also have to generate a name
 * via RegisterPage1.  In the former case, there is a list of MPs loaded for
 * them to choose from.
 */
namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage2 : ContentPage
    {
       private string address;
       private ReadingContext readingContext;
        public RegisterPage2(ReadingContext readingContext, bool showSkip)
        {
            InitializeComponent();
            BindingContext = readingContext;
            this.readingContext = readingContext;
            stateOrTerritoryPicker.ItemsSource = BackgroundElectorateAndMPData.StatesAndTerritories;
            
            addressSavingStack.IsVisible = false;
            FindMPsButton.IsVisible = false;
            if (!showSkip)
            {
                SkipButton.IsVisible = false;
            }
        }
        
        // TODO Refactor this nicely so it isn't copy-pasted in FindMyMP
        void OnStatePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
                    
            int selectedIndex = picker.SelectedIndex;
         
            if (selectedIndex != -1)
            {
                readingContext.ThisParticipant.StateOrTerritory = (string) picker.SelectedItem;
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
                readingContext.ThisParticipant.SelectedStateElectorate = selectedStateElectorate.TagEntity.EntityName;
                
                if (readingContext.ThisParticipant.SelectedFederalElectorate != null)
                {
                    FindMPsButton.IsVisible = true;
                    readingContext.ThisParticipant.MPsKnown = true;
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
                readingContext.ThisParticipant.SelectedFederalElectorate = selectedFederalElectorate.TagEntity.EntityName;

                if (readingContext.ThisParticipant.SelectedStateElectorate != null)
                {
                    FindMPsButton.IsVisible = true;
                    readingContext.ThisParticipant.MPsKnown = true;
                }
            }
        }

        private async void OnFindMPsButtonClicked(object sender, EventArgs e)
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
            var random = new Random();
            int stateElectorateCount = readingContext.StateElectorates.Count;
            int federalElectorateCount = readingContext.FederalElectorates.Count;

            readingContext.ThisParticipant.SelectedStateElectorate = readingContext.StateElectorates[random.Next(stateElectorateCount)].TagEntity.EntityName;
            readingContext.ThisParticipant.SelectedFederalElectorate= readingContext.FederalElectorates[random.Next(federalElectorateCount)].TagEntity.EntityName;
            readingContext.ThisParticipant.MPsKnown = true;

            await DisplayAlert("Electorates found!", 
                "State Electorate: "+readingContext.ThisParticipant.SelectedStateElectorate+"\nFederal Electorate: "
                +readingContext.ThisParticipant.SelectedFederalElectorate, "OK");
            ((Button) sender).IsVisible = false; 
            federalElectoratePicker.TextColor = Color.Black;
            stateElectoratePicker.TextColor = Color.Black;
            ((Button) sender).IsEnabled = false;
            addressSavingStack.IsVisible = true;

            FindMPsButton.IsVisible = true;
            SkipButton.IsVisible = false;
        }
        
        private void OnSaveAddressButtonClicked(object sender, EventArgs e)
        {
            readingContext.ThisParticipant.Address = address;
            saveAddressButton.Text = "Address saved";
            noSaveAddressButton.IsVisible = false;
            FindMPsButton.IsVisible = true;
        }

        private void OnNoSaveAddressButtonClicked(object sender, EventArgs e)
        {
            noSaveAddressButton.Text = "Address not saved";
            saveAddressButton.IsVisible = false;
            FindMPsButton.IsVisible = true;
        }

        // At the moment there is no distinction between registering and not registering,
        // except the flag set differently.
        
        private void OnNoRegisterButtonClicked(object sender, EventArgs e)
        {
            completeRegistration();
        }

        // Register both a name and electorates. At the moment, since there is no
        // public registration, this is identical to the case in which you register
        // a name and electorates.
        private void OnRegisterElectoratesButtonClicked(object sender, EventArgs e)
        {
            completeRegistration();
        }

        private void OnRegisterNameButtonClicked(object sender, EventArgs e)
        {
        }

        async private void completeRegistration()
        {
            if (!string.IsNullOrWhiteSpace(readingContext.ThisParticipant.Username))
            {
                readingContext.ThisParticipant.Is_Registered = true;
            }
            
            // Remove page before this, which should be RegisterPage1 
            // TODO should check that this is the page we expect it to be before removing it
            this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
            // This PopAsync will now go to wherever the user started registration from 
            // this.Navigation.PopAsync ();
            await Navigation.PopAsync();
        }

        // TODO Think about what should happen if the person has made 
        // some choices, then clicks 'skip'.  At the moment, it retains 
        // the choices they made and pops the page.
        private async void OnSkipButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}