using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using Xamarin.Forms;

namespace PassingData
{
	public partial class SecondPage : ContentPage
	{
		private string question;
		private ObservableCollection<Tag> otherAuthorities;
		private string MPFindPrompt = String.Empty;
		private bool isReadingOnly;

		public SecondPage (bool MPsAreSelected, bool IsReadingOnly)
		{
			
			InitializeComponent ();
			isReadingOnly = IsReadingOnly;	
			if(!MPsAreSelected)
			{
				MPFindPrompt = " - Find my MPs";
			}
			myMP.Text = "My MP" + MPFindPrompt;
			myMPShouldRaiseItButton.Text = "My MP should raise it" + MPFindPrompt;

			if (IsReadingOnly)
			{
				TitleBar.Title = "Help me find questions I care about";
				QuestionDraftingBox.IsVisible = false;
			}
			else
			{
				TitleBar.Title =  "Help me direct my question";
			}

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
		void Question_Entered(object sender, EventArgs e)
		{
			((ReadingContext) BindingContext).DraftQuestion = ((Editor) sender).Text;
		}
		
		// Initiate a question-reading page that is _not_ read only.
		async void OnNavigateForwardButtonClicked (object sender, EventArgs e)
		{
			
			var readingPage = new ReadingPage(isReadingOnly);
			readingPage.BindingContext = BindingContext;
			await Navigation.PushAsync (readingPage);
		}
		async void OnNavigateBackButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}

		async void OnMinisterOrDeptButtonClicked(object sender, EventArgs e)
		{

			// var readingContext = new ReadingContext{
         	//			SearchKeyword = "",
         //				TopTen = false,
          //              Departments = departmentAuthorities
         //			};
		
         	var departmentPickerPage = new PickerPage();
        	departmentPickerPage.BindingContext = BindingContext;
         	//var departmentExploringPage = new ExploringPage();
        	//departmentExploringPage.BindingContext = readingContext;
        	await Navigation.PushAsync (departmentPickerPage);

            //if (readingContext.SelectedDepartment != null)
            //{
				((Button) sender).Text = ((ReadingContext) BindingContext).SelectedDepartment;
            //}
		}

		async private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
        	
         			
           	var departmentExploringPage = new ExploringPage(((ReadingContext) BindingContext).OtherAuthorities);
            departmentExploringPage.BindingContext = BindingContext;
           	await Navigation.PushAsync (departmentExploringPage);
		}

		private async void OnMPAnswerButtonClicked(object sender, EventArgs e)
		{
			// ((Button) sender).Text = $"MP answering not implemented yet";
			var findMyMPPage = new FindMyMP();
			findMyMPPage.BindingContext = BindingContext;
			await Navigation.PushAsync (findMyMPPage);
		}

		private void OnFindCommitteeButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Finding Committees not implemented yet";	
		}

		private async void OnMPRaiseButtonClicked(object sender, EventArgs e)
		{
			//((Button) sender).Text = $"MP raising not implemented yet";
			var findMyMPPage = new FindMyMP();
			findMyMPPage.BindingContext = BindingContext;
			await Navigation.PushAsync (findMyMPPage);
		}
	}
}

