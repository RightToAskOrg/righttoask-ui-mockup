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
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Tag selectedDept = (Tag) picker.ItemsSource[selectedIndex];
                selectedDept.Selected = true;
                ((ReadingContext) BindingContext).SelectedDepartment = selectedDept.TagLabel;

            }
        }
    }
}