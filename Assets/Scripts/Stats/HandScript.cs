using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    private int MaxObjectOnHand = 1;
    private Transform HandStorage;
    private Transform HoldPosition;
    
    void Start()
    {
        HandStorage = GameObject.Find("HandStorage").GetComponent<Transform>();
    }

    
    void Update()
    {   
        if(transform.childCount > MaxObjectOnHand)      // When more than one child in HoldLocation
        {
            for(int i = MaxObjectOnHand; i < transform.childCount; i++)
            {   
                // Move to HandStorage
                transform.GetChild(0).position = HandStorage.transform.position;
                transform.GetChild(0).parent = HandStorage.transform;
            }
        }  
        // Move back to HoldLocation when HoldLocation is empty
        if(transform.childCount < MaxObjectOnHand && HandStorage.transform.childCount > 0)
        {
            HandStorage.transform.GetChild(0).position = transform.position;
            HandStorage.transform.GetChild(0).parent = transform;
        }
    }
}
