using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtAllWithTag : WinState
{
    [SerializeField] string Tag;
    [SerializeField] float HPThreshold;

    // Update is called once per frame
    void Update()
    {    
        bool check = true;
        foreach (obj in GameObject.FindGameObjectsWithTag(Tag))
        {           
            check = check && (obj.getComponent<Health>.HP < HPThreshold);
            
            Won = check;
        }
    }
}
