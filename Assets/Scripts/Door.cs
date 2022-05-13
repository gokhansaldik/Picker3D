using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float rotationSpeed = 1f;
    float rotationTimer = 1.4f;

    public Transform doorL;
    public Transform doorR;
        

    private void FixedUpdate()
    {
        if (isOpen)
        {
            if (rotationTimer > 0)
            {
                rotationTimer -= Time.fixedDeltaTime;
                doorL.transform.Translate(-rotationSpeed, 0, 0);
                doorR.transform.Translate(rotationSpeed, 0, 0);
            }
            else
            {
                this.enabled = false;
            }

        }
    }
    
}
