using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialoguestrings = new List<dialogueString>();
    [SerializeField] private GameObject other;
    private bool hasSpoken = false;
    //add the real trigger later
    //other.gameObject.GetComponent<DialougeManager>.DialogueStart();
    public void Trigger()
    {
        other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialoguestrings);
        Debug.Log("hi");
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

