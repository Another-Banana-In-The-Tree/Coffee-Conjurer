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
    [SerializeField] private TextMeshProUGUI roast;
    [SerializeField] private SpriteRenderer beanSprite;

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
            if (fill > minTarget)
            {
                timeCount += 0.2f * Time.deltaTime;

                // pointText.text = timeCount.ToString("F1");

                UpdateCurrentRoast(fill);
                
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

    private void UpdateCurrentRoast(float filling)
    {
        points.Add(filling);
    }
    public void Play()
    {
        if (currentCoffee.roast == null)
        {
           // if (fill >= 1) return;
            AddMore();
        }
    }

    private void AddMore()
    {
        fill += clickBonus;
        Mathf.Clamp(fill, 0f, 1f);
            float tempSum = 0;
        foreach (float i in points)
        {
            tempSum += i;
        }

        tempSum = tempSum / points.Count;

        beanSprite.color = Color.Lerp(Color.white, Color.black, tempSum);

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
        

        if( tempSum < 0.69)
        {
            currentCoffee.roast = "Light";
            print("light");
        }
        else if(tempSum > 0.7 && tempSum <= 0.82 )
        {
            currentCoffee.roast = "Medium";
            print("Medium");
        }
        else if (tempSum >= 0.83)
        {
            currentCoffee.roast = "Dark";
            print("Dark");
        }
        if (currentCoffee.roast != null)
        {
            timeCount = 0;
            fill = 0;
            points.Clear();
            roast.text = currentCoffee.roast;
        }
        

    }
    

    public void Exit()
    {
        roast.text = " ";
        fill = 0;
        timeCount = 0;
        points.Clear();
        isPlaying = false;
        canvas.SetActive(false);
        screen.SetActive(false);
        PlayerInput.EnableGame();
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
