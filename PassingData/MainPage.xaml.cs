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

			var readingPage = new ReadingPage ();
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
		}
		async void OnReadByKeywordFieldCompleted(object sender, EventArgs e)
		{
			readingContext.SearchKeyword = ((Entry)sender).Text;

			var readingPage = new ReadingPage ();
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
			
		}
		async void OnNavigateButtonClicked (object sender, EventArgs e)
		{
			
			var secondPage = new SecondPage ();
			secondPage.BindingContext = readingContext;
			await Navigation.PushAsync (secondPage);
		}
		
		void OnCommitteeButtonClicked (object sender, EventArgs e)
		{
			((Button) sender).Text = $"Committee lists not implemented yet";
		}
		void OnMPButtonClicked (object sender, EventArgs e)
		{
			((Button) sender).Text = $"MP lists not implemented yet";
		}
	}
}
