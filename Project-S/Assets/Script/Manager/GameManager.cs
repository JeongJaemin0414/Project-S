using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private DataSaveLoad dataSaveLoad;

    public bool isPlayerStop = false;

    protected override void Awake()
    {
        dataSaveLoad = GetComponent<DataSaveLoad>();

        DataLoad();
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        TimeManager.Instance.Init();
        AddressbleManager.Instance.Init();
        CharacterManager.Instance.Init();
        DOTweenManager.Instance.Init();
        ExcelManager.Instance.Init();  
        LanguageManager.Instance.Init();
        UIManager.Instance.Init();
    }

    public void SetPlayerStop(bool isStop)
    {
        isPlayerStop = isStop;
    }

    public void DataSave()
    {
        dataSaveLoad.DataSave();
    }

    public void DataLoad()
    {
        dataSaveLoad.DataLoad();
    }
}
