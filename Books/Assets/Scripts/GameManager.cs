using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField]
    private Image DayStartUI;
    [SerializeField]
    private GameObject FeedbackUI;
    [SerializeField]
    private GameConfig config;
    [SerializeField]
    private List<DayConfig> dayConfigs;

    private int currentDay = 0;
    private DayState gameState = DayState.Start;
    private float dayStarted;

    private int totalRunScore = 0;
    private int currentDayScore = 0;
    private int currentStreak = 0;
    private int correctlyPlacedBooksToday = 0;

    private Dictionary<DropZone, List<Genre>> correctZones = new()
    {
        { DropZone.Bag, new List<Genre>() { Genre.Rebel } },
        { DropZone.Burn, new List<Genre>() { Genre.Rebel, Genre.SoftScience, Genre.SoftScience, Genre.History, Genre.Vulgar } },
        { DropZone.Save, new List<Genre>() { Genre.GenericProse, Genre.HardScience, Genre.Propaganda } }
    };

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
    }

    void Update()
    {
        if (gameState == DayState.Start)
        {
            FeedbackUI.SetActive(false);
            DayStartUI.gameObject.SetActive(true);
            if (Input.GetKey(KeyCode.Space))
            {
                gameState = DayState.Game;
                StartCoroutine(FadeOutImage(DayStartUI, Color.black, new Color(0, 0, 0, 0)));
                InitializeDay();
            }
        }
        else if (gameState == DayState.Feedback)
        {
            FeedbackUI.SetActive(true);
            DayStartUI.gameObject.SetActive(false);
        }
        else
        {
            FeedbackUI.SetActive(false);
            if (Input.GetKey(KeyCode.Return))
            {
                gameState = DayState.Feedback;
            }
        }
    }

    public DayConfig GetDayConfig()
    {
        return dayConfigs[currentDay];
    }

    public DayState GetState()
    {
        return gameState;
    }

    public int DropBook(DropZone zone, Book book)
    {
        int score = 0;
        if (correctZones[zone].Contains(book.Genre))
        {
            currentStreak++;
            score = 10 + currentStreak;
            correctlyPlacedBooksToday++;
        }
        else
        {
            currentStreak = 0;
        }

        currentDayScore += score;
        totalRunScore += score;
        return score;
    }

    public void UpdateFeedback(FeedbackUI feedbackUI)
    {
        int totalBooksCount = BookManager.main.Books.Count;
        feedbackUI.UpdateScores(totalRunScore, currentDayScore, totalBooksCount, correctlyPlacedBooksToday, currentStreak);
    }

    private void InitializeDay()
    {
        dayStarted = Time.time;
        BookManager.main.InitializeDay(dayConfigs[currentDay]);
        currentDayScore = 0;
        correctlyPlacedBooksToday = 0;
    }

    private IEnumerator FadeOutImage(Image image, Color c1, Color c2)
    {
        for (float i = 0; i < 25; i++)
        {
            DayStartUI.color = Color.Lerp(c1, c2, Mathf.Pow(i / 25f, 2));
            yield return new WaitForSeconds(0.05f);
        }

        DayStartUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.05f);
    }
}

public enum DayState
{
    Start,
    Game,
    Feedback
}

public enum DropZone
{
    Save,
    Burn,
    Bag
}