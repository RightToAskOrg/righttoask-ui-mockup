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
        public string ElectorateRepresenting { get; set; }
    }

}