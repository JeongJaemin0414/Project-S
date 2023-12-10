using System;
using UnityEngine;

/// <summary>
/// 모든 UICanvas는 Hide 또는 Destory 타입이 있어야한다.
/// </summary>
[Flags]
public enum UIType
{
    /// <summary>
    /// UI 종료시 숨기기
    /// </summary>
    Hide = 1 << 0,

    /// <summary>
    /// UI 종료시 제거
    /// </summary>
    Destroy = 1 << 1,

    /// <summary>
    /// 백버튼 리스트에 추가
    /// </summary>
    Back = 1 << 2,

    /// <summary>
    ///  ShortCut시 제거목록에서 제외
    /// </summary>
    Fixed = 1 << 3,

    /// <summary>
    /// 중복 안되는 UI
    /// </summary>
    Single = 1 << 4,

    /// <summary>
    /// UIType.Single 일 경우에만 가능
    /// 백버튼으로 UI를 다시 표시하려고 할때 사용.
    /// </summary>
    Reactivation = 1 << 5,
}