﻿using System;
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

			var readingPage = new ReadingPage (true, readingContext.OtherAuthorities);
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
		}
		
		// If either 'enter' is pressed after a keyword change, or the 
		// 'search by keyword' button is pressed, launch the reading page.
		// Otherwise, if only the keyword is changed, update it but don't
		// launch a new page.
		async void OnReadByKeywordFieldCompleted(object sender, EventArgs e)
		{
			readingContext.SearchKeyword = ((SearchBar)sender).Text;
			launchKeywordReadingPage();
		}
		
		private void OnKeywordChanged(object sender, TextChangedEventArgs e)
		{
			readingContext.SearchKeyword = e.NewTextValue;
		}

		async void launchKeywordReadingPage()
		{
			var readingPage = new ReadingPage (true, readingContext.OtherAuthorities);
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync(readingPage);
		}
		async void OnNavigateButtonClicked (object sender, EventArgs e)
		{
			var secondPage = new SecondPage (readingContext.MPsSelected, false);
			secondPage.BindingContext = readingContext;
			await Navigation.PushAsync (secondPage);
		}
		
		async void OnReadButtonClicked(object sender, EventArgs e)
		{
			// ((Button) sender).Text = "This will take you to a reading page";
			var secondPage = new SecondPage (readingContext.MPsSelected, true);
			secondPage.BindingContext = readingContext;
			await Navigation.PushAsync(secondPage);
		}

		
		async void OnRegisterButtonClicked(object sender, EventArgs e)
		{
			// ((Button) sender).Text = "Registering not implemented yet";
			var registrationPage = new RegisterPage1(readingContext);
			await Navigation.PushAsync(registrationPage);
		}

		async void OnAnsweredByMPButtonClicked(object sender, EventArgs e)
		{
			// ((Button) sender).Text = "Not implemented yet";
			var registrationPage = new RegisterPage2(readingContext);
			await Navigation.PushAsync(registrationPage);
		}
	}
}
