using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private Joystick joystick;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10.0f;
    [SerializeField] private float rollMoveSpeed = 20.0f;
    [SerializeField] private float rollTurnSpeed = 4.0f;
    [SerializeField] private float moveLimit = 10.0f;

    public bool isRolling { get; private set; } = false;
    private Vector3 rollDirection = Vector3.zero;
    private float rollTime = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (isRolling)
        {
            rollTime += Time.deltaTime * rollTurnSpeed;
            Roll();
            if (rollTime >= 1.0f)
            {
                isRolling = false;
            }
        }
        else
        {
            Vector2 dir = joystick.axis;
            Move(dir);
            Lean(dir);
        }
    }

    public void Move(Vector2 direction)
    {
        this.transform.localPosition += (Vector3)direction * moveSpeed * Time.deltaTime;
        this.ClampPosition();
    }

    private void ClampPosition()
    {
        //Vector3 worldPosition = Camera.main.WorldToViewportPoint(transform.position);
        //worldPosition.x = Mathf.Clamp01(worldPosition.x);
        //worldPosition.y = Mathf.Clamp01(worldPosition.y);
        //transform.position = Camera.main.ViewportToWorldPoint(worldPosition);

        this.transform.localPosition = Vector3.ClampMagnitude(this.transform.localPosition, moveLimit);
    }

    //public void Look(Vector2 direction)
    //{
    //    aimTarget.parent.localPosition = Vector3.zero;
    //    aimTarget.localPosition = new Vector3(direction.x, direction.y, 1);
    //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(aimTarget.localPosition), Mathf.Deg2Rad * lookSpeed * Time.deltaTime);
    //}

    public void Lean(Vector2 direction)
    {
        Vector3 targetEulerAngles = this.transform.localEulerAngles;
        this.transform.localEulerAngles = new Vector3(Mathf.LerpAngle(targetEulerAngles.x, -direction.y * 45, 0.1f), targetEulerAngles.y, Mathf.LerpAngle(targetEulerAngles.z, -direction.x * 45, 0.1f));
    }

    public void BarrelRoll(SwipeEventData swipeEventData)
    {
        if (swipeEventData.direction == SwipeDirection.Left || swipeEventData.direction == SwipeDirection.Right)
        {
            isRolling = true;
            rollTime = 0.0f;
            rollDirection = swipeEventData.swipeVector.normalized;
        }
    }

    private void Roll()
    {
        this.transform.localEulerAngles = Vector3.Lerp(Vector3.zero, Vector3.forward * 360.0f * Mathf.Sign(-rollDirection.x), rollTime);
        this.transform.localPosition += rollDirection * rollMoveSpeed * Time.deltaTime;
        this.ClampPosition();
    }
}
