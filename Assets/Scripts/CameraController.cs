using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject Target;
    public Vector3 Offset;
    private void Update()
    {
        if (Target!=null)
        {
            transform.position = new Vector3(transform.position.x + Offset.x, transform.position.y + Offset.y, Target.transform.position.z + Offset.z);
        }
    }
}
