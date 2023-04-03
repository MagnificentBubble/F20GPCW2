using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/RubbleSpawnPoint", order = 2)]
public class RubbleSpawnPoint : ScriptableObject
{
    [SerializeField] public GameObject[] SpawnPoints;
    [SerializeField] public float Likelihood;
}
