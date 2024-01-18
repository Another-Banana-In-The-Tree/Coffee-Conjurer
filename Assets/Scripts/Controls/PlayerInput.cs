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
