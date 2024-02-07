using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee 
{


    private string name = null;


    private string roast = null;

    private bool toppingAdded = false;
    private float toppingAccuracy = 0;
    private float tampAccuracy = 0;
    private string size = null;

    private List<string> ingredientsUsed = new List<string>();



    public void SetCustomerOrder(string customerName, string customerRoast, string customerSize, List<string> customerIngredients)
    {
        name = customerName;
        roast = customerRoast;
        size = customerSize;
        ingredientsUsed = customerIngredients;






    }


   
}
