using System;
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
				TitleBar.Title = "Find questions";
				QuestionDraftingBox.IsVisible = false;
			}
			else
			{
				TitleBar.Title =  "Direct my question";
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
                ((ReadingContext) BindingContext).SelectedDepartment = selectedDept.TagEntity;
                questionAsker.IsVisible = true;
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

		// TODO At the moment, this just turns the 'who should ask it' on,
		// if you looked at the authority list, regardless of whether you actually
		// chose one.
		/*
		async private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
			string message = "Choose the authorities that should answer your question";
			
           	var departmentExploringPage = new ExploringPage(((ReadingContext) BindingContext).OtherAuthorities, message);
            departmentExploringPage.BindingContext = BindingContext;
           	await Navigation.PushAsync (departmentExploringPage);

            questionAsker.IsVisible = true;
		}
		*/


		async private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
			// var webViewAuthoritySelectPage = new WebviewAuthoritySelect((ReadingContext) BindingContext);
			// await Navigation.PushAsync(webViewAuthoritySelectPage);
			var exploringPageToSearchAuthorities= new ExploringPageWithSearch(((ReadingContext)BindingContext).OtherAuthorities,
				"Choose authorties");
			await Navigation.PushAsync(exploringPageToSearchAuthorities);
		}

		// If we already know the electorates (and hence responsible MPs), go
	    // straight to the Explorer page that lists them.
	    // If we don't, go to the page for entering address and finding them.
	    // It will pop back to here.
		async void OnAnsweredByMPButtonClicked(object sender, EventArgs e)
		{
            
            string message = "These are your MPs.  Select the one(s) who should answer the question";
           	var mpsExploringPage = new ExploringPage(((ReadingContext) BindingContext).MyMPs, message);
            mpsExploringPage.BindingContext = BindingContext;
            // mpsExploringPage.Appearing += FindMPsIfNotAlreadyKnown();
           	await Navigation.PushAsync (mpsExploringPage);
            
            FindMPsIfNotAlreadyKnown();
            
            questionAsker.IsVisible = true;
		}


		// TODO: at the moment this doesn't properly select the MPs-  it just lists them and lets
		// it looks like you've selected them.
		private async void OnMyMPRaiseButtonClicked(object sender, EventArgs e)
		{
            
            string message = "These are your MPs.  Select the one(s) who should raise the question in Parliament";
           	var mpsExploringPage = new ExploringPage(((ReadingContext) BindingContext).MyMPs, message);
            mpsExploringPage.BindingContext = BindingContext;
            // mpsExploringPage.Appearing += FindMPsIfNotAlreadyKnown();
           	await Navigation.PushAsync (mpsExploringPage);
			
            FindMPsIfNotAlreadyKnown();
		}

		// TODO: This is inelegant at the moment. The underlying page is briefly
		// visible before it appears - it would be better to pop this up first, then
		// insert the exploringpage underneath.
		async void FindMPsIfNotAlreadyKnown()
		{
			if (! ((ReadingContext) BindingContext).MPsKnown)
			{
				var registrationPage = new RegisterPage2((ReadingContext) BindingContext);
				
				// var waitHandle = new EventWaitHandle(false, EventResetMode.AutoReset);
				// registrationPage.Disappearing += (sender2, e2) =>
				// {
				// 	waitHandle.Set();
				// };
				
				await Navigation.PushAsync(registrationPage);
				// System.Diagnostics.Debug.WriteLine("The modal page is now on screen, hit back button");
				// await Task.Run(() => waitHandle.WaitOne());
				// System.Diagnostics.Debug.WriteLine("The modal page is dismissed, do something now");
			}
		}
		private void OnFindCommitteeButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Finding Committees not implemented yet";	
		}

		private async void OnOtherMPRaiseButtonClicked(object sender, EventArgs e)
		{
			var selectableMPs =
				new ObservableCollection<Tag>(BackgroundElectorateAndMPData.AllMPs.Select
				(mp => new Tag
				{
					TagEntity = mp, 
					Selected = false
				}
				)
				);

			var mpsPage = new ExploringPageWithSearch(selectableMPs, "Here is the complete list of MPs");
			await Navigation.PushAsync(mpsPage);
		}

		private void OnAnswerByOtherMPButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Listing other MPs not implemented yet";	
		}
	}
}

