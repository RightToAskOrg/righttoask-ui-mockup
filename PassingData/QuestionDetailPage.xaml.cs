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
        
        public QuestionDetailPage (Question question)
        {
            InitializeComponent ();
            QuestionDetailView.Text = question.ToString();
        }
        
        //void LinkOrComment_Entered(object sender, EventArgs e)
        //{
        //    linkOrAnswer = ((Editor) sender).Text;
        //}
        //void OnSaveButtonClicked (object sender, EventArgs e)
        //{
            // Note that this doesn't really save the question (yet)
            // It just updates the question list with some things like the draft q'n.
            // ((ReadingContext) BindingContext).ExistingQuestions.Insert(0,new Question{QuestionText = "Another question just like "+draftQuestion, QuestionAsker="Eli", DownVotes = 1, UpVotes = 4});
        //}
    }
}