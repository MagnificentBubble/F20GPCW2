using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    public float senseX;
    public float senseY;

    public Transform orientation;
    public Transform PlayerObj;
    public Transform player;

    float xRotation;
    float yRotation;

    public float rotationSpeed;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    
        senseX=400;
        senseY=400;
    }

    private void Update() 
    {
        // Get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime *senseX;   
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime *senseY;

        yRotation += mouseX;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        
    }

    public void LockCamera(Transform Cop){
        transform.LookAt(Cop.position);//***************Change this to lock camera
        senseX=0;
        senseY=0;
    }


    public void UnlockCamera(){
        senseX=400;
        senseY=400;
    }
}
