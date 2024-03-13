using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
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
    [SerializeField] Sprite happyImage;
    [SerializeField] Sprite disappointedImage;
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
        npcAnimation = GetComponent<Animator>(); //Necessary for animations to work
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
                if (!(timeWaiting > patience) && !isSitting) emoteRenderer.enabled = false;
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
        //UGH MAN WTF I CAN'T WORK WITH THIS AM I GETTING STUPIDER OR SOMETHING UGHGHHGHGH
        //CAN I DROP OUT WHILE STILL GIVING EVERYONE MY WORK AND BEING UNCREDITED AND ALSO NOT LOSING MONEY FUCK MAN
        // ლ(⋋_⋌)ლ - Sean
        
        
        //More Bad Animation Code courtesy of yours truly, Sean M
        moveHorizontal = (nextWaypointPos - transform.position).x;
        moveVertical = (nextWaypointPos - transform.position).y;

        if (moveHorizontal > 0.1)
        {
            npcSprite.flipX = false;
            npcAnimation.SetFloat("HoriSpeed", 1);
        } else if (moveHorizontal < 0.1)
        {
            npcSprite.flipX = true;
            npcAnimation.SetFloat("HoriSpeed", 1);
        }

        npcAnimation.SetBool("VertFlip", moveVertical < 0);
        npcAnimation.SetFloat("VertSpeed",moveVertical);
         
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
            emoteRenderer.sprite = disappointedImage;
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
        timeWaiting = 0;
        nextWaypointPos = seatWaypoint.transform.position;
        seatWaypoint.AddCustomer(this);
        isWaiting = true;
        isSitting = true;
        float distance = Vector3.Distance(transform.position, nextWaypointPos);
        if ((distance >= distancePadding)) 
        {
            emoteRenderer.enabled = true;
            Debug.Log(emoteRenderer.enabled);
            float tempScore = GameManager.Instance.GetScore();
            if (tempScore >= 7) emoteRenderer.sprite = happyImage; //Debug.Log("Happy");
            if (tempScore >= 3 && tempScore < 7) emoteRenderer.sprite = disappointedImage; //Debug.Log("Disppointed");
            if (tempScore < 3) emoteRenderer.sprite = angryImage; //Debug.Log("Gross");
        }
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

            CoffeeHandler.Instance.CompareCoffee(timeWaiting);
            EnableMovement();
            SitDown();
            nextWaypoint.RemoveCustomer();
            SetNextPoint();
        }
    }
}
