using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public static class Utilities 
{
    public static object[] GetStringFormatData(string[] values)
    {
        object[] result = new object[values.Length];

        for (int i = 0; i < values.Length; i++)
        {
            result[i] = values[i];
        }

        return result;
    }

    public static int[] GetArrayDataInt(string s1)
    {
        int[] iq = s1.Split(';').Select(n => Convert.ToInt32(n)).ToArray();
        return iq;
    }

    public static float[] GetArrayDataFloat(string s1)
    {
        float[] fq = s1.Split(';').Select(n => Convert.ToSingle(n)).ToArray();
        return fq;
    }

    public static string[] GetArrayDataString(string s1)
    {
        string[] sq = s1.Split(';').ToArray();
        return sq;
    }

    public static int ConvertDayToTime(int day)
    {
        return day * 3600 * 24;
    }
}
