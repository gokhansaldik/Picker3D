using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    GameManager gamaManager;

    public bool isMoving = false;
    public float moveSpeed = 1f;

    bool inputEnabled = true;
    public float strafeSpeed = 1f;
    float strafeDir = 0;
    bool mouseDown = false;
    Vector3 mousePos;
    Vector3 last_mousePos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        gamaManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if ((isMoving)&&(inputEnabled))
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseDown = true;
            }

            if (Input.GetMouseButton(0))
            {
                if (mouseDown)
                {
                    mousePos = Input.mousePosition - last_mousePos;
                }
                

                if (mousePos.x != 0f)
                {
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        strafeDir = 1;
                    }
                    else if (Input.GetAxis("Mouse X") < 0)
                    {
                        strafeDir = -1;
                    }
                }
                last_mousePos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                mouseDown = false;
                strafeDir = 0;
            }
        }
        

    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            rb.velocity = new Vector3(strafeSpeed* strafeDir, rb.velocity.y, moveSpeed);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StopZone")
        {
            Debug.Log("Player stopped");
            isMoving = false;
            mouseDown = false;
            strafeDir = 0;

            other.gameObject.SetActive(false);
            gamaManager.toggleFailTimer(true);
        }
        else if (other.gameObject.tag == "FinishLine")
        {
            Debug.Log("Player completed the level.");
            inputEnabled = false;
            mouseDown = false;
            strafeDir = 0;

            other.gameObject.SetActive(false);
            gamaManager.showLevelCompleteScreen();
        }
    }

    

    float SetLimits(float x ,float xL, float xR)
    {
        if (x < xL)
        {
            x = xL;
        }
        else if (x > xR)
        {
            x = xR;
        }
        return x;

    }

}
