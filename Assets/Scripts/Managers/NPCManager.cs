using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] List<NPC> _customers = new List<NPC>();
    [SerializeField] LineWaypoint _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint;
    List<LineWaypoint> waypoints;
    LineWaypoint currentWaypoint;
    int _nextWaypointCounter = 0;
    int _inStoreCustomers;
    private void Awake()
    {
        //Load waypoints into list for ease of use
        waypoints = new List<LineWaypoint> { _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint };

        //Set the next waypoint to the nextWaypointCounter, which starts with first waypoint in the list
        currentWaypoint = waypoints[_nextWaypointCounter];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Move NPC to the next waypoint, increase the _nextWaypointCounter and set the new currentWaypoint
            Debug.Log("Moving NPC!");
            StartCoroutine(_customers[0].MoveToWaypoint(currentWaypoint));
            if (_nextWaypointCounter < waypoints.Count - 1) _nextWaypointCounter += 1;
            currentWaypoint = waypoints[_nextWaypointCounter];

            //If the waypoint is the order waypoint or the pickup waypoint, set customer to Interactable, otherwise make them uninteractable
            if (waypoints[_nextWaypointCounter + 1] == _orderWaypoint || waypoints[_nextWaypointCounter + 1] == _pickupWaypoint)
            {
                _customers[0].SetInteractable(true);
            }
            else _customers[0].SetInteractable(false);
        }
    }
}
