using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

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

    #region NGUI 유틸 참고

    public static GameObject AddChild(GameObject parent) { return AddChild(parent, true, -1); }
    public static GameObject AddChild(this GameObject parent, int layer) { return AddChild(parent, true, layer); }
    public static GameObject AddChild(this GameObject parent, bool undo) { return AddChild(parent, undo, -1); }
    public static GameObject AddChild(this GameObject parent, bool undo, int layer)
    {
        var gObj = new GameObject();
#if UNITY_EDITOR
        if (undo && !Application.isPlaying)
            UnityEditor.Undo.RegisterCreatedObjectUndo(gObj, "Create Object");
#endif
        if (parent != null)
        {
            UnityEngine.Transform transform = gObj.transform;
            transform.parent = parent.transform;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            if (layer == -1) gObj.layer = parent.layer;
            else if (layer > -1 && layer < 32) gObj.layer = layer;
        }
        return gObj;
    }

#if UNITY_5_5_OR_NEWER
    /// <summary>
    /// Instantiate an object and add it to the specified parent.
    /// </summary>

    static public GameObject AddChild(this UnityEngine.Transform parent, GameObject prefab)
    {
        var gObj = UnityEngine.Object.Instantiate(prefab, parent.transform);
        var transform = gObj.transform;
        transform.parent = parent;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
        gObj.SetActive(true);
        return gObj;
    }
#endif
    public static GameObject AddChild(this GameObject parent, GameObject prefab) { return parent.AddChild(prefab, -1); }
    public static GameObject AddChild(this GameObject parent, GameObject prefab, int layer)
    {
#if UNITY_5_5_OR_NEWER
        var gObj = UnityEngine.Object.Instantiate(prefab, parent.transform);
#if UNITY_EDITOR
        if (!Application.isPlaying) UnityEditor.Undo.RegisterCreatedObjectUndo(gObj, "Create Object");
#endif
        if (gObj != null)
        {
            UnityEngine.Transform transform = gObj.transform;
            gObj.name = prefab.name;

            if (parent != null)
            {
                if (layer == -1) gObj.layer = parent.layer;
                else if (layer > -1 && layer < 32) gObj.layer = layer;
            }

            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
            transform.localScale = Vector3.one;
            gObj.SetActive(true);
        }
        return gObj;
#else
		var gObj = GameObject.Instantiate(prefab) as GameObject;
#if UNITY_EDITOR
		if (!Application.isPlaying) UnityEditor.Undo.RegisterCreatedObjectUndo(gObj, "Create Object");
#endif
		if (gObj != null)
		{
			UnityEngine.Transform transform = gObj.transform;
			gObj.name = prefab.name;

			if (parent != null)
			{
				t.parent = parent.transform;
				if (layer == -1) gObj.layer = parent.layer;
				else if (layer > -1 && layer < 32) gObj.layer = layer;
			}

			transform.localPosition = Vector3.zero;
			transform.localRotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			gObj.SetActive(true);
		}
		return gObj;
#endif
    }

    #endregion

    public static void SetLayer(GameObject go, int layer)
    {
        go.layer = layer;

        UnityEngine.Transform transform = go.transform;

        for (int i = 0, imax = transform.childCount; i < imax; ++i)
        {
            UnityEngine.Transform child = transform.GetChild(i);
            SetLayer(child.gameObject, layer);
        }
    }
}
