using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private float movementRange = 50.0f;
    private RectTransform thumb_rect;
    public Vector2 direction { get; private set; } = Vector2.zero;
    public bool isDragging { get; private set; } = false;

    private float ticks = 0.0f;

    // Start is called before the first frame update
    private void Start()
    {
        thumb_rect = this.transform.Find("Thumb").gameObject.GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!isDragging && ticks < 1.0f)
        {
            ticks += Time.deltaTime;
            thumb_rect.anchoredPosition = Vector2.Lerp(thumb_rect.anchoredPosition, Vector2.zero, ticks);
            direction = thumb_rect.anchoredPosition / movementRange;
        }
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;
        ticks = 0.0f;
    }
    public void OnDrag(PointerEventData eventData)
    {
        thumb_rect.anchoredPosition = Vector2.ClampMagnitude(thumb_rect.anchoredPosition + eventData.delta, movementRange);
        direction = thumb_rect.anchoredPosition / movementRange;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;
        //thumb_rect.anchoredPosition = Vector2.zero;
        //direction = Vector2.zero;
    }

}
