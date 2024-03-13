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
    [SerializeField] LineWaypoint exitWaypoint;

    [SerializeField] float moveDelay;
    private int customersInStore = 0;
    private float timeSinceLastMove = 0;
    private float lastMoveTime = 0;
    private void Awake()
    {
        //Load waypoints into list for ease of use
        waypoints = new LineWaypoint[] { _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint };
    }
    private void Update()
    {
        timeSinceLastMove = Time.realtimeSinceStartup - lastMoveTime;

        if (timeSinceLastMove >= moveDelay && customersInStore < _customers.Length)
        {
            MoveNext();
            lastMoveTime = Time.realtimeSinceStartup;
        }
    }

    public void MoveNext()
    {
        _customers[customersInStore].gameObject.SetActive(true);
        _customers[customersInStore].SetNextPoint();
        _customers[customersInStore].EnableMovement();
        customersInStore++;
    }
    public LineWaypoint[] GetWaypoints()
    {
        return waypoints;
    }
    public LineWaypoint GetExitWaypoint()
    {
        return exitWaypoint;
    }
    public NPC currentCustomer()
    {
        return _customers[_customers.Length - 1];
    }
}
