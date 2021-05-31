using System;
using Xamarin.Forms;

namespace PassingData
{
	public partial class MainPage : ContentPage
	{
		public MainPage (string date)
		{
			InitializeComponent();

		}

		async void OnTop10NowButtonClicked(object sender, EventArgs e)
		{
			var readingContext = new ReadingContext{
				SearchKeyword = "",
				TopTen = true 
			};

			var readingPage = new ReadingPage ();
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
			// ((Button) sender).Text = $"Top 10 now not implemented yet";
		}
		async void OnReadByKeywordFieldCompleted(object sender, EventArgs e)
		{
			var readingContext = new ReadingContext{
				SearchKeyword = ((Entry)sender).Text,
				TopTen = false
			};

			var readingPage = new ReadingPage ();
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
			
		}
		async void OnNavigateButtonClicked (object sender, EventArgs e)
		{
			var secondPage = new SecondPage ();
			// secondPage.BindingContext = readingContext;
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
