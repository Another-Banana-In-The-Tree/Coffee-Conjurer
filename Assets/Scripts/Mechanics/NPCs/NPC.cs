using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
//using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
//using UnityEditor.Timeline;
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
    private bool isLeaving = false;

    [SerializeField] Sprite talkingImage;
    [SerializeField] Sprite happyImage;
    [SerializeField] Sprite disappointedImage;
    [SerializeField] Sprite angryImage;
    [SerializeField] Sprite coffeeImage;
    [SerializeField] SpriteRenderer emoteRenderer;

    [SerializeField] string customerName = "";
    public enum CoffeeSize
    {
        Small,
        Medium,
        Large,
    }
    [SerializeField] CoffeeSize coffeeSize;
    public enum CoffeeRoast
    {
        Light,
        Medium,
        Dark
    }
    [SerializeField] CoffeeRoast coffeeRoast;
    public enum Ingredients
    {
        RegMilk,
        DMilk,
        VegetableMilk,
        Vanilla,
        Honey,
        Blood,
        Cinnamon,
        CaramelDrizzle,
        YetiTears
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
    DialogueTrigger DT;
    [SerializeField]DialogueObject dialogueObject;
    
    private void Start()
    {
        npcManager = FindAnyObjectByType<NPCManager>();
        waypoints = npcManager.GetWaypoints();
        exitWaypoint = npcManager.GetExitWaypoint();
        //Animation
        npcAnimation = GetComponent<Animator>();
       
        coffee = new Coffee(); //Preload coffee here

        //Load Coffee based on defined parameters
        coffee.name = customerName;
        coffee.size = coffeeSize.ToString();
        coffee.roast = coffeeRoast.ToString();
        foreach (Ingredients ingredient in ingredients)
        {
            coffee.ingredientsUsed.Add(ingredient.ToString());
        }
        DT =  gameObject.GetComponent<DialogueTrigger>();
        if(dialogueObject != null)
        {
            DT.SetDialogueList(dialogueObject.GetDialogueStrings());
        }
       
    }

    private void Update()
    {
        if (isWaiting)
        {
            timeWaiting += Time.deltaTime;
        }
        if (timeWaiting > patience && nextWaypoint == waypoints[1] && !isLeaving)
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
                //print("moving" + name);
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
        
        //More Bad Animation Code courtesy of yours truly, Sean M
        moveHorizontal = nextWaypointPos.x;
        moveVertical = nextWaypointPos.y;
        //Debug.Log("THE POWER OF NEXTWAYPOINTPOS IS  X:" +moveHorizontal + "  Y:" + moveVertical);

        if (npcAnimation != null)
        {
            if (isMoving)
            {
                if (moveHorizontal > 0.5)
                {

                    npcAnimation.SetBool("HoriFlip", true);
                    npcAnimation.SetFloat("HoriSpeed", 1);

                }
                else if (moveHorizontal < 0.5)
                {

                    npcAnimation.SetBool("HoriFlip", false);
                    npcAnimation.SetFloat("HoriSpeed", 1);


                }
                else
                {
                    npcAnimation.SetFloat("HoriSpeed", 0);
                }

                if (moveVertical > 0.5)
                {

                    npcAnimation.SetBool("VertFlip", true);
                    npcAnimation.SetFloat("VertSpeed", 1);

                }
                else if (moveVertical < 0.5)
                {

                    npcAnimation.SetBool("VertFlip", false);
                    npcAnimation.SetFloat("VertSpeed", 1);

                }
                else
                {
                    npcAnimation.SetFloat("VertSpeed", 0);
                }
            }
            else
            {
                npcAnimation.SetFloat("HoriSpeed", 0);
                npcAnimation.SetFloat("VertSpeed", 0);
            }
        }


        //npcAnimation.SetBool("HoriFlip", true);
        //npcSprite.flipX = false;
        
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
                print(nextWaypoint.GetLineLength());
                if (nextWaypoint.GetLineLength() == 1)
                {
                    SetInteractable();
                }
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

        FindObjectOfType<NPCManager>().UpdateCustomersInStore();
        isLeaving = true;
        
        isMoving = true;
        if (isMad)
        {
            emoteRenderer.enabled = true;
            emoteRenderer.sprite = disappointedImage;
            if (nextWaypoint == waypoints[1]) nextWaypointPos = exitWaypoint.transform.position;
            nextWaypoint.RemoveCustomer(this);

        }
        else
        {
            nextWaypointPos = exitWaypoint.transform.position;
        }
        
    }
    public void UpdateLine(int offset)
    {
        
        isMoving = true;
        isUpdatingLine = true;
        
            if (nextWaypoint.GetIsVeritcal())
            {
                nextWaypointPos = waypoints[3].transform.position + new Vector3(0,-nextWaypoint.GetLineLength()+offset, 0);
            }
            else
            {
            //print("horizontal");
            nextWaypointPos = waypoints[1].transform.position + new Vector3((nextWaypoint.GetLineLength() - offset), 0, 0);
            }

            if((nextWaypoint.GetLineLength() - offset) == 0 && !isInteractable)
             {

                 SetInteractable();
             }

        
    }
    public void SitDown()
    {
        timeWaiting = 0;
        nextWaypointPos = seatWaypoint.transform.position;
       // seatWaypoint.AddCustomer(this);
        isWaiting = true;
        isSitting = true;
        float distance = Vector3.Distance(transform.position, nextWaypointPos);
        if ((distance >= distancePadding)) 
        {
            emoteRenderer.enabled = true;
            //Debug.Log(emoteRenderer.enabled);
            float tempScore = GameManager.Instance.GetScore();
            if (tempScore >= 7) emoteRenderer.sprite = happyImage; FindObjectOfType<AudioManager>().Play("Done Well");//Debug.Log("Happy");
            if (tempScore >= 3 && tempScore < 7) emoteRenderer.sprite = disappointedImage; FindObjectOfType<AudioManager>().Play("Done Poor"); //Debug.Log("Disppointed");
            if (tempScore < 3) emoteRenderer.sprite = angryImage; FindObjectOfType<AudioManager>().Play("Guest Unsatisfied");//Debug.Log("Gross");
        }
    }
    public void FinishVisit()
    {
        LeaveStore(false);
       
        isWaiting = false;
        isSitting = false;
    }
    public int GetCurrentWaypoint()
    {
       // print("get current waypoint: " + _nextWaypointCounter);
        return _nextWaypointCounter;
    }
   
    //Enable interactivity
    public void SetInteractable()
    {
        isInteractable = !isInteractable;
        print(name + isInteractable);
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
    public void DialogueFinish()
    {
        //print(name + "finish dialogue");
        if (isMoving) return;
        isWaiting = true;
        EnableMovement();
        nextWaypoint.RemoveCustomer(this);
        SetNextPoint();
    }
    public void Interact(Player player)
    {
        //Debug.Log("Interacted!");
        if (!isInteractable) return;
        SetInteractable();
        if (nextWaypoint == waypoints[1])
        {
            isWaiting = false;
            DT.Trigger(false);
            //remember to chhange iswaiting to true when dialogue finishes (for later)
            //Debug.Log("Coffee Added!");
            //Show sprite image above head based on certain variables
            emoteRenderer.enabled = true;
            emoteRenderer.sprite = talkingImage;

            //Add coffee order and re-enable line movement
            CoffeeHandler.Instance.AddOrder(coffee);
            
            
        }
        if (nextWaypoint == waypoints[3])
        {
            //Show sprite image above head based on certain variables
            isWaiting = false;

            CoffeeHandler.Instance.CompareCoffee(timeWaiting);
            EnableMovement();
            SitDown();
            nextWaypoint.RemoveCustomer(this);
            SetNextPoint();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "GuestTrigger")
        {
            FindObjectOfType<AudioManager>().Play("Bell");
            Debug.Log("Trigger works");
        }
    }
}
