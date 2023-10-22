using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
    public List<GameObject> tools = new List<GameObject>();

    public override void Init()
    {

    }

    public GameObject GetTool(Tool tool)
    {
        GameObject obj = null;

        switch (tool)
        {
            case Tool.Axe:
                obj = tools[0];
                break;
            case Tool.Pickaxe:
                obj = tools[1];
                break;
            case Tool.Shovel:
                obj = tools[2];
                break;
        }

        return obj;
    }
}
