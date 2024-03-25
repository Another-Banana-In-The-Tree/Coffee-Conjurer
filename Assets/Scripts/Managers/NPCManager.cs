using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCManager : MonoBehaviour
{
    [SerializeField] NPC[] _customers;
    [SerializeField] public LineWaypoint _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint;
    LineWaypoint[] waypoints;
    [SerializeField] LineWaypoint exitWaypoint;
    private AudioManager audio;

    [SerializeField] float moveDelay;
    private int customersInStore = 0;
    private float timeSinceLastMove = 0;
    private float lastMoveTime = 0;
    private int customersLeft;
    private float totalTime;
    private void Awake()
    {
        //Load waypoints into list for ease of use
        waypoints = new LineWaypoint[] { _entranceWaypoint, _orderWaypoint, _inBetweenWaypoint, _pickupWaypoint };
        audio = FindObjectOfType<AudioManager>();
        customersLeft = _customers.Length;
    }
    private void Update()
    {
        totalTime += Time.deltaTime;
        timeSinceLastMove = totalTime - lastMoveTime;

        if (timeSinceLastMove >= moveDelay && customersInStore < _customers.Length)
        {
            MoveNext();
            lastMoveTime = totalTime;
        }

        if (customersInStore >= 5)
        {
            soundTimer += Time.deltaTime;
            if (soundTimer > noiseDelay + 0.5f)
            {
                Debug.Log("Should Make Sound?");
                audio.Play("Stirr");
                soundTimer = 0;
            }
        }
    }

    public void MoveNext()
    {
        _customers[customersInStore].gameObject.SetActive(true);
        _customers[customersInStore].SetNextPoint();
        _customers[customersInStore].EnableMovement();
        customersInStore++;
        audio.Play("Bell");
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

    public void UpdateCustomersInStore()
    {
        customersLeft--;
        if(customersLeft == 0)
        {
            Invoke("EndLevel", 8f);
        }
    }

    private void EndLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
