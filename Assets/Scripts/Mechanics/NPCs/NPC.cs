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

    private void Awake()
    {
        coffee = new Coffee(); //Preload coffee here
    }

    //Move the NPC towards a given LineWaypoint by the speed defined
    public IEnumerator MoveToWaypoint(LineWaypoint waypoint)
    {
        Vector3 startPosition = transform.position;
        float startTime = Time.timeSinceLevelLoad;
        while (Time.timeSinceLevelLoad - startTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPosition, waypoint.GetPosition(), (Time.timeSinceLevelLoad - startTime)/timeToMove);
            yield return null;
        }
        Debug.Log("Move to Waypoint Coroutine Ended");
    }
    
    //Enable interactivity
    public void SetInteractable(bool isInteractable)
    {
        this.isInteractable = isInteractable;
    }
    public void Interact(Player player)
    {
        if (isInteractable)
        {
            Debug.Log("Interacted!");
        }
    }
}
