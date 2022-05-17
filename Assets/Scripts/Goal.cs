using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    public Transform MainPlatform;
    private Vector3 _mainPlatformTargetPosition;
    public Color MainPlatformEndColor;
    public Door TargetDoor;
    public TextMesh CurrentPickupsText;
    public TextMesh RequiredPickupsText;
    public int CurrentPickups = 0;
    public int RequiredPickups = 5;
    public Image UnitStatus;
    public Color UnitCompleteColor;
    private bool _isComplete = false;
    private float _completeTimer = 2.5f;

    private void Awake()
    {
        CurrentPickupsText.text = CurrentPickups.ToString();
        RequiredPickupsText.text = " / " + RequiredPickups.ToString();
        _mainPlatformTargetPosition = new Vector3(MainPlatform.transform.position.x, 0, MainPlatform.transform.position.z);
    }

    private void FixedUpdate()
    {
        if (_isComplete)
        {
            MainPlatform.GetComponent<Renderer>().material.color = Color.Lerp(MainPlatform.GetComponent<Renderer>().material.color, MainPlatformEndColor, Mathf.PingPong(Time.fixedDeltaTime, 1));
            MainPlatform.position = Vector3.MoveTowards(MainPlatform.transform.position, _mainPlatformTargetPosition, 1f);
            if (_completeTimer > 0)
            {
                _completeTimer -= Time.fixedDeltaTime;
            }
            else
            {
                MainPlatform.GetComponent<Renderer>().material.color = MainPlatformEndColor;
                FindObjectOfType<PlayerController>().IsMoving = true;
                this.enabled = false;
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Ball")
        {
            Pickup pickup = col.gameObject.GetComponent<Pickup>();
            if (!pickup.IsCollected)
            {
                pickup.IsCollected = true;
                Destroy(pickup.gameObject);
                Collect(1);
            }
        }
    }

    void Collect(int p)
    {
        CurrentPickups += p;
        CurrentPickupsText.text = CurrentPickups.ToString();
        if ( (!_isComplete) && (CurrentPickups >= RequiredPickups) )
        {
            _isComplete = true;
            UnitStatus.color = UnitCompleteColor;
            TargetDoor.IsOpen = true;
            FindObjectOfType<GameManager>().toggleFailTimer(false);
            DestroyObject(CurrentPickupsText);
            DestroyObject(RequiredPickupsText);
        }
    }
}
