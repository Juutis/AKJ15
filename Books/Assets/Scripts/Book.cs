using System.Collections;
using System.Collections.Generic;
using Serializable = System.SerializableAttribute;

[Serializable]
public class Book
{
    private int pageCount;

    public List<BookPage> Pages { get; set; }

    public Book()
    {
        pageCount = BookManager.main.GetConfig().PageCount;
        Pages = new List<BookPage>();
        for (int i = 0; i < pageCount; i++)
        {
            Pages.Add(new BookPage());
        }
    }
}
