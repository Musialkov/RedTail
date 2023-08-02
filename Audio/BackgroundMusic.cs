using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Audio
{
    public class BackgroundMusic : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private float maxVolume;
        [SerializeField] private float fadeTime;

        private void Start() 
        {
            music.volume = 0;
            StartCoroutine(ChangeVolume());
        }

        private IEnumerator ChangeVolume()
        {
            float timer = 0f;
            while (timer < fadeTime)
            {
                timer += Time.deltaTime;
                music.volume = Mathf.Lerp(0f, maxVolume, timer / fadeTime);
                yield return null;
            }
        }
    }
}