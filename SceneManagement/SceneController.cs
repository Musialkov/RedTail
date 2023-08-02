using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Saving;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FoxRevenge.SceneManagement
{
    public class SceneController : MonoBehaviour
    {
        public void StartNewGame()
        {
            SavingSystem.ResetPlayerProgress();
            SceneManager.LoadScene("Level1");
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void Continue()
        {

        }

        public void LoadMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        public static void LoadLevel(string levelName)
        {
            SceneManager.LoadScene(levelName);
        }

        public static void LoadLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
        }

        public static void LoadNextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}