using System.Collections.Generic;
using UnityEngine;

public abstract class UIView : MonoBehaviour
{
    public interface IViewPresenter { }
    GameObject myGameObject;
    public bool IsShow { get; private set; }

    protected virtual void Awake()
    {
        myGameObject = gameObject;
        UI.AddEventLocalize(OnLocalize);
    }

    protected virtual void Start()
    {
        IsShow = true;
        OnLocalize();
    }

    protected virtual void OnDestroy()
    {
        UI.RemoveEventLocalize(OnLocalize);
        myGameObject = null;
    }

    protected abstract void OnLocalize();

    public virtual void Show()
    {
        SetActive(true);
    }

    public virtual void Hide()
    {
        SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        IsShow = isActive;
        myGameObject.SetActive(isActive);
    }

    public void Initialize(IViewPresenter presenter)
    {
        OnInit(presenter);
    }

    protected virtual void OnInit(IViewPresenter presenter)
    {
    }
}