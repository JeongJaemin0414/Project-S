using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using TMPro;
using static Spine.Unity.Examples.SpineboyFootplanter;
using static UnityEditor.Experimental.GraphView.GraphView;
using System.Linq;
using System;

public class UIManager : Singleton<UIManager>, IUIManager
{
    public System.Action OnLocalize { get; set; }

    /// <summary>
    /// UI Show 이벤트
    /// </summary>
    public System.Action<ICanvas> OnUIShow { get; set; }

    /// <summary>
    /// UI Close 이벤트
    /// </summary>
    public System.Action<ICanvas> OnUIClose { get; set; }

    /// <summary>
    /// 사용중인 모든 UI 목록
    /// </summary>
    private Dictionary<string, ICanvas> allCanvas = new Dictionary<string, ICanvas>();
    /// <summary>
    /// 백버튼 반응 UI 목록
    /// </summary>
    [SerializeField] private List<ICanvas> backList = new List<ICanvas>();
    [SerializeField] private List<string> backNameList = new List<string>();
    [SerializeField] private Dictionary<string, IUIData> backListData = new Dictionary<string, IUIData>();

    [SerializeField] private GameObject uiRoot;
    [SerializeField] private Transform layerUI;
    [SerializeField] private Transform layerChatting;
    [SerializeField] private Transform layerPopup;
    [SerializeField] private Transform layerExceptForCharZoom;
    [SerializeField] private Transform layerEmpty;

    [SerializeField]
    private DialogSystem dialogSystem;

    [SerializeField]
    private InventorySystem inventorySystem;

    [SerializeField]
    private TextMeshProUGUI timerText;

    private void Start()
    {
        dialogSystem.Init();
        inventorySystem.Init();
    }

    public override void Init()
    {

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            OpenDialog(101);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            OpenInventory();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Escape())
                return;
        }
    }

    public bool Escape()
    {
        // 추후에 튜토리얼 추카시 사용
        //if (Tutorial.isInProgress)
        //{
        //    UI.ShowToastPopup(LocalizeKey._26030.ToText()); // 튜토리얼 중에는 이용할 수 없습니다.
        //    return true;
        //}

        if (backList.Count > 0)
        {
            backList[backList.Count - 1].Back();

            // 리스트의 마지막 값이 Reactivation 타입일 때, Hide 타입은 Show
            if (backList.Count > 0)
            {
                var ui = backList[backList.Count - 1];

                if (ui == null)
                {
                    var name = backNameList[backNameList.Count - 1];

                    // UI가 Destroy타입일 경우 삭제하고 새로 생성
                    backList.RemoveAt(backList.Count - 1);
                    backNameList.RemoveAt(backNameList.Count - 1);

                    ShowCanvas(name, backListData[name], false);
                }
                else
                {
                    if (backList[backList.Count - 1].HasFlag(UIType.Reactivation))
                    {
                        if (backList[backList.Count - 1].HasFlag(UIType.Hide))
                        {
                            backList[backList.Count - 1].Show();
                        }
                    }
                }
            }
            return true;
        }

        return false;
    }

    public void OpenDialog(int groupIndex)
    {
        if (!dialogSystem.IsOpening())
        {
            dialogSystem.OpenUISystem();
            dialogSystem.SetDialogData(groupIndex);
            dialogSystem.UpdateDioalog();
        }
    }

    public void OpenInventory()
    {
        if (!inventorySystem.IsOpening())
        {
            inventorySystem.OpenUISystem();
        }
        else
        {
            inventorySystem.CloseUISystem();
        }
    }

    public void SetTimerText(string time)
    {
        timerText.text = time;
    }

    /// <summary>
    /// UI 생성
    /// 프리팹이름과 클래스이름이 동일해야 한다. 
    /// </summary>
    public void Show<T>(IUIData data, bool isSkipAni) where T : UICanvas
    {
        Show(typeof(T).Name, data, isSkipAni);
    }

    public void Show(string canvasName, IUIData data, bool isSkipAni)
    {
        // 백키로 파괴된 UI 생성시에 사용
        if (backListData.ContainsKey(canvasName))
            backListData.Remove(canvasName);

        backListData.Add(canvasName, data);

        ShowCanvas(canvasName, data, isSkipAni);
    }

    private void ShowCanvas(string behaviourName, IUIData data, bool isSkipAni)
    {
        if (!allCanvas.TryGetValue(behaviourName, out ICanvas canvas))
        {
            GameObject uiPrefab = GetUIPrefab(behaviourName);
            canvas = uiRoot.AddChild(uiPrefab).GetComponent<ICanvas>();
            switch ((LayerType)canvas.layer)
            {
                case LayerType.UI:
                    {
                        canvas.Transform.SetParent(layerUI, worldPositionStays: false);
                        Utilities.SetLayer(canvas.Transform.gameObject, Layer.UI);
                    }
                    break;
            }
            canvas.Init();

            allCanvas.Add(behaviourName, canvas);
        }

        if (canvas.HasFlag(UIType.Back))
        {
            if (!backList.Contains(canvas))
            {
                var cvs = canvas as UICanvas;
                backList.Add(cvs);
                backNameList.Add(cvs.CanvasName);
            }
        }

        if (canvas.HasFlag(UIType.Single))
        {
            foreach (var item in allCanvas.Values.ToList())
            {
                if (canvas.Equals(item)) continue;
                if (!item.HasFlag(UIType.Single)) continue;
                if (item.HasFlag(UIType.Fixed)) continue;
                if (backList.Contains(item))
                {
                    // Reactivation 타입은 backList에서 제거하지 않음
                    if (!item.HasFlag(UIType.Reactivation))
                    {
                        var itm = item as UICanvas;
                        backList.Remove(itm);
                        backNameList.Remove(itm.CanvasName);
                    }
                }

                if (!item.IsVisible) continue;

                CloseCanvas(item, isSkipAni: true, false);
            }
        }

        OnUIShow?.Invoke(canvas);
        canvas.Show(data, isSkipAni);
    }

    public bool Close<T>(bool isSkipAni = false, bool isDestroy = false) where T : ICanvas
    {
        return Close(typeof(T).Name, isSkipAni, isDestroy: isDestroy);
    }

    public bool Close(string behaviourName, bool isSkipAni = false, bool isDestroy = false)
    {
        if (!allCanvas.TryGetValue(behaviourName, out ICanvas canvas))
            return false;

        if (backList.Contains(canvas))
        {
            var cvs = canvas as UICanvas;
            backList.Remove(cvs);
            backNameList.Remove(cvs.CanvasName);
        }

        if (!canvas.IsVisible)
            return false;

        CloseCanvas(canvas, isSkipAni, isDestroy);

        return true;
    }

    private void CloseCanvas(ICanvas canvas, bool isSkipAni, bool isDestroy)
    {
        //if (canvas.HasFlag(UIType.Single))
        //    Close<UIEscape>(false, false);

        OnUIClose?.Invoke(canvas);

        if (isDestroy)
        {
            canvas.Close(isSkipAni);
            allCanvas.Remove(canvas.CanvasName);
            return;
        }

        if (canvas.HasFlag(UIType.Hide))
        {
            canvas.Hide(isSkipAni);
        }
        else if (canvas.HasFlag(UIType.Destroy))
        {
            canvas.Close(isSkipAni);
            allCanvas.Remove(canvas.CanvasName);
        }
    }

    /// <summary>
    /// 모든 UI 삭제
    /// </summary>
    public void CloseAll()
    {
        foreach (var canvas in allCanvas.Values.ToList())
        {
            canvas.Close(true);
        }
        allCanvas.Clear();
        backList.Clear();
        backNameList.Clear();
        backListData.Clear();
    }

    GameObject GetUIPrefab(string assetName)
    {
        GameObject go = AssetManager.Instance.GetUI(assetName);

        return go;
    }
    public UICanvas GetUI(string behaviourName)
    {
        if (allCanvas.TryGetValue(behaviourName, out ICanvas canvas))
        {
            return canvas as UICanvas;
        }
        return null;
    }

    public T GetUI<T>() where T : UICanvas
    {
        return GetUI(typeof(T).Name) as T;
    }

    /// <summary>
    /// 바로가기
    /// </summary>
    public void ShortCut(bool forceClose = false)
    {
        // 메인UI 제외하고 전부 끄거나 삭제
        foreach (var canvas in allCanvas.Values.ToList())
        {
            if (canvas.HasFlag(UIType.Fixed))
                continue;

            if (backList.Contains(canvas))
            {
                var cvs = canvas as UICanvas;
                backList.Remove(cvs);
                backNameList.Remove(cvs.CanvasName);
            }

            if (!canvas.IsVisible)
                continue;

            CloseCanvas(canvas, isSkipAni: true, forceClose);
        }
        backList.Clear();
        backNameList.Clear();
        backListData.Clear();
    }

    /// <summary>
    /// UI 강제로 파괴
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isSkipAni"></param>
    /// <returns></returns>
    public bool Destroy<T>(bool isSkipAni = true) where T : UICanvas
    {
        return Close(typeof(T).Name, isSkipAni: isSkipAni, isDestroy: true);
    }
}
