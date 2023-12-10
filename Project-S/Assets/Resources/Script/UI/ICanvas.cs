using System;
using UnityEngine;

/// <summary>
/// UI 기본 인터페이스
/// </summary>
public interface ICanvas
{
    int layer { get; }

    Transform Transform { get; }

    bool IsVisible { get; }

    void Init();

    void Show(IUIData data = null, bool skipAnim = false);

    void Hide(bool skipAnim = false);

    void Close(bool skipAnim = false);

    void Back();

    void Localize();

    bool HasFlag(UIType type);

    string CanvasName { get; }
}

public interface IUIData
{

}