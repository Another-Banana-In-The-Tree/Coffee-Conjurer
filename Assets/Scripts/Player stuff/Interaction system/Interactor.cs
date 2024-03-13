using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;

public class Interactor : MonoBehaviour
{
    private Vector2 interactPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask interactionLayer;

    private Collider2D[] ObjHit = new Collider2D[3];
    [SerializeField] private int numFound;

    [SerializeField] private GameObject toolTip;

    private Player player;

    

    private void Awake()
    {
        player = GetComponent<Player>();
        interactPoint = transform.position;
    }
    private void Update()
    {
        numFound = Physics2D.OverlapCircleNonAlloc(transform.position, radius, ObjHit, interactionLayer);

        if(numFound > 0)
        {
            toolTip.SetActive(true);
        }
        if(numFound == 0)
        {
            toolTip.SetActive(false);
        }
    }

    public void Active()
    {
        bool interacted = false;
        int loopNum = 0;
        print("active");
        if(numFound > 0)
        {
           // print(numFound);

            foreach(Collider2D i in ObjHit)
            {
                loopNum++;
                if (i != null)
                {
                    IInteractable interactable = null;
                    interactable = i.GetComponent<IInteractable>();

                    if (interactable != null && !interacted)
                    {
                        Coffee currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();


                        if (i.TryGetComponent(out MiniGameTrigger trigger))
                        {
                            //print(trigger.gameObject.name);
                            if (currentCoffee != null)
                            {
                                if (!currentCoffee.stirred)
                                {
                                    //print("trigger Chosen" + loopNum);
                                    interacted = true;
                                    interactable.Interact(player);
                                    //return;
                                }
                            }
                        }
                        if (i.TryGetComponent(out NPC npc))
                        {
                           // print(npc.gameObject.name );
                            if (currentCoffee == null || currentCoffee.name != i.name || (currentCoffee.stirred && npc.GetCurrentWaypoint() == 4))
                            {
                                //print("npc Chosen" + loopNum);
                                interacted = true;
                                interactable.Interact(player);
                                //return;
                            }

                        }


                    }
                }
                //if (interacted) break;
            }
            
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
