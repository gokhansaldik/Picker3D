using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    private void Update()
    {
        if (target!=null)
        {
            transform.position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, target.transform.position.z + offset.z);
        }
        
    }
}
