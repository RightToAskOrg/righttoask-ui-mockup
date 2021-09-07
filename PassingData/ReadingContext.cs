using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Reflection;
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
		public Entity SelectedDepartment { get; set; }
		public string SearchKeyword { get; set; }
		
		// Whether MPs have been selected for this question.
		// TODO perhaps we need a different bool for whether they've
		// been selected for asking or answering.
		public bool MPsKnown = false;

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
			MatchingQuestions = 4782;

			Departments = new ObservableCollection<Tag>();
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Environment" }, Selected = false });
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Home Affairs" }, Selected = false });
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Defence" }, Selected = false });
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Health" }, Selected = false });
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Treasury" }, Selected = false });
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Human Services" }, Selected = false });
			Departments.Add(new Tag
				{ TagEntity = new Entity { EntityName = "Innovation, Industry and Science" }, Selected = false });
			Departments.Add(new Tag { TagEntity = new Entity { EntityName = "Communications" }, Selected = false });

			MyMPs = new ObservableCollection<Tag>();
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Janet Rice" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Danny O'Brien" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Peter Dutton" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Penny Wong" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Daniel Andrews" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Ged Kearney" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Michael McCormack" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Mark Dreyfus" }, Selected = false });
			MyMPs.Add(new Tag { TagEntity = new Entity { EntityName = "Michaelia Cash" }, Selected = false });

			ExistingQuestions = new ObservableCollection<Question>();
			ExistingQuestions.Add(
				new Question
				{
					QuestionText = "What is the error rate of the Senate Scanning solution?",
					QuestionSuggester = "Alice",
					QuestionAsker = "",
					DownVotes = 1,
					UpVotes = 2
				});
			ExistingQuestions.Add(
				new Question
				{
					QuestionText = "What is the monthly payment to AWS for COVIDSafe?",
					QuestionSuggester = "Bob",
					QuestionAsker = "",
					DownVotes = 3,
					UpVotes = 1
				});
			ExistingQuestions.Add(
				new Question
				{
					QuestionText =
						"Why did the ABC decide against an opt-in consent model for data sharing with Facebook and Google?",
					QuestionSuggester = "Chloe",
					QuestionAsker = "",
					DownVotes = 1,
					UpVotes = 2
				});
			ExistingQuestions.Add(
				new Question
				{
					QuestionText =
						"What is the government's position on the right of school children to strike for climate?",
					QuestionSuggester = "Darius",
					DownVotes = 1,
					UpVotes = 2
				});

			readAuthoritiesFromCSV();
			/*
			OtherAuthorities = new ObservableCollection<Tag>();
			OtherAuthorities.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Australian Electoral Commission", NickName = "AEC" },
				Selected = false
			});
			OtherAuthorities.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Digital Transformation Authority", NickName = "DTA" },
				Selected = false
			});
			OtherAuthorities.Add(new Tag
			{
				TagEntity = new Entity
					{ EntityName = "Office of the Australian Information Commissioner", NickName = "OAIC" },
				Selected = false
			});
			OtherAuthorities.Add(new Tag
			{
				TagEntity = new Entity
					{ EntityName = "Australian Security Intelligence Organisation", NickName = "ASIO" },
				Selected = false
			});
			OtherAuthorities.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Australian Taxation Office", NickName = "ATO" }, Selected = false
			});
			*/

			StatesOrTerritories = new ObservableCollection<Tag>();
			StatesOrTerritories.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Australian Capital Territory", NickName = "ACT" },
				Selected = false
			});
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "Queensland", NickName = "Qld" }, Selected = false });
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "New South Wales", NickName = "NSW" }, Selected = false });
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "Northern Territory", NickName = "NT" }, Selected = false });
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "South Australia", NickName = "SA" }, Selected = false });
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "Tasmania", NickName = "Tas" }, Selected = false });
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "Victoria", NickName = "Vic" }, Selected = false });
			StatesOrTerritories.Add(new Tag
				{ TagEntity = new Entity { EntityName = "Western Australia", NickName = "WA" }, Selected = false });

			StateElectorates = new ObservableCollection<Tag>();
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Gembrook"}, 
				Selected = false 
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity{EntityName = "Nepean"}, 
				Selected = false
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Sunbury"}, 
				Selected = false 
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity{EntityName = "Brighton"}, 
				Selected = false
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Eildon"}, 
				Selected = false 
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity{EntityName = "Ovens Valley"}, 
				Selected = false
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Malvern"}, 
				Selected = false
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity{EntityName = "Northcote"}, 
				Selected = false
			});
			StateElectorates.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Gippsland South"}, 
				Selected = false
			});

			FederalElectorates = new ObservableCollection<Tag>();
			FederalElectorates.Add(new Tag
			{
				TagEntity = new Entity { EntityName = "Cooper" },
				Selected = false
			});
			FederalElectorates.Add( new Tag
			{
				TagEntity = new Entity { EntityName = "Higgins" } ,
				Selected = false
			});
			FederalElectorates.Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Flinders" },
				Selected = false 
			});
			FederalElectorates.Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Isaacs" } ,
				Selected = false
			});
			FederalElectorates.Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Melbourne" },
				Selected = false
			});
			FederalElectorates.Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Mallee"} ,
				Selected = false
			});
			FederalElectorates . Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Indi"} ,
				Selected = false
			});
			FederalElectorates . Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Monash"} ,
				Selected = false
			});
			FederalElectorates . Add ( new Tag
			{
				TagEntity = new Entity { EntityName = "Wills"},
				Selected = false
			});
		}

		private void readAuthoritiesFromCSV()
		{
			OtherAuthorities = new ObservableCollection<Tag>();
			try
			{
				Console.WriteLine("Trying to read the CSV file:");
				// Open the text file using a stream reader.
				var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ReadingContext)).Assembly;
				Stream stream = assembly.GetManifestResourceStream("PassingData.Resources.all-authorities.csv");
				using (var sr = new StreamReader(stream))
				// using (var sr = new StreamReader("TestFile.txt"))
				{
					// Read the first line, which just has headings we can ignore.
					sr.ReadLine();
					// Read the stream as a string, and write the string to the console.
					Console.WriteLine("Here is the first line of the CSV file:");
					Console.WriteLine(sr.ReadLine());
					// DisplayAlert("Read the CSV file", sr.ReadLine(), "OK");
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("Authorities file could not be read:");
				Console.WriteLine(e.Message);
			}
			
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
			       "My MPs" + (MPsKnown ? "" : " not" + " selected") + '\n' + 
			       "Question: " + DraftQuestion + '\n' +
			       "Number of matching questions: " + MatchingQuestions.ToString() + '\n' +
			       "Selected Department: " + SelectedDepartment.EntityName + '\n' +
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
