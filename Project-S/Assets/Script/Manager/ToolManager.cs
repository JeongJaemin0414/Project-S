using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
    [SerializeField]
    private SerializableDictionary<PlayerToolType, GameObject> tools;

    public override void Init()
    {

    }

    public GameObject GetTool(PlayerToolType toolType)
    {
        GameObject obj = tools[toolType];

        return obj;
    }
}
