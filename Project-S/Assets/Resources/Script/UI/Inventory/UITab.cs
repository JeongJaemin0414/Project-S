using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UITab : UIView
{
    public System.Action<int> OnChangedTab;
    [SerializeField] UITabButton[] Tabs;

    protected override void OnInit(IViewPresenter presenter)
    {
        base.OnInit(presenter);
        foreach (var tab in Tabs)
        {
            tab.OnClicked += OnClickedTab;
        }
    }

    protected void OnClickedTab(int index)
    {
        foreach (var tab in Tabs)
        {
            tab.DeactiveLinkedUI(index);
        }
    }

    protected override void OnLocalize()
    {

    }
}
