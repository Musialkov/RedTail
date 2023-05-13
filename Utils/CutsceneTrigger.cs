using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace FoxRevenge.Utils
{
    public class CutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private PlayableDirector timeline;
        private bool isPlayed = false;

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player") && !isPlayed)
            {
                timeline.Play();
                isPlayed = true;
            }
        }
    }
}