using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PassingData
{
	public class ReadingContext : INotifyPropertyChanged
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
        
		// Things about this user.
		// These selections are made at registration, or at 'complete registration.'
		private string username;
		private string userEmail;
		public bool Is_Registered { get; set; }
		// Note that people might register without knowing their electorate,
		// or might record state but not federal electorates, or vice versa.
		public bool State_Electorate_Known { get; set; }
		public bool Federal_Electorate_Known { get; set; }
		
		private string address;
		private string selectedStateOrTerritory;
		private string selectedStateElectorate;
		private string selectedFederalElectorate;
		
		
		// Things about the current search, draft question or other action.
		public string DraftQuestion { get; set; }
		public string SelectedDepartment { get; set; }
		public string SearchKeyword { get; set; }
		
		// Whether MPs have been selected for this question.
		// TODO perhaps we need a different bool for whether they've
		// been selected for asking or answering.
		public bool MPsSelected = false;

		// Whether this is a 'top ten' search.
		public bool TopTen { get; set; }

		// The number of questions 'matching' this query. 
		// At the moment, just a hardcoded value.
		public int MatchingQuestions { get; set; }
		public string GoDirect_Committee { get; set; }
		
		public string GoDirect_MP { get; set; }

		// Existing things about the world.
		// TODO: at the moment, the list of 'my MPs' is the 
		// same as the original complete set of MPs.
		// This is initiated with a default list of MPs,
		// from which at the moment you select the ones
		// that are yours
		public ObservableCollection<Tag> MyMPs { get; set; }

		public ObservableCollection<Question> ExistingQuestions { get; set; }
		
		public ObservableCollection<Tag> Departments { get; set; }
		
		public ObservableCollection<Tag> OtherAuthorities { get; set; }

		public ObservableCollection<Tag> StatesOrTerritories { get; set; }
		public ObservableCollection<Tag> StateElectorates { get; set; }
		public ObservableCollection<Tag> FederalElectorates { get; set; }
		


		// At the moment, this simply populates the reading context with a
		// hardcoded set of "existing" questions, authorities, etc.
		public void InitializeDefaultSetup()
		{
			MatchingQuestions= 4782;
			
			Departments = new ObservableCollection<Tag>();
			Departments.Add(new Tag{TagLabel = "Environment", Selected = false});
			Departments.Add(new Tag{TagLabel = "Home Affairs", Selected = false});
			Departments.Add(new Tag{TagLabel = "Defence", Selected = false});
			Departments.Add(new Tag{TagLabel = "Health", Selected = false});
			Departments.Add(new Tag{TagLabel = "Treasury", Selected = false});
			Departments.Add(new Tag{TagLabel = "Human Services", Selected = false});
			Departments.Add(new Tag{TagLabel = "Innovation, Industry and Science", Selected = false});
			Departments.Add(new Tag{TagLabel = "Communications", Selected = false});

			MyMPs = new ObservableCollection<Tag>();
			MyMPs.Add(new Tag{TagLabel = "Janet Rice", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Danny O'Brien", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Peter Dutton", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Penny Wong", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Daniel Andrews", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Ged Kearney", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Michael McCormack", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Mark Dreyfus", Selected = false});
			MyMPs.Add(new Tag{TagLabel = "Michaelia Cash", Selected = false});

			
			ExistingQuestions = new ObservableCollection<Question>();
			ExistingQuestions.Add(
				new Question
				{
					QuestionText   = "What is the error rate of the Senate Scanning solution?", 
					QuestionSuggester = "Alice", 
					QuestionAsker = "",
					DownVotes = 1, 
					UpVotes = 2
				});
			ExistingQuestions.Add(
				new Question
				{
					QuestionText   = "What is the monthly payment to AWS for COVIDSafe?", 
					QuestionSuggester = "Bob", 
					QuestionAsker = "",
					DownVotes = 3, 
					UpVotes = 1
				});
			ExistingQuestions.Add(
				new Question
				{
					QuestionText   = "Why did the ABC decide against an opt-in consent model for data sharing with Facebook and Google?", 
					QuestionSuggester = "Chloe", 
					QuestionAsker = "",
					DownVotes = 1, 
					UpVotes = 2
				});
			ExistingQuestions.Add(
				new Question
				{
					QuestionText   = "What is the government's position on the right of school children to strike for climate?", 
					QuestionSuggester = "Darius", 
					DownVotes = 1, 
					UpVotes = 2
				});
			
        	OtherAuthorities = new ObservableCollection<Tag>();
        	OtherAuthorities.Add(new Tag{TagLabel = "Australian Electoral Commission (AEC)", Selected = false});
        	OtherAuthorities.Add(new Tag{TagLabel = "Digital Transformation Authority (DTA)", Selected = false});
        	OtherAuthorities.Add(new Tag{TagLabel = "Office of the Australian Information Commissioner (OAIC)", Selected =false});
        	OtherAuthorities.Add(new Tag{TagLabel = "Australian Security Intelligence Organisation (ASIO)", Selected =false});
        	OtherAuthorities.Add(new Tag{TagLabel = "Australian Taxation Office (ATO)", Selected =false});
            
        	StatesOrTerritories = new ObservableCollection<Tag>();
        	StatesOrTerritories.Add(new Tag{TagLabel = "ACT", Selected = false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "Queensland", Selected = false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "New South Wales", Selected = false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "NT", Selected = false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "South Australia", Selected = false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "Tasmania", Selected = false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "Victoria", Selected =false});
        	StatesOrTerritories.Add(new Tag{TagLabel = "Western Australia", Selected =false});
            
        	StateElectorates = new ObservableCollection<Tag>();
        	StateElectorates.Add(new Tag{TagLabel = "Gippsland South", Selected = false});
        	StateElectorates.Add(new Tag{TagLabel = "Gembrook", Selected = false});
        	StateElectorates.Add(new Tag{TagLabel = "Nepean", Selected = false});
        	StateElectorates.Add(new Tag{TagLabel = "Sunbury", Selected = false});
        	StateElectorates.Add(new Tag{TagLabel = "Brighton", Selected = false});
        	StateElectorates.Add(new Tag{TagLabel = "Eildon", Selected = false});
        	StateElectorates.Add(new Tag{TagLabel = "Ovens Valley", Selected =false});
        	StateElectorates.Add(new Tag{TagLabel = "Malvern", Selected =false});
        	StateElectorates.Add(new Tag{TagLabel = "Northcote", Selected =false});
            
        	FederalElectorates = new ObservableCollection<Tag>();
        	FederalElectorates.Add(new Tag{TagLabel = "Cooper", Selected = false});
        	FederalElectorates.Add(new Tag{TagLabel = "Higgins", Selected = false});
        	FederalElectorates.Add(new Tag{TagLabel = "Flinders", Selected = false});
        	FederalElectorates.Add(new Tag{TagLabel = "Isaacs", Selected = false});
        	FederalElectorates.Add(new Tag{TagLabel = "Melbourne", Selected = false});
        	FederalElectorates.Add(new Tag{TagLabel = "Mallee", Selected = false});
        	FederalElectorates.Add(new Tag{TagLabel = "Indi", Selected =false});
        	FederalElectorates.Add(new Tag{TagLabel = "Monash", Selected =false});
        	FederalElectorates.Add(new Tag{TagLabel = "Wills", Selected =false});
		}
		
		// TODO This ToString doesn't really properly convey the state of
		// the ReadingContext, e.g. doesn't reflect registering or knowing your
		// MPs.
		// And it probably isn't necessary to write out all the unselectd things.
		
		public override string ToString ()
		{
			return "Keyword: " + SearchKeyword + '\n' +
			       "TopTen: " + TopTen.ToString() + '\n' +
			       "Direct Committee: " + GoDirect_Committee + '\n' +
			       "Direct MP: " + GoDirect_MP + '\n' +
			       "My MPs" + (MPsSelected ? "" : " not" + " selected") + '\n' + 
			       "Question: " + DraftQuestion + '\n' +
			       "Number of matching questions: " + MatchingQuestions.ToString() + '\n' +
			       "Selected Department: " + SelectedDepartment + '\n' +
			       "Departments: " + Departments.ToString() + '\n' +
			       "Other Authorities: " + OtherAuthorities.ToString() + '\n';
		}

		// These functions allow automatic UI updates when these values change.
		public string SelectedStateOrTerritory 
        {
	        get
	        {
		        return selectedStateOrTerritory;
	        }
	        set
	        {
		        selectedStateOrTerritory = value;
		        OnPropertyChanged("SelectedStateOrTerritory");
	        }
        }
		public string SelectedStateElectorate 
		{
		    get
		    {
                return selectedStateElectorate;
            }
            set
            {
                selectedStateElectorate= value;
                OnPropertyChanged("SelectedStateElectorate");
            }
        }

		public string SelectedFederalElectorate
		{
			get { return selectedFederalElectorate; }
			set
			{
				selectedFederalElectorate = value;
				OnPropertyChanged("SelectedFederalElectorate");
			}
		}

		public string Username
		{
			get { return username; }
			set
			{
				username = value;
				OnPropertyChanged("Username");
			}
		}

		public string UserEmail
		{
			get { return userEmail; }
			set
			{
				userEmail = value;
				OnPropertyChanged("UserEmail");
			}
		}
		
		public string Address
		{
			get { return address; }
			set
			{
				address = value;
				OnPropertyChanged("Address");
			}
		}
	}
}
