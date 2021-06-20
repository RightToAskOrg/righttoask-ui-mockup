using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PassingData
{
    public class Tag : INotifyPropertyChanged
    {
        private string tagLabel;
        private bool selected;
        public event PropertyChangedEventHandler PropertyChanged;
        
        // This function allows for automatic UI updates when these properties change.
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string TagLabel
        {
            get
            {
                return tagLabel;
            }
            set
            {
                tagLabel = value;
                OnPropertyChanged("TagLabel");
            }
        }
        
        public bool Selected 
        {
                    get
                    {
                        return selected;
                    }
                    set
                    {
                        selected = value;
                        OnPropertyChanged("Selected");
                    }
                }
        public override string ToString ()
        {
            return TagLabel + "\n" +
                (Selected ? "" : "Not ") +
                "Selected" + "\n";
        }
    }
}