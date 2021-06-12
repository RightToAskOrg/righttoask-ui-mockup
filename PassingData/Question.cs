using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PassingData
{
    public class Question : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string QuestionText { get; set; }
        
        public string QuestionAsker { get; set; }

        public string LinkOrAnswer { get; set; }

        public int UpVotes { get; set; }
        
        public int DownVotes { get; set; }
        
        public override string ToString ()
        {
            return QuestionText+ "\n" +
                   "Asked by: " + QuestionAsker + '\n' +
                   "UpVotes: " + UpVotes+ '\n' +
                   "DownVotes: " + DownVotes + '\n' +
                   "Link/Answer: " + LinkOrAnswer;
        }
    }
}