using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiralDraw : MonoBehaviour, MiniGame
{
    [SerializeField] private GameObject minigameScreen;
    [SerializeField] private GameObject boundsDraw;
    //[SerializeField] private Transform startPos;
    //  [SerializeField] private float spiralSize;
    // [SerializeField] private float growSpeed;
    // [SerializeField] private int numObjects;
    // [SerializeField] private float numRev;
    [SerializeField] private float soundTimer;
    [SerializeField] private float noiseDelay = 1;
    private AudioManager audio;

    private GameObject[] positions = new GameObject[50];
   
    public float sensitivity;
    public int currentSpace = 0;

    private Coffee currentCoffee;

    private bool gameRunning = false;
    public bool isTutorial;
    [SerializeField] private Oswald oswald;

    // Start is called before the first frame update
    private void Awake()
    {
        audio = FindObjectOfType<AudioManager>();
    }
    void Start()
    {
       /* for(int i = 0; i < numObjects; i++)
        {
            float angle = i * Mathf.PI * 2/numObjects * numRev  ;
            float x = Mathf.Cos(angle) * spiralSize; 
            float y = Mathf.Sin(angle) * spiralSize;
            Vector3 pos = startPos.position  + new Vector3(x, y, 0);
            positions.Add(Instantiate(boundsDraw, pos, Quaternion.identity, startPos));

            spiralSize += growSpeed;
        }

        boundsDraw.SetActive(false);
*/

        int iterate = 0;
        foreach (Transform child in boundsDraw.transform)
        {
            if (iterate == 50) break;
            positions[iterate] = child.gameObject;
            iterate++;
        }



    }



    // Update is called once per frame
    void Update()
    {
        if (oswald != null && oswald.WaitForDialogueFinish()) return;
        if (!gameRunning) return;
        if (oswald != null)
        {
            if (oswald.GetState() != MiniGameNumber() + 1)
            {
                return;
            }
        }
        if (!currentCoffee.stirred && currentCoffee.size != null)
        {


            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector2.Distance(mousePos, positions[currentSpace].transform.position) <= sensitivity && currentSpace < positions.Length )
            {
                // positions[currentSpace].GetComponent<Renderer>().material.color = Color.blue;
                positions[currentSpace].SetActive(false);
                currentSpace++;

                //Debug.Log("next spot");

                soundTimer += Time.deltaTime;
                if (soundTimer > noiseDelay + 0.5f)
                {
                    Debug.Log("Should Make Sound?");
                    audio.Play("Stirr");
                    soundTimer = 0;
                }

            }
            if (currentSpace == positions.Length && gameRunning)
            {
                gameRunning = false;
               // positions[currentSpace].GetComponent<Renderer>().material.color = Color.blue;
                currentCoffee.stirred = true;
                PlayerInput.EnableGame();
                Exit();
                Debug.Log("Wongame");
            }
        }
    }

    public void Play()
    {

    }

    public void Exit()
    {
        if (isTutorial)
        {
            GameManager.Instance.orderMenu.UpdateCompletion();
        }
        
        currentSpace = 0;
        minigameScreen.SetActive(false);
        boundsDraw.SetActive(false);
        gameRunning = false;
        PlayerInput.EnableGame();

        foreach (GameObject i in positions)
        {
            i.SetActive(true);
        }
    }

    public void gameStarted()
    {
        gameRunning = true;
        boundsDraw.SetActive(true);
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();


    }

    public int MiniGameNumber()
    {
        return 9;
    }
}
