using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScorePopUp : MonoBehaviour
{
    [SerializeField]
    private Color positiveColor, negativeColor;
    private int score;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize(int score)
    {
        var text = GetComponentInChildren<TMP_Text>();
        var scoreColor = score > 0 ? positiveColor : negativeColor;
        text.text = "+" + score;
        text.color = scoreColor;
    }

    public void Finished() 
    {
        Destroy(gameObject);
    }
}
