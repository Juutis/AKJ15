using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackUI : MonoBehaviour
{
    [SerializeField]
    private Text correctBooksText;
    [SerializeField]
    private Text streakText;
    [SerializeField]
    private Text dayScoreText;
    [SerializeField]
    private Text totalScoreText;

    [SerializeField]
    private Feedback supervisorFeedback;
    [SerializeField]
    private Feedback rebelFeedback;

    [SerializeField]
    private GameObject endOfDayPanel;
    [SerializeField]
    private GameObject supervisorFeedbackPanel;
    [SerializeField]
    private GameObject rebelFeedbackPanel;
    [SerializeField]
    private GameObject scorePanel;
    [SerializeField]
    private GameObject rebelIntroductionPanel;
    [SerializeField]
    private Text endOfDayText;

    private State status = State.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.main.UpdateFeedback(this);

        switch(status) 
        {
            case State.NONE:
                endOfDayPanel.SetActive(false);
                supervisorFeedbackPanel.SetActive(false);
                rebelFeedbackPanel.SetActive(false);
                scorePanel.SetActive(false);
                rebelIntroductionPanel.SetActive(false);
                break;
            case State.END_OF_DAY:
                endOfDayPanel.SetActive(true);
                supervisorFeedbackPanel.SetActive(false);
                rebelFeedbackPanel.SetActive(false);
                scorePanel.SetActive(false);
                rebelIntroductionPanel.SetActive(false);
                break;
            case State.SUPERVISOR_FEEDBACK:
                endOfDayPanel.SetActive(false);
                supervisorFeedbackPanel.SetActive(true);
                rebelFeedbackPanel.SetActive(false);
                scorePanel.SetActive(false);
                rebelIntroductionPanel.SetActive(false);
                break;
            case State.REBEL_FEEDBACK:
                endOfDayPanel.SetActive(false);
                supervisorFeedbackPanel.SetActive(false);
                rebelFeedbackPanel.SetActive(true);
                scorePanel.SetActive(false);
                rebelIntroductionPanel.SetActive(false);
                break;
            case State.SCORE:
                endOfDayPanel.SetActive(false);
                supervisorFeedbackPanel.SetActive(false);
                rebelFeedbackPanel.SetActive(false);
                scorePanel.SetActive(true);
                rebelIntroductionPanel.SetActive(false);
                break;
            case State.REBEL_INTRODUCTION:
                endOfDayPanel.SetActive(false);
                supervisorFeedbackPanel.SetActive(false);
                rebelFeedbackPanel.SetActive(false);
                scorePanel.SetActive(false);
                rebelIntroductionPanel.SetActive(true);
                break;
        }

        if (Input.anyKeyDown)
        {
            switch(status) 
            {
                case State.END_OF_DAY:
                    status = State.SUPERVISOR_FEEDBACK;
                    break;
                case State.SUPERVISOR_FEEDBACK:
                    if (GameManager.main.currentDay == 0)
                    {
                        status = State.SCORE;
                    }
                    else
                    {
                        status = State.REBEL_FEEDBACK;
                    }
                    break;
                case State.REBEL_FEEDBACK:
                    status = State.SCORE;
                    break;
                case State.SCORE:
                    if (GameManager.main.currentDay == 0)
                    {
                        status = State.REBEL_INTRODUCTION;
                    }
                    else
                    {
                        status = State.NONE;
                        GameManager.main.StartNextDay();
                    }
                    break;
                case State.REBEL_INTRODUCTION:
                    status = State.NONE;
                    GameManager.main.StartNextDay();
                    break;
            }
        }
    }

    public void TriggerEndOfDay()
    {
        if (status == State.NONE)
        {
            endOfDayText.text = "End of day " + (GameManager.main.currentDay + 1);
            status = State.END_OF_DAY;
        }
    }

    public void UpdateScores(float totalScore, float dayScore, int totalBooksDay, int correctBooks, int streak)
    {
//        correctBooksText.text = $"{correctBooks} / {totalBooksDay}";
//        streakText.text = streak.ToString();
//        totalScoreText.text = totalScore.ToString();
//        dayScoreText.text = dayScore.ToString();
    }

    public void SetSupervisorFeedback(string dayResult, string reputation)
    {
        supervisorFeedback.SetDayResultText(dayResult);
        supervisorFeedback.SetTotalReputationText(reputation);
    }

    public void SetRebelFeedback(string dayResult, string reputation)
    {
        rebelFeedback.SetDayResultText(dayResult);
        rebelFeedback.SetTotalReputationText(reputation);
    }

    public enum State
    {
        NONE,
        END_OF_DAY,
        SUPERVISOR_FEEDBACK,
        REBEL_FEEDBACK,
        SCORE,
        REBEL_INTRODUCTION

    }
}
