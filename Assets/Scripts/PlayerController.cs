using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidBody;
    private GameManager _gamaManager;
    
    
    public bool IsMoving = false;
    
    public float MoveSpeed = 1f;
    private bool InputEnabled = true;
    public float StrafeSpeed = 1f;
    private float _strafeDir = 0;
    private bool _mouseDown = false;
    private Vector3 _mousePos;
    private Vector3 _lastMousePos;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _gamaManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if ((IsMoving)&&(InputEnabled))
        {
            if (Input.GetMouseButtonDown(0))
            {
                _mouseDown = true;
            }

            if (Input.GetMouseButton(0))
            {
                if (_mouseDown)
                {
                    _mousePos = Input.mousePosition - _lastMousePos;
                }
                

                if (_mousePos.x != 0f)
                {
                    if (Input.GetAxis("Mouse X") > 0)
                    {
                        _strafeDir = 1;
                    }
                    else if (Input.GetAxis("Mouse X") < 0)
                    {
                        _strafeDir = -1;
                    }
                }
                _lastMousePos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _mouseDown = false;
                _strafeDir = 0;
            }
        }
        

    }

    private void FixedUpdate()
    {
        if (IsMoving)
        {
            _rigidBody.velocity = new Vector3(StrafeSpeed* _strafeDir, _rigidBody.velocity.y, MoveSpeed);
        }
        else
        {
            _rigidBody.velocity = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StopZone")
        {
            Debug.Log("Player stopped");
            IsMoving = false;
            _mouseDown = false;
            _strafeDir = 0;

            other.gameObject.SetActive(false);
            _gamaManager.toggleFailTimer(true);
        }
        else if (other.gameObject.tag == "FinishLine")
        {
            Debug.Log("Player completed the level.");
            InputEnabled = false;
            _mouseDown = false;
            _strafeDir = 0;

            other.gameObject.SetActive(false);
            _gamaManager.showLevelCompleteScreen();
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
