using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/New GameConfig")]
public class GameConfig : ScriptableObject
{
    [SerializeField]
    private float dayLength;

    public float DayLength { get { return dayLength; } }

    [SerializeField]
    private float dayStartLength;

    public float DayStartLength { get { return dayStartLength; } }
}
