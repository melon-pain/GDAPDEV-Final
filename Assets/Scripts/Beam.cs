using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    [SerializeField] private Element element;
    [SerializeField] private List<Material> materials = new List<Material>();
    [SerializeField] private List<Color> startColors;
    [SerializeField] private List<Color> endColors;
    
    [SerializeField] private Transform beamStartPos;
    [SerializeField] private Transform beamEndPos;
    private LineRenderer lineRend;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    private void Start()
    {
        lineRend = this.gameObject.GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            beamEndPos.localPosition -= new Vector3(Time.deltaTime * 50f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            beamEndPos.localPosition += new Vector3(Time.deltaTime * 50f, 0f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            beamEndPos.localPosition += new Vector3(0f, Time.deltaTime * 50f, 0f);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            beamEndPos.localPosition -= new Vector3(0f, Time.deltaTime * 50f, 0f);
        }

        if(Physics.Linecast(beamStartPos.position, beamEndPos.position, out hit))
        {
            print($"Line hit: {hit.collider.gameObject.name}");
        }

        Debug.DrawLine(beamStartPos.position, beamEndPos.position, Color.green);
        lineRend.SetPosition(0, beamStartPos.localPosition);
        lineRend.SetPosition(1, beamEndPos.localPosition);
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

}
