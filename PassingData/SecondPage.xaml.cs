using System;
using Xamarin.Forms;

namespace PassingData
{
	public partial class SecondPage : ContentPage
	{
		private string question;
		public SecondPage ()
		{
			InitializeComponent ();
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
	}
}

