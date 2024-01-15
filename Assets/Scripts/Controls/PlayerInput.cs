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
    }

    public static void EnableGame()
    {
        _controls.Game.Enable();
    }
}
