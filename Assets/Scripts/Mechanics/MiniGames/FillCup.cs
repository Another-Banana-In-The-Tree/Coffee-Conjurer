using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
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



     private float fillMod;
    [SerializeField] private float fillSpeed;

    [SerializeField] private float currentFill;

    [SerializeField] private Image fill;
    private bool gameActive = false;

    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField]private Oswald oswald;
    private bool isHeld = true;

    [SerializeField] private float timer;
    [SerializeField] private float soundDelay = 1;
    [SerializeField] private float soundTimer;
    [SerializeField] private float noiseDelay = 1;
    private AudioManager audio;

    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }
    private void Start()
    {
        timer = 3;
    }
    public void Fill(float value)
    {
        if (currentCoffee.roast == null) return;
        fillMod = Mathf.Pow( value, 2);
        isHeld = true;
        // print(fillMod);
        timer += Time.deltaTime;
        if (timer > soundDelay + 0.5f)
        {
            Debug.Log("Should Make Sound?");
            audio.Play("Pour");
            timer = 0;
        }
    }

    private void Update()
    {
        /*soundTimer += Time.deltaTime;
        if (soundTimer > noiseDelay + 0.5f)
        {
            Debug.Log("Should Make Sound?");
            audio.Play("Machine");
            soundTimer = 0;
        }*/

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
        /*if(!isHeld && Input.GetKeyDown(KeyCode.Mouse0))
        {
            isHeld = true;
        }*/
        if(isHeld && Input.GetKeyUp(KeyCode.Mouse0))
        {
            isHeld = false;
            audio.Stop("Pour");
            
        }
        if (!isHeld && fillMod > 0)
        {
            fillMod = 0f;
          
            slider.value = fillMod;
            



        }

        if (currentFill > 0.95)
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
            output = "Not Quite Small";
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
        if (currentCoffee.roast == null || currentFill < 0.1) return;
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
        else if (currentFill == 0)
        {
            currentCoffee.size = null;
        }
        else
        {
            currentCoffee.size = "Not Right";
        }
        // Exit();
        audio.Stop("Pour");
        print(currentCoffee.size);
        
    }
    public void OverFill()
    {

        //finish();
        audio.Stop("Pour");
        Exit();

        player.StartMinigame(spill);
        
    }

    public void Play()
    {

    }
    public void ResetCup()
    {
        if (currentCoffee.ingredientsUsed.Count > 0 || currentCoffee.stirred) return;
        sizeText.text = "empty";
        slider.value = 0;
        currentFill = 0;
        fill.fillAmount = 0;

        currentCoffee.size = null;
    }
    public void Exit()
    {
        if(currentCoffee.size == null)
        {
            finish();
        }
        
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
