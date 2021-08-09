using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
public class GestureManager : MonoBehaviour
{
    public static GestureManager instance = null;
    private Touch finger_1;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private TapProperty tapProperty;
    public UnityEvent<TapEventData> OnTap;

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
            //Let UI consume input  
            if (eventSystem.currentSelectedGameObject)
                return;
            GameObject hitObj = null;
            Ray r = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit = new RaycastHit();

            if (Physics.Raycast(r, out hit, Mathf.Infinity, layerMask))
            {
                hitObj = hit.collider.gameObject;
                Debug.Log(hitObj.name);
            }

            TapEventData tapEventData = new TapEventData(pos, hitObj);
            OnTap.Invoke(tapEventData);
        }
    }
}
