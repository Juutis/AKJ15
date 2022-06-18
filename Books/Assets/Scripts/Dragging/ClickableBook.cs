using UnityEngine;

public class ClickableBook : ClickableObject
{
    private Book book;
    override public void OnClick()
    {
        BrowsableBook.main.OpenBook(book);
    }

    public void Initialize(Book book) 
    {
        this.book = book;
    }
}
