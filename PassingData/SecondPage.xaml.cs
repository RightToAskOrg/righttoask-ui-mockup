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

		public SecondPage (bool MPsAreSelected)
		{

			InitializeComponent ();
			
			if(!MPsAreSelected)
			{
				MPFindPrompt = " - Find my MPs";
			}
			//myMP.Text = "My MP";

			myMP.Text = "My MP" + MPFindPrompt;
			myMPShouldRaiseItButton.Text = "My MP should raise it" + MPFindPrompt;

	}

		void Question_Entered(object sender, EventArgs e)
		{
			((ReadingContext) BindingContext).DraftQuestion = ((Editor) sender).Text;
		}
		async void OnNavigateForwardButtonClicked (object sender, EventArgs e)
		{
			var readingPage = new ReadingPage();
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
        	
         			
           	var departmentExploringPage = new ExploringPage();
            departmentExploringPage.BindingContext = BindingContext;
           	await Navigation.PushAsync (departmentExploringPage);
		}

		private void OnMPAnswerButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"MP answering not implemented yet";
		}

		private void OnFindCommitteeButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Finding Committees not implemented yet";	
		}

		private void OnMPRaiseButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"MP raising not implemented yet";
		}
	}
}

