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
            BindingContext = context;
            InitializeComponent();

            // ((Label) WelcomeName).Text = context.Username;
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

        private void OnFindMPsButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Finding MPs not implemented yet";
        }

        private void OnSkipButtonClicked(object sender, EventArgs e)
        {
            ((Button) sender).Text = "Skip not implemented yet";
        }
    }
}