using System;
using System.Collections.ObjectModel;
using System.Net.Mime;
using Xamarin.Forms;

namespace PassingData
{
	public partial class SecondPage : ContentPage
	{
		private string question;
		private ObservableCollection<Tag> departmentAuthorities;
		private ObservableCollection<Tag> otherAuthorities;

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
			var readingPage = new ReadingPage();
			
			readingPage.BindingContext = readingContext;
			await Navigation.PushAsync (readingPage);
		}
		async void OnNavigateBackButtonClicked (object sender, EventArgs e)
		{
			await Navigation.PopAsync ();
		}

		async void OnMinisterOrDeptButtonClicked(object sender, EventArgs e)
		{
			departmentAuthorities = new ObservableCollection<Tag>();
			departmentAuthorities.Add(new Tag{TagLabel = "Environment", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Home Affairs", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Defence", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Health", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Treasury", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Human Services", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Innovation, Industry and Science", Selected = false});
			departmentAuthorities.Add(new Tag{TagLabel = "Communications", Selected = false});
			
			var readingContext = new ReadingContext{
         				SearchKeyword = "",
         				TopTen = false,
                        Departments = departmentAuthorities
         			};
			
         	var departmentPickerPage = new PickerPage();
        	departmentPickerPage.BindingContext = readingContext;
         	//var departmentExploringPage = new ExploringPage();
        	//departmentExploringPage.BindingContext = readingContext;
        	await Navigation.PushAsync (departmentPickerPage);

            //if (readingContext.SelectedDepartment != null)
            //{
				((Button) sender).Text = readingContext.SelectedDepartment;
            //}
		}

		async private void OnOtherPublicAuthorityButtonClicked(object sender, EventArgs e)
		{
        	otherAuthorities = new ObservableCollection<Tag>();
        	otherAuthorities.Add(new Tag{TagLabel = "This is a test other authority", Selected = true});
        	otherAuthorities.Add(new Tag{TagLabel = "This is a different test other authority", Selected =false});
        	
        	var readingContext = new ReadingContext{
        				SearchKeyword = "",
          				TopTen = false,
                        Departments = otherAuthorities
           			};
         			
           	var departmentExploringPage = new ExploringPage();
            departmentExploringPage.BindingContext = readingContext;
           	await Navigation.PushAsync (departmentExploringPage);
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

