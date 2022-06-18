using UnityEngine;

public class DraggableObject : MonoBehaviour
{
    private Vector2 dragOffset;
    private Camera mainCam;
    private Vector2 snapPosition;
    private bool snapping = false;
    private float snapSpeed = 10.0f;

    private DraggingTarget targetDropPoint = null;

    public void OnBeginDrag()
    {
        if (!snapping)
        {
            snapPosition = transform.position;
        }
        snapping = false;
        var mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        dragOffset = transform.position - mouseWorldPos;
    }

    public void OnDrag()
    {
        var mouseWorldPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = (Vector2)mouseWorldPos + dragOffset;
    }

    public void OnEndDrag(DraggingTarget target)
    {
        if (target != null) 
        {
            if (target.TargetType == DraggingTarget.Type.TABLE)
            {
                snapPosition = transform.position;
            }
            else
            {
                snapPosition = target.transform.position;
                targetDropPoint = target;
            }
            snapping = true;
        }
        else
        {
            snapping = true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (snapping)
        {
            transform.position = Vector2.MoveTowards(transform.position, snapPosition, Time.deltaTime * snapSpeed);
        }

        if (targetDropPoint != null && Vector2.Distance(transform.position, snapPosition) < 0.1f)
        {
            var book = GetComponent<ClickableBook>();
            if (book != null) 
            {
                DropZone zone = DropZone.Save;
                switch (targetDropPoint.TargetType)
                {
                    case DraggingTarget.Type.SAVE:
                        zone = DropZone.Save;
                        break;
                    case DraggingTarget.Type.BURN:
                        zone = DropZone.Burn;
                        break;
                    case DraggingTarget.Type.BAG:
                        zone = DropZone.Bag;
                        break;
                }
                GameManager.main.DropBook(zone, book.book);
            }
            
            Destroy(gameObject);
        }
    }
}
