using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    [SerializeField]
    private Image DayStartUI;
    [SerializeField]
    private FeedbackUI FeedbackUI;
    [SerializeField]
    private GameObject RulesUI;
    [SerializeField]
    private GameConfig config;
    [SerializeField]
    private List<DayConfig> dayConfigs;
    [SerializeField]
    private GameObject RulesHilight;

    private int currentDay = 0;
    private DayState gameState = DayState.Start;
    private float dayStarted;

    private int totalRunScore = 0;
    private int currentDayScore = 0;
    private int currentStreak = 0;
    private int correctlyPlacedBooksToday = 0;
    private bool firstDay = true;
    private int correctlySavedBooksToday = 0;
    private int handledBooksToday = 0;
    private int rebelBooksDeliveredToday = 0;

    public ReputationManager ReputationManager { get; private set; } = new ReputationManager();

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
            RulesUI.SetActive(false);
            FeedbackUI.gameObject.SetActive(false);
            DayStartUI.gameObject.SetActive(true);
            if (Input.anyKeyDown)
            {
                if (firstDay)
                {
                    gameState = DayState.FirstDayRules;
                }
                else
                {
                    gameState = DayState.Game;
                }
                StartCoroutine(FadeOutImage(DayStartUI, Color.black, new Color(0, 0, 0, 0)));
                InitializeDay();
            }
        }
        else if (gameState == DayState.Feedback)
        {
            FeedbackUI.gameObject.SetActive(true);
            RulesUI.SetActive(false);
            DayStartUI.gameObject.SetActive(false);
            FeedbackUI.TriggerEndOfDay();
        }
        else if (gameState == DayState.FirstDayRules)
        {

        }
        else
        {
            firstDay = false;
            FeedbackUI.gameObject.SetActive(false);
            if (Input.GetKey(KeyCode.Return))
            {
                gameState = DayState.Feedback;
            }
        }

        RulesHilight.SetActive(firstDay);
        Debug.Log(firstDay);
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
            if (zone == DropZone.Save)
            {
                correctlySavedBooksToday++;
            }
            if (zone == DropZone.Bag)
            {
                rebelBooksDeliveredToday++;
            }
        }
        else
        {
            currentStreak = 0;
        }

        handledBooksToday++;

        currentDayScore += score;
        totalRunScore += score;
        return score;
    }

    public void UpdateFeedback(FeedbackUI feedbackUI)
    {
        int totalBooksCount = BookManager.main.Books.Count;
        feedbackUI.UpdateScores(totalRunScore, currentDayScore, totalBooksCount, correctlyPlacedBooksToday, currentStreak);
    }

    public void ShowRules()
    {
        RulesUI.SetActive(true);
    }

    public void HideRules()
    {
        Debug.Log("OK");
        if (gameState == DayState.FirstDayRules)
        {
            gameState = DayState.Game;
        }

        RulesUI.SetActive(false);
    }

    private void InitializeDay()
    {
        dayStarted = Time.time;
        BookManager.main.InitializeDay(dayConfigs[currentDay]);
        currentDayScore = 0;
        correctlyPlacedBooksToday = 0;
        correctlySavedBooksToday = 0;
        handledBooksToday = 0;
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

    private void handleReputationChangeAfterDay()
    {
        int totalBooksToday = BookManager.main.Books.Count;
        int totalBooksHandled = correctlyPlacedBooksToday;
        ReputationManager.UpdateStateReputation(totalBooksToday, handledBooksToday, correctlySavedBooksToday);

        if (!firstDay)
        {
            var rebelBookConfig = GetDayConfig().GenreBookCounts.Where(it => it.genre == Genre.Rebel).FirstOrDefault();
            if (rebelBookConfig != null) {
                var totalRebelBooksToday = rebelBookConfig.count;
                ReputationManager.UpdateRebelReputation(totalRebelBooksToday, rebelBooksDeliveredToday);
            }
        }

    }
}

public enum DayState
{
    Start,
    FirstDayRules,
    Game,
    Feedback
}

public enum DropZone
{
    Save,
    Burn,
    Bag
}
