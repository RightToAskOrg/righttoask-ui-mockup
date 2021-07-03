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
		private string selectedAuthorities = "";
		//private ObservableCollection<Question> questions = new ObservableCollection<Question>();
		// public ObservableCollection<Question> Questions
		// {
			// get { return questions; }
		// }
		public ReadingPage (bool isReadingOnly, ObservableCollection<Tag> authorities)
		{
			InitializeComponent ();

			if (isReadingOnly)
			{
				TitleBar.Title = "Read Questions";
				QuestionDraftingBox.IsVisible = false;
				keepButton.IsVisible = false;
				discardButton.IsVisible = false;
			}
			else
			{
				TitleBar.Title = "Direct your question";
			}

			fillInSelectedAnswerers(authorities);

			// QuestionListView.ItemsSource = questions;
			// QuestionListView.ItemsSource = ((ReadingContext) BindingContext).ExistingQuestions;

		}
		void Question_Entered(object sender, EventArgs e)
		{
			draftQuestion = ((Editor) sender).Text;
			((ReadingContext) BindingContext).DraftQuestion = draftQuestion;
		}

		// Note: it's possible that this would be better with an ItemTapped event instead.
		private async void Question_Selected(object sender, ItemTappedEventArgs e)
		{
			var questionDetailPage = new QuestionDetailPage(false, (Question) e.Item);
			questionDetailPage.BindingContext = BindingContext;
			await Navigation.PushAsync(questionDetailPage);
		}

		private void fillInSelectedAnswerers(ObservableCollection<Tag> authorities)
		{
			foreach (var authority in authorities)
			{
				if (authority.Selected)
				{
					selectedAuthorities += authority.TagLabel + "\n";
				}
			}


			AnsweredBySelections.Text = "or "
			                            + selectedAuthorities;
		}

		private void OnDiscardButtonClicked(object sender, EventArgs e)
		{
			// throw new NotImplementedException();
			((Button) sender).Text = "Draft Discarded";
			((ReadingContext) BindingContext).DraftQuestion = "";
			keepButton.IsVisible = false;
		}


    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        ReadingContext context = (ReadingContext) BindingContext;

        // Tag the new question with the authorities that have been selected.
        ObservableCollection<string> questionAnswerers; 
	    questionAnswerers = new ObservableCollection<string>(context.OtherAuthorities.Where(w => w.Selected).Select(a=> a.TagLabel));
	    if (context.SelectedDepartment != null)
	       questionAnswerers.Insert(0, context.SelectedDepartment);
	    
	    Question newQuestion = new Question {
    			QuestionText = draftQuestion,
                // TODO: Enforce registration before question-suggesting.
    			QuestionSuggester = context.Is_Registered ? context.Username : "Anonymous user", 
                QuestionAnswerers = questionAnswerers, 
                // TODO: set this.
                // QuestionAsker = ...;  
                DownVotes = 0, 
                UpVotes = 0
    		};

	        
		var questionDetailPage = new QuestionDetailPage(true, newQuestion);
		questionDetailPage.BindingContext = BindingContext;
		await Navigation.PushAsync (questionDetailPage);
    }
}
}