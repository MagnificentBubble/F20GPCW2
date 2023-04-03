using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RubbleCount : MonoBehaviour
{
    
    
    public int NoRubble;
    [SerializeField] private Text rubblesNo;

    private void Start()
    {
        NoRubble = PlayerInventory.NumberOfRubble;

    }

    private void Update()
    {
        NoRubble = PlayerInventory.NumberOfRubble;
        rubblesNo.text = "Rubble:" + NoRubble.ToString();
        
      //  Debug.Log(NoRubble);
    }
}
