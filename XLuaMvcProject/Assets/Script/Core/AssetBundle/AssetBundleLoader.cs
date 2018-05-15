using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 同步加载资源包
/// </summary>
public class AssetBundleLoader : IDisposable
{

    /// <summary>
    /// 资源包
    /// </summary>
    private AssetBundle bundle;


    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="assetBundlePath">资源包路径</param>
    /// <param name="isFullPath">是否完整路径</param>
    public AssetBundleLoader(string assetBundlePath, bool isFullPath = false)
    {
        string fullPath = isFullPath ? assetBundlePath : LocalFileMgr.Instance.LocalFilePath + assetBundlePath;

        //从内存加载资源包
        bundle = AssetBundle.LoadFromMemory(LocalFileMgr.Instance.GetBuffer(fullPath));
        for (int i = 0; i < bundle.LoadAllAssets().Length; i++)
        {
            Debug.Log(bundle.LoadAllAssets()[i].name);
        }
    }
    

    /// <summary>
    /// 加载Asset资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        if (bundle == null) return default(T);
        return bundle.LoadAsset(name) as T;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public UnityEngine.Object LoadAsset(string name)
    {
        return bundle.LoadAsset(name);
    }


    /// <summary>
    /// 加载所有资源
    /// </summary>
    /// <returns></returns>
    public UnityEngine.Object[] LoadAllAssets()
    {
        return bundle.LoadAllAssets();
    }




    /// <summary>
    /// 释放资源
    /// </summary>
    public void Dispose()
    {
        //卸载所有包含在bundle中的对象
        if (bundle != null) bundle.Unload(false);
    }


}

