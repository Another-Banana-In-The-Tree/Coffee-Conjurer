using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    Coffee coffee;
    bool isInteractable = false;
    float timeWaiting = 0; //Time waiting since entering store
    float patience = 0; //How much time they'll wait before becoming mad

    private void Awake()
    {
        coffee = new Coffee(); //Preload coffee here
    }

    void Update()
    {
        //Enter store and walk towards the current line
        //Make sure the NPC lines up behind any currently lined up NPCs
        //Once the NPC is at the front of the line, make them interactable
        //NPC orders the coffee defined above
    }

    public void Interact(Player player)
    {
        Debug.Log("Interacted!");
    }
}
