using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int MaxRubbleInventory = 5;

    [HideInInspector]
    public int NumberOfRubble;
    
    void Start()
    {
        NumberOfRubble = 5;
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
}