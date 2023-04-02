using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int MaxRubbleInventory = 5;
    static public bool childExists = false;

    public GameObject playerHand;

    [HideInInspector]
    public int NumberOfRubble;
    
    void Start()
    {
        NumberOfRubble = 0;

        playerHand = GameObject.FindGameObjectWithTag("HoldLocation");
    }

    private void Update() 
    {
        HandCheck();
    }

    public void RubbleCollected()
    {
        if (NumberOfRubble <= MaxRubbleInventory)
        {
            NumberOfRubble++;
        }
    }

    public void RubbleThrown()
    {
        if (NumberOfRubble > 0)
        {
            NumberOfRubble--;
        }
    }

    public void HandCheck()
    {
        childExists = playerHand.transform.childCount > 0;
    }
}