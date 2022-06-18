using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayConfig", menuName = "Configs/New DayConfig")]
public class DayConfig : ScriptableObject
{
    [SerializeField]
    private int bookCount;

    public int BookCount { get { return bookCount; } }
}
