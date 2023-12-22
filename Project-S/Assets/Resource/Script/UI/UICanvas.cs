using static System.Net.Mime.MediaTypeNames;
using System.Collections.Generic;
using System.Diagnostics;
using Assets.Script.Utility;
using UnityEngine;


/// <summary>
/// 모든 최상단 UI는 이 클래스를 상속받아야 한다.
/// </summary>
public abstract class UICanvas : MonoBehaviour, ICanvas
{
    [Tooltip("무시 가능")]
    [SerializeField]
    private TransitionComponent animIn;
    [Tooltip("무시 가능")]
    [SerializeField]
    private TransitionComponent animOut;
    public Transform Transform { get { return transform; } }
    public virtual bool IsVisible { get { return gameObject.activeSelf; } }
    protected abstract UIType uiType { get; }
    public virtual int layer => Layer.UI;
    public string CanvasName => gameObject.name;

    ISoundManager soundManager;
    IUIManager uiManager;
    public void Init()
    {
        soundManager = SoundManager.Instance;
        uiManager = UIManager.Instance;
        OnInit();
        Localize();
    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {
    }

    protected virtual void OnDestroy()
    {
        GC();
    }

    public void Show(IUIData data = null, bool skipAnim = false)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);

            Localize();
        }

        OnShow(data);
        animIn?.Animate(skipAnim);
    }

    public void Hide(bool skipAnim = false)
    {
        if (animOut == null || skipAnim)
        {
            hide();
            return;
        }

        animOut.onFinished += hide;

        animOut.Animate(skipAnim);
        if (!skipAnim && animOut.IsSkip())
        {
            hide();
        }
    }

    public void Close(bool skipAnim = false)
    {
        if (animOut == null || skipAnim)
        {
            close();
            return;
        }

        animOut.onFinished += close;
        animOut.Animate(skipAnim);
        if (!skipAnim && animOut.IsSkip())
        {
            close();
        }
    }

    private void close()
    {
        // OnClose 전에 OnHide 호출
        OnHide();

        OnClose();
        Destroy(gameObject);
    }

    private void hide()
    {
        gameObject.SetActive(false);
        OnHide();
    }

    public void Back()
    {
        OnBack();
    }

    public void Localize()
    {
        OnLocalize();
    }

    /// <summary>
    /// UI의 Entry 애니메이션이 시작 되기 전의 상태를 감춤. 
    /// OnShow 이벤트에서 사용할 것
    /// </summary>
    protected virtual void CorrectUIEntryAnimation()
    {
        StartCoroutine(YieldCorrectUIEntryAnimation());
    }

    private IEnumerator<float> YieldCorrectUIEntryAnimation()
    {
        Transform cachedTransform = transform;

        cachedTransform.position = Vector3.one * 1000f;
        yield return 0f;
        cachedTransform.position = Vector3.zero;
    }

    protected virtual void OnBack()
    {
        UIManager.Instance.Close(name);
    }

    protected void AniInFinish()
    {
        animIn?.Finish();
    }

    protected void AniOutFinish()
    {
        animOut?.Finish();
    }

    protected bool IsPlayingAniIn()
    {
        return animIn == null ? false : animIn.IsPlaying();
    }

    protected bool IsPlayingAniOut()
    {
        return animOut == null ? false : animOut.IsPlaying();
    }

    /// <summary>
    /// 오브젝트 생성될때만 실행된다.
    /// </summary>
    protected abstract void OnInit();

    protected abstract void OnClose();

    protected abstract void OnShow(IUIData data = null);

    protected abstract void OnHide();

    protected abstract void OnLocalize();

    protected void PlaySfx(string name)
    {
        soundManager.PlaySfx(name);
    }
    
    private static void GC()
    {
        if (UnityEngine.Application.platform == RuntimePlatform.IPhonePlayer)
        {
            UnityEngine.Scripting.GarbageCollector.CollectIncremental(5000000000);
            Resources.UnloadUnusedAssets();
            UnityEngine.Debug.Log("GC 호출");
        }
    }

    public bool HasFlag(UIType type)
    {
        if (ISSUE.UI_CACHING)
            return uiType.HasFlag(type);

        if (type == UIType.Hide)
            return false;

        if (type == UIType.Destroy)
            return true;

        return uiType.HasFlag(type);
    }
}

public abstract class UICanvas<T> : UICanvas
    where T : ViewPresenter
{
    protected T presenter;
}