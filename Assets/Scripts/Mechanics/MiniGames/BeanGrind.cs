using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BeanGrind : MonoBehaviour, MiniGame 
{
    [SerializeField] private Image fillBar;
    [SerializeField]private float fill = 0;
    [SerializeField] private float timePenalty;
    [SerializeField] private float clickBonus;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject screen;
    private bool isPlaying = false;
    [SerializeField] private float minTarget;
    [SerializeField] private float maxTarget;
    [SerializeField] private float points;
    [SerializeField] private TextMeshProUGUI pointText;
   
    private void Update()
    {
        if (isPlaying)
        {
            if (points >= 1)
            {
                isPlaying = false;
            }
            if (fill > minTarget && fill < maxTarget)
            {
                points += 0.1f * Time.deltaTime;
                pointText.text = points.ToString();

            }
            if (isPlaying && fill > 0)
            {


                fill -= timePenalty * Time.deltaTime;


            }
            if (fill < 0)
            {
                fill = 0;
            }
            fillBar.fillAmount = fill;
        }
    }

    
    public void Play()
    {

        AddMore();
    }

    private void AddMore()
    {
        fill += clickBonus;
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
