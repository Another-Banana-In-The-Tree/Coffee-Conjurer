using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCManager : MonoBehaviour, IInteractable
{
    Coffee coffee;
    LineWaypoint currentLine;
    bool isInteractable = false;
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
