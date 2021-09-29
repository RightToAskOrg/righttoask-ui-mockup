using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace PassingData
{
	public class FilterChoices : INotifyPropertyChanged
	{
		private string searchKeyword;
		private ObservableCollection<Entity> selectedAnsweringMPs;
		private ObservableCollection<Entity> selectedAskingMPs;

		private ObservableCollection<Entity> selectedAuthorities;

		public FilterChoices()
		{
			selectedAnsweringMPs = new ObservableCollection<Entity>();
			selectedAskingMPs = new ObservableCollection<Entity>();
			selectedAuthorities = new ObservableCollection<Entity>();
		}

		public ObservableCollection<Entity> SelectedAuthorities
		{
			get { return selectedAuthorities; }
			set
			{
				selectedAuthorities = value;
				OnPropertyChanged("SelectedAuthorities");
			}
		}

		public string SearchKeyword
		{
			get { return searchKeyword; }
			set
			{
				searchKeyword = value;
				OnPropertyChanged("SearchKeyword");
			}
		}

		public ObservableCollection<Entity> SelectedAskingMPs
		{
			get { return selectedAskingMPs; }
			set
			{
				selectedAskingMPs = value;
				OnPropertyChanged("SelectedAskingMPs");
			}
		}

		public ObservableCollection<Entity> SelectedAnsweringMPs
		{
			get { return selectedAnsweringMPs; }
			set
			{
				selectedAnsweringMPs = value;
				OnPropertyChanged("SelectedAnsweringMPs");
			}
		}
		
        public event PropertyChangedEventHandler PropertyChanged;
        
        // This function allows for automatic UI updates when these properties change.
        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
	}
}
