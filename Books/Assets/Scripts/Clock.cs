using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    [SerializeField]
    private Transform MinuteHand;

    [SerializeField]
    private Transform HourHand;

    private float timeStarted;
    private float dayDuration = 60;
    private float hourDuration = 5f;
    private DayState previousState;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        DayState state = GameManager.main.GetState();
        if (previousState == DayState.Start && state == DayState.Game)
        {
            Reset();
        }

        if (state == DayState.Game)
        {
            float minuteAngle = -(Time.time - timeStarted) / hourDuration * 360;
            MinuteHand.rotation = Quaternion.Euler(0, 0, minuteAngle);

            float hourAngle = -(Time.time - timeStarted) / dayDuration * 360;
            HourHand.rotation = Quaternion.Euler(0, 0, hourAngle - 95);
        }

        previousState = state;
    }

    public void Reset()
    {
        timeStarted = Time.time;
        MinuteHand.rotation = Quaternion.identity;
        HourHand.rotation = Quaternion.Euler(0, 0, -95);
    }
}
