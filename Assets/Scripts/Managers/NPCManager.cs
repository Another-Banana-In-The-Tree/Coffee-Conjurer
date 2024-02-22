using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] List<NPC> _customers = new List<NPC>();
    [SerializeField] public LineWaypoint _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint;
    List<LineWaypoint> waypoints;
    int _inStoreCustomers;
    private void Awake()
    {
        //Load waypoints into list for ease of use
        waypoints = new List<LineWaypoint> { _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint };
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Move NPC to the next waypoint, increase the _nextWaypointCounter, set the new currentWaypoint and set the next waypoint
            Debug.Log("Moving NPC!");
            StartCoroutine(_customers[0].MoveToWaypoint(_customers[0].GetNextWaypoint()));
        }

        //If the waypoint is the order waypoint or the pickup waypoint, set customer to Interactable, otherwise make them uninteractable
        if (_customers[0].GetCurrentWaypoint() == _orderWaypoint || _customers[0].GetCurrentWaypoint() == _pickupWaypoint)
        {
            _customers[0].SetInteractable(true);
        }
        else _customers[0].SetInteractable(false);
    }
    public List<LineWaypoint> GetWaypoints()
    {
        return waypoints;
    }

    public NPC currentCustomer()
    {
        return _customers[_customers.Count - 1];
    }
}
