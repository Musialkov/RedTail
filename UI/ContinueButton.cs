using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Saving;
using FoxRevenge.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace FoxRevenge.UI
{
    public class ContinueButton : MonoBehaviour
    {
        ButtonColorChanger colorChanger;
        Button button;

        private void Awake() 
        {
            colorChanger = GetComponent<ButtonColorChanger>();
            button = GetComponent<Button>();
        }

        private void Start() 
        {
            if(SavingSystem.LoadLevelIndex() > 1) button.onClick.AddListener(() => SceneController.LoadLevel(SavingSystem.LoadLevelIndex()));
            else
            {
                colorChanger.SetInactiveColor();
                colorChanger.enabled = false;
            } 
        }
    }
}