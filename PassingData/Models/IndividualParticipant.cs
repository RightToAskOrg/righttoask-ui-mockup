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
		// Initially, when we don't know the state, it's only the Australian
		// Parliament.
		private List<(BackgroundElectorateAndMPData.Chamber, string)> ElectoratesRepresentedIn
			= new List<(BackgroundElectorateAndMPData.Chamber, string)>()
			{
				(BackgroundElectorateAndMPData.Chamber.Australian_House_Of_Representatives, ""),
				(BackgroundElectorateAndMPData.Chamber.Australian_Senate, "")
			};

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
		        UpdateChambers(value);
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

		private void UpdateChambers(string state)
		{
			List<(BackgroundElectorateAndMPData.Chamber, string)> NewElectoratesRepresentedIn
				= new List<(BackgroundElectorateAndMPData.Chamber, string)>();
			
			switch (state.ToUpper())
			{
				case ("ACT"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.ACT_Legislative_Assembly, ""));
					break;
				case ("NSW"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.NSW_Legislative_Assembly,""));
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.NSW_Legislative_Council,""));
					break;
				case ("NT"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.NT_Legislative_Assembly, ""));
					break;
				case ("QLD"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.Qld_Legislative_Assembly, ""));
					break;
				case ("SA"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.SA_Legislative_Assembly,""));
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.SA_Legislative_Council,""));
					break;
				case ("VIC"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.Vic_Legislative_Assembly,""));
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.Vic_Legislative_Council,""));
					break;
				case ("TAS"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.Tas_House_Of_Assembly,""));
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.Tas_Legislative_Council,""));
					break;
				case ("WA"):
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.WA_Legislative_Assembly,""));
					NewElectoratesRepresentedIn.Add((BackgroundElectorateAndMPData.Chamber.WA_Legislative_Council,""));
					break;
				
			}	
		}
		
    }
} 