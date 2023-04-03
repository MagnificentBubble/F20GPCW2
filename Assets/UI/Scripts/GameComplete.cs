using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour


{


    public void PlayAgainButton()
    {
        SceneManager.LoadScene("Menu");
        Debug.Log("Loading menu...");
    }
}
