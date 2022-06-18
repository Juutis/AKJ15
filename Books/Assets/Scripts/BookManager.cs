using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager main;

    [SerializeField]
    private BookConfig config;

    [SerializeField]
    private int bookCount = 3;
    public List<Book> Books { get; set; }

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        if (Books == null)
        {
            Books = new List<Book>();
        }

        for (int i = 0; i < bookCount; i++)
        {
            Books.Add(new Book());
        }
    }

    void Update()
    {
        
    }

    public BookConfig GetConfig()
    {
        return config;
    }
}
