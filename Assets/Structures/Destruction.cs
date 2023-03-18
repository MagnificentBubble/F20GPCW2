using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] DestructionStage[] DestructionStages;
    private int _currentStage;

    // Start is called before the first frame update
    void Start()
    {
        _currentStage = 0;
        sortStages();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<Health>().HP < DestructionStages[_currentStage].HPThreshold && _currentStage != DestructionStages.Length - 1) DestroyStage();
        if (this.GetComponentInParent<Health>().HP > DestructionStages[_currentStage - 1].HPThreshold && _currentStage != 0) FixStage();

    }

    private void sortStages() {
        DestructionStage _temp;

        for (int i = 0; i < DestructionStages.Length; i++)
        {
            for (int j = i + 1; i < DestructionStages.Length; i++)
            {
                if (DestructionStages[i].HPThreshold < DestructionStages[j].HPThreshold) {
                    _temp = DestructionStages[i];
                    DestructionStages[i] = DestructionStages[j];
                    DestructionStages[j] = _temp;
                }
            }
        }    
    }

    private void swapMesh(int stage) {
        //Mesh _newMesh = DestructionStages[stage].Mesh;
        Debug.LogError("Not Implemented: Mesh Swap");
    }

    public void DestroyStage() {
        _currentStage += 1;
        swapMesh(_currentStage);
    }

    public void FixStage() {
        _currentStage -=  1;
        swapMesh(_currentStage);
    }
}
