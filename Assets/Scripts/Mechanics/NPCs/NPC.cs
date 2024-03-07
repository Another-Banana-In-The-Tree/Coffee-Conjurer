using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour, IInteractable
{
    Coffee coffee;
    bool isInteractable = false;
    float timeWaiting = 0; //Time waiting since entering store
    [SerializeField] float patience = 0; //How much time they'll wait before becoming mad
    [SerializeField] float timeToMove; //Time it takes to move towards the given waypoint
    private bool isWaiting = false;
    [SerializeField] Sprite talkingImage;
    [SerializeField] Sprite angryImage;
    [SerializeField] Sprite coffeeImage;
    [SerializeField] SpriteRenderer emoteRenderer;

    [SerializeField] string customerName = "";
    public enum CoffeeSize
    {
        small,
        medium,
        large,
    }
    [SerializeField] CoffeeSize coffeeSize;
    public enum CoffeeRoast
    {
        light,
        medium,
        dark
    }
    [SerializeField] CoffeeRoast coffeeRoast;
    public enum Ingredients
    {
        RegMilk,
        DMilk,
        Vanilla,
        Honey,
        Blood,
        Cinnamon
    }
    [SerializeField] Ingredients[] ingredients;

    [SerializeField] LineWaypoint seatWaypoint;
    LineWaypoint currentWaypoint;
    LineWaypoint nextWaypoint;
    int _nextWaypointCounter = 0;

    NPCManager npcManager;

    //ANIMATION CODE - EDIT AT THE RISK OF BREAKING ALL OF THE ANIMATION
    private Animator npcAnimation;
    [SerializeField] private SpriteRenderer npcSprite;
    float moveHorizontal;
    float moveVertical;
    float moveHorizontalAbs;
    float moveVerticalAbs;


    private void Awake()
    {
        npcManager = FindAnyObjectByType<NPCManager>();
        coffee = new Coffee(); //Preload coffee here

        coffee.name = customerName;
        coffee.size = coffeeSize.ToString();
        coffee.roast = coffeeSize.ToString();
        foreach (Ingredients ingredient in ingredients)
        {
            coffee.ingredientsUsed.Add(ingredient.ToString());
        }
        
    }
    private void Start()
    {
        //Set the next waypoint to the nextWaypointCounter, which starts with first waypoint in the list
        nextWaypoint = npcManager.GetWaypoints()[_nextWaypointCounter];
       
    }

    private void Update()
    {
        if (isWaiting)
        {
            timeWaiting += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        
        //More Bad Animation Code courtesy of yours truly, Sean M
        //moveHorizontal = moveDir.x;
        //moveVertical = moveDir.y;
        moveHorizontalAbs = Mathf.Abs(moveHorizontal);
        moveVerticalAbs = Mathf.Abs(moveVertical);

        if (moveHorizontalAbs * -1 > 0)
        {
            moveHorizontalAbs = moveHorizontalAbs * -1;
        }
        
        if (moveVerticalAbs * -1 > 0)
        {
            moveVerticalAbs = moveVerticalAbs * -1;
        }
        
        if (moveHorizontalAbs != 0)
        {
            if (moveHorizontal < 0)
            {
                npcAnimation.SetBool("HoriFlip", true);
                npcSprite.flipX = false;
            }
            else
            {
                npcSprite.flipX = true;
                npcAnimation.SetBool("HoriFlip", false);
            }
            npcAnimation.SetFloat("HoriSpeed",moveHorizontalAbs);
            
        } else if (moveVerticalAbs != 0)
        {
            if (moveVertical < 0)
            {
                npcAnimation.SetBool("VertFlip", true);
            }
            else
            {
                npcAnimation.SetBool("VertFlip", false);
            }
            npcAnimation.SetFloat("VertSpeed",moveVerticalAbs);
            
        }
        else
        { 
            npcAnimation.SetFloat("HoriSpeed",0);
            npcAnimation.SetFloat("VertSpeed",0);
        }
    }


    //Move the NPC towards a given LineWaypoint by the speed defined
    public IEnumerator MoveToWaypoint(LineWaypoint waypoint)
    {
        emoteRenderer.enabled = false;
        Vector3 startPosition = transform.position;
        float startTime = Time.timeSinceLevelLoad;
        while (Time.timeSinceLevelLoad - startTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPosition, waypoint.GetPosition(), (Time.timeSinceLevelLoad - startTime)/timeToMove);
            yield return null;
        }
        Debug.Log("Move to Waypoint Coroutine Ended");
        currentWaypoint = nextWaypoint;
        if (_nextWaypointCounter < npcManager.GetWaypoints().Length - 1) _nextWaypointCounter += 1;
        nextWaypoint = npcManager.GetWaypoints()[_nextWaypointCounter];
    }
    public LineWaypoint GetCurrentWaypoint()
    {
        return currentWaypoint;
    }
    public void SetCurrentWaypoint(LineWaypoint waypoint)
    {
        currentWaypoint = waypoint;
    }
    public LineWaypoint GetNextWaypoint()
    {
        return nextWaypoint;
    }
    public void SetNextWaypoint(LineWaypoint waypoint)
    {
        nextWaypoint = waypoint;
    }
    //Enable interactivity
    public void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }
    public float GetWaitTime()
    {
        return timeWaiting;
    }

    public void Interact(Player player)
    {
       // if (isInteractable)
        //{
            Debug.Log("Interacted!");
            if (currentWaypoint == npcManager._orderWaypoint)
            {
                //Show sprite image above head based on certain variables
                emoteRenderer.enabled = true;
                emoteRenderer.sprite = talkingImage;
                CoffeeHandler.Instance.AddOrder(coffee);
                isWaiting = true;
            }
            if (currentWaypoint == npcManager._pickupWaypoint)
            {
            //Show sprite image above head based on certain variables
                isWaiting = false;
                emoteRenderer.enabled = true;
                emoteRenderer.sprite = angryImage;

                CoffeeHandler.Instance.CompareCoffee();
            }
       // }
    }
}
