using System.Collections.Generic;
using UnityEngine;
using Serializable = System.SerializableAttribute;

[Serializable]
public class BookPage
{
    private Genre genre;
    private int lineCount = 6;
    public List<BookLine> Lines { get; set; }

    public BookPage(Genre genre)
    {
        this.genre = genre;
        Lines = new List<BookLine>();
        int nonRandom = Random.Range(0, lineCount);
        for (int i = 0; i < lineCount; i++)
        {
            if (i == nonRandom)
            {
                Lines.Add(new BookLine(false)
                {
                    Text = BookManager.main.GetRandomLine(genre)
                });
            }
            else
            {
                Lines.Add(new BookLine());
            }
        }
    }
}
