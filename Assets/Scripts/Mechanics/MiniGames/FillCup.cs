using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillCup : MonoBehaviour, MiniGame
{

    [SerializeField] private GameObject MiniGameScreen;

    private Coffee currentCoffee;



    [SerializeField] private float fillMod;
    [SerializeField] private float fillSpeed;

    [SerializeField] private float currentFill;

    [SerializeField] private Image fill;
    private bool gameActive = false;

    public void Fill(float value)
    {
        fillMod = Mathf.Pow( value, 2);
    }

    private void Update()
    {
        if (!gameActive) return;
        if (currentCoffee.roast != null)
        {


            if (fill.fillAmount < 1)
            {
                currentFill += (fillMod * fillSpeed) / 100;

                fill.fillAmount = currentFill;
            }
        }
    }

    public void Play()
    {

    }
    public void Exit()
    {
        gameActive = false;
    }

    public void gameStarted()
    {
        gameActive = true;
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
    }
}
