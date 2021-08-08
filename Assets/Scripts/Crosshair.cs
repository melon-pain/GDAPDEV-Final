using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [Header("Target to follow")]
    [SerializeField] private Transform target;

    [Header("Settings")]
    [SerializeField] private float smoothTime = 0.1f;
    [SerializeField] private float distance = 1.0f;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void LateUpdate()
    {
        this.transform.localPosition = Vector3.SmoothDamp(this.transform.localPosition, target.localPosition + (Vector3.forward * distance), ref velocity, smoothTime);
        this.transform.forward = target.forward;
    }
}
