using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool {

    private PoolItem poolItem;
    private Queue<GameObject> objQueue;
    private List<GameObject> objList;
    public ObjectPool(PoolItem poolItem)
    {
        this.poolItem = poolItem;
        objQueue = new Queue<GameObject>();
        objList = new List<GameObject>();
    }

    public GameObject Get(Transform parent = null)
    {
        if (objQueue.Count == 0)
        {
            if (objList.Count == 0)
            {
                GameObject obj = GameObject.Instantiate(poolItem.obj);
                objQueue.Enqueue(obj);
            }
            else
            {
                Alloc(poolItem.allocCount);
            }
        }
        GameObject _obj = objQueue.Dequeue(); 
        _obj.SetActive(true);
        if (parent!=null)
        {
            _obj.transform.SetParent(parent, false);
        }
        objList.Add(_obj);
        Debug.Log("queueCount = " + objQueue.Count + "    listCount = " + objList.Count);
        return _obj;
    }

    /// <summary>
    /// 分配对象
    /// </summary>
    /// <param name="count">分配个数</param>
    public void Alloc(uint count)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.Instantiate(poolItem.obj);
            Recycle(obj);
        }
    }

    /// <summary>
    /// 回收对象(将对象回收到对象池，不是销毁)
    /// </summary>
    /// <param name="obj"></param>
    public void Recycle(GameObject obj)
    {
        if (obj!=null)
        {
            obj.transform.SetParent(poolItem.parent, false);
            obj.SetActive(false);
            objQueue.Enqueue(obj);
            objList.Remove(obj);
        }
    }

    public void RecycleAll()
    {
        for (int i = objList.Count; i > 0 ; i--)
        {
            Recycle(objList[i-1]);
        }
    }

    public void Clear()
    {
        int queueCount = objQueue.Count;
        for (int i = 0; i < queueCount; i++)
        {
            GameObject obj = objQueue.Dequeue();
            //DestroyImmediate是立即销毁，立即释放资源，做这个操作的时候，会消耗很多时间，影响主线程运行 
            MonoBehaviour.Destroy(obj);
        }
        objQueue.Clear();

        int listCount = objList.Count;
        for (int i = 0; i < listCount; i++)
        {
            GameObject obj = objList[i];
            MonoBehaviour.Destroy(obj);
        }
        objList.Clear();
    }
}
