using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Joystick joystick;
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float lookSpeed = 100.0f;
    [SerializeField] private Transform aimTarget;
    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 dir = joystick.direction;
        Move(dir);
        Look(dir);
        Lean(dir);
    }

    public void Move(Vector2 direction)
    {
        this.transform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
        this.ClampPosition();

    }

    private void ClampPosition()
    {
        Vector3 worldPosition = Camera.main.WorldToViewportPoint(transform.position);
        worldPosition.x = Mathf.Clamp01(worldPosition.x);
        worldPosition.y = Mathf.Clamp01(worldPosition.y);
        transform.position = Camera.main.ViewportToWorldPoint(worldPosition);
    }

    public void Look(Vector2 direction)
    {
        aimTarget.parent.position = Vector3.zero;
        aimTarget.localPosition = new Vector3(direction.x, direction.y, 1);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.localPosition), Mathf.Deg2Rad * lookSpeed);
    }

    public void Lean(Vector2 direction)
    {
        Vector3 targetEulerAngles = this.transform.localEulerAngles;
        this.transform.localEulerAngles = new Vector3(targetEulerAngles.x, targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -direction.x * 45, 0.25f));
    }
}
