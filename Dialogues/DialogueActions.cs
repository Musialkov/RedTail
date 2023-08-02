using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FoxRevenge.Dialogues
{
    public class DialogueActions : MonoBehaviour
    {
        [SerializeField] private Camera playerCamera;
        [SerializeField] private Camera otherCamera;

        [SerializeField] private UnityEvent onDialogueStart;
        [SerializeField] private UnityEvent onPlayerSpeak;
        [SerializeField] private UnityEvent onOtherSpeak;
        [SerializeField] private UnityEvent onDialogueEnd;

        public void RunStartAction()
        {
            onDialogueStart.Invoke();
        }

        public void RunSentenceAction(bool isPlayerSpeaking)
        {
            if(isPlayerSpeaking)
            {
                playerCamera.gameObject.SetActive(true);
                otherCamera.gameObject.SetActive(false);
            } 
            else
            {
                playerCamera.gameObject.SetActive(false);
                otherCamera.gameObject.SetActive(true);
            }


            if(isPlayerSpeaking) onPlayerSpeak.Invoke();
            else onOtherSpeak.Invoke();
        }

        public void RunEndAction()
        {
            onDialogueEnd.Invoke();
        }
    }
}
