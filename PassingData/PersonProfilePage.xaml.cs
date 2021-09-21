using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Application = Xamarin.Forms.Application;

namespace PassingData
{
    public partial class PersonProfilePage : ContentPage
    {
        public PersonProfilePage (string name) 
        {
            InitializeComponent ();
            DMButton.Text = "Send Direct Message to " + name;
            SeeQuestionsButton.Text = "Read questions from " + name;
            FollowButton.Text = "Follow " + name;
        }
        
        private void FollowButton_OnClicked(object sender, EventArgs e)
        {
            ((Xamarin.Forms.Button) sender).Text = "Following not implemented";
            
        }

        private void DMButton_OnClicked(object sender, EventArgs e)
        {
            ((Xamarin.Forms.Button) sender).Text = "DMs not implemented";
        }
        
        // At the moment, this pushes a brand new question-reading page,
        // which is meant to have only questions from this person, but
        // at the moment just has everything.
        // 
        // Think a bit harder about how people will navigate or understand this:
        // Will they expect to be adding a new stack layer, or popping off old ones?
        private async void SeeQuestionsButton_OnClicked(object sender, EventArgs e)
        {
			var readingPage = new ReadingPage(true, ((ReadingContext) BindingContext).SelectableAuthorities);
			readingPage.BindingContext = BindingContext;
			await Navigation.PushAsync (readingPage);
        }
    }
}