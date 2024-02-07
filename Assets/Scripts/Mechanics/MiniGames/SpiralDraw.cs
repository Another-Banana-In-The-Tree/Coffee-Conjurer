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

    private GameObject[] positions = new GameObject[50];
   
    public float sensitivity;
    public int currentSpace = 0;

    private Coffee currentCoffee;

    private bool gameRunning = false;

    // Start is called before the first frame update
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
            positions[iterate] = child.gameObject;
            iterate++;
        }

        boundsDraw.SetActive(false);

    }



    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        if (Vector2.Distance(mousePos, positions[currentSpace].transform.position  ) <= sensitivity && currentSpace < positions.Length -1 )
        {
            positions[currentSpace].GetComponent<Renderer>().material.color = Color.blue;
            currentSpace++;
           
            //Debug.Log("next spot");
            

              
        }
        if( currentSpace == positions.Length - 1 && gameRunning)
        {
            gameRunning = false;
            positions[currentSpace].GetComponent<Renderer>().material.color = Color.blue;
            currentCoffee.stirred = true;

            Debug.Log("Wongame"); 
        }
    }

    public void Play()
    {

    }

    public void Exit()
    {
        
        currentSpace = 0;
        minigameScreen.SetActive(false);
        boundsDraw.SetActive(false);

        CoffeeHandler.Instance.testSpecificCoffee(currentCoffee.name);

        foreach(GameObject i in positions)
        {
            i.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    public void gameStarted()
    {
        gameRunning = true;
        boundsDraw.SetActive(true);
        currentCoffee = CoffeeHandler.Instance.GetCurrentCoffee();


    }
}
