using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System.Text.RegularExpressions;
using FoxRevenge.Localisation;

namespace FoxRevenge.Dialogues
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private UnityEvent onDialogueStart;
        [SerializeField] private GameObject NextButton;
        [SerializeField] private GameObject EndButton;
        [SerializeField] private GameObject PreviousButton;
        private Dialogue currentDialogue;
        private DialogueActions dialogueActions;
        private int currentNodeID;

        public void StartDialogue(Dialogue dialogue, DialogueActions dialogueActions)
        {
            currentDialogue = dialogue;
            currentNodeID = 0;
            this.dialogueActions = dialogueActions;

            onDialogueStart.Invoke();
            dialogueActions.RunStartAction();
            DisplaySentence();
        }

        public void DisplayNextSentence()
        {
            if(currentNodeID >= currentDialogue.GetDialogues().Count - 1) return;

            currentNodeID++;
            DisplaySentence();
        }

        public void DisplayPreviousSentence()
        {
            if(currentNodeID <= 0) return;
            currentNodeID--;

            DisplaySentence();
        }

        public void EndDialogue()
        {
            dialogueActions.RunEndAction();
        }

        private void DisplaySentence()
        {
            if(currentNodeID >= currentDialogue.GetDialogues().Count) return;
            SetButtons();

            dialogueActions.RunSentenceAction(currentDialogue.GetDialogues()[currentNodeID].isPlayerSpeaking);

            StopAllCoroutines();
            StartCoroutine(TypeSentence(currentDialogue.GetDialogues()[currentNodeID].sentenceID));
        }
        private void SetButtons()
        {
            if(currentNodeID == 0) 
            {
                NextButton.SetActive(true);
                EndButton.SetActive(false);
                PreviousButton.SetActive(false);
            }
            else if(currentNodeID == currentDialogue.GetDialogues().Count - 1)
            {
                NextButton.SetActive(false);
                EndButton.SetActive(true);
                PreviousButton.SetActive(true);
            }
            else
            {
                NextButton.SetActive(true);
                EndButton.SetActive(false);
                PreviousButton.SetActive(true);
            }
        }

        IEnumerator TypeSentence (string sentenceID)
        {
            string key = Regex.Replace(sentenceID, @"\s+", "");
            string sentence = LocalisationSystem.GetLocalisedValue(key);
            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return null;
            }
        }
    }
}
