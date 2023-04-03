using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAnimationEvent : MonoBehaviour
{
    public void MeleeAnimationEnd()
    {
        GameObject.FindGameObjectWithTag("MeleeWeapon").GetComponent<MeleeWeapon>().InflictMeleeDamage = false;
    }
}
