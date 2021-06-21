using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Application = Xamarin.Forms.Application;

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
				navigateButton.IsVisible = false;
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
		void OnSaveButtonClicked (object sender, EventArgs e)
		{
			// Note that this doesn't really save the question (yet)
			// It just updates the question list with some things like the draft q'n.
			((ReadingContext) BindingContext).ExistingQuestions.Insert(0,
				new Question{QuestionText = "Another question like "+draftQuestion, 
					QuestionSuggester="Eli", DownVotes = 1, UpVotes = 4});
		}

		// Note: it's possible that this would be better with an ItemTapped event instead.
		private async void Question_Selected(object sender, ItemTappedEventArgs e)
		{
			var questionDetailPage = new QuestionDetailPage((Question) e.Item);
			questionDetailPage.BindingContext = BindingContext;
			await Navigation.PushAsync (questionDetailPage);
		}

		private void fillInSelectedAnswerers(ObservableCollection<Tag> authorities)
		{
			foreach (var authority in authorities)
			{
				if (authority.Selected)
				{
					selectedAuthorities += authority.TagLabel + ", ";
				}
			}


			AnsweredBySelections.Text = "or "
			                            + selectedAuthorities;
		}
	}
}