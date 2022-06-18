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
    private Text supervisorFeedbackText;
    [SerializeField]
    private Text rebelFeedbackText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameManager.main.UpdateFeedback(this);
    }

    public void UpdateScores(float totalScore, float dayScore, int totalBooksDay, int correctBooks, int streak)
    {
        correctBooksText.text = $"{correctBooks} / {totalBooksDay}";
        streakText.text = streak.ToString();
        totalScoreText.text = totalScore.ToString();
        dayScoreText.text = dayScore.ToString();
    }

    public void SetSupervisorFeedback(string feedback)
    {
        supervisorFeedbackText.text = feedback;
    }

    public void SetRebelFeedback(string feedback)
    {
        rebelFeedbackText.text = feedback;
    }
}
