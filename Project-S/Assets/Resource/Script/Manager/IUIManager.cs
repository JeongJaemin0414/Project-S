using System;

public interface IUIManager
{
    Action OnLocalize { get; set; }
    Action<ICanvas> OnUIClose { get; set; }
    Action<ICanvas> OnUIShow { get; set; }

    bool Close(string behaviourName, bool isSkipAni = false, bool isDestroy = false);
    bool Close<T>(bool isSkipAni = false, bool isDestroy = false) where T : ICanvas;
    void CloseAll();
    bool Destroy<T>(bool isSkipAni = true) where T : UICanvas;
    bool Escape();
    UICanvas GetUI(string behaviourName);
    T GetUI<T>() where T : UICanvas;
    void Init();
    void OpenDialog(int groupIndex);
    void OpenInventory();
    void SetTimerText(string time);
    void ShortCut(bool forceClose = false);
    void Show(string canvasName, IUIData data, bool isSkipAni);
    void Show<T>(IUIData data, bool isSkipAni) where T : UICanvas;
}