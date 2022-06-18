using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BookLineUI : MonoBehaviour
{
    private List<string> alphabetUpper = new List<string>() { "Æ", "Σ", "Ω", "Ц", "З", "Я", "I", "Y", "E", "Ջ", "Ի", "Ԓ", "Ϩ", "Ϫ", "Є", "Э", "Ө", "Մ", "Ӯ", "Ӑ", "Ծ", "Ӵ", "Ж", "Λ", "Ҁ" };
    private List<string> alphabetLower = new List<string>() { "æ", "σ", "ω", "ц", "з", "я", "i", "y", "e", "ջ", "ի", "ԓ", "ϩ", "ϫ", "є", "э", "ө", "մ", "ӯ", "ӑ", "ծ", "ӵ", "ж", "λ", "ҁ" };
    private string line;

    [SerializeField]
    private bool randomWord;

    [SerializeField]
    private int lineLength;

    [SerializeField]
    private Text uiText;

    [SerializeField]
    private int minWordLength;

    [SerializeField]
    private int maxWordLength;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void Initialize(bool random, string text)
    {
        randomWord = random;
        uiText.text = text;
        if (random)
        {
            uiText.color = new Color(0.25f, 0.25f, 0.25f);
        }
        else
        {
            uiText.color = new Color(0, 0, 0);
            // uiText.fontStyle = FontStyle.Bold;
        }
    }

    // private void GenerateRandomWord()
    // {
    //     int charsLeft = lineLength;
    //     bool firstWord = true;
    //     while (charsLeft > 0)
    //     {
    //         int wordLen = Random.Range(minWordLength, maxWordLength);
    //         charsLeft -= wordLen + 1; // space between words
    //         string firstLetter = firstWord ? alphabetUpper[Random.Range(0, alphabetUpper.Count)] : alphabetLower[Random.Range(0, alphabetUpper.Count)];
    //         firstWord = false;
    // 
    //         StringBuilder word = new StringBuilder(firstLetter);
    // 
    //         for (int i = 1; i < wordLen; i++)
    //         {
    //             word.Append(alphabetLower[Random.Range(0, alphabetUpper.Count)]);
    //         }
    // 
    //         word.Append(" ");
    //         line += word;
    //     }
    // 
    //     uiText.text = line;
    //     uiText.color = new Color(0.25f, 0.25f, 0.25f);
    // }

    // Update is called once per frame
    void Update()
    {
    }
}
