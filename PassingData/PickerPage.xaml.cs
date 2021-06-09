using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PassingData
{
    public partial class PickerPage : ContentPage
    {
		public PickerPage()
        {
            InitializeComponent();
		}
        void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            String chosenDept;
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            Tag selectedDept = (Tag) picker.ItemsSource[selectedIndex];

            if (selectedIndex != -1)
            {
                selectedDept.Selected = true;
                chosenDept = selectedDept.TagLabel;
                ((ReadingContext) BindingContext).SelectedDepartment = chosenDept;

            }
        }
    }
}