using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderMenu : MonoBehaviour
{
    private bool menuOpen = false;
    private float moveDistance;
    private RectTransform menuObject;
    private int numActiveOrders;
    private int lastActiveNum;
    private Queue<Coffee> ActiveOders = new Queue<Coffee>();
    private Animator anim;

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
            
            menuOpen = true;
        }
        else
        {
            
            menuOpen = false;
        }

        anim.SetBool("menuOpen", menuOpen);
    }

    public void changeActiveOrders(int modifier)
    {
        lastActiveNum = numActiveOrders;
        numActiveOrders += modifier;
        ActiveOders = CoffeeHandler.Instance.GetCurrentOrders();
        UpdateActiveOrders();
    }

    private void UpdateActiveOrders()
    {
        if(lastActiveNum > numActiveOrders)
        {

        }
        else
        {

        }
    }
}
