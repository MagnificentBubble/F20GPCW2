using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    public int MaxRubbleInventory = 5;
    static public bool childExists = false;
    static public bool aim = false;

    public GameObject playerHand;

    public string tagToCheck = "FixerHat";

    private GameObject[] gameObjectsWithTag;
    

    [HideInInspector]
    public int NumberOfRubble;
    
    void Start()
    {

       tagToCheck = "FixerHat";
       NumberOfRubble = 0;


        playerHand = GameObject.FindGameObjectWithTag("HoldLocation");

        gameObjectsWithTag = GameObject.FindGameObjectsWithTag(tagToCheck);
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