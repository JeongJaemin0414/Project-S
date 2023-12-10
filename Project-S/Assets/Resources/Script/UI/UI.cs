using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public sealed class UI
{
    public static void AddEventLocalize(Action action)
    {
        UIManager.Instance.OnLocalize += action;
    }

    public static void RemoveEventLocalize(Action action)
    {
        UIManager.Instance.OnLocalize -= action;
    }
    /// <summary>
    /// UI 켜기
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="isSkipAni">오픈 애니메이션 스킵여부</param>
    /// <returns></returns>
    public static T Show<T>(IUIData data = null, bool isSkipAni = false) where T : UICanvas
    {
        UIManager.Instance.Show<T>(data, isSkipAni);
        return GetUI<T>();
    }

    /// <summary>
    /// UI 켜기
    /// </summary>
    public static void Show(string canvasName, IUIData data = null, bool isSkipAni = false)
    {
        UIManager.Instance.Show(canvasName, data, isSkipAni);
    }

    public static T GetUI<T>() where T : UICanvas
    {
        return UIManager.Instance.GetUI<T>();
    }
    /// <summary>
    /// UIType.Fixed를 제외한 모든 UI를 제거하고 지정 UI를 킨다.
    /// forceClose가 true 일 경우 UIType.Close 타입으로 실행
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="data"></param>
    /// <param name="isSkipAni">종료 애니메이션 스킵여부</param>
    /// <param name="isDestroy">UI 강제 제거 여부</param>
    public static T ShortCut<T>(IUIData data = null, bool isSkipAni = false, bool isDestroy = false) where T : UICanvas
    {
        UIManager.Instance.ShortCut(isDestroy);
        UIManager.Instance.Show<T>(data, isSkipAni);
        return GetUI<T>();
    }

    /// <summary>
    /// UI 켜기
    /// </summary>
    public static void ShortCut(string canvasName, IUIData data = null, bool isSkipAni = false, bool isDestroy = false)
    {
        UIManager.Instance.ShortCut(isDestroy);
        UIManager.Instance.Show(canvasName, data, isSkipAni);
    }

    public static bool Close<T>(bool isSkipAni = false, bool isDestroy = false) where T : UICanvas
    {
        return UIManager.Instance.Close<T>(isSkipAni, isDestroy);
    }

    public static void CloseAll()
    {
        UIManager.Instance.CloseAll();
    }

    public static bool Destroy<T>(bool skipAnim = true) where T : UICanvas
    {
        return UIManager.Instance.Destroy<T>(skipAnim);
    }
}