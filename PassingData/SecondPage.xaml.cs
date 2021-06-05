using System;
using Xamarin.Forms;

namespace PassingData
{
	public partial class SecondPage : ContentPage
	{
		private string question;

		private string matchCountVar;
		//public string MatchCount
		//{
		//	get { return matchCountVar; }
		//	set
		//	{
		//		matchCountVar = value;
		//		OnPropertyChanged(nameof(matchCountVar)); // Notify that there was a change on this property	
		//	}
		//}
		public SecondPage ()
		{
			InitializeComponent ();
			//BindingContext = this;
			//matchCountVar = "Current matching questions: 4332";
		}

		void Question_Entered(object sender, EventArgs e)
		{
			question = ((Editor) sender).Text;
		}
		async void OnNavigateForwardButtonClicked (object sender, EventArgs e)
		{
			var readingContext = new ReadingContext{
				SearchKeyword = "",
				TopTen = false,
				DraftQuestion = question
			};
			// ((Button) sender).Text = $"Forward navigation isn't implemented yet.";
			var readingPage = new ReadingPage();
			
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
		}
		async void OnNavigateBackButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}

		void OnMinisterOrDeptButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Finding Minister or Dept not implemented yet";
		}

		private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Public Authority list not implemented yet";
		}

		private void OnMPAnswerButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"MP answering not implemented yet";
		}

		private void OnFindCommitteeButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"Finding Committees not implemented yet";	
		}

		private void OnMPRaiseButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = $"MP raising not implemented yet";
		}
	}
}

