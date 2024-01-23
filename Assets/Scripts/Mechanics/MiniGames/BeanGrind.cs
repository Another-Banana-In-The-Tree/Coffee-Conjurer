using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanGrind : MonoBehaviour, MiniGame 
{
    [SerializeField] private Image fillBar;
    private float fill = 0;
    [SerializeField] private float timePenalty;
    

    private void Update()
    {
        if(fill >= 1)
        {
            Exit();
            return;
        }
        
        fill -= timePenalty  * Time.deltaTime;

        fillBar.fillAmount = fill;
    }
    public void Play()
    {

        AddMore();
    }

    private void AddMore()
    {
        fill += 0.1f;
    }

    

    public void Exit()
    {
        Debug.Log("Game Won"); 
    }
}
