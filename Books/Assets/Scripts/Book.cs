using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Serializable = System.SerializableAttribute;

[Serializable]
public class Book
{
    private int pageCount;
    public Genre Genre { get; set; }

    public List<BookPage> Pages { get; set; }

    public Book(Genre genre)
    {
        Genre = genre;
        pageCount = BookManager.main.GetConfig().PageCount;
        Pages = new List<BookPage>();

        if (genre == Genre.Vulgar)
        {
            int countOfOffensivePages = pageCount / 3;
            List<int> offensivePageNumbers = new List<int>();

            while (offensivePageNumbers.Count < countOfOffensivePages)
            {
                int randomPage = Random.Range(0, pageCount);
                if (offensivePageNumbers.Contains(randomPage)) continue;
                offensivePageNumbers.Add(randomPage);
            }

            for (int i = 0; i < pageCount; i++)
            {
                Pages.Add(new BookPage(genre, !offensivePageNumbers.Contains(i)));
            }
        }
        else
        {
            for (int i = 0; i < pageCount; i++)
            {
                Pages.Add(new BookPage(genre));
            }
        }
    }
}
