using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject minigameScreen;
    [SerializeField] private MiniGame game;

    private void Awake()
    {
        game = minigameScreen.GetComponent<MiniGame>();
    }
    public void Interact(Player player)
    {
        Debug.Log("minigametrigger");

        minigameScreen.SetActive(true);  
        player.StartMinigame(game);
        
    }
}
