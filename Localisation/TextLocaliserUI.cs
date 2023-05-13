using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace FoxRevenge.Localisation
{
    
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class TextLocaliserUI : MonoBehaviour
    {
        [SerializeField] string key = "none";

        private TextMeshProUGUI textField;

        private void Awake() 
        {
            textField = GetComponent<TextMeshProUGUI>();
            key = Regex.Replace(key, @"\s+", "");
        }

        private void OnEnable() 
        {
            LocalisationSystem.onLanguageChange += SetText;
            SetText();
        }

        private void OnDisable() 
        {
            LocalisationSystem.onLanguageChange -= SetText;
        }

        private void SetText() 
        {
            string value = LocalisationSystem.GetLocalisedValue(key);
            textField.text = value;
        }
    }
}