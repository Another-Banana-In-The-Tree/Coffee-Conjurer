using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientChoice : MonoBehaviour, MiniGame
{

    private Coffee currentCoffee;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject backGround;
    [SerializeField] private Oswald oswald;
    public void AddIngredient(string ingredient)
    {
        if (oswald != null && oswald.WaitForDialogueFinish()) return;
        if (oswald != null)
        {
            if (oswald.GetState() != MiniGameNumber() + 1)
            {
                return;
            }
        }
        if (!currentCoffee.stirred && currentCoffee.size !=null)
        {
            currentCoffee.ingredientsUsed.Add(ingredient);
            GameManager.Instance.orderMenu.IngredientInput();
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
    public int MiniGameNumber()
    {
        return 7;
    }
}
