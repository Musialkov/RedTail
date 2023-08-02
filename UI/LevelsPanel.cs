using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FoxRevenge.Saving;
using UnityEngine.UI;
using FoxRevenge.SceneManagement;

namespace FoxRevenge.UI
{
    public class LevelsPanel : MonoBehaviour
    {
        [SerializeField] private List<TextMeshProUGUI> points;
        [SerializeField] private List<Button> buttons;

        private void Start() 
        {
            List<int> playerPoints = SavingSystem.LoadPlayerPoints();
            for(int i = 0; i < points.Count; i++)
            {
                int levelIndex = i + 1;
                if(playerPoints.Count > i) 
                {
                    points[i].text = playerPoints[i].ToString();
                    buttons[i].onClick.AddListener(() => SceneController.LoadLevel(levelIndex));
                }
                else if(playerPoints.Count == i)
                {
                    points[i].text = "---";
                    buttons[i].onClick.AddListener(() => SceneController.LoadLevel(levelIndex));
                }
                else
                {
                    points[i].text = "---";
                    buttons[i].GetComponent<ButtonColorChanger>().SetInactiveColor();
                    buttons[i].GetComponent<ButtonColorChanger>().enabled = false;
                }
            }
        }
    }
}