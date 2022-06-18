using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayConfig", menuName = "Configs/New DayConfig")]
public class DayConfig : ScriptableObject
{
    [SerializeField]
    private List<GenreCounts> genreBookCounts;

    public List<GenreCounts> GenreBookCounts { get { return genreBookCounts; } }

    public float MinBookSpawnInterval = 10.0f;
    public float MaxBookSpawnInterval = 20.0f;
}

[System.Serializable]
public class GenreCounts
{
    public Genre genre;
    public int count;
}