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
    public class MP : Person
    {
        public BackgroundElectorateAndMPData.Chamber ChamberSeatedIn { get; set; }
        public string Salutation { get; set; }
        public string ElectorateRepresenting { get; set; }
    
        public override string ToString()
        {
            var StateIfNeeded = Salutation == "Senator" ? "" : ", " + (StateOrTerritory ?? "");
            // Could use String.Equals(str1, str2, StringComparison.OrdinalIgnoreCase) to ignore case.
            return base.ToString()
                   + "\n" + (Salutation ?? "")
                   + " for " + (ElectorateRepresenting ?? "")
                   + StateIfNeeded;
        }

    }
}