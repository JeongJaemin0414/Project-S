using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Timer
{
    public int time; 
    public Action onTimerEnd;
    
    public Timer(int _time, Action _onTimerEnd)
    {
        time = _time + TimeManager.Instance.TimeData.time;
        onTimerEnd = _onTimerEnd;
    }
}


[Serializable]
public struct TimeData
{
    public SeasonType seasonType;
    public int day;
    public int time;
}


public class TimeManager : Singleton<TimeManager>
{
    private TimeData timeData = new();
    public TimeData TimeData { get => timeData; set => timeData = value; }
    
    public List<Timer> timers = new();

    private int timePass;
    private int maxDay;

    public override void Init()
    {
        SetSeason();

        StartCoroutine(TimerCoroution());
    }

    public void AddTimer(int _time, Action _onTimerEnd)
    {
        Timer timer = new(_time, _onTimerEnd);
        timers.Add(timer);
    }

    public void SetSeason()
    {
        TimeTableEntity timeTableEntity = ExcelManager.Instance.GetExcelData<TimeTable>().time[(int)timeData.seasonType];
        timePass = timeTableEntity.timePass * 100;
        maxDay = timeTableEntity.maxDay;
    }

    IEnumerator TimerCoroution()
    {
        timeData.time += timePass;

        int day = timeData.time / 3600 / 24;
        int hour = timeData.time / 3600 % 24;
        int min = timeData.time / 60 % 60 / 10 * 10;

        if (day > maxDay)
        {
            timeData.seasonType = (SeasonType)(((int)timeData.seasonType + 1) % 4);
            timeData.time = 0;
            day = 0;
            hour = 0;
            min = 0;

            SetSeason();
        }

        UIManager.Instance.SetTimerText(timeData.seasonType.ToString() + " " + day.ToString() + "ÀÏ " + hour.ToString("D2") + ":" + min.ToString("D2")); //+ ":" + (timeData.time % 60).ToString("D2")
        GameManager.Instance.DataSave();

        if(timers.Count != 0)
        {
            for(int i = timers.Count - 1; i >= 0; i--)
            {
                Timer timer = timers[i];

                if (timer.time <= timeData.time) //timer End
                {
                    timer.onTimerEnd?.Invoke();
                    timers.Remove(timer);
                }
            }
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(TimerCoroution());
    }
    
}
