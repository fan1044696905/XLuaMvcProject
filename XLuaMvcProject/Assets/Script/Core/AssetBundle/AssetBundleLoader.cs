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
    }
    

    /// <summary>
    /// 加载Asset资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        if (bundle == null)
        {
            Debuger.LogError("bundle is null the name:"+name);
            return default(T);
        }
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
    /// 加载图集里面的小图片
    /// </summary>
    /// <param name="atlasName">图集名字</param>
    /// <param name="name">小图片名字</param>
    /// <returns></returns>
    public Sprite LoadAtlasSprite(string atlasName,string name)
    {
        UnityEngine.Object[] objs = bundle.LoadAssetWithSubAssets(atlasName);
        Debug.Log(objs.Length);
        for (int i = 1; i < objs.Length; i++)
        {
            if (name.Equals(objs[i].name))
            {
                Sprite texture = objs[i] as Sprite;
                return texture;
            }
        }
        Debuger.LogError("cant find:" + name + " at the atlasName:" + atlasName);
        return null;
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

