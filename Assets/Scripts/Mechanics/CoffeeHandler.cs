using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeHandler : MonoBehaviour
{


    public static CoffeeHandler Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        CreateNewCoffee("Karen");
    }

    //COFFEE
    private Coffee currentCoffee;


    private List<Coffee> coffeeQueue = new List<Coffee>();
    private List<Coffee> customerOders = new List<Coffee>();


    public void AddOrder(Coffee newOrder)
    {
        customerOders.Add(newOrder);
    }

    public void CreateNewCoffee(string name)
    {
        Coffee newCoffee = new Coffee();
        newCoffee.name = name; 
        currentCoffee = newCoffee;
        coffeeQueue.Add(newCoffee);

        print("Coffee created " + coffeeQueue[0].name);
    }

    public Coffee GetCurrentCoffee()
    {
        return currentCoffee;
    }

    public Coffee SearchForCoffee(string Name, List<Coffee> coffeeList)
    {
        
        foreach(Coffee i in coffeeList)
        {
            if(i.name == Name)
            {
                print(i.name);
                return i;
            }
        }

       

        return null;
    }


    public void testSpecificCoffee(string Name)
    {
        Coffee tempCoffee = SearchForCoffee(Name, coffeeQueue);

        CoffeeStats(tempCoffee);
    }

    public void CoffeeStats(Coffee coffee)
    {

        Debug.Log("This coffee is: ");
        Debug.Log(coffee.name);
        
        Debug.Log(coffee.roast);
        Debug.Log(coffee.toppingAdded);
       
      
    }



}
