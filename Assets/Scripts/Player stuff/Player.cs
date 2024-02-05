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
