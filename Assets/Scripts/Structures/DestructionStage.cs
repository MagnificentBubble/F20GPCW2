using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DestructionStages", order = 1)]
public class DestructionStage : ScriptableObject
{
    [SerializeField] public float HPThreshold;
    [SerializeField] public GameObject Prefab;
}
