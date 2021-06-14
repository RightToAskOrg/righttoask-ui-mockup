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
		//private ObservableCollection<Question> questions = new ObservableCollection<Question>();
		// public ObservableCollection<Question> Questions
		// {
			// get { return questions; }
		// }
		public ReadingPage (bool isReadingOnly)
		{
			InitializeComponent ();

			if (isReadingOnly)
			{
				TitleBar.Title = "Read Questions";
			}
			else
			{
				TitleBar.Title = "Direct your question";
			}

			// QuestionListView.ItemsSource = questions;
			// QuestionListView.ItemsSource = ((ReadingContext) BindingContext).ExistingQuestions;

		}
		void Question_Entered(object sender, EventArgs e)
		{
			draftQuestion = ((Editor) sender).Text;
		}
		void OnSaveButtonClicked (object sender, EventArgs e)
		{
			// Note that this doesn't really save the question (yet)
			// It just updates the question list with some things like the draft q'n.
			((ReadingContext) BindingContext).ExistingQuestions.Insert(0,new Question{QuestionText = "Another question just like "+draftQuestion, QuestionAsker="Eli", DownVotes = 1, UpVotes = 4});
		}

		// Note: it's possible that this would be better with an ItemTapped event instead.
		private async void Question_Selected(object sender, ItemTappedEventArgs e)
		{
			var questionDetailPage = new QuestionDetailPage((Question) e.Item);
			questionDetailPage.BindingContext = BindingContext;
			await Navigation.PushAsync (questionDetailPage);
		}
	}
}