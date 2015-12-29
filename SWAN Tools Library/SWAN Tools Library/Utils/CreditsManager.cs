using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace swantiez.unity.tools.utils
{
    public class CreditsManager
    {
        private const string CREDITS_FILE = "credits";

        private static CreditsManager _instance = null;

        public static CreditsManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new CreditsManager();
                }

                return _instance;
            }
        }

        private Regex locKeyRegex = new Regex(@"##(\w+?)##");

        private CreditsManager()
        {
        }

        public List<string> LoadCredits(string creditsText, Func<string, string> convertLocalizedKey = null)
        {
            List<string> creditsLines = new List<string>();
            if (creditsText != "")
            {
	            using (StringReader creditsFileReader = new StringReader(creditsText))
	            {
	                string creditsLine;
		            while ((creditsLine = creditsFileReader.ReadLine()) != null)
		            {
	                    if (convertLocalizedKey != null)
	                    {
		                    Match match = locKeyRegex.Match(creditsLine);
	                        List<string[]> valuesToReplace = new List<string[]>();
		                    while (match.Success)
		                    {
		                        string locKeyWithSharps = match.Value;
		                        string locKey = locKeyWithSharps.Substring(2, locKeyWithSharps.Length - 4).ToLower();
	                            string locValue = convertLocalizedKey(locKey);
	                            valuesToReplace.Add(new string[] { locKeyWithSharps, locValue });
	                            match = match.NextMatch();
	                        }
	
	                        foreach(string[] valuesPairToReplace in valuesToReplace)
	                        {
	                            creditsLine = creditsLine.Replace(valuesPairToReplace[0], valuesPairToReplace[1]);
	                        }
	                    }
	
	                    creditsLines.Add(creditsLine);
	                }
	            }
            }
            return creditsLines;
        }

        public List<string> LoadCredits(TextAsset creditsFile, Func<string, string> convertLocalizedKey = null)
        {
            if ((creditsFile == null) || (creditsFile.text == ""))
            {
                return new List<string>();
            }
            return LoadCredits(creditsFile.text, convertLocalizedKey);
        }

        // Store credits in file "resources/credits.txt" with localized keys as ##MYVALUE##.
        // Then, provide the function convertLocalizedKey to the LoadCredits method in order to convert the "myvalue" keys to the appropriate localized value.
        public List<string> LoadCredits(Func<string, string> convertLocalizedKey = null)
        {
            TextAsset creditsFile = Resources.Load(CREDITS_FILE) as TextAsset;
            return LoadCredits(creditsFile, convertLocalizedKey);
        }
    }
}
