using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanGrind : MonoBehaviour, MiniGame 
{
    [SerializeField] private Image fillBar;
    private float fill = 0;
    [SerializeField] private float timePenalty;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject screen;
    private bool isPlaying = false;

    private void Update()
    {
        if(fill >= 1 && isPlaying)
        {
            isPlaying = false;
            
        }
        if (isPlaying && fill > 0)
        {


            fill -= timePenalty * Time.deltaTime;

            
        }
        if(fill == 0)
        {
            fill = 0;
        }
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
        fill = 0;
        isPlaying = false;
        canvas.SetActive(false);
        screen.SetActive(false);
    }

    public void gameStarted()
    {
        isPlaying = true;
        canvas.SetActive(true);
        screen.SetActive(true);
    }
}
