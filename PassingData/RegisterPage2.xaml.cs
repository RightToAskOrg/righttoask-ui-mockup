using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

/*
 * This page allows a person to find which electorates they live in,
 * and hence which MPs represent them.
 *
 * This is used in two possible places:
 * (1) if the person clicks on 'My MP' when setting question metadata,
 * we need to know who their MPs are. After this page,
 * there is a list of MPs loaded for them to choose from.
 * This is implemented by inputing a page to go to next.
 * 
 * (2) if the person tries to vote or post a question.
 * In this case, they have generated a name via RegisterPage1
 * and can skip this step.  
 */
namespace PassingData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage2 : ContentPage
    {
        private string address;
        private ReadingContext readingContext;
        private Page nextPage;

        private List<string> allFederalElectorates;
        private List<string> allStateLAElectorates;
        private List<string> allStateLCElectorates;
        public RegisterPage2(ReadingContext readingContext, bool showSkip, Page nextPage = null)
        {
            InitializeComponent();
            BindingContext = readingContext;
            this.readingContext = readingContext;
            this.nextPage = nextPage;
            stateOrTerritoryPicker.ItemsSource = BackgroundElectorateAndMPData.StatesAndTerritories;
            
            addressSavingStack.IsVisible = false;
            FindMPsButton.IsVisible = false;
            if (!showSkip)
            {
                SkipButton.IsVisible = false;
            }
        }
        
        void OnStatePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
                    
            int selectedIndex = picker.SelectedIndex;
         
            if (selectedIndex != -1)
            {
                string state = (string) picker.SelectedItem;
                readingContext.ThisParticipant.StateOrTerritory = state; 
                UpdateElectoratePickerSources(state);
            }
        }

        // TODO This treats everyone as if they're VIC at the moment.
        // Add specific sources for LC and LA in specific states.
        private void UpdateElectoratePickerSources(string state)
        {
            allFederalElectorates = BackgroundElectorateAndMPData.ListElectoratesInChamber(BackgroundElectorateAndMPData.Chamber.Australian_House_Of_Representatives);
            federalElectoratePicker.ItemsSource = allFederalElectorates;
            
            allStateLAElectorates 
                = BackgroundElectorateAndMPData.ListElectoratesInChamber(BackgroundElectorateAndMPData.Chamber.Vic_Legislative_Assembly);
            stateLAElectoratePicker.ItemsSource = allStateLAElectorates;
            allStateLCElectorates 
                = BackgroundElectorateAndMPData.ListElectoratesInChamber(BackgroundElectorateAndMPData.Chamber.Vic_Legislative_Council);
            stateLCElectoratePicker.ItemsSource = allStateLCElectorates;
        }

        void OnStateLCElectoratePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;
            readingContext.ThisParticipant.SelectedLCStateElectorate = ChooseElectorate(picker, allStateLCElectorates);
            RevealNextStepIfElectoratesKnown();
        }
        void OnStateLAElectoratePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;
            readingContext.ThisParticipant.SelectedLAStateElectorate = ChooseElectorate(picker, allStateLAElectorates);
            RevealNextStepIfElectoratesKnown();
        }

        void OnFederalElectoratePickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;
            readingContext.ThisParticipant.SelectedFederalElectorate = ChooseElectorate(picker, allFederalElectorates);
            RevealNextStepIfElectoratesKnown();

        }

        // TODO: Deal intelligently with error handling if the array index is out of bounds.
        private string ChooseElectorate(Picker p, List<string> allElectorates)
        {
            int selectedIndex = p.SelectedIndex;

            if (selectedIndex >= 0 && selectedIndex < allElectorates.Count)
            {
                return allElectorates[selectedIndex];
            }

            return null;

        }
        
        private void RevealNextStepIfElectoratesKnown()
        {
            if(!String.IsNullOrEmpty(readingContext.ThisParticipant.SelectedLAStateElectorate)
                && !String.IsNullOrEmpty(readingContext.ThisParticipant.SelectedLCStateElectorate)
                && !String.IsNullOrEmpty(readingContext.ThisParticipant.SelectedFederalElectorate))
                {
                    FindMPsButton.IsVisible = true;
                    readingContext.ThisParticipant.MPsKnown = true;
                }
        }
        
        // If we've been given a nextPage, go there and remove this page,
        // otherwise just pop.
        private async void OnFindMPsButtonClicked(object sender, EventArgs e)
        {
            var currentPage = Navigation.NavigationStack.LastOrDefault();
        
            if (nextPage != null)
            {
                await Navigation.PushAsync(nextPage);
            }
            
            Navigation.RemovePage(currentPage); 
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

            if(String.IsNullOrEmpty(readingContext.ThisParticipant.SelectedLAStateElectorate))
            {
                readingContext.ThisParticipant.SelectedLAStateElectorate 
                    = allStateLAElectorates[random.Next(allStateLAElectorates.Count)];   
            }

            if (String.IsNullOrEmpty(readingContext.ThisParticipant.SelectedLCStateElectorate))
            {
                readingContext.ThisParticipant.SelectedLCStateElectorate 
                    = allStateLCElectorates[random.Next(allStateLCElectorates.Count)];
            }

            if (String.IsNullOrEmpty(readingContext.ThisParticipant.SelectedFederalElectorate))
            {
                readingContext.ThisParticipant.SelectedFederalElectorate 
                    = allFederalElectorates[random.Next(allFederalElectorates.Count)];
            }
            
            readingContext.ThisParticipant.MPsKnown = true;

            await DisplayAlert("Electorates found!", 
                "State Assembly Electorate: "+readingContext.ThisParticipant.SelectedLAStateElectorate+"\n"
                +"State Legislative Council Electorate: "+readingContext.ThisParticipant.SelectedLCStateElectorate+"\n"
                +"Federal Electorate: "+readingContext.ThisParticipant.SelectedFederalElectorate, "OK");
            ((Button) sender).IsVisible = false; 
            federalElectoratePicker.TextColor = Color.Black;
            stateLAElectoratePicker.TextColor = Color.Black;
            stateLCElectoratePicker.TextColor = Color.Black;
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
            // this.Navigation.RemovePage(this.Navigation.NavigationStack[this.Navigation.NavigationStack.Count - 2]);
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