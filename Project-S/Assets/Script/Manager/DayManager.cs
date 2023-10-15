using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : Singleton<DayManager>
{
    private int year;
    private int month;
    private int day;

    private int time;

    public void Start()
    {
        StartCoroutine(TimerCoroution());
    }

    IEnumerator TimerCoroution()
    {
        time += 60;

        UIManager.Instance.SetTimerText((time / 3600).ToString("D2") + ":" + ((time / 60 % 60) / 10 * 10).ToString("D2") + ":" + (time % 60).ToString("D2"));

        yield return new WaitForSeconds(1f);

        StartCoroutine(TimerCoroution());
    }
}
