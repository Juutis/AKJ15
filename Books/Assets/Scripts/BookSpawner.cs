using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    [SerializeField]
    private ClickableBook bookPrefab;

    [SerializeField]
    private Rect targetRect;

    [SerializeField]
    private Transform bookContainer;

    public static BookSpawner main;

    void Awake() {
        main = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnBook(Book book)
    {
        var bookObject = Instantiate(bookPrefab, bookContainer);
        bookObject.Initialize(book);
        bookObject.transform.position = (Vector2)transform.position + Random.insideUnitCircle * 0.1f;
        var draggable = bookObject.GetComponent<DraggableObject>();
        draggable.SetTargetPosition(new Vector2(Random.Range(targetRect.xMin, targetRect.xMax), Random.Range(targetRect.yMin, targetRect.yMax)));
    }
    
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        DrawRect(targetRect);
    }
    
    void DrawRect(Rect rect)
    {
        Gizmos.DrawWireCube(new Vector3(rect.center.x, rect.center.y, 0.01f), new Vector3(rect.size.x, rect.size.y, 0.01f));
    }

    public void ClearBooks()
    {
        foreach (Transform transform in bookContainer)
        {
            Destroy(transform.gameObject);
        }
    }
}
