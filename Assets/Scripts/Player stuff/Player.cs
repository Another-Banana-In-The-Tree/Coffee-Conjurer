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
    private AudioManager audio;

    //MINIGAMES
    private MiniGame currentGame;

    //ANIMATION
    private Animator playerAnimation;
    [SerializeField] private SpriteRenderer playerSprite;
    float moveHorizontal;
    float moveVertical;
    float moveHorizontalAbs;
    float moveVerticalAbs;
    
    
    private void Awake()
    {
        PlayerInput.Init(this);
        PlayerInput.EnableGame();

        playerAnimation = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        interactor = GetComponent<Interactor>();
        audio = FindObjectOfType<AudioManager>();
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

        if (moveHorizontalAbs * -1 > 0)
        {
            moveHorizontalAbs = moveHorizontalAbs * -1;
            FindObjectOfType<AudioManager>().Play("Walking");
        }
        
        if (moveVerticalAbs * -1 > 0)
        {
            moveVerticalAbs = moveVerticalAbs * -1;
        }
        
        if (moveHorizontalAbs != 0)
        {
            if (moveHorizontal < 0)
            {
                playerAnimation.SetBool("HoriFlip", true);
                playerSprite.flipX = false;
            }
            else
            {
                playerSprite.flipX = true;
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
        SetMovementDir(Vector2.zero);
        interactor.Active();
        audio.Play("Select");
    }

    public void PlayMiniGame()
    {
        currentGame.Play();  
    }

    public void ExitMiniGame()
    {
        //PlayerInput.EnableGame();
        
        currentGame.Exit();
        currentGame = null;
        
    }

    public void StartMinigame(MiniGame newGame)
    {
       
        if(currentGame != null)
        {
            
            currentGame = null;
        }
        Debug.Log("setgame");
        currentGame = newGame;
        currentGame.gameStarted();
        PlayerInput.EnableMinigame();
    }
}
