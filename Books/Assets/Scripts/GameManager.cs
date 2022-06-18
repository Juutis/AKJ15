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
    private DayState gameState = DayState.Game;

    private void Awake()
    {
        main = this;
    }

    void Start()
    {
        
    }

    void Update()
    {
        // if (gameState == DayState.Start)
        // {
        //     DayStartUI.gameObject.SetActive(true);
        //     if (Input.GetKey(KeyCode.Space))
        //     {
        //         gameState = DayState.Game;
        //         StartCoroutine(FadeOutImage(DayStartUI, Color.black, new Color(0, 0, 0, 0)));
        //     }
        // }
        // else if (gameState == DayState.Feedback)
        // {
        //     FeedbackUI.SetActive(true);
        //     DayStartUI.gameObject.SetActive(false);
        // }
        // else
        // {
        //     FeedbackUI.SetActive(false);
        // }
    }

    private IEnumerator FadeOutImage(Image image, Color c1, Color c2)
    {
        for (float i = 0; i > 25; i++)
        {
            DayStartUI.color = Color.Lerp(c1, c2, i / 25f);
            yield return new WaitForSeconds(0.075f);
        }

        DayStartUI.gameObject.SetActive(false);
    }
}

public enum DayState
{
    Start,
    Game,
    Feedback
}
