using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pressure : MonoBehaviour, MiniGame 
{
    [SerializeField] private Image fillBar;
    [SerializeField] private Image fillTimer;
    [SerializeField]private float fill = 0;
    [SerializeField] private float timePenalty;
    [SerializeField] private float clickBonus;
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject screen;
    private bool isPlaying = false;
    [SerializeField] private float minTarget;
    [SerializeField] private float maxTarget;
    private int miniGameNum = 2;
    private Coffee currentCoffee;
    private Oswald oswald;

    [SerializeField] private float soundTimer;
    [SerializeField] private float roastDelay = 2;
    private AudioManager audio;


    [SerializeField] private float timeCount;
    [SerializeField] private TextMeshProUGUI pointText;
    [SerializeField] private TextMeshProUGUI roast;
    [SerializeField] private SpriteRenderer beanSprite;

    [SerializeField] Color startColor;
    [SerializeField] Color endColor;

    private List<float> points = new List<float>();

    private void Awake()
    {
        oswald = FindObjectOfType<Oswald>();
        audio = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        soundTimer = 2;
    }
    private void Update()
    {
        if (isPlaying)
        {
            soundTimer += Time.deltaTime;
            if (soundTimer > roastDelay + 0.5f)
            {
                Debug.Log("Should Make Sound?");
                audio.Play("Heat");
                soundTimer = 0;
            }
            if (timeCount >= 1)
            {
                GameEnd();
                isPlaying = false;
            }
            if (fill > minTarget)
            {
                timeCount += 0.2f * Time.deltaTime;

                // pointText.text = timeCount.ToString("F1");
                fillTimer.fillAmount = timeCount;
                UpdateCurrentRoast(fill);
                
            }
            if (isPlaying && fill > 0)
            {


                fill -= timePenalty * Time.deltaTime;


            }
            if (fill < 0)
            {
                fill = 0;
            }
            fillBar.fillAmount = fill;
        }
    }

    private void UpdateCurrentRoast(float filling)
    {
        points.Add(filling);
    }
    public void Play()
    {
        if(oswald != null)
        {
            if(oswald.GetState() != MiniGameNumber() + 1)
            {
                return;
            }
        }
        if (currentCoffee.roast == null)
        {
           // if (fill >= 1) return;
            AddMore();
        }
        
        
    }

    private void AddMore()
    {
        fill += clickBonus;
        Mathf.Clamp(fill, 0f, 1f);
            float tempSum = 0;
        foreach (float i in points)
        {
            tempSum += i;
        }

        tempSum = tempSum / points.Count;

        beanSprite.color = Color.Lerp(startColor, endColor, tempSum);

    }

    private void GameEnd()
    {
        float tempSum = 0;
        foreach(float i in points)
        {
            tempSum += i;
        }

        tempSum = tempSum / points.Count;

        print("you scored an average of: " +  tempSum);
        

        if( tempSum < 0.69)
        {
            currentCoffee.roast = "Light";
            print("light");
        }
        else if(tempSum > 0.7 && tempSum <= 0.82 )
        {
            currentCoffee.roast = "Medium";
            print("Medium");
        }
        else if (tempSum >= 0.83)
        {
            currentCoffee.roast = "Dark";
            print("Dark");
        }
        if (currentCoffee.roast != null)
        {
            timeCount = 0;
            fill = 0;
            points.Clear();
            roast.text = currentCoffee.roast;
        }
        

    }
    

    public void Exit()
    {
        beanSprite.color = startColor;
        print("this is your coffees roast" + currentCoffee.roast);
        roast.text = " ";
        fill = 0;
        timeCount = 0;
        fillTimer.fillAmount = 0;
        points.Clear();
        isPlaying = false;
        canvas.SetActive(false);
        screen.SetActive(false);
        PlayerInput.EnableGame();
        // CoffeeHandler.Instance.testSpecificCoffee(currentCoffee.name);
        audio.Stop("Heat");
    }

    public void gameStarted()
    {
        isPlaying = true;
        canvas.SetActive(true);
        screen.SetActive(true);
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
    }

    public int MiniGameNumber()
    {
        return 3;
    }
}
