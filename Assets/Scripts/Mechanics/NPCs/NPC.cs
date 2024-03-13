using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UIElements;

public class NPC : MonoBehaviour, IInteractable
{
    Coffee coffee;
    bool isInteractable = false;
    [SerializeField] bool isMoving = false;
    [SerializeField] float speed = 5;
    float timeWaiting = 0; //Time waiting since entering store
    [SerializeField] float patience = 0; //How much time they'll wait before becoming mad
    [SerializeField] float timeInStore = 0; //Time the customer will spend sitting in the store
    private bool isWaiting = false;
    private bool isSitting = false;
    private bool isUpdatingLine = false;

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
    Vector3 nextWaypointPos;
    int _nextWaypointCounter = 0;
    [SerializeField] float distancePadding = 0.5f;

    NPCManager npcManager;
    LineWaypoint[] waypoints;
    LineWaypoint exitWaypoint;

    //ANIMATION CODE - EDIT AT THE RISK OF BREAKING ALL OF THE ANIMATION
    private Animator npcAnimation;
    [SerializeField] private SpriteRenderer npcSprite;
    float moveHorizontal;
    float moveVertical;
    float moveHorizontalAbs;
    float moveVerticalAbs;


    private void Start()
    {
        npcManager = FindAnyObjectByType<NPCManager>();
        waypoints = npcManager.GetWaypoints();
        exitWaypoint = npcManager.GetExitWaypoint();

       
        coffee = new Coffee(); //Preload coffee here

        //Load Coffee based on defined parameters
        coffee.name = customerName;
        coffee.size = coffeeSize.ToString();
        coffee.roast = coffeeSize.ToString();
        foreach (Ingredients ingredient in ingredients)
        {
            coffee.ingredientsUsed.Add(ingredient.ToString());
        }
    }

    private void Update()
    {
        if (isWaiting)
        {
            timeWaiting += Time.deltaTime;
        }
        if (!isSitting && timeWaiting > patience)
        {
            LeaveStore(true);
        }
        if (isSitting && timeWaiting > timeInStore)
        {
            FinishVisit();
        }
        if (isMoving)
        {
            float distance = Vector3.Distance(transform.position, nextWaypointPos);
            if ((distance >= distancePadding))
            {
                print("moving" + name);
                if (!(timeWaiting > patience)) emoteRenderer.enabled = false;
                transform.position = Vector3.Lerp(transform.position, nextWaypointPos, speed * Time.deltaTime);
            }
            else
            {
               // if (isUpdatingLine) isUpdatingLine = false;
                if (nextWaypoint.GetIsLine())
                {
                    DisableMovement();
                }
                else
                {
                    SetNextPoint();
                }
            }
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
    public void SetNextPoint()
    {
        if (_nextWaypointCounter < waypoints.Length)
        {
            nextWaypoint = waypoints[_nextWaypointCounter];
            if (nextWaypoint.GetIsLine())
            {
                if (nextWaypoint.GetIsVeritcal())
                {
                    nextWaypointPos = nextWaypoint.transform.position + new Vector3(0, -nextWaypoint.GetLineLength(), 0);
                }
                else
                {
                    nextWaypointPos = nextWaypoint.transform.position + new Vector3(nextWaypoint.GetLineLength(), 0, 0);
                }
                nextWaypoint.AddCustomer(this);
                isWaiting = true;
            }
            else
            {
                nextWaypointPos = nextWaypoint.transform.position;
                isWaiting = false;
                timeWaiting = 0;
            }
             _nextWaypointCounter++;
            
        }
    }
    public void LeaveStore(bool isMad)
    {
        print("Leaving...");
        isMoving = true;
        if (isMad)
        {
            emoteRenderer.enabled = true;
            emoteRenderer.sprite = angryImage;
            if (nextWaypoint == waypoints[1]) nextWaypointPos = exitWaypoint.transform.position;
            nextWaypoint.RemoveCustomer();
        }
        else
        {
            nextWaypointPos = exitWaypoint.transform.position;
        }
    }
    public void UpdateLine()
    {
        isMoving = true;
        isUpdatingLine = true;
        if (nextWaypoint.GetIsLine())
        {
            if (nextWaypoint.GetIsVeritcal())
            {
                nextWaypointPos = nextWaypoint.transform.position + new Vector3(0,-nextWaypoint.GetLineLength()+1, 0);
            }
            else
            {
                nextWaypointPos = nextWaypoint.transform.position + new Vector3(nextWaypoint.GetLineLength() - 1, 0, 0);
            }
           
        }
    }
    public void SitDown()
    {
        nextWaypointPos = seatWaypoint.transform.position;
        seatWaypoint.AddCustomer(this);
        isWaiting = true;
        isSitting = true;
    }
    public void FinishVisit()
    {
        LeaveStore(false);
        seatWaypoint.RemoveCustomer();
        isWaiting = false;
        isSitting = false;
    }
    public int GetCurrentWaypoint()
    {
        print("get current waypoint: " + _nextWaypointCounter);
        return _nextWaypointCounter;
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
    public void EnableMovement()
    {
        isMoving = true;
    }
    public void DisableMovement()
    {
        isMoving = false;
    }
    public void Interact(Player player)
    {
        Debug.Log("Interacted!");
        if (nextWaypoint == waypoints[1])
        {
            Debug.Log("Coffee Added!");
            //Show sprite image above head based on certain variables
            emoteRenderer.enabled = true;
            emoteRenderer.sprite = talkingImage;

            //Add coffee order and re-enable line movement
            CoffeeHandler.Instance.AddOrder(coffee);
            isWaiting = true;
            EnableMovement();
            nextWaypoint.RemoveCustomer();
            SetNextPoint();
        }
        if (nextWaypoint == waypoints[3])
        {
            //Show sprite image above head based on certain variables
            isWaiting = false;
            emoteRenderer.enabled = true;
            emoteRenderer.sprite = angryImage;

            CoffeeHandler.Instance.CompareCoffee(timeWaiting);
            EnableMovement();
            SitDown();
            nextWaypoint.RemoveCustomer();
            SetNextPoint();
        }
    }
}
