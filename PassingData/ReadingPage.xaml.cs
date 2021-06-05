using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace PassingData
{
	public partial class ReadingPage : ContentPage
	{
		private string draftQuestion;
		private ObservableCollection<Question> questions = new ObservableCollection<Question>();
		public ObservableCollection<Question> Questions
		{
			get { return questions; }
		}
		public ReadingPage ()
		{
			InitializeComponent ();

			QuestionListView.ItemsSource = questions;
			
			questions.Add(new Question{QuestionText   = "This is a test question", QuestionAsker = "Alice", DownVotes = 1, UpVotes = 2});
			questions.Add(new Question{QuestionText   = "This is a another test question", QuestionAsker = "Bob", DownVotes = 3, UpVotes = 1});
			questions.Add(new Question{QuestionText   = "This is an interesting question", QuestionAsker = "Chloe", DownVotes = 1, UpVotes = 2});
			questions.Add(new Question{QuestionText   = "This is a test question", QuestionAsker = "Darius", DownVotes = 1, UpVotes = 2});
			
		}
		void Question_Entered(object sender, EventArgs e)
		{
			draftQuestion = ((Editor) sender).Text;
		}
		void OnSaveButtonClicked (object sender, EventArgs e)
		{
			// Note that this doesn't really save the question (yet)
			// It just updates the question list with some things like the draft q'n.
			questions.Insert(0,new Question{QuestionText = "Another question just like "+draftQuestion, QuestionAsker="Eli", DownVotes = 1, UpVotes = 4});
		}
	}
}