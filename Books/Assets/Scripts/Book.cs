using System.Collections;
using System.Collections.Generic;
using Serializable = System.SerializableAttribute;

[Serializable]
public class Book
{
    private int pageCount = 6;

    public List<BookPage> Pages { get; set; }

    public Book()
    {
        Pages = new List<BookPage>();
        for (int i = 0; i < pageCount; i++)
        {
            Pages.Add(new BookPage());
        }
    }
}

public enum Genre
{
    Rebel,
    GenericProse,
    Vulgar,
    HardScience,
    SoftScience
}