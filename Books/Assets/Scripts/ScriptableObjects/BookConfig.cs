using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BookConfig", menuName = "Configs/New BookConfig")]
public class BookConfig : ScriptableObject
{
    [SerializeField]
    private int pageCount;

    [SerializeField]
    private int lineCharCount;

    [SerializeField]
    private int minWordLength;

    [SerializeField]
    private int maxWordLength;

    public int PageCount { get { return pageCount; } }
    public int LineCharCount { get { return lineCharCount; } }
    public int MinWordLength { get { return minWordLength; } }
    public int MaxWordLength { get { return maxWordLength; } }

}
