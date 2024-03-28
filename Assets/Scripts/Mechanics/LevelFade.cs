using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFade : MonoBehaviour
{
    Animator animator;
    [SerializeField] NPCManager npcManager;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (npcManager != null)
        {
            if (npcManager.GetCustomersLeft() == 0) animator.SetBool("isLevelEnd", true);
        }
    }
}
