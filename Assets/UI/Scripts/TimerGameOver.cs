using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerGameOver : MonoBehaviour
{
    int countDownStartValue = 70;
    public Text timerUI;
    //public Text timerUI1;
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
            timerUI.text = "Timer : " + spanTime.Minutes + ":" + spanTime.Seconds;
            Debug.Log("Timer : " + countDownStartValue);
            countDownStartValue--;
            Invoke("countDownTimer", 1.0f);
        }

        else
        {
            SceneManager.LoadScene("DeathScreen");

        }
    }
    /*
    void Health
        {
            if (Health = HPMax)
            SceneManager.LoadScene("GameComplete");
        }
    */
}
