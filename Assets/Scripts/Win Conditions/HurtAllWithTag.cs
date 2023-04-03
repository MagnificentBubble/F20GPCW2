using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtAllWithTag : WinState
{
    [SerializeField] string Tag;
    [SerializeField] float HPThreshold;

    // Update is called once per frame
    void Update()
    {    
        bool check = true;
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(Tag))
        {           
            check = check && (obj.GetComponent<Health>().HP < HPThreshold);
            
            Won = check;
        }

        if (Won)
        {
            SceneManager.LoadScene("GameComplete");
        }
    }
}
