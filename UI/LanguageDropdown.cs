using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FoxRevenge.Localisation;

namespace FoxRevenge.UI
{
    public class LanguageDropdown : MonoBehaviour
    {
        void Start() 
        {
            TMP_Dropdown dropdown = transform.GetComponent<TMP_Dropdown>();
            dropdown.options.Clear();

            List<string> items = new List<string>();
            items.Add("English");
            items.Add("Polski");

            foreach(string item in items) 
            {
                dropdown.options.Add(new TMP_Dropdown.OptionData() {text = item});
            }

            switch(LocalisationSystem.currentlanguage)
            {
                case Language.English:
                    dropdown.value = 0;
                    break;
                case Language.Polish:
                    dropdown.value = 1;
                    break;
                default:
                    dropdown.value = 0;
                    break;
            }
            
            dropdown.captionText.text = items[dropdown.value];
            DropdownItemSelected(dropdown);
            dropdown.onValueChanged.AddListener(delegate {DropdownItemSelected(dropdown);});
        }

        void DropdownItemSelected(TMP_Dropdown dropdown)
        {
            Language language;
            int index = dropdown.value;
            switch (index)
            {
                case 0:
                    language = Language.English;
                    break;
                case 1:
                    language = Language.Polish;
                    break;
                default:
                    language = Language.English;
                    break;
            }

            LocalisationSystem.ChangeLocalisation(language);
        }
    }
}