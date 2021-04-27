using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PassingData
{
	public partial class ReadingPage : ContentPage
	{
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

		async void OnNavigateButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}
	}
}