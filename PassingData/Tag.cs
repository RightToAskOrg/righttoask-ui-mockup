using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PassingData
{
    public class Tag : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string TagLabel { get; set; }
        
        public bool Selected { get; set; }
        
        public override string ToString ()
        {
            return TagLabel + "\n" +
                (Selected ? "" : "Not ") +
                "Selected" + "\n";
        }
    }
}