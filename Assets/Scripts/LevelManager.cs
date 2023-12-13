using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;

    public int currency; //paramız

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Debug.Log(currency);
        currency = 100; //başlangıç paramız 100 olsun
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount; //paramıza para ekleyelim
    }

    public bool SpendCurrency(int amount)
    {
        if(amount <= currency) //elimizdeki para, genel paradan az ise
        {
            //buy item
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("you dont have enough to purchase this item.");
            return false;
        }
    }
}
