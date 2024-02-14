using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static Controls _controls;

    public static void Init(Player player)
    {
        _controls = new Controls();

        _controls.Game.Movement.performed += ctx =>
        {
            player.SetMovementDir(ctx.ReadValue<Vector2>());
        };

        _controls.Game.Interaction.performed += ctx =>
        {
            player.Interact();
        };

        _controls.MiniGame.Interact.performed += ctx =>
        {
            player.PlayMiniGame(); 
        };

        _controls.MiniGame.Exit.performed += ctx =>
        {
            player.ExitMiniGame();  
        };

        _controls.DevTool.SetRoast.performed += ctx =>
        {

            switch (ctx.ReadValue<float>())
            {
                case 1:
                    CoffeeHandler.Instance.GetCurrentCoffee().roast = "Light";
                    break;
                case 2:
                    CoffeeHandler.Instance.GetCurrentCoffee().roast = "Medium";
                    break;
                case 3:
                    CoffeeHandler.Instance.GetCurrentCoffee().roast = "Dark";
                    break;
            }
            print(CoffeeHandler.Instance.GetCurrentCoffee().roast);
            
        };
        _controls.DevTool.SetSize.performed += ctx =>
        {

            switch (ctx.ReadValue<float>())
            {
                case 1:
                    CoffeeHandler.Instance.GetCurrentCoffee().size = "Small";
                    break;
                case 2:
                    CoffeeHandler.Instance.GetCurrentCoffee().size = "Medium";
                    break;
                case 3:
                    CoffeeHandler.Instance.GetCurrentCoffee().size = "Large";
                    break;
            }
            print(CoffeeHandler.Instance.GetCurrentCoffee().size);

        };

        _controls.DevTool.CompareCoffee.performed += ctx => 
         {
             CoffeeHandler.Instance.CompareCoffee();
        };



        _controls.DevTool.Enable();
    }

    public static void EnableGame()
    {
        _controls.Game.Enable();
        _controls.MiniGame.Disable();
    }

    public static void EnableMinigame()
    {
        _controls.Game.Disable();
        _controls.MiniGame.Enable();
    }
}
