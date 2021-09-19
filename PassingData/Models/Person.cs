using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

// This class represents a public authority,
// represented in the RightToKnow list.
namespace PassingData
{
    public class Person : Entity
    {
        // public string PreferredName { get; set; }
        // public string FamilyName { get; set; }
        public string StateOrTerritory { get; set; }
        protected string federalElectorate;
        protected string stateOrTerritoryLCElectorate;
        protected string stateOrTerritoryLAElectorate;

        // TODO add attributes for a nice profile, such as a photo.

    }

}