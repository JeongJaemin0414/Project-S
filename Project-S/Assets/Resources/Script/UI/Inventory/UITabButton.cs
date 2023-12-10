using UnityEngine;
using UnityEngine.UI;

public class UITabButton : UIView
{

    [Tooltip("버튼 클릭시 활성화할 UI")]
    [SerializeField] UIView LinkedUI;
    [SerializeField] Button button;
    private int _index;

    public System.Action<int> OnClicked;

    protected override void Start()
    {
        AddEvent();
        base.Start();
    }

    protected override void OnDestroy()
    {
        RemoveEvent();
        base.OnDestroy();
    }

    public void SetData(int index)
    {
        this._index = index;
    }

    private void AddEvent()
    {
        button.onClick.AddListener(ActiveLinkedUI);
    }

    private void RemoveEvent()
    {
        button.onClick.RemoveListener(ActiveLinkedUI);
    }

    protected void ActiveLinkedUI()
    {
        if (LinkedUI == null)
            return;

        if (LinkedUI.IsShow)
            return;

        LinkedUI.Show();
        OnClicked?.Invoke(_index);
    }

    //입력받은 인덱스에 해당되지 않는경우 연동된 UI 숨김
    public void DeactiveLinkedUI(int index)
    {
        if (LinkedUI == null)
            return;

        if (index == _index)
            return;

        if (LinkedUI.IsShow)
            LinkedUI.Hide();
    }

    protected override void OnLocalize()
    {

    }
}
