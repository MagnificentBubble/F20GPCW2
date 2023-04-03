using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JailTime : MonoBehaviour

{
    public int countDownStartValue = 10;
    public Text jailTimeUI;
    
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        GameObject.FindWithTag("JailPic").SetActive(false);
    }

    // Update is called once per frame 
    public void countDownTimer()
    {
        gameObject.SetActive(true);
        GameObject.FindWithTag("JailPic").SetActive(true);
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
            gameObject.SetActive(false);
            GameObject.FindWithTag("JailPic").SetActive(false);
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().UnfreezePlayer();
			GameObject.FindGameObjectWithTag("MainCamera").GetComponent<PlayerCam>().UnlockCamera();

        }
    }
   
}
