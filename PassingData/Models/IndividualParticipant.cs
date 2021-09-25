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
		private string selectedStateOrTerritory;
		private string selectedFederalElectorate; 
		private string selectedStateElectorate;
		private string address;
		private string userName;
		
		// This is a list of chamber-electorate pairs in which the person
		// has representatives.
		private List<(BackgroundElectorateAndMPData.Chamber, string)> ElectoratesRepresentedIn;

		public IndividualParticipant() : base()
		{
			MPsKnown = false;
			Is_Registered = false;
		}
		public bool Is_Registered { get; set; }
		public bool MPsKnown { get; set; }


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