using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WinState : MonoBehaviour
{
    public bool Won;

    // Start is called before the first frame update
    void Start()
    {
        Won = false;
    }
}
