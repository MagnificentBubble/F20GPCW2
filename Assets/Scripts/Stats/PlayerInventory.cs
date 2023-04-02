using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
       NumberOfRubble = 5;


        playerHand = GameObject.FindGameObjectWithTag("HoldLocation");

        gameObjectsWithTag = GameObject.FindGameObjectsWithTag(tagToCheck);
    }

    private void Update() 
    {
        HandCheck();
        ClosestEnemy();
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

    public void ClosestEnemy()
    {
        // Check which game object is closest
        GameObject closestGameObject = null;
        float closestDistance = float.MaxValue;
        foreach (GameObject gameObject in gameObjectsWithTag)
        {
            float distance = Vector3.Distance(transform.position, gameObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGameObject = gameObject;
                
            }
        }
       
        // Do something with the closest game object
        if (closestGameObject != null)
        {
            
            closestGameObject.GetComponent<HatBehaviour>().SetClosestToPlayer();
            // Debug.Log(closestGameObject);
            // closestGameObject.SetClosestToPlayer();
            // HatBehaviour.SetClosestToPlayer();
            // closestGameObject.SetBool("closestToPlayer",true);
            // GameObject CGO = closestGameObject.GetComponent<HatBehaviour>();
            // CGO.SetBool("closestToPlayer",true);
            
        }
        
    }

    
}