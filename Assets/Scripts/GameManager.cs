using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public WinState[] WinStates;
    public bool Won;

    // Start is called before the first frame update
    void Start()
    {
        Won = false;
    }

    // Update is called once per frame
    void Update()
    {
        checkWinStates();
    }

    private void checkWinStates()
    {
        bool _check =  true;
        foreach( WinState x in WinStates)
        {
            _check = _check && x.Won;
        }

        Won = _check;
    }
}
