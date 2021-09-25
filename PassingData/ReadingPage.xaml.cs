using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;
using Button = Xamarin.Forms.Button;

namespace PassingData
{
	public partial class ReadingPage : ContentPage
	{
		private string draftQuestion;
		private ReadingContext readingContext;

		private string selectedAuthorities = ""; 

		public ReadingPage(bool isReadingOnly, ObservableCollection<Tag> authorities, ReadingContext readingContext)
		{
			InitializeComponent();
			this.readingContext = readingContext;
			BindingContext = readingContext;

			if (isReadingOnly)
			{
				TitleBar.Title = "Read Questions";
				QuestionDraftingBox.IsVisible = false;
				keepButton.IsVisible = false;
				discardButton.IsVisible = false;
			}
			else
			{
				TitleBar.Title = "Similar questions";
				finishedReadingButton.IsVisible = false;
			}

			FillInSelectedAnswerers(authorities);

		}

		void Question_Entered(object sender, EventArgs e)
		{
			draftQuestion = ((Editor) sender).Text;
			((ReadingContext) BindingContext).DraftQuestion = draftQuestion;
		}

		// Note: it's possible that this would be better with an ItemTapped event instead.
		private async void Question_Selected(object sender, ItemTappedEventArgs e)
		{
			var questionDetailPage 
				= new QuestionDetailPage(false, (Question) e.Item, readingContext);
			await Navigation.PushAsync(questionDetailPage);
		}

		private void FillInSelectedAnswerers(ObservableCollection<Tag> authorities)
		{
			foreach (var authority in authorities)
			{
				if (authority.Selected)
				{
					selectedAuthorities += authority.TagEntity.NickName + "\n";
				}
			}


			AnsweredBySelections.Text = selectedAuthorities;
		}

		async void OnDiscardButtonClicked(object sender, EventArgs e)
		{
            bool goHome = await DisplayAlert("Draft discarded", "", "Home", "Related questions");
            readingContext.DraftQuestion = null;
            if (goHome)
            {
                await Navigation.PopToRootAsync();
            }

            ((Button) sender).Text = "Draft Discarded";
			// ((ReadingContext) BindingContext).DraftQuestion = "";
			keepButton.IsVisible = false;
		}


		async void OnSaveButtonClicked(object sender, EventArgs e)
		{
			// Tag the new question with the authorities that have been selected.
			ObservableCollection<Entity> questionAnswerers;
			questionAnswerers =
				new ObservableCollection<Entity>(
					readingContext.SelectableAuthorities.Where(w => w.Selected).Select(a => a.TagEntity));
			if (readingContext.SelectedDepartment != null)
				questionAnswerers.Insert(0, readingContext.SelectedDepartment);

			IndividualParticipant thisParticipant = readingContext.ThisParticipant;
			Question newQuestion = new Question
			{
				QuestionText = readingContext.DraftQuestion,
				// TODO: Enforce registration before question-suggesting.
				QuestionSuggester 
					= (thisParticipant != null && thisParticipant.Is_Registered) ? thisParticipant.Username : "Anonymous user",
				QuestionAnswerers = questionAnswerers,
				// TODO: set this.
				// QuestionAsker = ...;  
				DownVotes = 0,
				UpVotes = 0
			};


			var questionDetailPage = new QuestionDetailPage(true, newQuestion, readingContext);
			await Navigation.PushAsync(questionDetailPage);
		}

		async void OnFinishedReadingButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PopAsync();
		}

		private void OnUpVoteButtonClicked(object sender, EventArgs e)
		{
			bool upVoteMode;
			string upVoteMessage = "+1";
			string undoMessage = "Undo upvote";
			Question q = (Question)((Button)sender).BindingContext;

			upVoteMode = !((Button)sender).Text.Equals(undoMessage);

			if (upVoteMode)
			{
			    q.UpVotes++;
			    ((Button)sender).Text = undoMessage;
			}
			else
			{
				q.UpVotes--;
			    ((Button)sender).Text = upVoteMessage;
			}
		}
	}
}