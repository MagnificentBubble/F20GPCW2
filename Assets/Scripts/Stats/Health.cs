using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] public float HP;
    [SerializeField] public float HPMax;
    [SerializeField] public float HPMin;

    private void Update()
    {
        if (HP > HPMax) HP = HPMax;
        if (HP < HPMin) HP = HPMin;
    }

    public void Hurt(float dmg) {
        HP -= dmg;
    }

    public void Heal(float dmg) {
        HP += dmg;
    }
}
