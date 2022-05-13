using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Transform mainPlatform;
    Vector3 mainPlatform_targetPosition;
    public Color mainPlatform_endColor;

    public Door targetDoor;

    public TextMesh currentPickupsText;
    public TextMesh requiredPickupsText;

    public int _currentPickups = 0;
    public int _requiredPickups = 5;

    [Header("UI")]
    public Image unitStatus;
    public Color unitCompleteColor;

    bool isComplete = false;
    float _completeTimer = 2.5f;

    private void Awake()
    {
        currentPickupsText.text = _currentPickups.ToString();
        requiredPickupsText.text = " / " + _requiredPickups.ToString();
        mainPlatform_targetPosition = new Vector3(mainPlatform.transform.position.x, 0, mainPlatform.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (isComplete)
        {
            mainPlatform.GetComponent<Renderer>().material.color = Color.Lerp(mainPlatform.GetComponent<Renderer>().material.color, mainPlatform_endColor, Mathf.PingPong(Time.fixedDeltaTime, 1));
            mainPlatform.position = Vector3.MoveTowards(mainPlatform.transform.position, mainPlatform_targetPosition, 1f);
            if (_completeTimer > 0)
            {
                _completeTimer -= Time.fixedDeltaTime;
            }
            else
            {
                mainPlatform.GetComponent<Renderer>().material.color = mainPlatform_endColor;
                FindObjectOfType<PlayerController>().isMoving = true;
                this.enabled = false;
            }
        }
        
        
    }

    private void OnCollisionEnter(Collision col)
    {
        
        if(col.gameObject.tag == "Ball")
        {
            Pickup pickup = col.gameObject.GetComponent<Pickup>();
            if (!pickup.isCollected)
            {
                pickup.isCollected = true;
                Destroy(pickup.gameObject);
                Collect(1);
            }
        }
    }

    void Collect(int p)
    {
        _currentPickups += p;
        currentPickupsText.text = _currentPickups.ToString();
        
        if ( (!isComplete) && (_currentPickups >= _requiredPickups) )
        {
            isComplete = true;
            unitStatus.color = unitCompleteColor;
            targetDoor.isOpen = true;
            FindObjectOfType<GameManager>().toggleFailTimer(false);
            DestroyObject(currentPickupsText);
            DestroyObject(requiredPickupsText);
        }
    }
}
