using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private Element element;
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private List<Color> startColors;
    [SerializeField] private List<Color> endColors;
    
    [SerializeField] private Transform beamStart;
    [SerializeField] private Transform beamEnd;
    private LineRenderer lineRend;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    private void Start()
    {
        lineRend = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            beamEnd.localPosition -= new Vector3(Time.deltaTime * 50f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            beamEnd.localPosition += new Vector3(Time.deltaTime * 50f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            beamEnd.localPosition += new Vector3(0f, Time.deltaTime * 50f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            beamEnd.localPosition -= new Vector3(0f, Time.deltaTime * 50f, 0f);
        }

        if(Physics.Linecast(beamStart.position, beamEnd.position, out hit))
        {
            print($"Line hit: {hit.collider.gameObject.name}");
        }

        Debug.DrawLine(beamStart.position, beamEnd.position, Color.green);
        lineRend.SetPosition(0, beamStart.localPosition);
        lineRend.SetPosition(1, beamEnd.localPosition);
    }

    private void OnValidate()
    {
        this.gameObject.GetComponent<LineRenderer>().material = materials[(int)element];
        if(startColors.Count >= 4 && endColors.Count >= 4)
        {
            this.gameObject.GetComponent<LineRenderer>().startColor = startColors[(int)element];
            this.gameObject.GetComponent<LineRenderer>().endColor = endColors[(int)element];
        }
    }

    public void SetBeamStartPosition(Vector3 pos)
    {
        beamStart.position = pos;
    }

    public void SetBeamEndPosition(Vector3 pos)
    {
        beamEnd.position = pos;
    }

}
