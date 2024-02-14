using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillCup : MonoBehaviour, MiniGame
{

    [SerializeField] private GameObject MiniGameScreen;
    [SerializeField] private GameObject background;

    private Coffee currentCoffee;



     private float fillMod;
    [SerializeField] private float fillSpeed;

    [SerializeField] private float currentFill;

    [SerializeField] private Image fill;
    private bool gameActive = false;

    public void Fill(float value)
    {
        
        fillMod = Mathf.Pow( value, 2);
        print(fillMod);
    }

    private void Update()
    {
        if (!gameActive) return;
        //print(currentCoffee.roast);

        if (currentCoffee.roast != null && currentCoffee.size == null)
        {
            //print("not null");

            if (fill.fillAmount < 1)
            {
               // print("notFull");
                currentFill += (fillMod * fillSpeed) / 100;

                fill.fillAmount = currentFill;
            }
        }
    }

    public void finish()
    {
        if(currentFill > 0.28 && currentFill < 0.38)
        {
            currentCoffee.size = "Small";
        }
        else if (currentFill >= 0.38 && currentFill < 0.71) 
        {
            currentCoffee.size = "Medium";
        }
        else if (currentFill >= 0.71)
        {
            currentCoffee.size = "Large";
        }
        print(currentCoffee.size);
        
    }

    public void Play()
    {

    }
    public void Exit()
    {
        gameActive = false;
        currentFill = 0;
        fill.fillAmount = 0;
        background.SetActive(false);
        MiniGameScreen.SetActive(false);
    }

    public void gameStarted()
    {
        background.SetActive(true);
        MiniGameScreen.SetActive(true);
        gameActive = true;
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
    }
}
