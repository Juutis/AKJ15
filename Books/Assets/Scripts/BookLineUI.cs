using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BookLineUI : MonoBehaviour
{
    private List<string> alphabetUpper = new List<string>() { "Æ", "Σ", "Ω", "Ц", "З", "Я", "I", "Y", "E", "Ջ", "Ի", "Ԓ", "Ϩ", "Ϫ", "Є", "Э", "Ө", "Մ", "Ӯ", "Ӑ", "Ծ", "Ӵ", "Ж", "Λ", "Ҁ" };
    private List<string> alphabetLower = new List<string>() { "æ", "σ", "ω", "ц", "з", "я", "i", "y", "e", "ջ", "ի", "ԓ", "ϩ", "ϫ", "є", "э", "ө", "մ", "ӯ", "ӑ", "ծ", "ӵ", "ж", "λ", "ҁ" };

    [SerializeField]
    private bool randomWord;

    [SerializeField]
    private Text uiText;

    void Start()
    {
    }

    public void Initialize(bool random, string text)
    {
        randomWord = random;
        uiText.text = text;
        if (random)
        {
            uiText.color = new Color(0.9765625f, 0.6171875f, 0.3984375f);
        }
        else
        {
            uiText.color = new Color(0, 0, 0);
            // uiText.fontStyle = FontStyle.Bold;
        }
    }

    void Update()
    {
    }
}
