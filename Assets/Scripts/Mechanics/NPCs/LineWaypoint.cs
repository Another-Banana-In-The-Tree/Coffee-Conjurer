using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWaypoint : MonoBehaviour
{
    List<NPC> npc;
    
    //Return the number of NPCs in line at this waypoint
    public int GetNPCCount()
    {
        return npc.Count;
    }

    //Return the position of the GameObject
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }

    public void AddCustomer(NPC customer)
    {
        this.npc.Add(customer);
    }
}
