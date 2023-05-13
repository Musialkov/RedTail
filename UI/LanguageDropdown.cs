using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FoxRevenge.Localisation;

namespace FoxRevenge.UI
{
    public class LanguageDropdown : MonoBehaviour
    {
        void Awake() 
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