using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Dialogues
{
    public class DialogueTrigger : MonoBehaviour
    {
        [SerializeField] private Dialogue dialogue;
        [SerializeField] private DialogueActions dialogueActions;

        private bool wasPlayed = false;
        private DialogueManager dialogueManager;

        private void Start() 
        {
            dialogueManager = FindObjectOfType<DialogueManager>();    
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player") && dialogueManager && !wasPlayed)
            {
                dialogueManager.StartDialogue(dialogue, dialogueActions);
                wasPlayed = true;
            }
        }
    }
}