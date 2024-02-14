using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pressure : MonoBehaviour, MiniGame 
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

    private Coffee currentCoffee;
    




    [SerializeField] private float timeCount;
    [SerializeField] private TextMeshProUGUI pointText;

    private List<float> points = new List<float>();
   
    private void Update()
    {
        if (isPlaying)
        {
            if (timeCount >= 1)
            {
                GameEnd();
                isPlaying = false;
            }
            if (fill > minTarget && fill < maxTarget)
            {
                timeCount += 0.1f * Time.deltaTime;
                pointText.text = timeCount.ToString();

                points.Add(fill);

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
        if (currentCoffee.roast == null)
        {
            AddMore();
        }
    }

    private void AddMore()
    {
        fill += clickBonus;
    }

    private void GameEnd()
    {
        float tempSum = 0;
        foreach(float i in points)
        {
            tempSum += i;
        }

        tempSum = tempSum / points.Count;

        print("you scored an average of: " +  tempSum);
        

        if(tempSum > 0.49 && tempSum < 0.69)
        {
            currentCoffee.roast = "light";
            print("light");
        }
        else if(tempSum > 0.7 && tempSum <= 0.82 )
        {
            currentCoffee.roast = "medium";
            print("Medium");
        }
        else if (tempSum >= 0.83)
        {
            currentCoffee.roast = "dark";
            print("Dark");
        }
        timeCount = 0;
        fill = 0;
        points.Clear();
       

    }
    

    public void Exit()
    {
        fill = 0;
        timeCount = 0;
        points.Clear();
        isPlaying = false;
        canvas.SetActive(false);
        screen.SetActive(false);

       // CoffeeHandler.Instance.testSpecificCoffee(currentCoffee.name);
    }

    public void gameStarted()
    {
        isPlaying = true;
        canvas.SetActive(true);
        screen.SetActive(true);
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
    }
}
