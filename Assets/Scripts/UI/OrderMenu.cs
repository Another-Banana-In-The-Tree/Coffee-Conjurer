using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderMenu : MonoBehaviour
{
    private bool menuOpen = false;
    private float moveDistance;
    private RectTransform menuObject;
    private int ingredientIndexer = 0;
    
   
    // private Queue<Coffee> ActiveOders = new Queue<Coffee>();
    private Coffee currentCoffee;
    private Animator anim;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI roastText;
    [SerializeField] private TextMeshProUGUI sizeText;
    [SerializeField] private TextMeshProUGUI[] ingredients;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private TextMeshProUGUI repText;
    [SerializeField] private TextMeshProUGUI goldText;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        menuObject = gameObject.GetComponent<RectTransform>();
        moveDistance = menuObject.rect.size.x;
        menuObject.localPosition +=  new Vector3(moveDistance, 0,0);

    }
    public void ChangeMenuState()
    {
        if (!menuOpen)
        {
            buttonText.text = "Close";
            menuOpen = true;
        }
        else
        {
            buttonText.text = "Open";
            menuOpen = false;
        }

        anim.SetBool("menuOpen", menuOpen);
    }

    public void changeActiveOrders(Coffee newCoffee)
    {
        /* lastActiveNum = numActiveOrders;
         numActiveOrders += modifier;*/
        //ActiveOders = CoffeeHandler.Instance.GetCurrentOrders();
        Clear();
        currentCoffee = newCoffee;
        UpdateActiveOrders();
    }

    private void UpdateActiveOrders()
    {
        int temp = 0;
        nameText.text = currentCoffee.name;
        roastText.text = currentCoffee.roast;
        sizeText.text = currentCoffee.size;
        foreach(string i in currentCoffee.ingredientsUsed)
        {
            ingredients[temp].text = IngredientNameCorrector(i);
            temp++;
        }
    }

    public void UpdateCompletion()
    {
        Coffee playerMade = CoffeeHandler.Instance.GetCurrentCoffee();

        if(playerMade.roast != null)
        {
            roastText.color = Compare(playerMade.roast, currentCoffee.roast);
            
        } 
        if(playerMade.size != null)
        {
            sizeText.color = Compare(playerMade.size, currentCoffee.size);
            
        }
        foreach (string i in currentCoffee.ingredientsUsed)
        {
            if (playerMade.ingredientsUsed.Contains(i))
            {
                ingredients[ingredientIndexer].color = Color.green;
                ingredientIndexer++;
            }
        }
    }
    



    private Color Compare(string player, string customer)
    {
        print("player: " + player + " customer: " + customer);
        if (player.Equals(customer, System.StringComparison.OrdinalIgnoreCase))
        {
            return Color.green;
        }
        else return Color.red;
    }

    private string IngredientNameCorrector(string thing)
    {
        switch(thing)
        {
            case "DMilk":
                return "Dragon Milk";
            case "RegMilk":
                return "2% Milk";
            case "Honey":
                return "Zombee Honey";
            case "Blood":
                return "Blood of the forgotten one";
            case "CaramelDrizzle":
                return "Caramel Drizzle";
            case "VegetableMilk":
                return "Vegetable Milk";
            case "YetiTears":
                return "Yeti Tears";
            default:
                return thing;

        }
    }
    public void Clear()
    {
        nameText.text = " ";
        nameText.color = Color.black;
        roastText.text = " ";
        roastText.color = Color.black;
        sizeText.text = " ";
        sizeText.color = Color.black;

        foreach(TextMeshProUGUI i in ingredients)
        {
            i.color = Color.black;
            i.text = " ";
            
        }
        ingredientIndexer = 0;
    }

    public void UpdateRep(float newScore)
    {
        repText.text = "Reputation: " + newScore.ToString("F1");

    }
    public void UpdateGold(float gold)
    {
        repText.text = "Gold: " + gold.ToString("F1");
    } 

}

