using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueObjects", order = 1)]
public class DialogueObject : ScriptableObject
{
    [SerializeField] private List<dialogueString> dialogueStrings;
    
    public List<dialogueString> GetDialogueStrings()
    {
        return dialogueStrings;
    }
}
