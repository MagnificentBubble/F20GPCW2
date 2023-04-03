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
        //sortStages();
        CurrentStage = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponentInParent<Health>().HP < DestructionStages[CurrentStage].HPThreshold && CurrentStage != DestructionStages.Length - 1) destroyStage();
        if (CurrentStage != 0) { if (this.GetComponentInParent<Health>().HP > DestructionStages[CurrentStage - 1].HPThreshold) fixStage(); };

    }

    private void sortStages() {
        DestructionStage _temp;
        bool swapped = true;

        while (swapped) {
            swapped = false;
            for (int i = 0; i < (DestructionStages.Length - 1); i++) {
                if (DestructionStages[i].HPThreshold < DestructionStages[i+1].HPThreshold)
                {
                    _temp = DestructionStages[i];
                    DestructionStages[i] = DestructionStages[i+1];
                    DestructionStages[i+1] = _temp;
                    swapped = true;
                }
            }
        }


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
        this.gameObject.GetComponent<MeshFilter>().mesh = DestructionStages[stage].Prefab.GetComponent<MeshFilter>().sharedMesh;
        this.gameObject.GetComponent<MeshRenderer>().material = DestructionStages[stage].Prefab.GetComponent<MeshRenderer>().sharedMaterial;
        this.gameObject.GetComponent<Transform>().localRotation = DestructionStages[stage].Prefab.GetComponent<Transform>().localRotation;
        this.gameObject.GetComponent<Transform>().localScale = DestructionStages[stage].Prefab.GetComponent<Transform>().localScale;
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
