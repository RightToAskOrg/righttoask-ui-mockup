using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
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
        private ReadingContext readingContext;
        public QuestionDetailPage (bool isNewQuestion, Question selectedQuestion, ReadingContext readingContext)
        {
            BindingContext = readingContext;
            this.readingContext = readingContext;
            
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
                QuestionSuggesterButton.Text = "Edit your profile";
            }
            else
            {
                BackgroundSegment.IsVisible = false;
                SaveBackgroundButton.IsVisible = false;
                QuestionSuggesterButton.Text = "View " + question.QuestionSuggester + "'s profile";
            }
            
        }
        
        private void UpVoteButton_OnClicked(object sender, EventArgs e)
        {
            question.UpVotes++;
        }

        // TODO: Present the UI more nicely here - this should happen if you click on the person's 
        // name, not with a separate button.
        private async void QuestionSuggesterButton_OnClicked(object sender, EventArgs e)
        {
			var personProfilePage = new PersonProfilePage(question.QuestionSuggester, readingContext);
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
        // TODO: Think about the flow in the case where you get the popup but then cancel/back
        // the registration screen. At the moment, it will just go back and (irritatingly)
        // give you the same options.
        async void SubmitNewQuestionButton_OnClicked(object sender, EventArgs e)
        {
            if (!readingContext.ThisParticipant.Is_Registered)
            {
                RegisterPage1 registrationPage = new RegisterPage1(readingContext);
                registrationPage.Disappearing += saveQuestion;
                Navigation.PushAsync(registrationPage);
            }
            else
            {
                saveQuestion(null, null);
            }
            
            
        }
        
        private async void saveQuestion(object sender, EventArgs e)
        {
            
        // Note the condition here is necessary because they might have been offered the chance to
        // register, but have declined.
        // Also note that setting QuestionSuggester may be unnecessary - it may already be set correctly -
        // but is needed if the person has just registered.
            if (readingContext.ThisParticipant.Is_Registered)
            {
                question.QuestionSuggester = readingContext.ThisParticipant.Username;
	            readingContext.ExistingQuestions.Insert(0, question);
                
                readingContext.DraftQuestion = null;
                
            }
            
            bool goHome = await DisplayAlert("Question published!", "", "Home", "Write another one");
                
            if (goHome)
            {
                await Navigation.PopToRootAsync();
            }
            else  // Pop back to readingpage. TODO: fix the context so that it doesn't think you're drafting
                // a question.  Possibly the right thing to do is pop everything and then push a reading page.
            {
                await Navigation.PopAsync();
            }
        }

        private void Background_Entered(object sender, EventArgs e)
        {
            // Do nothing.
        }
    }
}