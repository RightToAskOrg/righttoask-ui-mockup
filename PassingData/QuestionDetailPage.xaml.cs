using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;
using Button = Xamarin.Forms.Button;

namespace PassingData
{
    public partial class QuestionDetailPage : ContentPage
    {
        private string linkOrAnswer;
        private Question question;
        public QuestionDetailPage (bool isNewQuestion, Question selectedQuestion)
        {
            question = selectedQuestion;
            InitializeComponent ();
            QuestionDetailView.Text = question.ToString();
            // Different actions depending on whether it's a new question you're about to submit,
            // or an existing question you're answering, upvoting or adding links for.
            if (isNewQuestion)
            {
                UpVoteButton.IsVisible = false;
                LinkOrAnswerSegment.IsVisible = false;
                SaveAnswerButton.IsVisible = false;
            }
            else
            {
                BackgroundSegment.IsVisible = false;
                SaveBackgroundButton.IsVisible = false;
            }
            
            QuestionSuggesterButton.Text = "View " + question.QuestionSuggester + "'s profile";
        }
        
        private void UpVoteButton_OnClicked(object sender, EventArgs e)
        {
            question.UpVotes++;
        }

        // TODO: Present the UI more nicely here - this should happen if you click on the person's 
        // name, not with a separate button.
        private async void QuestionSuggesterButton_OnClicked(object sender, EventArgs e)
        {
			var personProfilePage = new PersonProfilePage(question.QuestionSuggester);
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
            ((Button) sender).Text = "Answer saving not implemented";
        }

        // TODO: Re-enable button if you choose to draft another question.
        private void SubmitNewQuestionButton_OnClicked(object sender, EventArgs e)
        {
	        ((ReadingContext) BindingContext).ExistingQuestions.Insert(0, question);
            ((Button) sender).Text = "Submitted!";
            ((Button) sender).IsEnabled = false;
        }

        private void Background_Entered(object sender, EventArgs e)
        {
            // Do nothing.
        }
    }
}