using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PassingData
{
    // This class reads in information about electorates, MPs, etc, from static files.
    public static class BackgroundElectorateAndMPData
    {
	    private static readonly List<MP> FederalMPs 
		    = readMPsFromCSV(Chamber.Australian_House_Of_Representatives, "StateRepsCSV.csv");
	    private static readonly List<MP> Senators 
		    = readMPsFromCSV(Chamber.Australian_Senate, "allsenstate.csv");
	    // TODO - at the moment, we only have Vic MPs. Add other states.
	    private static readonly List<MP> VicLA_MPs 
		    = readMPsFromCSV(Chamber.Vic_Legislative_Assembly, "VicLegislativeAssemblymembers.csv");
	    private static readonly List<MP> VicLC_MPs 
		    =  readMPsFromCSV(Chamber.Vic_Legislative_Council, "VicLegislativeCouncilmembers.csv");

	    public static readonly ObservableCollection<MP> AllMPs = new ObservableCollection<MP>(
		    FederalMPs.Concat(Senators).Concat(VicLA_MPs).Concat(VicLC_MPs)
		    );

	    public static readonly ObservableCollection<string> StatesAndTerritories = new ObservableCollection<string>()
	    {
		    "ACT",
			"NSW",
			"NT",
			"QLD",
			"SA",
			"Tas",
			"Vic",
			"WA"
	    };

	    public enum Chamber 
	    {
			ACT_Legislative_Assembly,
		    Australian_House_Of_Representatives,
		    Australian_Senate,
		    NSW_Legislative_Assembly,
			NSW_Legislative_Council,
			NT_Legislative_Assembly,
		    Qld_Legislative_Assembly,
		    SA_Legislative_Assembly,
			SA_Legislative_Council,
		    Vic_Legislative_Assembly,
			Vic_Legislative_Council,
			Tas_House_Of_Assembly,
			Tas_Legislative_Council,
			WA_Legislative_Assembly,
			WA_Legislative_Council
	    }

		public static readonly ObservableCollection<Entity> AllAuthorities = new ObservableCollection<Entity>(readAuthoritiesFromFiles());

		private static List<MP> readMPsFromCSV(Chamber chamber, string filename)
		{
			var MPs = new List<MP>();
			readDataFromCSV(filename, MPs, (string line) =>  parseCSVLineAsMP(chamber,line) );
			return MPs;
		}
		
       private static List<Entity> readAuthoritiesFromFiles()
       {
		    var AllAuthorities = new List<Entity>();
		    readDataFromCSV("all-authorities.csv",AllAuthorities,parseCSVLineAsAuthority);
		    return AllAuthorities;
       }
        
		private static void readDataFromCSV<T>(string filename, List<T> MPCollection, Func<string,T> parseLine)
		{
			string line;

			try
			{
				T MPToAdd;
				var assembly = IntrospectionExtensions.GetTypeInfo(typeof(ReadingContext)).Assembly;
				Stream stream = assembly.GetManifestResourceStream("PassingData.Resources." + filename);
				using (var sr = new StreamReader(stream))
				{
					// Read the first line, which just has headings we can ignore.
					sr.ReadLine();
					while ((line = sr.ReadLine()) != null)
					{
						MPToAdd = parseLine(line);
						if (MPToAdd != null)
						{
							MPCollection.Add(MPToAdd);
						}
					}
				}
			}
			catch (IOException e)
			{
				Console.WriteLine("MP file could not be read: " + filename);
				Console.WriteLine(e.Message);
			}
		}
		private static MP parseCSVLineAsMP(Chamber chamber, string line)
		{
			string[] words = line?.Split(',');
			if (words?.Length >= 5)
			{
				MP newMP = new MP
				{
					ChamberSeatedIn = chamber,
					Salutation = (words[0] == "Senator" ? "Senator" : "Member"),
					EntityName = words[2] +" "+ words[1],
					ElectorateRepresenting = words[3],
					StateOrTerritory = words[4],
					
				};
				return newMP;
			}
			
			return null;
		}	
		
		// This parses a line from Right To Know's CSV file as an Authority.
		// It is, obviously, very specific to the expected file format.
		// Ignore any line that doesn't produce at least 3 words.
		private static Entity parseCSVLineAsAuthority(string line)
		{
			string[] words = line.Split(',');
			if (words.Length >= 3)
			{
				
				Entity newAuthority = new Entity
				{
					EntityName = words[0],
					NickName = words[1],
					RightToKnowURLSuffix = words[2]
				};
				return newAuthority;
			}
			else
			{
				return null;
			}
		}

		public static List<string> ListElectoratesInChamber(Chamber chamber)
		{
			return new List<string>(AllMPs.Where(mp => mp.ChamberSeatedIn == chamber).Select(mp => mp.ElectorateRepresenting));
		}

		/* Finds all the chambers in which a citizen of this state is represented,
		 * including the House of Representatives and the Senate.
		 * If the string input doesn't match any states, it simply
		 * returns the federal chambers.
		 */
		public static List<Chamber> FindChambers(string state)
		{
			var chambersForTheState = new List<Chamber>()
			{
				Chamber.Australian_House_Of_Representatives,
				Chamber.Australian_Senate
			};

			switch (state.ToUpper())
			{
				case ("ACT"):
					chambersForTheState.Add(Chamber.ACT_Legislative_Assembly);
					break;
				case ("NSW"):
					chambersForTheState.Add(Chamber.NSW_Legislative_Assembly);
					chambersForTheState.Add(Chamber.NSW_Legislative_Council);
					break;
				case ("NT"):
					chambersForTheState.Add(Chamber.NT_Legislative_Assembly);
					break;
				case ("QLD"):
					chambersForTheState.Add(Chamber.Qld_Legislative_Assembly);
					break;
				case ("SA"):
					chambersForTheState.Add(Chamber.SA_Legislative_Assembly);
					chambersForTheState.Add(Chamber.SA_Legislative_Council);
					break;
				case ("VIC"):
					chambersForTheState.Add(Chamber.Vic_Legislative_Assembly);
					chambersForTheState.Add(Chamber.Vic_Legislative_Council);
					break;
				case ("TAS"):
					chambersForTheState.Add(Chamber.Tas_House_Of_Assembly);
					chambersForTheState.Add(Chamber.Tas_Legislative_Council);
					break;
				case ("WA"):
					chambersForTheState.Add(Chamber.WA_Legislative_Assembly);
					chambersForTheState.Add(Chamber.WA_Legislative_Council);
					break;
			}

			return chambersForTheState;
		}
    }
}