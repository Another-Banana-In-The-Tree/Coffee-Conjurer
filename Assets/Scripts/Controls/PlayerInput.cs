using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private static Controls _controls;
    private DialogueManager dialogue;
    public static bool isPaused = false;
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

        _controls.DevTool.CreateCoffee.performed += ctx =>
        {
            CoffeeHandler.Instance.CreateNewCoffee("test");
        };

        _controls.DevTool.CompareCoffee.performed += ctx => 
         {
             CoffeeHandler.Instance.PrintCurrentCoffee();
        };

        _controls.Dialogue.Skip.performed += ctx =>
        {
            DialogueManager.Instance.SkipDialogue();
        };


        _controls.MenuControls.Pause.performed += ctx =>
        {
            if (!isPaused)
            {
                
                Time.timeScale = 0;
                Settings.Instance.gameObject.SetActive(true);
            }
            else
            {
                
                Time.timeScale = 1;
                Settings.Instance.gameObject.SetActive(false);
            }
            isPaused = !isPaused;
        };

        _controls.MenuControls.Enable();
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
    public static void DisableGame()
    {
        _controls.Game.Disable();
    }
    public static void DialogueMode()
    {
        _controls.Dialogue.Enable();
        _controls.Game.Disable();
        _controls.MiniGame.Disable();
    }
    public static void EndDialogueMode()
    {
        _controls.Dialogue.Disable();
        _controls.Game.Enable();
    }
}
