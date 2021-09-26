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
		private ObservableCollection<Tag> SelectableAuthorities;
		private bool isReadingOnly;
		private ReadingContext readingContext;

		public SecondPage(bool IsReadingOnly, ReadingContext readingContext)
		{
			
			InitializeComponent ();
			BindingContext = readingContext;
			this.readingContext = readingContext;
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
		
		/*
		void OnPickerSelectedIndexChanged(object sender, EventArgs e) 
		{
            var picker = (Picker)sender;
            
            int selectedIndex = picker.SelectedIndex;

            if (selectedIndex != -1)
            {
                Tag selectedDept = (Tag) picker.ItemsSource[selectedIndex];
                selectedDept.Selected = true;
                _context.SelectedDepartment = selectedDept.TagEntity;
                questionAsker.IsVisible = true;
            }
        }
        */
		
		void Question_Entered(object sender, EventArgs e)
		{
			readingContext.DraftQuestion = ((Editor) sender).Text;
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
			bool needToFindAnswerer = readingContext.SelectedDepartment != null
			       || readingContext.SelectableAuthorities.Where(w => w.Selected).Count() != 0;
			
			if (isReadingOnly || !needToFindAnswerer)
			{
				var readingPage = new ReadingPage(isReadingOnly, readingContext.SelectableAuthorities, readingContext);
				await Navigation.PushAsync (readingPage);
			}
			else 
			{
				var questionAskerPage = new QuestionAskerPage(readingContext);
				await Navigation.PushAsync(questionAskerPage);
			}
		}
		async void OnNavigateBackButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}

		async private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
			// var webViewAuthoritySelectPage = new WebviewAuthoritySelect((ReadingContext) BindingContext);
			// await Navigation.PushAsync(webViewAuthoritySelectPage);
			var exploringPageToSearchAuthorities= new ExploringPageWithSearch(readingContext.SelectableAuthorities,
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
           	var mpsExploringPage = new ExploringPage(readingContext.MyMPs, message);
           	await Navigation.PushAsync (mpsExploringPage);
            
            FindMPsIfNotAlreadyKnown();
            
            questionAsker.IsVisible = true;
		}


		// TODO: at the moment this doesn't properly select the MPs-  it just lists them and lets
		// it looks like you've selected them.
		private async void OnMyMPRaiseButtonClicked(object sender, EventArgs e)
		{
            
            string message = "These are your MPs.  Select the one(s) who should raise the question in Parliament";
           	var mpsExploringPage = new ExploringPage(readingContext.MyMPs, message);
           	await Navigation.PushAsync (mpsExploringPage);
			
            FindMPsIfNotAlreadyKnown();
		}

		// TODO: This is inelegant at the moment. The underlying page is briefly
		// visible before it appears - it would be better to pop this up first, then
		// insert the exploringpage underneath.
		async void FindMPsIfNotAlreadyKnown()
		{
			var thisParticipant = readingContext.ThisParticipant;
			
			if (thisParticipant == null || ! thisParticipant.MPsKnown)
			{
				var registrationPage = new RegisterPage2(readingContext);
				await Navigation.PushAsync(registrationPage);
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

