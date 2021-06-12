using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace PassingData
{
    public partial class QuestionDetailPage : ContentPage
    {
        private string linkOrAnswer;
        private Question question;
        public QuestionDetailPage (Question selectedQuestion)
        {
            question = selectedQuestion;
            InitializeComponent ();
            QuestionDetailView.Text = question.ToString();
            QuestionAskerButton.Text = "View " + question.QuestionAsker + "'s profile";
        }
        
        private void UpVoteButton_OnClicked(object sender, EventArgs e)
        {
            question.UpVotes++;
        }

        private async void QuestionAskerButton_OnClicked(object sender, EventArgs e)
        {
			var personProfilePage = new PersonProfilePage(question.QuestionAsker);
			personProfilePage.BindingContext = BindingContext;
			await Navigation.PushAsync (personProfilePage);
        }
        
        // I'm not actually sure what triggers the 'send' event here, and hence not sure
        // which of these two functions should be doing the saving.
		void Answer_Entered(object sender, EventArgs e)
		{
			question.LinkOrAnswer = ((Editor) sender).Text;
		}

        private void SaveAnswerButton_OnClicked(object sender, EventArgs e)
        {
            ((Xamarin.Forms.Button) sender).Text = "Answer saved";
        }
    }
}