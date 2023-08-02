using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

namespace FoxRevenge.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnStart;
        private void Start() 
        {
            OnStart.Invoke();
        }
        public static void SaveLanguage(string language)
        {
            PlayerPrefs.SetString(SavingKeys.LANGUAGE, language);
        }

        public static string LoadLanguage()
        {
            return PlayerPrefs.GetString(SavingKeys.LANGUAGE);
        }

        public static void SaveFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
            PlayerPrefs.Save();
        }

        public static float ReadFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public static void SaveLevelIndex(int index)
        {
            int savedLevelId = PlayerPrefs.GetInt(SavingKeys.LEVEL_ID);
            if(savedLevelId >= index) return;

            PlayerPrefs.SetInt(SavingKeys.LEVEL_ID, index);
            PlayerPrefs.Save();
        }

        public static int LoadLevelIndex()
        {
            return PlayerPrefs.GetInt(SavingKeys.LEVEL_ID, 1);
        }

        public static void ResetPlayerProgress()
        {
            PlayerPrefs.DeleteKey(SavingKeys.LEVEL_ID);
            PlayerPrefs.SetInt(SavingKeys.LEVEL_ID, 1);
            PlayerPrefs.DeleteKey(SavingKeys.PLAYER_POINTS);
        }

        public static void SavePlayerPoints(int sceneIndex, int points)
        {
            PlayerPointsSaveableObject loadedPlayerPointsObject = JsonUtility.FromJson<PlayerPointsSaveableObject>(PlayerPrefs.GetString(SavingKeys.PLAYER_POINTS));
            if(loadedPlayerPointsObject == null) loadedPlayerPointsObject = new PlayerPointsSaveableObject();
            List<int> currentPoints = loadedPlayerPointsObject.points;

            if(currentPoints.Count < sceneIndex - 1) return; 
            else if(currentPoints.Count == sceneIndex - 1) currentPoints.Add(points);
            else
            {
                if(currentPoints[sceneIndex - 1] < points) currentPoints[sceneIndex - 1] = points;
            }

            PlayerPointsSaveableObject playerPointsObject = new PlayerPointsSaveableObject(){points = currentPoints};
            PlayerPrefs.SetString(SavingKeys.PLAYER_POINTS, JsonUtility.ToJson(playerPointsObject));
            PlayerPrefs.Save();     
        }

        public static List<int> LoadPlayerPoints()
        {
            PlayerPointsSaveableObject loadedPlayerPointsObject = JsonUtility.FromJson<PlayerPointsSaveableObject>(PlayerPrefs.GetString(SavingKeys.PLAYER_POINTS));
            if(loadedPlayerPointsObject != null) return loadedPlayerPointsObject.points;

            return new List<int>();
        }

        [System.Serializable]
        public class PlayerPointsSaveableObject 
        {
            public List<int> points = new List<int>();
        }
    }
}