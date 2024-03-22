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
    private List<NPC> linedNPC = new List<NPC>();
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
        linedNPC.Add(newNPC);
    }
    public void RemoveCustomer(NPC removeThisOne)
    {
        lineLength--;
        linedNPC.Remove(removeThisOne);
        foreach(NPC i in linedNPC)
        {
            print(i.name);
        }
        int t = 1;
        for(int x = linedNPC.Count-1; x >= 0; x--)
        {
            linedNPC[x].UpdateLine(t);
            t++;
            
        }
        
    }
}
