using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SocialPlatforms;
using UnityEngine;

public class AssetManager : Singleton<AssetManager>
{
    public static string PREFAP_PATH = "Prefab/";
    public static string UI_PREFAB_PATH = PREFAP_PATH + "UI/";
    public static string CHAR_PREFAB_PATH = PREFAP_PATH + "Char/";
    public static string CROPS_PREFAB_PATH = PREFAP_PATH + "Crops/";
    public static string TOOL_PREFAB_PATH = PREFAP_PATH + "Tool/";

    public override void Init()
    {
        
    }

    public GameObject GetUI(string assetName)
    {
        GameObject go = Resources.Load<GameObject>(new StringBuilder().Append(UI_PREFAB_PATH).Append(assetName).ToString());

        if (go == null)
            go = Resources.Load<GameObject>(new StringBuilder().Append(UI_PREFAB_PATH).Append(assetName).ToString());

        if (go == null)
            Debug.LogError($"UI가 존재하지 않습니다: assetName = {assetName}");

        return go;
    }
}
