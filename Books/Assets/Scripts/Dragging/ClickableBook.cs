using UnityEngine;

public class ClickableBook : ClickableObject
{
    override public void OnClick()
    {
        BrowsableBook.main.OpenBook(new Book(Genre.History));
    }
}
