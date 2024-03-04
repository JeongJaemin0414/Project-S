using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;


public class AddressbleManager : Singleton<AddressbleManager>
{
    private Dictionary<string, Object> _loadedAssets = new Dictionary<string, Object>();

    public override void Init()
    {

    }

    public T LoadAsset<T>(string assetName, Transform parentTrans = null) where T : Object
    {
        T obj = null;

        if (_loadedAssets.TryGetValue(assetName, out Object loadedAsset) && loadedAsset)
        {
            obj = Instantiate(loadedAsset as T, parentTrans);
        }
        else
        {
            AsyncOperationHandle<T> handle = Addressables.LoadAssetAsync<T>(assetName);
            handle.WaitForCompletion();

            switch (handle.Status)
            {
                case AsyncOperationStatus.Succeeded:
                    obj = Instantiate(handle.Result, parentTrans);
                    _loadedAssets[assetName] = handle.Result;
                    break;

                case AsyncOperationStatus.Failed:
                    Debug.LogError($"Failed to load asset: {assetName}");
                    break;
            }
        }

        if (obj == null)
        {
            Debug.LogWarning($"Failed to instantiate asset: {assetName}");
        }

        return obj;
    }

    public void SetSprite(Image image, string spriteName, System.Action onCompleted = null)
    {
        Addressables.LoadAssetAsync<Sprite>(spriteName).Completed += handle =>
        {
            image.sprite = handle.Result;
            onCompleted?.Invoke();
        };
    }

    public void Start()
    {
        //LoadAssetAsync<Sprite>("Char/Skunk2");
        //LoadAssetAsync<Sprite>("Char/Wolf");
    }

    public void CreateBox()
    {
    }
}
