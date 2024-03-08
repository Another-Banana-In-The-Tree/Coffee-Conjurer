using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OrderMenu : MonoBehaviour
{
    private bool menuOpen = false;
    private float moveDistance;
    private RectTransform menuObject;
    private int numActiveOrders;
    private int lastActiveNum;
    // private Queue<Coffee> ActiveOders = new Queue<Coffee>();
    private Coffee currentCoffee;
    private Animator anim;
    [SerializeField] private TextMeshProUGUI receipt;
    [SerializeField] private TextMeshProUGUI buttonText;
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
        currentCoffee = newCoffee;
        UpdateActiveOrders();
    }

    private void UpdateActiveOrders()
    {
       
        receipt.text = currentCoffee.name + "\n  Roast: " + currentCoffee.roast + "\n Size: " + currentCoffee.size;

        print(receipt.text);
        foreach(string i in currentCoffee.ingredientsUsed)
        {
            receipt.text += "\n" + i;
        }
    }
}