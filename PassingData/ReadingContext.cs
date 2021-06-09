using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace PassingData
{
	public class ReadingContext
	{
		public string SearchKeyword { get; set; }

		public bool TopTen { get; set; }

		public string Committee { get; set; }
		
		public string MP { get; set; }
		
		public ObservableCollection<Question> ExistingQuestions { get; set; }
		public string DraftQuestion { get; set; }
		
		public ObservableCollection<Tag> Departments { get; set; }
		public String SelectedDepartment { get; set; }
		
		public ObservableCollection<Tag> OtherAuthorities { get; set; }
		
		public int MatchingQuestions { get; set; }

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

			ExistingQuestions = new ObservableCollection<Question>();
			ExistingQuestions.Add(new Question{QuestionText   = "This is a test question", QuestionAsker = "Alice", DownVotes = 1, UpVotes = 2});
			ExistingQuestions.Add(new Question{QuestionText   = "This is a another test question", QuestionAsker = "Bob", DownVotes = 3, UpVotes = 1});
			ExistingQuestions.Add(new Question{QuestionText   = "This is an interesting question", QuestionAsker = "Chloe", DownVotes = 1, UpVotes = 2});
			ExistingQuestions.Add(new Question{QuestionText   = "This is a test question", QuestionAsker = "Darius", DownVotes = 1, UpVotes = 2});
			
        	OtherAuthorities = new ObservableCollection<Tag>();
        	OtherAuthorities.Add(new Tag{TagLabel = "Australian Electoral Commission", Selected = true});
        	OtherAuthorities.Add(new Tag{TagLabel = "Digital Transformation Authority", Selected = true});
        	OtherAuthorities.Add(new Tag{TagLabel = "Office of the Australian Information Commissioner", Selected =false});
        	OtherAuthorities.Add(new Tag{TagLabel = "Australian Taxation Office", Selected =false});
		}
		public override string ToString ()
		{
			return "Keyword: " + SearchKeyword + '\n' +
			       "TopTen: " + TopTen.ToString() + '\n' +
			       "Committee: " + Committee + '\n' +
			       "MP: " + MP + '\n' +
			       "Question: " + DraftQuestion + '\n' +
			       "Number of matching questions: " + MatchingQuestions.ToString() + '\n' +
			       "Selected Department: " + SelectedDepartment + '\n' +
			       "Departments: " + Departments.ToString() + '\n' +
			       "Other Authorities: " + OtherAuthorities.ToString() + '\n';
		}
	}
}
