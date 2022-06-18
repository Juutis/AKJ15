using System.Collections.Generic;
using UnityEngine;
using Serializable = System.SerializableAttribute;

[Serializable]
public class BookPage
{
    private int lineCount = 14;
    public List<BookLine> Lines { get; set; }

    public BookPage()
    {
        Lines = new List<BookLine>();
        int nonRandom = Random.Range(0, lineCount);
        for (int i = 0; i < lineCount; i++)
        {
            Lines.Add(new BookLine(i != nonRandom));
        }
    }
}
