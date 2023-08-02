using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using FoxRevenge.Stats;
using FoxRevenge.Utils;
using FoxRevenge.Saving;
using UnityEngine.SceneManagement;

namespace FoxRevenge.UI
{
    public class LevelCompleteUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentPointsText;
        [SerializeField] private AudioSource winSound;

        private PlayerStatsComponent playerStats;

        private void Awake() 
        {
            playerStats = FindObjectOfType<PlayerStatsComponent>();
            gameObject.SetActive(false);
        }

        private void OnEnable() 
        {
            currentPointsText.text = playerStats.GetStat(Stat.Points).ToString();
            SavingSystem.SaveLevelIndex(SceneManager.GetActiveScene().buildIndex + 1 > 5 ? 5 : SceneManager.GetActiveScene().buildIndex + 1);
            SavingSystem.SavePlayerPoints(SceneManager.GetActiveScene().buildIndex, (int) playerStats.GetStat(Stat.Points));
            winSound.Play();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}