using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oswald : MonoBehaviour, IInteractable
{
    DialogueTrigger dialogueTrigger;
    [SerializeField] List<DialogueObject> dialogueList;
    [SerializeField] DialogueObject incorrectDialogue;
    [SerializeField] FadeScreen fadeScreen;
    [SerializeField] Player player;
    [SerializeField] Coffee coffee;
    Animator animator;
    public string levelScene;
    public bool hasLeft = false;
    bool isWrong = false;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueTrigger.SetDialogueList(dialogueList[state].GetDialogueStrings());
        coffee = new Coffee();
        coffee.name = "Oswald";
        coffee.size = "Medium";
        coffee.roast = "Dark";
        coffee.ingredientsUsed = new List<string> {"RegMilk", "Vanilla"};
        Debug.Log(coffee.size + " " + coffee.roast + " roast coffee created with " + coffee.ingredientsUsed[0] + " and " + coffee.ingredientsUsed[1]);
    }
    private void Update()
    {
        Coffee tempCoffee;
        if (state >= 2)
        {
            tempCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
            if (tempCoffee.roast != null && state == 2)
            {
                Debug.Log("Coffee exists");
                if (tempCoffee.roast != coffee.roast)
                {
                    Debug.Log("Roast is incorrect");
                    tempCoffee.roast = null;
                    Incorrect();
                }
                else if (player.GetMinigame() != null)
                {
                    Debug.Log("Roast is correct");
                    dialogueTrigger.Trigger(true);
                }
            }
        }
    }
    public void Interact(Player player)
    {
        Debug.Log("Oswald is Talking");
        dialogueTrigger.Trigger(true);
    }
    public void DialogueFinish()
    {
        if (state < dialogueList.Count) dialogueTrigger.SetDialogueList(dialogueList[state].GetDialogueStrings());
        //When dialogue finishes, check state condition and
        //Do things on specific states
        switch (state) {
            case 2:
                Debug.Log(CoffeeHandler.Instance);
                CoffeeHandler.Instance.AddOrder(coffee); //Load coffee when on state 2
                break;
            case 8:
                fadeScreen.FadeOutToLevel(levelScene); //Fade out to the level scene on state 8
                break;
        }
        //Set the animator state to the next state
        if (!isWrong) NextState();
        isWrong = false;
    }
    public void NextState()
    {
        Debug.Log("switching state");
        int currentState = animator.GetInteger("state");
        animator.SetInteger("state", currentState + 1);
        state++;
    }
    public void Incorrect()
    {
        isWrong = true;
        dialogueTrigger.SetDialogueList(incorrectDialogue.GetDialogueStrings());
        dialogueTrigger.Trigger(true);
    }
}
