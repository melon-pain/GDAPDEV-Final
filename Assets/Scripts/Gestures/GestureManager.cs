using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GestureManager : MonoBehaviour
{
    public static GestureManager instance = null;
    private Touch finger_1;

    [Header("Layers"), Tooltip("Which layers to test for gestures")]
    [SerializeField] private LayerMask cullingMask;

    [Header("Tap")]
    [SerializeField] private TapProperty tapProperty;
    [Space(4.0f)] public UnityEvent<TapEventData> OnTap;

    [Header("Swipe")]
    [SerializeField] private SwipeProperty swipeProperty;
    [Space(4.0f)] public UnityEvent<SwipeEventData> OnSwipe;

    private Vector2 startPoint = Vector2.zero;
    private Vector2 endPoint = Vector2.zero;
    private float gestureTime = 0.0f;

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
        if (Input.touchCount > 0)
        {
            finger_1 = Input.GetTouch(0);

            switch (finger_1.phase)
            {
                case TouchPhase.Began:
                    startPoint = finger_1.position;
                    gestureTime = 0.0f;
                    break;
                case TouchPhase.Ended:
                    endPoint = finger_1.position;
                    if (gestureTime <= tapProperty.GetTapTime() && Vector2.Distance(startPoint, endPoint) < (Screen.dpi * tapProperty.GetTapDistance()) )
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
        }
    }

    private void Tap(Vector2 pos)
    {
        //Check for event listeners
        if (OnTap.GetPersistentEventCount() > 0)
        {
            //Let UI consume 
            if (eventSystem.currentSelectedGameObject)
                return;
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

            //Let UI consume 
            if (eventSystem.currentSelectedGameObject)
                return;

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
        }
    }
}
