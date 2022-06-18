using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Serializable = System.SerializableAttribute;

[Serializable]
public class BookLine
{
    private List<string> alphabetUpper = new List<string>() { "Æ", "Σ", "Ω", "Ц", "З", "Я", "I", "Y", "E", "Ջ", "Ի", "Ԓ", "Ϩ", "Ϫ", "Є", "Э", "Ө", "Մ", "Ӯ", "Ӑ", "Ծ", "Ӵ", "Ж", "Λ", "Ҁ" };
    private List<string> alphabetLower = new List<string>() { "æ", "σ", "ω", "ц", "з", "я", "i", "y", "e", "ջ", "ի", "ԓ", "ϩ", "ϫ", "є", "э", "ө", "մ", "ӯ", "ӑ", "ծ", "ӵ", "ж", "λ", "ҁ" };

    private int lineLength;
    private int minWordLength;
    private int maxWordLength;

    public bool IsRandom { get; set; }
    public string Text { get; set; }

    public BookLine(bool isRandom = true)
    {
        BookConfig config = BookManager.main.GetConfig();
        lineLength = config.LineCharCount;
        maxWordLength = config.MaxWordLength;
        minWordLength = config.MinWordLength;

        Text = "";
        IsRandom = isRandom;
        if (isRandom)
        {
            GenerateRandomLine();
        }
    }

    private void GenerateRandomLine()
    {
        int charsLeft = lineLength;
        bool firstWord = true;
        while (charsLeft > 0)
        {
            int wordLen = Random.Range(minWordLength, maxWordLength);
            charsLeft -= wordLen + 1; // space between words
            string firstLetter = firstWord ? alphabetUpper[Random.Range(0, alphabetUpper.Count)] : alphabetLower[Random.Range(0, alphabetUpper.Count)];
            firstWord = false;

            StringBuilder word = new StringBuilder(firstLetter);

            for (int i = 1; i < wordLen; i++)
            {
                word.Append(alphabetLower[Random.Range(0, alphabetUpper.Count)]);
            }

            word.Append(" ");
            Text += word;
        }
    }
}
