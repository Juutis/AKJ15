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
    private Text DayStartText;
    [SerializeField]
    private FeedbackUI FeedbackUI;
    [SerializeField]
    private GameObject RulesUI;
    [SerializeField]
    public GameConfig config;
    [SerializeField]
    private List<DayConfig> dayConfigs;
    [SerializeField]
    private GameObject RulesHilight;

    public int currentDay { get; private set; } = 0;
    private DayState gameState = DayState.Start;
    public float dayStarted { get; private set; }

    private int totalRunScore = 0;
    private int currentDayScore = 0;
    private int currentStreak = 0;
    private int correctlyPlacedBooksToday = 0;
    public bool firstDay { get; private set; } = true;
    private int correctlySavedBooksToday = 0;
    private int handledBooksToday = 0;
    private int rebelBooksDeliveredToday = 0;
    private int otherBooksDeliveredToday = 0;
    private int incorrectlySavedBooksToday = 0;

    public GameObject Bag;

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
            if (Input.GetKey(KeyCode.Return))
            {
                gameState = DayState.Feedback;
            }
            if (Time.time > dayStarted + config.DayLength)
            {
                gameState = DayState.Feedback;
            }
        }

        RulesHilight.SetActive(firstDay);
    }

    public DayConfig GetDayConfig()
    {
        return dayConfigs[currentDay];
    }

    public DayState GetState()
    {
        return gameState;
    }

    public EndState GetEndState()
    {
        if (ReputationManager.IsGameOverByState())
        {
            return EndState.KilledBySupervisor;
        }
        if (ReputationManager.IsGameOverByRebels())
        {
            return EndState.KilledByRebels;
        }

        if (currentDay + 1 >= dayConfigs.Count)
        {
            if (ReputationManager.IsVictoryByRebels())
            {
                return EndState.VictoryByRebels;
            }
            else
            {
                return EndState.victoryBySupervisor;
            }
        }
        
        return EndState.None;
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
            if (zone == DropZone.Bag)
            {
                otherBooksDeliveredToday++;
            }
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
        feedbackUI.SetSupervisorFeedback(ReputationManager.GetStateDayResult(totalBooksCount, handledBooksToday, correctlyPlacedBooksToday, incorrectlySavedBooksToday), ReputationManager.GetStateReputationText());

        var rebelBookConfig = GetDayConfig().GenreBookCounts.Where(it => it.genre == Genre.Rebel).FirstOrDefault();
        var totalRebelBooksToday = 0;
        if (rebelBookConfig != null) {
            totalRebelBooksToday = rebelBookConfig.count;
        }

        feedbackUI.SetRebelFeedback(ReputationManager.GetRebelDayResult(totalRebelBooksToday, rebelBooksDeliveredToday, otherBooksDeliveredToday), ReputationManager.GetRebelReputationText());
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
            dayStarted = Time.time;
            BookManager.main.InitializeDay(dayConfigs[currentDay]);
        }

        RulesUI.SetActive(false);
    }

    public void StartNextDay()
    {
        currentDay++;
        if (currentDay < dayConfigs.Count)
        {
            InitializeDay();
            gameState = DayState.Start;
            BrowsableBook.main.CloseBook(true);
            BookSpawner.main.ClearBooks();
        }
        else
        {
            Debug.Log("YOU WIN!");
        }
    }

    private void InitializeDay()
    {
        dayStarted = Time.time;
        if (currentDay != 0)
        {
            BookManager.main.InitializeDay(dayConfigs[currentDay]);
        }
        currentDayScore = 0;
        correctlyPlacedBooksToday = 0;
        correctlySavedBooksToday = 0;
        handledBooksToday = 0;
        rebelBooksDeliveredToday = 0;
        otherBooksDeliveredToday = 0;
        incorrectlySavedBooksToday = 0;
        DayStartUI.color = Color.black;
        DayStartText.text = "Day " + (currentDay + 1);

        if (currentDay == 0)
        {
            Bag.SetActive(false);
        }
        else
        {
            Bag.SetActive(true);
        }
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
        ReputationManager.UpdateStateReputation(totalBooksToday, handledBooksToday, correctlySavedBooksToday, incorrectlySavedBooksToday);

        if (!firstDay)
        {
            var rebelBookConfig = GetDayConfig().GenreBookCounts.Where(it => it.genre == Genre.Rebel).FirstOrDefault();
            if (rebelBookConfig != null) {
                var totalRebelBooksToday = rebelBookConfig.count;
                ReputationManager.UpdateRebelReputation(totalRebelBooksToday, rebelBooksDeliveredToday, otherBooksDeliveredToday);
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

public enum EndState
{
    None,
    KilledBySupervisor,
    KilledByRebels,
    VictoryByRebels,
    victoryBySupervisor
}
