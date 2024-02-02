using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWaypoint : MonoBehaviour
{
    List<NPCManager> npc;
    int NPCLimit = 5;

    public int GetNPCCount()
    {
        return npc.Count;
    }
}
