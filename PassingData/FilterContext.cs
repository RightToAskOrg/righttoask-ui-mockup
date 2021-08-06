using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PassingData
{
	public class FilterContext : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;
        
        // This function allows for automatic UI updates when these properties change.
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
		private string filterKeyword;
		// private string matchingQuestions;
		
		// Things about the current search, draft question or other action.
		// public string SelectedDepartment { get; set; }
		
		// Existing things about the world.
		// TODO: at the moment, the list of 'my MPs' is the 
		// same as the original complete set of MPs.
		// This is initiated with a default list of MPs,
		// from which at the moment you select the ones
		// that are yours
		// public ObservableCollection<Tag> MyMPs { get; set; }

		// public ObservableCollection<Tag> Departments { get; set; }
		
		// public ObservableCollection<Tag> OtherAuthorities { get; set; }

		// At the moment, this simply populates the reading context with a
		// hardcoded set of "existing" questions, authorities, etc.
		/*
		public void InitializeDefaultSetup()
		{
			matchingQuestions = 4782;
			
			Departments = new ObservableCollection<Tag>();

			MyMPs = new ObservableCollection<Tag>();
			
        	OtherAuthorities = new ObservableCollection<Tag>();
		}
		*/
		
		/*
		public override string ToString ()
		{
			return "Keyword: " + SearchKeyword + '\n' +
			       "TopTen: " + TopTen.ToString() + '\n' +
			       "Direct Committee: " + GoDirect_Committee + '\n' +
			       "Direct MP: " + GoDirect_MP + '\n' +
			       "My MPs" + (MPsKnown ? "" : " not" + " selected") + '\n' + 
			       "Question: " + DraftQuestion + '\n' +
			       "Number of matching questions: " + MatchingQuestions.ToString() + '\n' +
			       "Selected Department: " + SelectedDepartment + '\n' +
			       "Departments: " + Departments.ToString() + '\n' +
			       "Other Authorities: " + OtherAuthorities.ToString() + '\n';
		}
		*/
		
		// These functions allow automatic UI updates when these values change.
		public string FilterKeyword
		{
			get { return filterKeyword; }
			set
			{
				filterKeyword = value;
				OnPropertyChanged("FilterKeyword");
			}
		}
	}
}
