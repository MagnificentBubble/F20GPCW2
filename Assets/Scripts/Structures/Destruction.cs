using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{
    [SerializeField] DestructionStage[] DestructionStages;
    public int CurrentStage;

    // Start is called before the first frame update
    void Start()
    {
        CurrentStage = 0;
        sortStages();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<Health>().HP < DestructionStages[CurrentStage].HPThreshold && CurrentStage != DestructionStages.Length - 1) destroyStage();
        if (this.GetComponentInParent<Health>().HP > DestructionStages[CurrentStage - 1].HPThreshold && CurrentStage != 0) fixStage();

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

    private void destroyStage() {
        CurrentStage += 1;
        swapMesh(CurrentStage);
    }

    private void fixStage() {
        CurrentStage -=  1;
        swapMesh(CurrentStage);
    }
}
