using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : Singleton<TimeManager>
{
    private TimeData timeData;
    public TimeData TimeData { get => timeData; set => timeData = value; }

    private int timePass;
    private int maxDay;

    public override void Init()
    {
        TimeTableEntity timeTableEntity = ExcelManager.Instance.GetExcelData<TimeTable>().time[(int)timeData.seasonType];
        timePass = timeTableEntity.timePass;
        maxDay = timeTableEntity.maxDay;

        StartCoroutine(TimerCoroution());
    }

    IEnumerator TimerCoroution()
    {
        timeData.time += timePass;

        UIManager.Instance.SetTimerText((timeData.time / 3600).ToString("D2") + ":" + ((timeData.time / 60 % 60) / 10 * 10).ToString("D2") + ":" + (timeData.time % 60).ToString("D2"));

        GameManager.Instance.DataSave();

        yield return new WaitForSeconds(1f);

        StartCoroutine(TimerCoroution());
    }
}
