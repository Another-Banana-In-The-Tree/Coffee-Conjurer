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

    private void Update()
    {
       
    }

    private void ChangeRep(float change)
    {
        reputation += change;
        UpdateRep();
    }

    private void UpdateRep()
    {

    }

    
    public void CalculateReputation(int correct, int incorrect, float time)
    {
        float tempScore = 0;

        float timePenalty = time;

        print("time penalty: " + timePenalty / 10);
        tempScore = correct + (incorrect * (timePenalty / 10));

        ChangeRep(tempScore);


        print(tempScore);




    }
    

    public void RemoveMess(TargetTap removeTagret)
    {
        messes.Remove(removeTagret);
    }
   public void AddMess()
    {

    }
   
}
