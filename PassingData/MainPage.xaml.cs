using System;
using Xamarin.Forms;

namespace PassingData
{
	public partial class MainPage : ContentPage
	{
		public ReadingContext readingContext;
		public MainPage (string date)
		{
			InitializeComponent();

			readingContext = new ReadingContext { };
			readingContext.InitializeDefaultSetup();
		}

		async void OnTop10NowButtonClicked(object sender, EventArgs e)
		{
			readingContext.TopTen = true;

			var readingPage = new ReadingPage (true);
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
		}
		async void OnReadByKeywordFieldCompleted(object sender, EventArgs e)
		{
			readingContext.SearchKeyword = ((Entry)sender).Text;

			var readingPage = new ReadingPage (true);
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
			
		}
		async void OnNavigateButtonClicked (object sender, EventArgs e)
		{
			var secondPage = new SecondPage (readingContext.MPsSelected, false);
			secondPage.BindingContext = readingContext;
			await Navigation.PushAsync (secondPage);
		}
		
		async void OnReadButtonClicked (object sender, EventArgs e)
		{
			// ((Button) sender).Text = "This will take you to a reading page";
			var secondPage = new SecondPage (readingContext.MPsSelected, true);
			secondPage.BindingContext = readingContext;
			await Navigation.PushAsync (secondPage);
		}

		
		void OnRegisterButtonClicked(object sender, EventArgs e)
		{
			((Button) sender).Text = "Registering not implemented yet";
		}
	}
}
