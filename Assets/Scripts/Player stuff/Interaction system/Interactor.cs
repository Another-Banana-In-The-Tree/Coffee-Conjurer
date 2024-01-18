using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private Vector2 interactPoint;
    [SerializeField] private float radius;
    [SerializeField] private LayerMask interactionLayer;

    private Collider2D[] ObjHit = new Collider2D[3];
    [SerializeField] private int numFound;

    

    private void Awake()
    {
        interactPoint = transform.position;
    }
    private void Update()
    {
        numFound = Physics2D.OverlapCircleNonAlloc(transform.position, radius, ObjHit, interactionLayer);
    }

    public void Active()
    {
        if(numFound > 0)
        {
            var interactable = ObjHit[0].GetComponent<IInteractable>();
            
            if(interactable != null)
            {
                interactable.Interact();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
