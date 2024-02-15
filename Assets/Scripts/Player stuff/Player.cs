using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //MOVEMENT
    [field: Header("Movement")]
    [SerializeField] private float speed;
    private Vector2 moveDir;

    //Components
     private Rigidbody2D rb;
    private Interactor interactor;

    //MINIGAMES
    private MiniGame currentGame;

    //ANIMATION
    [SerializeField] private Animator playerAnimation;
    float moveHorizontal;
    float moveVertical;
    float moveHorizontalAbs;
    float moveVerticalAbs;
    private void Awake()
    {
        PlayerInput.Init(this);
        PlayerInput.EnableGame();

        rb = GetComponent<Rigidbody2D>();
        interactor = GetComponent<Interactor>();
    }
    void Start()
    {
       


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMovementDir(Vector2 Dir)
    {
        moveDir = Dir.normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(moveDir * speed, ForceMode2D.Impulse);
        
        //Horrific Disgusting and Borderline Criminal Code Below - Sean
        moveHorizontal = moveDir.x;
        moveVertical = moveDir.y;
        moveHorizontalAbs = Mathf.Abs(moveHorizontal);
        moveVerticalAbs = Mathf.Abs(moveVertical);
        
        
        
        if (moveHorizontalAbs != 0)
        {
            if (moveHorizontal < 0)
            {
                playerAnimation.SetBool("HoriFlip", true);
            }
            else
            {
                playerAnimation.SetBool("HoriFlip", false);
            }
            playerAnimation.SetFloat("HoriSpeed",moveHorizontalAbs);
            
        } else if (moveVerticalAbs != 0)
        {
            if (moveVertical < 0)
            {
                playerAnimation.SetBool("VertFlip", true);
            }
            else
            {
                playerAnimation.SetBool("VertFlip", false);
            }
            playerAnimation.SetFloat("VertSpeed",moveVerticalAbs);
            
        }
        else
        {
            
            playerAnimation.SetFloat("HoriSpeed",0);
            playerAnimation.SetFloat("VertSpeed",0);
        }
    }

    public void Interact()
    {
        interactor.Active();
    }

    public void PlayMiniGame()
    {
        currentGame.Play();  
    }

    public void ExitMiniGame()
    {
        PlayerInput.EnableGame();
        currentGame.Exit();
        
    }

    public void StartMinigame(MiniGame newGame)
    {
        Debug.Log("setgame");
        currentGame = newGame;
        PlayerInput.EnableMinigame();
    }
}
