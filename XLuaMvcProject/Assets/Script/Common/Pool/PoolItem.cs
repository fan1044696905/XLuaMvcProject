using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolItem {

    private string poolName;
    public Transform parent;
    public GameObject obj;
    private string objName;
    public uint allocCount;

    public PoolItem(string poolName, Transform parent, GameObject obj, uint allocCount)
    {
        this.poolName = poolName;
        this.parent = parent;
        this.obj = obj;
        this.objName = obj.name;
        this.allocCount = allocCount;
    }
}
