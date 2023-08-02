using System.Collections;
using System.Collections.Generic;
using FoxRevenge.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace FoxRevenge.Utils
{
    public class ArenaLever : MonoBehaviour, IInteractable
    {
        [SerializeField] PlayableDirector timeline;

        private int enemiesToDestroy = 4;
        private bool isPlayed = false;
        public void Interact(GameObject trigger)
        {
            if(enemiesToDestroy <= 0 && !isPlayed)
            {
                timeline.Play();
                isPlayed = true;
            }
        }

        public void DecreaseEnemiesToDestroy()
        {
            enemiesToDestroy--;
            if(enemiesToDestroy == 0)
            {
                gameObject.AddComponent<HelpObject>().SetHelpType(EHelpType.Lever);
            }
        }
    }
}