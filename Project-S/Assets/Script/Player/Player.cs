using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform pivotTooltr;
    private GameObject toolObj;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTool(int keyNumber)
    {
        Tool tool = (Tool)keyNumber;
        GameObject Obj = ToolManager.Instance.GetTool(tool);

        if(toolObj != null)
        {
            Destroy(toolObj);
            toolObj = null;
        }

        if(toolObj != Obj)
            toolObj = Instantiate(Obj, pivotTooltr);
    }
}
