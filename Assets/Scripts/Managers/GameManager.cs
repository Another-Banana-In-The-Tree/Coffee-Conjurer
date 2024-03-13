using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static float reputation;
    private static float gold;
    private List<TargetTap> messes = new List<TargetTap>();
    [SerializeField] private NPCManager npcManager;
    float currentScore = 0;
    [SerializeField] OrderMenu orderMenu;
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

    public void UpdateGold(float change)
    {
        gold += change;

    }

    public void ChangeRep(float change)
    {
        reputation += change;
        UpdateRep();
    }

    private void UpdateRep()
    {
        orderMenu.UpdateRep(reputation);
    } 
    private void UpdateGold()
    {
        orderMenu.UpdateGold(gold);
    }

    
    public void CalculateReputation(int correct, int incorrect, float time)
    {
        float tempScore = 0;

        float timePenalty = time;

        
        tempScore = correct + (incorrect * (timePenalty / 10));
        print("correct: " + correct + " -incorrect: " + incorrect + " * time penalty:" + timePenalty / 10);

        ChangeRep(tempScore);


        print(tempScore);

        currentScore = tempScore;


    }
    
    public float GetScore()
    {
        return currentScore;
    }

    public void RemoveMess(TargetTap removeTagret)
    {
        messes.Remove(removeTagret);
    }
   public void AddMess()
    {

    }
   
}
