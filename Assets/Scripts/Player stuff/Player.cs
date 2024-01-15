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
    private void Awake()
    {
        PlayerInput.Init(this);
        PlayerInput.EnableGame();

        rb = GetComponent<Rigidbody2D>();
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
        Debug.Log("test");
    }
}
