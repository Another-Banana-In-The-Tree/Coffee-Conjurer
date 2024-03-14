using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class LineWaypoint : MonoBehaviour
{
    [SerializeField] bool isLine = false;
    [SerializeField] bool isLineVertical = false;
    private int lineLength = 0;
    private Queue<NPC> linedNPC = new Queue<NPC>();
    //Return the number of NPCs in line at this waypoint
    public int GetLineLength()
    {
        return lineLength;
    }

    public bool GetIsVeritcal()
    {
        return isLineVertical;
    }

    public bool GetIsLine()
    {
        return isLine;
    }

    //Return the position of the GameObject
    public Vector3 GetPosition()
    {
        return gameObject.transform.position;
    }
    public void AddCustomer(NPC newNPC)
    {
        lineLength++;
        linedNPC.Enqueue(newNPC);
    }
    public void RemoveCustomer()
    {
        lineLength--;
        linedNPC.Dequeue();
        int x = lineLength;
        foreach(NPC i in linedNPC)
        {
            
            print(i.gameObject.name);
            i.UpdateLine( x);
            x--;
        }
    }
}
