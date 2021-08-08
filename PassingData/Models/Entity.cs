using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

// This class represents an entity such as a person or authority.
// Can be subclassed for a person (add a picture)
// or an authority or committee.
// Also, in future, this can include public keys for signing & decryption.
namespace PassingData
{
    public class Entity : INotifyPropertyChanged
    {
        private string entityName;
        private string nickName;
        private UrlWebViewSource url;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string EntityName
        {
            get { return entityName; }
            set
            {
                entityName = value;
                OnPropertyChanged("EntityName");
            }
        }

        public string NickName
        {
            get { return nickName; }
            set
            {
                nickName = value;
                OnPropertyChanged("NickName");
            }
        }
        public UrlWebViewSource URL
        {
            get { return url; }
            set
            {
                url = value;
                OnPropertyChanged("URL");
            }
        }
    }
}