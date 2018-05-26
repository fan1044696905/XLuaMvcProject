using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager> {

    private Dictionary<string, ObjectPool> poolDic = new Dictionary<string, ObjectPool>();

    private Transform poolParent;

    public Transform PoolParent
    {
        get
        {
            if (poolParent == null)
            {
                GameObject obj = GameObject.Find("ObjectPool");
                if (obj == null)
                {
                    obj = new GameObject("ObjectPool");
                }
                MonoBehaviour.DontDestroyOnLoad(obj);
                poolParent = obj.transform;
            }
            return poolParent;
        }
    }

    public bool IsRegister(string poolName)
    {
        return poolDic.ContainsKey(poolName);
    }

    public void Register(string poolName,GameObject obj,uint allocCount)
    {
        if (!poolDic.ContainsKey(poolName) && obj != null)
        {
            PoolItem item = new PoolItem(poolName, PoolParent, obj, allocCount);
            poolDic.Add(poolName, new ObjectPool(item));
        }
    }

    public void UnRegister(string poolName)
    {
        if (poolDic.ContainsKey(poolName))
        {
            ObjectPool objectPool = poolDic[poolName];
            objectPool.Clear();
            poolDic.Remove(poolName);
            objectPool = null;
        }
    }

    public void UnRegisterAll()
    {
        List<string> strList = new List<string>(poolDic.Keys);
        for (int i = 0; i < strList.Count; i++)
        {
            UnRegister(strList[i]);
        }
        strList.Clear();
        poolDic.Clear();
        strList = null;
    }

    public GameObject Get(string poolName,Transform parent = null)
    {
        if (!poolDic.ContainsKey(poolName))
        {
            Debuger.LogError("The poolName:"+poolName+" UnRegister");
            return null;
        }

        return poolDic[poolName].Get(parent);
    }

    public T Get<T>(string poolName,Transform parent = null) where T:Component
    {
        return Get(poolName, parent).GetOrAddComponent<T>();
    }

    public void Recycle(string poolName,GameObject obj)
    {
        ObjectPool pool = null;
        if (!poolDic.TryGetValue(poolName,out pool))
        {
            Debuger.LogError("The poolName:" + poolName + " UnRegister");
            return;
        }
        pool.Recycle(obj);
    }

    public void RecycleAll(string poolName)
    {
        if (!poolDic.ContainsKey(poolName))
        {
            Debuger.LogError("The poolName:" + poolName + " UnRegister");
            return;
        }
        poolDic[poolName].RecycleAll();
    }

    public void Clear(string poolName)
    {
        if (!poolDic.ContainsKey(poolName))
        {
            Debuger.LogError("The poolName:" + poolName + " UnRegister");
            return;
        }
        poolDic[poolName].Clear();
    }
}
