using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countText;
    public int BoxCount;
    public int MinCount;
    private void Start()
    {
        countText.text = BoxCount + "/" + MinCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            BoxCount++;
            countText.text = BoxCount + "/" + MinCount;
            Destroy(other.gameObject);
        }

    } 
}
