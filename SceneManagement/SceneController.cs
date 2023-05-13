using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FoxRevenge.SceneManagement
{
    public class SceneController : MonoBehaviour
    {
        public void StartNewGame()
        {
            SceneManager.LoadScene("Level1");
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }
}