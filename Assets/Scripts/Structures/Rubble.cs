using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubble : MonoBehaviour
{
    [SerializeField] GameObject Rubble_Type;
    [SerializeField] GameObject[] SpawnPoints;
    private float _hpChecker;


    // Start is called before the first frame update
    void Start()
    {
        _hpChecker = this.GetComponent<Health>().HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Health>().HP < _hpChecker) {
            spawnRubble();
        }

        _hpChecker = this.GetComponent<Health>().HP;

    }

    private void spawnRubble() 
    {

        foreach (GameObject X in SpawnPoints) {

            if (Random.Range(0, 100) < X.GetComponent<RubbleSpawner>().Likelihood)
            {
                Instantiate(Rubble_Type, X.transform.position, Quaternion.identity);
            }
        }
        
    
    }
}
