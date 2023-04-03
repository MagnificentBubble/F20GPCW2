using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JailTime : MonoBehaviour

{
    int countDownStartValue = 10;
    public Text jailTimeUI;
    
    // Start is called before the first frame update
    void Start()
    {
        countDownTimer();
    }

    // Update is called once per frame 
    void countDownTimer()
    {
        if (countDownStartValue > 0)
        {

            TimeSpan spanTime = TimeSpan.FromSeconds(countDownStartValue);
            jailTimeUI.text = "Jail Time : " + spanTime.Minutes + ":" + spanTime.Seconds;
            Debug.Log("Jail Time : " + countDownStartValue);
            countDownStartValue--;
            Invoke("countDownTimer", 1.0f);
        }

        else
        {
            Destroy(gameObject);
            Destroy(GameObject.FindWithTag("JailPic"));

        }
    }
   
}
