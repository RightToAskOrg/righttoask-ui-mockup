using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

// This class represents a person who uses the
// system and is not an MP or org representative.
namespace PassingData
{
    public class IndividualParticipant : Person 
    {
		private string address;
		
		private string selectedStateOrTerritory;
		private string selectedFederalElectorate; 
		private string selectedLAStateElectorate;
		private string selectedLCStateElectorate;
		
		// Initially, when we don't know the state, it's only the Australian
		// Parliament.
		private List<BackgroundElectorateAndMPData.Chamber> chambersRepresentedIn 
			= BackgroundElectorateAndMPData.FindChambers("");

		// This is a list of chamber-electorate pairs in which the person
		// has representatives.
		private List<(BackgroundElectorateAndMPData.Chamber, string)> electoratesRepresentedIn
			= new List<(BackgroundElectorateAndMPData.Chamber, string)>() { };

		public IndividualParticipant() : base()
		{
			MPsKnown = false;
			Is_Registered = false;
			MyMPs = new ObservableCollection<Entity>();
		}
		public bool Is_Registered { get; set; }
		public bool MPsKnown { get; set; }
		
		public ObservableCollection<Entity> MyMPs { get; set; }


		public string SelectedStateOrTerritory 
        {
	        get
	        {
		        return selectedStateOrTerritory;
	        }
	        set
	        {
		        selectedStateOrTerritory = value;
		        UpdateChambers(value);
		        OnPropertyChanged("SelectedStateOrTerritory");
	        }
        }
		public string SelectedLCStateElectorate 
		{
		    get
		    {
                return selectedLCStateElectorate;
            }
            set
            {
                selectedLCStateElectorate= value;
                OnPropertyChanged("SelectedLCStateElectorate");
            }
        }

		public string SelectedLAStateElectorate
		{
			get { return selectedLAStateElectorate; }
			set
			{
				selectedLAStateElectorate = value;
				OnPropertyChanged("SelectedLAStateElectorate");
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

		public void UpdateChambers(string state)
		{
			chambersRepresentedIn = BackgroundElectorateAndMPData.FindChambers(state);
		}
    }
} 