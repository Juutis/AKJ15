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

    private float totalRunScore = 0;
    private float currentDayScore = 0;
    private int currentStreak = 0;
    private int correctlyPlacedBooksToday = 0;

    private Dictionary<DropZone, List<Genre>> correctZones = new()
    {
        { DropZone.Bag, new List<Genre>() { Genre.Rebel } },
        { DropZone.Burn, new List<Genre>() { Genre.Rebel, Genre.SoftScience, Genre.SoftScience, Genre.History } },
        { DropZone.Save, new List<Genre>() { Genre.GenericProse, Genre.HardScience } }
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

    public float DropBook(DropZone zone, Book book)
    {
        float score = 0;
        if (correctZones[zone].Contains(book.Genre))
        {
            currentStreak++;
            score = 1 + 0.1f * currentStreak;
            correctlyPlacedBooksToday++;
        }

        // TODO: Delete book here?

        currentDayScore += score;
        return score;
    }

    private void InitializeDay()
    {
        dayStarted = Time.time;
        BookManager.main.InitializeDay(dayConfigs[currentDay]);
        currentDayScore = 0;
        correctlyPlacedBooksToday = 0;
        // currentStreak = 0; // Reset streak?
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