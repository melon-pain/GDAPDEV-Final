using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GestureManager : MonoBehaviour
{
    public static GestureManager instance = null;
    private Touch finger_1;
    private Touch finger_2;

    [Header("Layers"), Tooltip("Which layers to test for gestures")]
    [SerializeField] private LayerMask cullingMask;

    [Header("Tap")]
    [SerializeField] private TapProperty tapProperty;
    [Space(4.0f)] public UnityEvent<TapEventData> OnTap;

    [Header("Swipe")]
    [SerializeField] private SwipeProperty swipeProperty;
    [Space(4.0f)] public UnityEvent<SwipeEventData> OnSwipe;

    [Header("Drag")]
    [SerializeField] private DragProperty dragProperty;
    [Space(4.0f)] public UnityEvent<DragEventData> OnDrag;

    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0.0f;

    private bool wasTouchingUI = false;

    private EventSystem eventSystem;

    // Start is called before the first frame update
    private void Start()
    {
        if (!instance)
            instance = this;
        else
            Destroy(this.gameObject);

        eventSystem = EventSystem.current;
    }

    // Update is called once per frame
    private void Update()
    {
        int touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            switch (touchCount)
            {
                case 1:
                    OneFingerGesture();
                    break;
                case 2:
                    TwoFingerGesture();
                    break;
            }

        }
    }

    private void OneFingerGesture()
    {
        //Consume UI
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || EventSystem.current.currentSelectedGameObject)
        {
            wasTouchingUI = true;
            return;
        }

        else if (wasTouchingUI)
        {
            wasTouchingUI = false;
            return;
        }

        finger_1 = Input.GetTouch(0);

        switch (finger_1.phase)
        {
            case TouchPhase.Began:
                startPoint = finger_1.position;
                gestureTime = 0.0f;
                break;
            case TouchPhase.Ended:
                endPoint = finger_1.position;
                if (gestureTime <= tapProperty.GetTapTime() && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * tapProperty.GetTapDistance()))
                {
                    Tap(finger_1.position);
                }
                else if (gestureTime <= swipeProperty.GetSwipeTime() && Vector2.Distance(startPoint, endPoint) >= (Screen.dpi * swipeProperty.GetMinSwipeDistance()))
                {
                    Swipe();
                }
                break;
            default:
                gestureTime += Time.deltaTime;
                break;
        }
        if (gestureTime >= dragProperty.dragBufferTime)
        {
            Drag();
        }

    }

    private void TwoFingerGesture()
    {
        //Consume UI
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId) || 
            EventSystem.current.IsPointerOverGameObject(Input.GetTouch(1).fingerId) || 
            EventSystem.current.alreadySelecting)
            return;

        finger_1 = Input.GetTouch(0);
        finger_2 = Input.GetTouch(1);

    }

    private void Tap(Vector2 pos)
    {
        //Check for event listeners
        if (OnTap.GetPersistentEventCount() > 0)
        {
            GameObject hitObj = null;
            Ray r = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(r, out hit, Mathf.Infinity, cullingMask))
            {
                hitObj = hit.collider.gameObject;
                Debug.Log(hitObj.name);
            }

            TapEventData tapEventData = new TapEventData(pos, hitObj);
            OnTap.Invoke(tapEventData);
        }
    }

    private void Swipe()
    {
        //Check for event listeners
        if (OnSwipe.GetPersistentEventCount() > 0)
        {
            Vector2 dir = endPoint - startPoint;

            GameObject hitObj = null;
            Ray r = Camera.main.ScreenPointToRay(startPoint);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(r, out hit, Mathf.Infinity, cullingMask))
            {
                hitObj = hit.collider.gameObject;
                Debug.Log(hitObj.name);
            }

            SwipeDirection swipeDir = SwipeDirection.Right;

            if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
            {
                if (dir.x > 0.0f)
                    swipeDir = SwipeDirection.Right;
                else
                    swipeDir = SwipeDirection.Left;
            }
            else
            {
                if (dir.y > 0.0f)
                    swipeDir = SwipeDirection.Up;
                else
                    swipeDir = SwipeDirection.Down;
            }

            SwipeEventData swipeEventData = new SwipeEventData(startPoint, swipeDir, dir, hitObj);
            OnSwipe.Invoke(swipeEventData);

            Debug.Log("Swipe");
        }
    }

    private void Drag()
    {
        Ray r = Camera.main.ScreenPointToRay(finger_1.position);
        RaycastHit hit = new RaycastHit();
        GameObject hitObj = null;

        if (Physics.Raycast(r, out hit, Mathf.Infinity, cullingMask))
        {
            hitObj = hit.collider.gameObject;
            Debug.Log("Drag hit");
        }

        DragEventData dragEventData = new DragEventData(finger_1, hitObj);
        if (OnDrag != null)
        {
            OnDrag.Invoke(dragEventData);
        }

    }

}
