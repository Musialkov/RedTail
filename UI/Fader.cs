using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.UI
{
    public class Fader : MonoBehaviour
    {
        [SerializeField] private float startDelay = 1f;
        [SerializeField] private float fadeTime = 5f;
        [SerializeField] private CanvasGroup canvasGroup;

        private float timer = 0;

        private void OnEnable() 
        {
            StartCoroutine(Fade());
        }

        IEnumerator Fade()
        {
            yield return new WaitForSeconds(startDelay);

            LeanTween.alphaCanvas(canvasGroup, 1, fadeTime);
        }
    }
}
