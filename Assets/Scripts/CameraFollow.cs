using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;

    [Space]
    [SerializeField] private Vector3 offset = Vector3.zero;
    [Range(0, 1)]
    [SerializeField] private float smoothTime = 0.2f;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    private void Update()
    {
        if (!Application.isPlaying)
        {
            transform.localPosition = offset;
        }
        FollowTarget();
    }

    //private void LateUpdate()
    //{
    //    Vector3 localPos = transform.localPosition;
    //    transform.localPosition = new Vector3(Mathf.Clamp(localPos.x, -limits.x, limits.x), Mathf.Clamp(localPos.y, -limits.y, limits.y), localPos.z);
    //}

    private void FollowTarget()
    {
        Vector3 localPos = this.transform.localPosition;
        Vector3 targetLocalPos = target.localPosition;
        this.transform.localPosition = Vector3.SmoothDamp(localPos, targetLocalPos + offset, ref velocity, smoothTime);
    }
}
