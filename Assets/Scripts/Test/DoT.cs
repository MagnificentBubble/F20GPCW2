using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoT : MonoBehaviour
{

    private float _reaper;
    // Start is called before the first frame update
    void Start()
    {
        _reaper = Time.time + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_reaper < Time.time) {
            this.gameObject.GetComponent<Health>().Hurt(1);
            _reaper = Time.time + 1;
        }
    }
}
