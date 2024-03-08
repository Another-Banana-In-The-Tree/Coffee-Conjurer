using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] NPC[] _customers = new NPC[3];
    [SerializeField] public LineWaypoint _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint;
    LineWaypoint[] waypoints;
    int _npcMoveCounter = 0;
    bool isLineMoving = true;
    float _customerDelay = 3.00f;
    private void Awake()
    {
        //Load waypoints into list for ease of use
        waypoints = new LineWaypoint[] { _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint };
    }
    private void Update()
    {
        //Debug.Log(_npcMoveCounter);
        //Debug.Log(MathF.Round(Time.timeSinceLevelLoad, 2) % _customerDelay == 0);
        //if (Input.GetKeyDown(KeyCode.Space))
        if (MathF.Round(Time.timeSinceLevelLoad, 3) % _customerDelay == 0)
        {
            //Debug.Log("Boo");
            for (int i = 0; i < _customers.Length; i++)
            {
                if (i <= _npcMoveCounter && isLineMoving)
                {
                    //Move NPC to the next waypoint, increase the _nextWaypointCounter, set the new currentWaypoint and set the next waypoint
                    //Debug.Log("Moving NPC!");
                    //if (_customers[i].GetCurrentWaypoint() == _inBetweenWaypoint)
                    //{
                    //    StartCoroutine(_customers[i].MoveToWaypoint(_customers[i].GetNextWaypoint()));
                    //}
                    //If the waypoint is the order waypoint or the pickup waypoint, set customer to Interactable, otherwise make them uninteractable
                    if (_customers[i].GetNextWaypoint() == _orderWaypoint || _customers[i].GetNextWaypoint() == _pickupWaypoint)
                    {
                        StartCoroutine(_customers[i].MoveToWaypoint(_customers[i].GetNextWaypoint()));
                        _customers[i].SetInteractable(true);
                        DisableLineMovement();
                    }
                    else
                    {
                        StartCoroutine(_customers[i].MoveToWaypoint(_customers[i].GetNextWaypoint()));
                        _customers[i].SetInteractable(false);
                    }
                }
            }
            if (isLineMoving) IncrementNpcCounter();
        }
    }
    //EnableLineMovement
    public void EnableLineMovement()
    {
        isLineMoving = true;
    }
    //DisableLineMovement
    public void DisableLineMovement()
    {
        isLineMoving = false;
    }
    public void IncrementNpcCounter()
    {
        _npcMoveCounter++;
    }
    public LineWaypoint[] GetWaypoints()
    {
        return waypoints;
    }

    public NPC currentCustomer()
    {
        return _customers[_customers.Length - 1];
    }
}
