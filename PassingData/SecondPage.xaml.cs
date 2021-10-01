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
			// bool needToFindAnswerer = readingContext.Filters.SelectedDepartment != null
			//        || readingContext.SelectableAuthorities.Where(w => w.Selected).Count() != 0;

			var selectedAuthorities = readingContext.Filters.SelectedAuthorities;
			bool needToFindAnswerer = selectedAuthorities != null && selectedAuthorities.Any();
			
			if (isReadingOnly || !needToFindAnswerer)
			{
				var readingPage = new ReadingPage(isReadingOnly, readingContext);
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
			var exploringPageToSearchAuthorities
				= new ExploringPageWithSearch(BackgroundElectorateAndMPData.AllAuthorities, readingContext.Filters.SelectedAuthorities,
				"Choose authorities");
			await Navigation.PushAsync(exploringPageToSearchAuthorities);
		}

		// If we already know the electorates (and hence responsible MPs), go
	    // straight to the Explorer page that lists them.
	    // If we don't, go to the page for entering address and finding them.
	    // It will pop back to here.
		async void OnAnsweredByMPButtonClicked(object sender, EventArgs e)
		{
            string message = "These are your MPs.  Select the one(s) who should answer the question";
            // TODO (Issue #9) update to use the properly-computed MPs in ThisParticipant.MyMPs
           	var mpsExploringPage = new ExploringPage(readingContext.TestCurrentMPs, readingContext.Filters.SelectedAnsweringMPsMine, message);
            
            ListMPsFindFirstIfNotAlreadyKnown(mpsExploringPage);
		}


		// TODO: at the moment this doesn't properly select the MPs-  it just lists them and lets
		// it looks like you've selected them.
		private void OnMyMPRaiseButtonClicked(object sender, EventArgs e)
		{
            string message = "These are your MPs.  Select the one(s) who should raise the question in Parliament";
            
            // TODO (Issue #9) update to use the properly-computed MPs in ThisParticipant.MyMPs
           	var mpsExploringPage = new ExploringPage(readingContext.TestCurrentMPs, readingContext.Filters.SelectedAskingMPsMine, message);
			
            ListMPsFindFirstIfNotAlreadyKnown(mpsExploringPage);
		}

		// TODO: This is inelegant at the moment. The underlying page is briefly
		// visible before it appears - it would be better to pop this up first, then
		// insert the exploringpage underneath.
		void ListMPsFindFirstIfNotAlreadyKnown(ExploringPage mpsExploringPage)
		{
			var thisParticipant = readingContext.ThisParticipant;
			
			if (thisParticipant == null || ! thisParticipant.MPsKnown)
			{
				var registrationPage = new RegisterPage2(readingContext.ThisParticipant, false, mpsExploringPage);
				
				Navigation.PushAsync(registrationPage);
			}
			else
			{
				Navigation.PushAsync(mpsExploringPage);
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

			var allMPsAsEntities = new ObservableCollection<Entity>(BackgroundElectorateAndMPData.AllMPs); 
			ExploringPageWithSearch mpsPage 
				= new ExploringPageWithSearch(allMPsAsEntities, readingContext.Filters.SelectedAskingMPs, "Here is the complete list of MPs");
			await Navigation.PushAsync(mpsPage);
		}

		private async void OnAnswerByOtherMPButtonClicked(object sender, EventArgs e)
		{
			var allMPsAsEntities = new ObservableCollection<Entity>(BackgroundElectorateAndMPData.AllMPs); 
			ExploringPageWithSearch mpsPage 
				= new ExploringPageWithSearch(allMPsAsEntities, readingContext.Filters.SelectedAnsweringMPs, "Here is the complete list of MPs");
			await Navigation.PushAsync(mpsPage);
		}
	}
}

