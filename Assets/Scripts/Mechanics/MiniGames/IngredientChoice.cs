using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientChoice : MonoBehaviour, MiniGame
{

    private Coffee currentCoffee;
    public void AddIngredient(string ingredient)
    {

        if (!currentCoffee.stirred)
        {
            currentCoffee.ingredientsUsed.Add(ingredient);
        }

    }
    public void Play()
    {

    }
    public void Exit()
    {

    }

    public void gameStarted()
    {
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();

    }
}
