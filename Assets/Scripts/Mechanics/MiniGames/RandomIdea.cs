using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdea : MonoBehaviour, MiniGame
{
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform movingBar;
    [SerializeField] private Transform target;
    
    [SerializeField] private float maxRange, minRange;
    private float minWinRange, maxWinRange;
  
    void Start()
    {
        minRange = start.position.x;
        maxRange = end.position.x;
        var temp = Random.Range(minRange + (target.localScale.x / 2), maxRange - (target.localScale.x / 2));
        target.position = new Vector3(temp, target.position.y, target.position.z);

        minWinRange = temp - (target.localScale.x / 2);
        maxWinRange = temp + (target.localScale.x / 2);


    }

    // Update is called once per frame
    void Update()
    {


        movingBar.position = Vector3.Lerp(start.position, end.position, Mathf.PingPong(Time.time / 2, 1));

    }
   



    public void Play()
    {
        if(movingBar.position.x >= minWinRange && movingBar.position.x <= maxWinRange)
        {
            var temp = Random.Range(minRange + (target.localScale.x / 2), maxRange - (target.localScale.x / 2));
            target.position = new Vector3(temp, target.position.y, target.position.z);

            minWinRange = temp - (target.localScale.x / 2);
            maxWinRange = temp + (target.localScale.x / 2);
        }
    }
    public void Exit()
    {

    }

    public void gameStarted()
    {

    }
}
