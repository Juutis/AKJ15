using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Feedback : MonoBehaviour
{
    [SerializeField]
    private Text dayResult;
    [SerializeField]
    private Text totalReputation;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDayResultText(string text)
    {
        dayResult.text = text;
    }

    public void SetTotalReputationText(string text)
    {
        totalReputation.text = text;
    }
}
