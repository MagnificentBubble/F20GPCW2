using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public Animator Animation;
    public KeyCode MeleeKey = KeyCode.Mouse0;

    void Update()
    {
        if(Input.GetKeyDown(MeleeKey) == true)
        {
            Animation.SetTrigger("isMelee");
        }
    }
}
