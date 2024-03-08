using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetTap : MonoBehaviour, MiniGame
{
    [SerializeField] private GameObject minigameScreen;
    [SerializeField] private GameObject background;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private Transform movingBar;
    [SerializeField] private Transform target;
    [SerializeField] private float speed;
    private float fakeTime = 0;
    private float maxRange, minRange;
    private float minWinRange, maxWinRange;

   [SerializeField] private int numWinsReq = 3;
    private int numWins = 0;
    [SerializeField]private bool isActive = false;
  
    void OnEnable()
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
       // print("target tap");
        if (isActive)
        {
            //fakeTime += 0.1f;
            //print("fakeTime");
            movingBar.position = Vector3.Lerp(start.position, end.position, Mathf.PingPong(Time.time/speed, 1));
        }
    }
   



    public void Play()
    {
        print("tapped");
        if(movingBar.position.x >= minWinRange && movingBar.position.x <= maxWinRange)
        {
            numWins++;
            if(numWins >= numWinsReq)
            {
                EndGame();
            }
            var temp = Random.Range(minRange + (target.localScale.x / 2), maxRange - (target.localScale.x / 2));
            target.position = new Vector3(temp, target.position.y, target.position.z);

            minWinRange = temp - (target.localScale.x / 2);
            maxWinRange = temp + (target.localScale.x / 2);
        }
    }
    private void EndGame()
    {
        PlayerInput.EnableGame();
        minigameScreen.SetActive(false);
        background.SetActive(false);
        numWins = 0;
    }
    public void Exit()
    {
        //minigameScreen.SetActive(false);
    }

    public void gameStarted()
    {
        isActive = true;
        print("trgetTapStarted");
        minigameScreen.SetActive(true);
        background.SetActive(true);
    }
}
