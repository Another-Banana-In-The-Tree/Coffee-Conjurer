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

    public void changeGold(float change)
    {
        gold += change;
        UpdateGold();

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
        //float superDelay = 0;
        
        tempScore = correct + (incorrect * (timePenalty / 50));
       
        

        tempScore = Mathf.Clamp(tempScore, -10f, 10f);
        if(reputation > 0)
        {
            tempScore += (Mathf.Log(reputation));
            print("2log(rep) = " + (Mathf.Log(reputation)));
        }

        ChangeRep(tempScore);


        print("rep change: " + tempScore);

        currentScore = tempScore;

        if(currentScore > 0)
        {
            float tempCoinCalc = 0;
            tempCoinCalc = ((tempScore / 3) * Mathf.Log(2+reputation))/4;
            print("log(2+rep) " + Mathf.Log(2 + reputation));
            tempCoinCalc = Mathf.Clamp(tempCoinCalc, 0, 100);
            print("gold earned: " + tempCoinCalc);
            changeGold(tempCoinCalc);
        }

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
