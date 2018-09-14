using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(HandwritingPointsManager))]
public class MouseInput : MonoBehaviour {

    public Vector2 sPos;
    public Vector3 wPos;
    HandwritingPointsManager hpm;
    public GameObject box;
    private void Start()
    {
        hpm = GetComponent<HandwritingPointsManager>();
    }


    private void Update()
    {
        sPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        wPos = Camera.main.ScreenToWorldPoint(new Vector3(sPos.x, sPos.y, 10f));

        RaycastHit2D hit = Physics2D.Raycast(wPos, Vector2.zero);
        if(hit.collider != null && hit.collider.CompareTag("box"))
        {
            //Debug.Log(hit.collider.CompareTag("box"));
            if (Input.GetMouseButtonDown(0))
            {

                hpm.OnInitDrawing(sPos, wPos);
            }
            if (Input.GetMouseButton(0))
            {
                hpm.OnDrawing(sPos, wPos);
            }
            if (Input.GetMouseButtonUp(0))
            {
                hpm.OnEndDrawing(sPos, wPos);
            }
        }
        
    }

}
