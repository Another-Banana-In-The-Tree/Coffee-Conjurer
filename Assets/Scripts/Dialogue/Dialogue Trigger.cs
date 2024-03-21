using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialoguestrings = new List<dialogueString>();
    [SerializeField] private GameObject other;
    private DialogueManager dialogue;
    private NPC npc;
    //private Oswald oswald;
    private bool hasSpoken = false;
    //add the real trigger later
    //other.gameObject.GetComponent<DialougeManager>.DialogueStart();
    private void Start()
    {
        npc = GetComponent<NPC>();
        //if (GetComponent<Oswald>() != null) oswald = GetComponent<Oswald>();
        dialogue = other.gameObject.GetComponent<DialogueManager>();
    }
    public void Trigger(bool isTutorial)
    {
        if (isTutorial) dialogue.DialogueStart(dialoguestrings);
        else dialogue.DialogueStart(dialoguestrings, npc);
    }
    public void SetDialogueList(List<dialogueString> list)
    {
        dialoguestrings = list;
    }

}
[System.Serializable]
public class dialogueString {
    public string text;
    public bool isEnd;
    [Header("Branch")]
        public bool isQuestion;
        public string dialogueOption1;
        public string dialogueOption2;
        public string dialogueOption3;
        public int option1IndexJump;
        public int option2IndexJump;
        public int option3IndexJump;
    [Header("Branch")]
    public UnityEvent startDialogueEvent;
    public UnityEvent endDialogueEvent;
}

