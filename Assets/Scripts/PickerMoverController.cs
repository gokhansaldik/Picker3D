using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickerMoverController : MonoBehaviour
{
    [SerializeField] private float furtherMoveSpeed; 
    public float RoadSize = 10;
    public float SwipeSpeed = 5;
    public float Sensetive = 3;
    private float _initalX = 0;
    private float _startX;
    
    private void Update()
    {
        PlayerSwipe();
    }
    
    public void PlayerSwipe()
    {
        this.transform.Translate(0, 0, furtherMoveSpeed * Time.deltaTime);
        if (Input.GetMouseButtonDown(0))
        {
            _initalX = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            _startX = transform.localPosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            float screenPos = Camera.main.ScreenToViewportPoint(Input.mousePosition).x;
            screenPos = Mathf.Clamp(screenPos, 0, 1);
            float newX = _startX + (RoadSize / 2) * (screenPos - _initalX) * SwipeSpeed;
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(newX, transform.localPosition.y, transform.localPosition.z), Sensetive * Time.deltaTime);
            var localPos = transform.localPosition;
            localPos.x = Mathf.Clamp(localPos.x, -RoadSize / 2, RoadSize / 2);
            transform.localPosition = localPos;
        }
    }
}
