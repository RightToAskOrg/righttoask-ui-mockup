﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
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
				questionAsker.IsVisible = false;
				navigateForwardButton.Text = "Next";
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
		
		// If in read-only mode, initiate a question-reading page.
		// Similarly if my MP is answering.
		// If drafting, load a question-asker page, which will then 
		// lead to a question-reading page.
		// TODO at the moment, it gives you question-directing if you've chosen
		// anything other than your MP.  Think about whether this is the right
		// behaviour. 
		// TODO also doesn't do the right thing if you've previously selected
		// someone other than your MP, because this is stored in the global binding
		// context.
		async void OnNavigateForwardButtonClicked (object sender, EventArgs e)
		{
			bool needToFindAnswerer = ((ReadingContext) BindingContext).SelectedDepartment != null
			       || ((ReadingContext) BindingContext).OtherAuthorities.Where(w => w.Selected).Count() != 0;
			
			if (isReadingOnly || !needToFindAnswerer)
			{
				var readingPage = new ReadingPage(isReadingOnly, ((ReadingContext) BindingContext).OtherAuthorities);
				readingPage.BindingContext = BindingContext;
				await Navigation.PushAsync (readingPage);
			}
			else 
			{
				var questionAskerPage = new QuestionAskerPage((ReadingContext) BindingContext);
				await Navigation.PushAsync(questionAskerPage);
			}
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
			
			if (! ((ReadingContext) BindingContext).MPsKnown)
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

