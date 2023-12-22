using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ObjectPool<T> where T : Component
{
    private T prefab;
    private Transform parent;
    private Queue<T> objectPool = new Queue<T>();

    public ObjectPool(T prefab, int size, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < size; i++)
        {
            T obj = UnityEngine.Object.Instantiate(prefab, parent);
            obj.gameObject.SetActive(false);
            objectPool.Enqueue(obj);
        }
    }

    public T GetObject()
    {
        T obj = null;

        if (objectPool.Count > 0)
        {
            obj = objectPool.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = UnityEngine.Object.Instantiate(prefab, parent);
        }

        return obj;
    }

    public void ReturnObject(T obj)
    {
        obj.gameObject.SetActive(false);
        objectPool.Enqueue(obj);
    }
}

public class PoolManager : Singleton<PoolManager>
{
    public List<Pool> pools = new List<Pool>();

    private Dictionary<string, ObjectPool<Component>> objectPools = new Dictionary<string, ObjectPool<Component>>();


    public override void Init()
    {

    }

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public Component prefab;
        public int size;
    }

    private void Start()
    {
        foreach (Pool pool in pools)
        {
            ObjectPool<Component> objectPool = new ObjectPool<Component>(pool.prefab, pool.size, transform);
            objectPools.Add(pool.tag, objectPool);
        }
    }

    public T GetObject<T>(string tag) where T : Component
    {
        if (objectPools.TryGetValue(tag, out var objectPool))
        {
            T obj = objectPool.GetObject() as T;
            return obj;
        }

        return null;
    }

    public void ReturnObject(Component obj)
    {
        if (objectPools.TryGetValue(obj.name, out var objectPool))
        {
            objectPool.ReturnObject(obj);
        }
    }
}
