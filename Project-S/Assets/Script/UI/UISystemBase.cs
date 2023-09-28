using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISystemBase : MonoBehaviour
{
    public virtual void OpenUISystem()
    {
        if (gameObject.activeSelf) return;

        GameManager.Instance.SetPlayerStop(true);
        gameObject.SetActive(true);
    }

    public virtual void CloseUISystem()
    {
        if (!gameObject.activeSelf) return;

        GameManager.Instance.SetPlayerStop(false);
        gameObject.SetActive(false);
    }

    public bool IsOpening()
    {
        return gameObject.activeSelf;
    }
}
