using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PassingData
{
	public partial class SecondPage : ContentPage
	{
		private string question;
		private ObservableCollection<Tag> otherAuthorities;
		private bool isReadingOnly;

		public SecondPage (bool MPsAreSelected, bool IsReadingOnly)
		{
			
			InitializeComponent ();
			isReadingOnly = IsReadingOnly;

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
			
			var readingPage = new ReadingPage(isReadingOnly, ((ReadingContext) BindingContext).OtherAuthorities);
			readingPage.BindingContext = BindingContext;
			await Navigation.PushAsync (readingPage);
		}
		async void OnNavigateBackButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}

		async private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
			string message = "Choose the authorities that should answer your question";
			
           	var departmentExploringPage = new ExploringPage(((ReadingContext) BindingContext).OtherAuthorities, message);
            departmentExploringPage.BindingContext = BindingContext;
           	await Navigation.PushAsync (departmentExploringPage);
		}

	    // If we already know the electorates (and hence responsible MPs), go
	    // straight to the Explorer page that lists them.
	    // If we don't, go to the page for entering address and finding them.
	    // It will pop back to here.
		async void OnAnsweredByMPButtonClicked(object sender, EventArgs e)
		{
            string message = "These are your MPs.  Select the one(s) you want to answer your question";
			
			if (! ((ReadingContext) BindingContext).MPsSelected)
			{
				var registrationPage = new RegisterPage2((ReadingContext) BindingContext);
				
				var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
				registrationPage.Disappearing += (sender2, e2) =>
				{
					waitHandle.Set();
				};
				
				await Navigation.PushAsync(registrationPage);
				System.Diagnostics.Debug.WriteLine("The modal page is now on screen, hit back button");
				await Task.Run(() => waitHandle.WaitOne());
				System.Diagnostics.Debug.WriteLine("The modal page is dismissed, do something now");

			}
           	var mpsExploringPage = new ExploringPage(((ReadingContext) BindingContext).MyMPs, message);
            mpsExploringPage.BindingContext = BindingContext;
           	await Navigation.PushAsync (mpsExploringPage);
		}

		private void OnFindCommitteeButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Finding Committees not implemented yet";	
		}

		private async void OnMPRaiseButtonClicked(object sender, EventArgs e)
		{
			//((Button) sender).Text = $"MP raising not implemented yet";
			// var findMyMPPage = new FindMyMP((ReadingContext)BindingContext);
			var registerPage1 = new RegisterPage1((ReadingContext) BindingContext);
			// findMyMPPage.BindingContext = BindingContext;
			await Navigation.PushAsync (registerPage1);
		}

	}
}

