using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace iot_webapp.Services {
    public class CensoringService : ICensoringService {
        public IList<string> CensoredWords { get; private set; }
 
        public CensoringService() {
            LoadBadWordsList();
        }

        public bool ContainsCensoredWords(string text) {
            var censoredText = CensorText(text);
            return !censoredText.Equals(text);
        }
 
        public string CensorText(string text) {
            if (text == null)
                throw new ArgumentNullException("text");
            return CensorTextImpl(text);
        }

        private string CensorTextImpl(string text) {
            string censoredText = text;
            foreach (string censoredWord in CensoredWords) {
                string regularExpression = ToRegexPattern(censoredWord);
 
                censoredText = Regex.Replace(censoredText, regularExpression, StarCensoredMatch,
                    RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
 
            return censoredText;
        }
 
        private static string StarCensoredMatch(Match m) {
            string word = m.Captures[0].Value;
 
            return new string('*', word.Length);
        }
 
        private string ToRegexPattern(string wildcardSearch) {
            string regexPattern = Regex.Escape(wildcardSearch);
 
            regexPattern = regexPattern.Replace(@"\*", ".*?");
            regexPattern = regexPattern.Replace(@"\?", ".");
 
            if (regexPattern.StartsWith(".*?")) {
                regexPattern = regexPattern.Substring(3);
                regexPattern = @"(^\b)*?" + regexPattern;
            }
 
            regexPattern = @"\b" + regexPattern + @"\b";
 
            return regexPattern;
        }

        private void LoadBadWordsList() {
            CensoredWords = new List<string>();
            string filePath = "CensoredWords.json";
            using (var censoredWordsFile = new StreamReader(filePath)) {
                CensoredWords = JsonConvert.DeserializeObject<List<string>>(censoredWordsFile.ReadToEnd());
            }
        }
    }
}

