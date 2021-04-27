namespace PassingData
{
    public class Question
    {
        public string QuestionText { get; set; }
        
        public string QuestionAsker { get; set; }
        
        public int UpVotes { get; set; }
        
        public int DownVotes { get; set; }
        
        public override string ToString ()
        {
            return QuestionText+ "\n" +
                   "Asked by: " + QuestionAsker + '\n' +
                   "UpVotes: " + UpVotes+ '\n' +
                   "DownVotes: " + DownVotes ;
        }
    }
}