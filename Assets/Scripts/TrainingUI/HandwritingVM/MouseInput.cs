using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Strokes.Handwriting
{
    /// <summary>
    /// 
    /// </summary>
    [RequireComponent(typeof(HandwritingController))]
    public class MouseInput : MonoBehaviour
    {

        Vector2 sPos;
        Vector3 wPos;
       
        HandwritingController controller;



        private void Start()
        {
            //hpm = GetComponent<HandwritingPointsManager>();
            controller = GetComponent<HandwritingController>();
        }


        private void Update()
        {
            
            sPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            wPos = Camera.main.ScreenToWorldPoint(new Vector3(sPos.x, sPos.y, 10f));

            controller.onUpdate.Invoke(sPos, wPos);

            RaycastHit2D hit = Physics2D.Raycast(wPos, Vector2.zero);
            if (hit.collider != null && hit.collider.CompareTag("box"))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    controller.onStartDrawing.Invoke();
                }
                if (Input.GetMouseButton(0))
                {
                    controller.onDrawing.Invoke();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    controller.onEndDrawing.Invoke();
                }
            }

        }

    }
}