using UnityEngine;

public class ClickableBook : ClickableObject
{
    public Book book { get; private set; }
    override public void OnClick()
    {
        if (BrowsableBook.main.IsOpen) 
        {
            return;
        }
        BrowsableBook.main.OpenBook(book);
    }

    public void Initialize(Book book) 
    {
        this.book = book;
    }
}
