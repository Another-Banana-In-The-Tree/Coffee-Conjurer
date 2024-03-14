using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientChoice : MonoBehaviour, MiniGame
{

    private Coffee currentCoffee;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject backGround;
    public void AddIngredient(string ingredient)
    {

        if (!currentCoffee.stirred && currentCoffee.size !=null)
        {
            currentCoffee.ingredientsUsed.Add(ingredient);
        }

    }
    public void Play()
    {

    }
    public void Exit()
    {
        screen.SetActive(false);
        backGround.SetActive(false);
        PlayerInput.EnableGame();
    }

    public void gameStarted()
    {
        backGround.SetActive(true);
        screen.SetActive(true);
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();

    }
}
