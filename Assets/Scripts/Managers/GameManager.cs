using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private static float reputation;
    private List<TargetTap> messes = new List<TargetTap>();
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


    public void ChangeRep(float change)
    {
        reputation += change;
    }

    public void CreateMess()
    {

    }
   
}
