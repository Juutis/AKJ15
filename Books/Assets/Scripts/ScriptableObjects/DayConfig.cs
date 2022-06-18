using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayConfig", menuName = "Configs/New DayConfig")]
public class DayConfig : ScriptableObject
{
    [SerializeField]
    private List<GenreCounts> genreBookCounts;

    public List<GenreCounts> GenreBookCounts { get { return genreBookCounts; } }
}

[System.Serializable]
public class GenreCounts
{
    public Genre genre;
    public int count;
}