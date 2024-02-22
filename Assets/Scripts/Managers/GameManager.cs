using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static float reputation;
    private List<TargetTap> messes = new List<TargetTap>();
    [SerializeField] private NPCManager npcManager;
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }


    private void ChangeRep(float change)
    {
        reputation += change;
    }

    public void CreateMess()
    {

    }

    public void CalculateReputation(int correct, int incorrect)
    {
        float tempScore = 0;

        float timePenalty = npcManager.currentCustomer().GetWaitTime();


        tempScore = correct + (incorrect * (timePenalty / 10));

        print(tempScore);




    }
   
}
