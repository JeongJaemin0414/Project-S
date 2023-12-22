using UnityEngine;

public class UIInven : UICanvas
{
    public interface IPresenter
    {
        public UIInvenGroupView.Ipresenter GetUIItemInvenViewPresenter();
        public UIInvenGroupView.Ipresenter GetUIToolInvenViewPresenter();
    }

    protected override UIType uiType => UIType.Back | UIType.Hide | UIType.Single;
    protected IPresenter presenter;

    [SerializeField] UITab invenType;
    [SerializeField] UIInvenGroupView itemInvenView;
    [SerializeField] UIInvenGroupView toolInvenView;

    protected override void OnClose()
    {
    }

    protected override void OnHide()
    {
    }

    //UI 초기화 직후 호출. Awake 처럼 사용.
    protected override void OnInit()
    {
        //프리젠터 추후 추가.
        //presenter = new UIInvenPresenter();
        if (presenter == null)
            return;
        itemInvenView.Initialize(presenter.GetUIItemInvenViewPresenter());
        toolInvenView.Initialize(presenter.GetUIToolInvenViewPresenter());
    }

    protected override void OnLocalize()
    {
    }

    // UI 시작. Start 처럼 사용
    protected override void OnShow(IUIData data = null)
    {

    }
}