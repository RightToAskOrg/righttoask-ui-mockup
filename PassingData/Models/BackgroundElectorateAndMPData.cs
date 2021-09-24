using System;
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
	    private static readonly List<MP> FederalMPs = readMPsFromCSV("StateRepsCSV.csv");
	    private static readonly List<MP> Senators = readMPsFromCSV("allsenstate.csv");
	    // TODO - at the moment, we only have Vic MPs. Add other states.
	    private static readonly List<MP> VicLA_MPs = readMPsFromCSV("VicLegislativeAssemblymembers.csv");
	    private static readonly List<MP> VicLC_MPs =  readMPsFromCSV("VicLegislativeCouncilmembers.csv");

	    public static readonly ObservableCollection<MP> AllMPs = new ObservableCollection<MP>(
		    FederalMPs.Concat(Senators).Concat(VicLA_MPs).Concat(VicLC_MPs)
		    );
		public static readonly ObservableCollection<string> StatesAndTerritories = extractStatesFromMPList();
		public enum StateOrTerritory
		{
			Vic, ACT, NSW, Qld, WA, SA, NT, Tas 
		}

		private static ObservableCollection<string> extractStatesFromMPList()
		{
			return new ObservableCollection<string>(AllMPs.Select(mp => mp.StateOrTerritory.ToUpper()).Distinct());
		}

		public static readonly ObservableCollection<Entity> AllAuthorities = new ObservableCollection<Entity>(readAuthoritiesFromFiles());

		private static List<MP> readMPsFromCSV(string filename)
		{
			var MPs = new List<MP>();
			readDataFromCSV(filename, MPs, parseCSVLineAsMP);
			return MPs;
		}
		
		/*
        private static ObservableCollection<MP> readMPAndElectorateInfoFromFiles()
        {
		    // var AllMPs = new ObservableCollection<MP>();
		    readDataFromCSV("StateRepsCSV.csv",AllMPs, parseCSVLineAsMP);
		    readDataFromCSV("allsenstate.csv",AllMPs,parseCSVLineAsMP);
		    readDataFromCSV("VicLegislativeAssemblymembers.csv",AllMPs, parseCSVLineAsMP);
		    readDataFromCSV("VicLegislativeCouncilmembers.csv", AllMPs, parseCSVLineAsMP);
		    
		    return AllMPs;
        }
        */

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
		private static MP parseCSVLineAsMP(string line)
		{
			string[] words = line.Split(',');
			if (words.Length >= 5)
			{
				MP newMP = new MP
				{
					Salutation = (words[0] == "Senator" ? "Senator" : "Member"),
					EntityName = words[2] +" "+ words[1],
					// FamilyName = words[1],
					// PreferredName = words[2],
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
    }
}