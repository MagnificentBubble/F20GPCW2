using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float HP;

    public void Hurt(float dmg) {
        HP -= dmg;
    }

    public void Heal(float dmg) {
        HP += dmg;
    }
}
