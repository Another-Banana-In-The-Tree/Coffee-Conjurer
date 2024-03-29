using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Rendering.Universal;
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
    private bool menuOpened;
    private bool interacted = false;
    private bool menuOpenWasTriggered = false;
    private bool waitForDialogueFinish = false;
    [SerializeField] Light2D[] minigameHighlights;
    [SerializeField] SpriteRenderer orderMenuArrow;
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
       
        if(state ==2 && menuOpened && (Vector2.Distance(player.transform.position, transform.position)< 0.9f) && !menuOpenWasTriggered)
        {
            //NextState();
            menuOpened = false;
            menuOpenWasTriggered = true;
            dialogueTrigger.Trigger(true);
        }

    }
    public void Interact(Player player)
    {
        if ( !interacted)
        {
            interacted = true;

            Debug.Log("Oswald is Talking");
            
            dialogueTrigger.Trigger(true);
            if(state == 11)
            {
                int currentState = animator.GetInteger("state");
                animator.SetInteger("state", currentState + 1);
            }
            
        }
        
    }
    public void DialogueFinish()
    {
        // if (state < dialogueList.Count) dialogueTrigger.SetDialogueList(dialogueList[state].GetDialogueStrings());
        //When dialogue finishes, check state condition and
        //Do things on specific states
       // print("this is the dialogue finish script");
        switch (state) {
            case 1:
                Debug.Log("addingCoffee");
                CoffeeHandler.Instance.AddOrder(coffee); //Load coffee when on state 2
                break;
            case 12:
                fadeScreen.FadeOutToLevel(levelScene); //Fade out to the level scene on state 8
                break;
        }
        //Set the animator state to the next state
        if (state == 3 || state == 5 || state == 7 || state == 9)
        {
            PlayerInput.EnableMinigame();
            if (waitForDialogueFinish)
            {
                waitForDialogueFinish = false;
            }
        }
            if (!isWrong) NextState();
        isWrong = false;
    }
    public void NextState()
    {
        Debug.Log("switching state");
        int currentState = animator.GetInteger("state");
        if(currentState != 2 && currentState != 5 && currentState != 4 && currentState !=8 && currentState !=9) animator.SetInteger("state", currentState + 1);
        
        state++;
        if (state < dialogueList.Count) dialogueTrigger.SetDialogueList(dialogueList[state].GetDialogueStrings());
        if (state < 2 || state == 12)
        {
            print("going to next dialogue");
            dialogueTrigger.Trigger(true);
        }
        switch(state)
        {
            case 2:
                orderMenuArrow.enabled = true;
                break;
            case 3:
                orderMenuArrow.enabled = false;
                minigameHighlights[0].enabled = true;
                break;
            case 5:
                minigameHighlights[0].enabled = false;
                minigameHighlights[1].enabled = true;
                break;
            case 7:
                minigameHighlights[1].enabled = false;
                minigameHighlights[2].enabled = true; 
                break;
            case 9:
                minigameHighlights[2].enabled = false;
                minigameHighlights[3].enabled = true; 
                break;
            case 11:
                minigameHighlights[3].enabled = false;
                break;
        }

    }
    public void Incorrect()
    {
        print("incorrect");
        isWrong = true;
        dialogueTrigger.SetDialogueList(incorrectDialogue.GetDialogueStrings());
        dialogueTrigger.Trigger(true);
    }

    public void CheckTaskStatus()
    {
        print("testing chck status");
        Coffee tempCoffee;
        
            tempCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
        if (state < dialogueList.Count) dialogueTrigger.SetDialogueList(dialogueList[state].GetDialogueStrings());
        if (tempCoffee.roast != null && state == 4)
            {
                Debug.Log("Coffee exists");
                if (tempCoffee.roast != coffee.roast)
                {
                    Debug.Log("Roast is incorrect");
                    tempCoffee.roast = null;
                    Incorrect();
                }
            else
            {
                dialogueTrigger.Trigger(true);
                int currentState = animator.GetInteger("state");
                animator.SetInteger("state", currentState + 1);
            }
            return;
                
            }
        if (tempCoffee.size != null && state == 6)
        {
            if (tempCoffee.size != coffee.size)
            {
                tempCoffee.size = null;
                Incorrect();
            }
            else
            {
        
                dialogueTrigger.Trigger(true);
                int currentState = animator.GetInteger("state");
                animator.SetInteger("state", currentState + 1);
            }
            return;
        }
        if(state == 8)
        {
            if (tempCoffee.ingredientsUsed.Contains("RegMilk") && tempCoffee.ingredientsUsed.Contains("Vanilla"))
            {
                dialogueTrigger.Trigger(true);
                int currentState = animator.GetInteger("state");
                animator.SetInteger("state", currentState + 1);
            }
            else
            {
                Incorrect();
            }
        }
        if (tempCoffee.stirred && state == 10)
        {
            interacted = false;
            dialogueTrigger.Trigger(true);
            int currentState = animator.GetInteger("state");
            animator.SetInteger("state", currentState + 1);
            
        }
        

                
    }


    public void MiniGameOpened()
    {
        
        print("minigame opened");
        if(player.GetMinigame().MiniGameNumber() == state)
        {
            waitForDialogueFinish = true;
            PlayerInput.DialogueMode();
            dialogueTrigger.Trigger(true);
            
        }
    }
    public void MenuOpened(bool open)
    {
        if (!menuOpened)
        {
            menuOpened = true;
        }

    }

    public bool WaitForDialogueFinish()
    {
        return waitForDialogueFinish;
    }

    public bool GetInteracted()
    {
        return interacted;
    }

    public int GetState()
    {
        return state;
    }
}
