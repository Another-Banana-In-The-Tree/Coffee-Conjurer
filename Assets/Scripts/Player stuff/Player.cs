using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private OrderMenu menu;

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
    float timer = 0.5f;
    float footDelay;

    private Oswald oswald;
    public bool isTutorial = false;
    [SerializeField] GameObject controlsTooltip;
    bool hasMoved = false;
    bool updatedTooltips = false;

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
        menu = GameManager.Instance.orderMenu;

        footDelay = audio.GetAudioLength("Walking");
        oswald = FindObjectOfType<Oswald>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (moveDir.magnitude !=0)
        {
            if (!hasMoved) hasMoved = true;
            
            if (timer > footDelay + 0.01f)
            {
                audio.Play("Walking");
                timer = 0;
            }
        }
        if (isTutorial && hasMoved && !updatedTooltips)
        {
            controlsTooltip.GetComponent<Animator>().SetBool("isHidden", true);
            updatedTooltips = true;
        }
        //playerSprite.sortingOrder = 8 - (int)Mathf.Clamp(transform.position.y, -2f, 7f);
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
        menu.UpdateCompletion();
        currentGame.Exit();
        currentGame = null;
        
    }

    public void StartMinigame(MiniGame newGame)
    {
        PlayerInput.EnableMinigame();
        if (currentGame != null)
        {
            
            currentGame = null;
        }
        Debug.Log("setgame");
        currentGame = newGame;
        currentGame.gameStarted();
        if (isTutorial)
        {
            oswald.MiniGameOpened();
        }
        
    }
    public MiniGame GetMinigame()
    {
        return currentGame;
    }
}
