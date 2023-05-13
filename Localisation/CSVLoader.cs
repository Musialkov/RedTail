using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace FoxRevenge.Localisation
{
    public class CSVLoader
    {
        private TextAsset csvFile;
        private char lineSeparator = '\n';
        private char fieldSeparator = ';';
        
        public void LoadCSV()
        {
            csvFile = Resources.Load<TextAsset>("localisation");
        }

        public Dictionary<string, string> GetDictionaryValues(string attributeId)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string[] lines = csvFile.text.Split(lineSeparator);

            string[] headers = lines[0].Split(fieldSeparator);

            int attributeIndex = -1;

            for(int i = 0; i < headers.Length; i++)
            {
                if(headers[i].Contains(attributeId))
                {
                    attributeIndex = i;
                    break;
                }
            }

            for(int i = 1; i < lines.Length; i++)
            {
                string line = lines[i];
                string[] fields = line.Split(fieldSeparator);

                if(fields.Length > attributeIndex)
                {
                    string key = Regex.Replace(fields[0], @"\s+", "");
                    if(dictionary.ContainsKey(key)) continue;

                    var value = fields[attributeIndex];
                    dictionary.Add(key, value);
                }
            }
            return dictionary;
        }
    }
}