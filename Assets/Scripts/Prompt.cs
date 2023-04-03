using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{
    public Text PromptUI;
    // Start is called before the first frame update
    void Start()
    {
        transform.position= new Vector3 (transform.position.x,-120,0);
    }

    // Update is called once per frame

    public void BringPrompt(string Item){
        PromptUI.text = "Press F to Pickup " + Item;
        this.gameObject.SetActive(true);
    }

    public void ClosePrompt(){
        this.gameObject.SetActive(false);
    }
}
