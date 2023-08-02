using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FoxRevenge.Dialogues
{
    [CreateAssetMenu(menuName = "FoxRevenge/Dialogue")]
    public class Dialogue : ScriptableObject
    {
        [SerializeField] private List<DialogueNode> dialogues = new List<DialogueNode>();

        public List<DialogueNode> GetDialogues() {  return dialogues; }

        [System.Serializable]
        public struct DialogueNode
        {
            public bool isPlayerSpeaking;
            public string sentenceID;
        }
    }
}