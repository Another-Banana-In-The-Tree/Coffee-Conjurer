using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Oswald : MonoBehaviour, IInteractable
{
    DialogueTrigger dialogueTrigger;
    [SerializeField] FadeScreen fadeScreen;
    [SerializeField] Player player;
    [SerializeField] Coffee coffee;
    Animator animator;
    public string levelScene;
    public bool hasLeft = false;
    public int state = 0;
    // Start is called before the first frame update
    void Start()
    {
        coffee = new Coffee();
        coffee.size = "Medium";
        coffee.roast = "Dark";
        coffee.ingredientsUsed = new List<string> {"RegMilk", "Vanilla"};
        Debug.Log(coffee.size + " " + coffee.roast + " roast coffee created with " + coffee.ingredientsUsed[0] + " and " + coffee.ingredientsUsed[1]);
        //animator.SetInteger("state", state);
        animator = GetComponent<Animator>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
    }
    public void Interact(Player player)
    {
        Debug.Log("Oswald is Talking");
        dialogueTrigger.Trigger(true);
    }
    public void DialogueFinish()
    {
        Debug.Log("switching state");
        //fadeScreen.FadeOut();
        int currentState = animator.GetInteger("state");
        animator.SetInteger("state", currentState + 1);
        state++;
        if (state == 2) CoffeeHandler.Instance.AddOrder(coffee);
        if (state == 8)
        {
            fadeScreen.FadeOutToLevel(levelScene);
        }
        //dialogueTrigger.SetDialogueList();
    }
}
