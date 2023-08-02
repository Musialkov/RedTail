using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using FoxRevenge.Saving;

namespace FoxRevenge.Localisation
{
    public class LocalisationSystem : MonoBehaviour
    {
        public static Language currentlanguage;

        private static Dictionary<string, string> englishSubtitles;
        private static Dictionary<string, string> polishSubtitles;

        public static event Action onLanguageChange;

        private static bool isInit = false;

        public static void Init() 
        {
            CSVLoader csvLoader = new CSVLoader();
            csvLoader.LoadCSV();

            englishSubtitles = csvLoader.GetDictionaryValues("en");
            polishSubtitles = csvLoader.GetDictionaryValues("pl");

            isInit = true;
            string loadedLanguage = SavingSystem.LoadLanguage();
            switch(loadedLanguage)
            {
                case "English":
                    currentlanguage = Language.English;
                    break;
                case "Polish":
                    currentlanguage = Language.Polish;
                    break;
                default:
                    currentlanguage = Language.English;
                    break;
            }
        }

        public static string GetLocalisedValue(string key)
        {
            if(!isInit) Init();

            string value = key;
            switch(currentlanguage)
            {
                case Language.English:
                    englishSubtitles.TryGetValue(key, out value);
                    break;
                case Language.Polish:
                    polishSubtitles.TryGetValue(key, out value);
                    break;
            }

            return value;
        }

        public static void ChangeLocalisation(Language language)
        {
            if(currentlanguage != language)
            {
                currentlanguage = language;
                SavingSystem.SaveLanguage(language.ToString());
                onLanguageChange?.Invoke();
            }
        }
    }
}
