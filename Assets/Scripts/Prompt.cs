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
