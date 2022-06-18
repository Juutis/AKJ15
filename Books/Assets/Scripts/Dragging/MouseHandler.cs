using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseHandler : MonoBehaviour
{
    private Camera mainCam;

    private int draggables;
    private int hoverables;

    private DraggableObject currentDraggable;
    private ClickableObject currentClickable;

    private float dragDistanceThreshold = 0.25f;
    private float clickMaxDelay = 0.25f;
    private float dragStartTime;
    private Vector2 dragStartPos;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        draggables = LayerMask.GetMask("Draggable");
        hoverables = LayerMask.GetMask("Hoverable", "Draggable");
    }

    // Update is called once per frame
    void Update()
    {
        var mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        var hoverHits = Physics2D.RaycastAll(mouseWorldPos, Vector2.zero, 0, hoverables);
        var currentDraggingTarget = hoverHits.Select(it => it.transform.GetComponent<DraggingTarget>())
            .Where(it => it != null)
            .FirstOrDefault();

        var clickables = hoverHits.Select(it => it.transform.GetComponent<ClickableObject>())
            .Where(it => it != null);
        
        if (!clickables.Contains(currentClickable))
        {
            currentClickable = clickables.FirstOrDefault();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (currentClickable != null)
            {
                var draggable = currentClickable.GetComponent<DraggableObject>();
                if (draggable == null)
                {
                    currentClickable.OnClick();
                }
                else
                {
                    draggable.OnBeginDrag();
                    currentDraggable = draggable;
                    dragStartPos = mouseWorldPos;
                    dragStartTime = Time.time;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (currentDraggable != null)
            {
                currentDraggable.OnEndDrag(currentDraggingTarget);
                currentDraggable = null;
            }
            if (currentClickable != null)
            {
                if (Time.time < dragStartTime + clickMaxDelay && Vector2.Distance(dragStartPos, mouseWorldPos) < dragDistanceThreshold)
                {
                    currentClickable.OnClick();
                }
            }
        }

        if (currentDraggable != null)
        {
            currentDraggable.OnDrag();
        }
    }
}
