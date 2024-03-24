using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillCup : MonoBehaviour, MiniGame
{

    [SerializeField] private GameObject MiniGameScreen;
    [SerializeField] private GameObject background;
    [SerializeField] private TargetTap spill;
    [SerializeField] private Player player;
    [SerializeField] private Slider slider;
    public bool isTutorial;
    private Coffee currentCoffee;

    //for audio
    [SerializeField] private float timer;
    [SerializeField] private float soundDelay = 1;
    private AudioManager audio;



    private float fillMod;
    [SerializeField] private float fillSpeed;

    [SerializeField] private float currentFill;

    [SerializeField] private Image fill;
    private bool gameActive = false;

    [SerializeField] private TextMeshProUGUI sizeText;
<<<<<<< HEAD

=======
    [SerializeField]private Oswald oswald;
    
>>>>>>> main

    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }
    public void Fill(float value)
    {
        timer += Time.deltaTime;
        if (timer > soundDelay + 0.5f)
        {
            Debug.Log("Should Make Sound?");
            audio.Play("Heat");
            timer = 0;
        }
        fillMod = Mathf.Pow( value, 2);
       // print(fillMod);
    }

    private void Update()
    {
        if (oswald != null && oswald.WaitForDialogueFinish()) return;
        if (oswald != null)
        {
            if (oswald.GetState() != MiniGameNumber() + 1)
            {
                return;
            }
        }
        if (!gameActive) return;
        //print(currentCoffee.roast);

        if(currentFill > 0.95)
        {
            gameActive = false;
            print("Overfill!");
            sizeText.text = "Over filled!";
            OverFill();
        }
        if (currentCoffee.roast != null && currentCoffee.size == null)
        {
            //print("not null");

            if (fill.fillAmount < 1 && fillMod > 0)
            {
               // print("notFull");
                currentFill += (fillMod * fillSpeed) *Time.deltaTime;

                fill.fillAmount = currentFill;
                TestSize();
            }
        }
    }


    private void TestSize()
    {
        //print("testsize");
        string output = "";
        if(currentFill < 0.28)
        {
            output = "Empty";
        }
       else if (currentFill > 0.28 && currentFill < 0.38)
        {
            output = "Small";
        }
        else if(currentFill > 0.38 && currentFill < 0.61)
        {
            output = "not quite Medium";
        }
        else if (currentFill >= 0.61 && currentFill < 0.71)
        {
            output = "Medium";
        }
        else if(currentFill >0.71 && currentFill < 0.89)
        {
            output = "not quite Large";
        }
        else if (currentFill >= 0.89)
        {
            output = "Large";
        }
        else
        {
            output = "Over filled!!";
        }
        sizeText.text = output;
    }

    public void finish()
    {
        if (currentCoffee.roast == null) return;
        if(currentFill > 0.28 && currentFill < 0.38)
        {
            currentCoffee.size = "Small";
        }
        else if (currentFill >= 0.61 && currentFill < 0.71) 
        {
            currentCoffee.size = "Medium";
        }
        else if (currentFill >= 0.89)
        {
            currentCoffee.size = "Large";
        }
        else
        {
            currentCoffee.size = "Not Right";
        }
        Exit();
        print(currentCoffee.size);
        
    }
    public void OverFill()
    {
        
        finish();
       // Exit();

        player.StartMinigame(spill);
        
    }

    public void Play()
    {

    }
    public void Exit()
    {
        if (!isTutorial || currentFill < 0.95)
        {
            GameManager.Instance.orderMenu.UpdateCompletion();
        }
        sizeText.text = "empty";
        slider.value = 0;
        gameActive = false;
        currentFill = 0;
        fill.fillAmount = 0;
        background.SetActive(false);
        MiniGameScreen.SetActive(false);
        if (currentFill < 0.95)
        {
            PlayerInput.EnableGame();
        }
       
    }

    public void gameStarted()
    {
        background.SetActive(true);
        MiniGameScreen.SetActive(true);
        gameActive = true;
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();
    }

    public int MiniGameNumber()
    {
        return 5;
    }
}
