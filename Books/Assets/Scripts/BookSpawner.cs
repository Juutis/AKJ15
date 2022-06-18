using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour
{
    [SerializeField]
    private ClickableBook bookPrefab;

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
        var bookObject = Instantiate(bookPrefab);
        bookObject.Initialize(book);
        bookObject.transform.position = (Vector2)transform.position + Random.insideUnitCircle * 0.5f;
    }
}
