namespace PassingData
{
	public class ReadingContext
	{
		public string SearchKeyword { get; set; }

		public bool TopTen { get; set; }

		public string Committee { get; set; }
		
		public string MP { get; set; }
		
		public string DraftQuestion { get; set; }	
		
		public int MatchingQuestions { get; set; }
		
		public override string ToString ()
		{
			return "Keyword: " + SearchKeyword + '\n' +
			       "TopTen: " + TopTen.ToString() + '\n' +
			       "Committee: " + Committee + '\n' +
			       "MP: " + MP + '\n' +
			       "Question: " + DraftQuestion + '\n' +
			       "Number of matching questions: " + MatchingQuestions.ToString() + '\n';
		}
	}
}
