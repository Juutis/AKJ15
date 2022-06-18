using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public static BookManager main;

    [SerializeField]
    private BookConfig config;

    [SerializeField]
    private int bookCount = 3;

    public List<Book> Books { get; set; }

    public Dictionary<Genre, List<string>> GenreText { get; set; }

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        InitializeGenres();
        if (Books == null)
        {
            Books = new List<Book>();
        }
    }

    void Update()
    {
        
    }
    
    public void InitializeDay(DayConfig dayConfig)
    {
        bookCount = 0;
        Books = new List<Book>();
        dayConfig.GenreBookCounts.ForEach(x =>
        {
            for (int i = 0; i < x.count; i++)
            {
                Books.Add(new Book(Genre.History));
                bookCount++;
            }
        });
    }

    public BookConfig GetConfig()
    {
        return config;
    }

    public string GetRandomLine(Genre genre)
    {
        List<string> strings = GenreText[genre];
        return strings[Random.Range(0, strings.Count)];
    }

    private void InitializeGenres()
    {
        GenreText = new Dictionary<Genre, List<string>>();
        GenreText.Add(Genre.GenericProse, GenericProseTexts.Texts);
        GenreText.Add(Genre.HardScience, HardScienceTexts.Texts);
        GenreText.Add(Genre.Rebel, RebelTexts.Texts);
        GenreText.Add(Genre.SoftScience, SoftScienceTexts.Texts);
        GenreText.Add(Genre.Vulgar, VulgarTexts.Texts);
        GenreText.Add(Genre.History, HistoryTexts.Texts);
    }
}
