using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClickableBook : ClickableObject
{
    public Book book { get; private set; }

    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private List<Color> coverColors;

    private Color coverColor;

    override public void OnClick()
    {
        if (BrowsableBook.main.IsOpen) 
        {
            return;
        }
        BrowsableBook.main.OpenBook(book, coverColor);
    }

    public void Initialize(Book book) 
    {
        this.book = book;

        var style = Random.Range(0, sprites.Count);
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[style];
        coverColor = coverColors[style];
    }
}
