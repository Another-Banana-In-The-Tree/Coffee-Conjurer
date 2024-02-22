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

       
    }

    //COFFEE
    private Coffee currentCoffee;

    [SerializeField] private OrderMenu orderMenu;
    private Queue<Coffee> coffeeQueue = new Queue<Coffee>();
    private Queue<Coffee> customerOders = new Queue<Coffee>();


    public void AddOrder(Coffee newOrder)
    {
        customerOders.Enqueue(newOrder);
        CreateNewCoffee(newOrder.name);
        Coffee test = customerOders.Peek();
        print("Order Added");
        print(test.name);
        print(test.roast);
        print(test.size);
        foreach(string i in test.ingredientsUsed)
        {
            print(i);
        }
        orderMenu.changeActiveOrders(test);

    }

    public void CreateNewCoffee(string name)
    {
        Coffee newCoffee = new Coffee();
        newCoffee.name = name; 
        currentCoffee = newCoffee;
        coffeeQueue.Enqueue(newCoffee);

        print("Coffee created " + coffeeQueue.Peek().name);
    }

    public Coffee GetCurrentCoffee()
    {
        return currentCoffee;
    }

   /* public Coffee SearchForCoffee(string Name, List<Coffee> coffeeList)
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
    }*/

    
    public Queue<Coffee> GetCurrentOrders()
    {
        return customerOders;
    }

    public void CoffeeStats(Coffee coffee)
    {

        Debug.Log("This coffee is: ");
        Debug.Log(coffee.name);
        
        Debug.Log(coffee.roast);
        //Debug.Log(coffee.toppingAdded);
       
      
    }


    public void CompareCoffee()
    {
        int points = 0;
        var customerOrder = customerOders.Peek();
        var finishedCoffee = coffeeQueue.Peek();

        if(finishedCoffee.size == customerOrder.size)
        {
            points++;
            print("size match");
        }
        else
        {
            print("size dont match");
        }

        if (finishedCoffee.roast == customerOrder.roast)
        {
            points++;
            print("roast match");
        }
        else
        {
            print("roast dont match");
        }
        if (finishedCoffee.stirred)
        {
            points++;
            print("stirred");
        }
        foreach(string i in customerOrder.ingredientsUsed)
        {
            if (finishedCoffee.ingredientsUsed.Contains(i))
            {
                points++;
                print("contains " + i);
            }
        }
        foreach(string i in finishedCoffee.ingredientsUsed)
        {
            if (!customerOrder.ingredientsUsed.Contains(i))
            {
                print("customerDidnt want that");
            }
        }

    }


}
