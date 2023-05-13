using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace FoxRevenge.Utils
{
    public class NewLevelCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private PlayableDirector timeline;
        [SerializeField] private string levelName;
        private bool isPlayed = false;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                timeline.Play();
                isPlayed = true;
                StartCoroutine(LoadLevelAsync());
            }
        }

        IEnumerator LoadLevelAsync()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelName);

            asyncLoad.allowSceneActivation = false;
    
            while (true)
            {
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
    
                if (progress == 1f && isPlayed && timeline.state == PlayState.Paused)
                {
                    asyncLoad.allowSceneActivation = true;
                }
    
                yield return null;
            }
        }
    }
}