using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIInvenGroupView : UIView
{
    public interface IData
    {
    }

    public interface Ipresenter : UIView.IViewPresenter
    {
        public (ItemData, int) GetInvenData(int index);
        public InventoryItemType GetInventoryItemType();
    }

    [SerializeField] private InventoryItemType inventoryItemType;
    [SerializeField] private List<UIItemElement> items;
    Ipresenter presenter;

    protected override void OnInit(IViewPresenter presenter)
    {
        base.OnInit(presenter);
        this.presenter = presenter as Ipresenter;
    }

    public void SetData()
    {
        if (presenter == null)
            return;

        inventoryItemType = presenter.GetInventoryItemType();
        for (int i = 0; i < items.Count; i++)
        {
            (ItemData, int) invenData = presenter.GetInvenData(i);
            if (invenData.Item2 == -1)
                continue;
            items[i].SetData(invenData.Item1,invenData.Item2);
        }
    }

    protected override void OnLocalize()
    {
        //추후 언어 변경 같은 기능 사용대비 텍스트 세팅은 가능한 여기서
    }
}

