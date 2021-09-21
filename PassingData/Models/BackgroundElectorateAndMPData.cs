using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace PassingData
{
    // This class reads in information about electorates, MPs, etc, from static files.
    public static class BackgroundElectorateAndMPData 
    {
		public static readonly ObservableCollection<MP> AllMPs = readMPInfoFromFiles();
		public static readonly ObservableCollection<Entity> AllAuthorities = readAuthoritiesFromFiles();

        private static ObservableCollection<MP> readMPInfoFromFiles()
        {
		    var AllMPs = new ObservableCollection<MP>();
		    readDataFromCSV("StateRepsCSV.csv",AllMPs, parseCSVLineAsMP);
		    readDataFromCSV("allsenstate.csv",AllMPs,parseCSVLineAsMP);
		    readDataFromCSV("VicLegislativeAssemblymembers.csv",AllMPs,parseCSVLineAsMP);
		    readDataFromCSV("VicLegislativeCouncilmembers.csv", AllMPs,parseCSVLineAsMP);
		    
		    return AllMPs;
        }

       private static ObservableCollection<Entity> readAuthoritiesFromFiles()
       {
		    var AllAuthorities = new ObservableCollection<Entity>();
		    readDataFromCSV("all-authorities.csv",AllAuthorities,parseCSVLineAsAuthority);
		    return AllAuthorities;
       }
        
		private static void readDataFromCSV<T>(string filename, ObservableCollection<T> MPCollection, Func<string,T> parseLine)
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