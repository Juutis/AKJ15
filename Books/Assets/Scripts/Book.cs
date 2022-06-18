using System.Collections;
using System.Collections.Generic;
using Serializable = System.SerializableAttribute;

[Serializable]
public class Book
{
    private int pageCount;
    private Genre genre;

    public List<BookPage> Pages { get; set; }

    public Book(Genre genre)
    {
        this.genre = genre;
        pageCount = BookManager.main.GetConfig().PageCount;
        Pages = new List<BookPage>();
        for (int i = 0; i < pageCount; i++)
        {
            Pages.Add(new BookPage(genre));
        }
    }
}
