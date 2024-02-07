using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] List<NPC> _customers = new List<NPC>();
    int _inStoreCustomers;
    [SerializeField] float _npcSpeed;
    [SerializeField] float _npcDelayInSeconds;
    [SerializeField] LineWaypoint _entranceWaypoint, _orderWaypoint, _pickupWaypoint;
}
