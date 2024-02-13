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
        if(numFound > 0)
        {
            var interactable = ObjHit[0].GetComponent<IInteractable>();
            
            if(interactable != null)
            {
                interactable.Interact(player);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
