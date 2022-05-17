using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public GameObject Particle;
    public ParticleSystem ParticleSpawner;
    public bool IsOpen = false;
    public float RotationSpeed = 1f;
    private float _rotationTimer = 1.4f;
    public Transform DoorLeft;
    public Transform DoorRight;


   
    private void FixedUpdate()
    {
        if (IsOpen)
        {
            if (_rotationTimer > 0)
            {
                _rotationTimer -= Time.fixedDeltaTime;
                DoorLeft.transform.Translate(-RotationSpeed, 0, 0);
                DoorRight.transform.Translate(RotationSpeed, 0, 0);
                Particle.SetActive(true);
                ParticleSpawner.Play();

            }
            else
            {
                this.enabled = false;
            }
        }
    }
    
}
