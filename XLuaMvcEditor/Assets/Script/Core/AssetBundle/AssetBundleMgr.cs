using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



/// <summary>
/// AssetBundle管理类
/// </summary>
public class AssetBundleMgr : Singleton<AssetBundleMgr>
{
      
    /// <summary>
    ///  资源包名单,依赖文件配置
    /// </summary>
    private AssetBundleManifest m_Manifest;

    private AssetBundleLoader abLoader;
    /// <summary>
    /// 所有加载的Asset资源镜像
    /// </summary>
    private Dictionary<string, UnityEngine.Object> m_AssetDic = new Dictionary<string, UnityEngine.Object>();

    /// <summary>
    /// 依赖项的列表
    /// </summary>
    private Dictionary<string, AssetBundleLoader> m_AssetBundleDic = new Dictionary<string, AssetBundleLoader>();



    /// <summary>
    /// 加载依赖文件配置
    /// </summary>
    private void LoadManifestBundle()
    {
        if (m_Manifest != null)
        {
            return;
        }

        string assetName = string.Empty;
#if UNITY_STANDALONE_WIN
        assetName = "Windows";
#elif UNITY_ANDROID
        assetName = "Android";
#elif UNITY_IPHONE
        assetName = "iOS";
#endif

        using (AssetBundleLoader loader = new AssetBundleLoader(assetName))
        {
            abLoader = loader;
            m_Manifest = loader.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
        Debuger.Log("加载依赖文件配置 完毕");
    }

    
    public void LoadOrDownloadForLua(string path, string name, XLuaCustomExport.OnCreate OnCreate)
    {
        LoadOrDownload<GameObject>(path, name, null, OnCreate: OnCreate, type: 0);
    }


    /// <summary>
    /// 加载或者下载资源
    /// </summary>
    /// <param name="path">短路径</param>
    /// <param name="name"></param>
    /// <param name="onComplete"></param>
    public void LoadOrDownload(string path, string name, Action<GameObject> onComplete)
    {
        LoadOrDownload<GameObject>(path, name, onComplete, type: 0);
    }


    /// <summary>
    /// 加载或下载Lua代码
    /// </summary>
    /// <param name="path">代码路径</param>
    /// <param name="onComplete">加载完成回调</param>
    /// <param name="OnCreate"></param>
    /// <param name="type"></param>
    public void LoadOrDownloadLuaCode(string path, Action<TextAsset> onComplete, XLuaCustomExport.OnCreate OnCreate = null, byte type = 0)
    {   
        string[] strs = path.Split('/');
        string str = strs[strs.Length - 1].Split('.')[0];
        LoadOrDownload<TextAsset>(AppConst.XLuaCodeTxtPath+path, str+".lua", onComplete, OnCreate, 0);
    }

    /// <summary>
    /// 加载或者下载资源
    /// </summary>
    /// <param name="path">短路径</param>
    /// <param name="onComplete">C#回调</param>
    /// <param name="OnCreate">Lua回调</param>
    public void LoadOrDownloadScene(string path, Action<TextAsset> onComplete, XLuaCustomExport.OnCreate OnCreate = null, byte type = 0)
    {
        string[] strs = path.Split('/');
        string str = strs[strs.Length - 1].Split('.')[0];
        LoadOrDownload<TextAsset>(AppConst.XLuaCodeTxtPath + path, str + ".lua", onComplete, OnCreate, 0);
    }
    public void LoadSprite(string path, string name, Action<Texture2D> onComplete, XLuaCustomExport.OnCreate OnCreate = null,  byte type = 0)
    {
        LoadOrDownload<Texture2D>(string.Format(AppConst.UISourcePath,path), name, onComplete, OnCreate, 0);
    }
    public Sprite LoadAtlasSprite(string atlasName, string name)
    {
        LoadManifestBundle();
        string path = LocalFileMgr.Instance.LocalFilePath + string.Format(AppConst.UISourcePath,"Atlas/"+ atlasName);
        using (AssetBundleLoader loader = new AssetBundleLoader(path, isFullPath: true))
        {
            return loader.LoadAtlasSprite(atlasName, name);
        }
    }
    public Sprite LoadBgSprite(string bgName)
    {
        LoadManifestBundle();
        string path = LocalFileMgr.Instance.LocalFilePath + string.Format(AppConst.UISourcePath, "BackGround/" + bgName);
        using (AssetBundleLoader loader = new AssetBundleLoader(path, isFullPath: true))
        {
            return loader.LoadBgSprite(bgName);
        }
    }
    /// <summary>
    /// 加载或者下载资源
    /// </summary>
    /// <param name="path">短路径</param>
    /// <param name="name">加载对象名</param>
    /// <param name="onComplete">C#回调</param>
    /// <param name="OnCreate">Lua回调</param>
    /// <param name="type">0 = Prefab 1 = PNG</param>
    public void LoadOrDownload<T>(string path, string name, Action<T> onComplete,XLuaCustomExport.OnCreate OnCreate=null, byte type = 0)where T: UnityEngine.Object
    {
        lock (this)
        {
#if DISABLE_ASSETBUNDLE

            string newPath = string.Empty;

            switch (type)
            {
                case 0:
                    newPath = string.Format("Assets/{0}", path.Replace("assetbundle", "prefab"));
                    break;
                case 1:
                    newPath = string.Format("Assets/{0}", path.Replace("assetbundle", "png"));
                    break;
            }

            if (onComplete != null)
            {
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<T>(newPath);
                onComplete(obj as T);
            }

            if (OnCreate != null)
            {
                UnityEngine.Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(newPath);
                OnCreate(obj as GameObject);
            }
        }

#else
            //1.加载依赖文件配置
            LoadManifestBundle();
            //2.加载依赖项开始
            string[] arrDps = m_Manifest.GetAllDependencies(path);
            //3.先检查所有依赖项 是否已经下载 没下载的就下载
            CheckDps(0, arrDps, () =>
            {
                string fullPath = (LocalFileMgr.Instance.LocalFilePath + path).ToLower();
                #region 下载或者加载主资源

                if (!File.Exists(fullPath))
                {
                    //如果文件不存在 需要下载          
                    DownloadDataEntity entity = DownloadMgr.Instance.GetServerData(path);
                    if (entity != null)
                    {
                        AssetBundleDownload.Instance.StartCoroutine(AssetBundleDownload.Instance.DownloadData(entity,
                            (bool isSuccess) =>
                            {
                                if (isSuccess) //如果下载成功
                                {
                                    if (m_AssetDic.ContainsKey(fullPath))
                                    {
                                        if (onComplete != null)
                                        {
                                            //进行回调
                                            onComplete(m_AssetDic[fullPath] as T);
                                        }
                                        return;
                                    }

                                    //先加载依赖项
                                    for (int i = 0; i < arrDps.Length; i++)
                                    {
                                        if (!m_AssetDic.ContainsKey((LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()))
                                        {
                                            AssetBundleLoader loader = new AssetBundleLoader(arrDps[i]);
                                            UnityEngine.Object obj = loader.LoadAsset(GameUtil.GetFileName(arrDps[i]));
                                            m_AssetBundleDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = loader;
                                            m_AssetDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = obj;
                                        }
                                    }

                                    //直接加载
                                    using (AssetBundleLoader loader = new AssetBundleLoader(fullPath, isFullPath: true))
                                    {

                                        if (onComplete != null)
                                        {
                                            UnityEngine.Object obj = loader.LoadAsset<T>(name);
                                            m_AssetDic[fullPath] = obj;
                                            //进行回调
                                            onComplete(obj as T);
                                        }

                                        //todu 进行xlua的回调
                                        if (OnCreate != null)
                                        {
                                            UnityEngine.Object obj = loader.LoadAsset<T>(name);
                                            m_AssetDic[fullPath] = obj;
                                            OnCreate(obj as GameObject);
                                        }
                                    }
                                }
                            }
                        ));
                    }
                    else
                    {
                        Debuger.LogError("The path is error:"+path);
                    }
                }
                else
                {
                    if (m_AssetDic.ContainsKey(fullPath))
                    {
                        if (onComplete != null)
                        {
                            onComplete(m_AssetDic[fullPath] as T);
                        }
                        //todu 进行xlua的回调
                        if (OnCreate != null)
                        {
                            OnCreate(m_AssetDic[fullPath] as GameObject);
                        }
                        return;
                    }

                    //===================================
                    for (int i = 0; i < arrDps.Length; i++)
                    {
                        if (!m_AssetDic.ContainsKey((LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()))
                        {
                            AssetBundleLoader loader = new AssetBundleLoader(arrDps[i]);
                            UnityEngine.Object obj = loader.LoadAsset(GameUtil.GetFileName(arrDps[i]));
                            m_AssetBundleDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = loader;
                            m_AssetDic[(LocalFileMgr.Instance.LocalFilePath + arrDps[i]).ToLower()] = obj;
                        }
                    }

                    //直接加载
                    using (AssetBundleLoader loader = new AssetBundleLoader(fullPath, isFullPath: true))
                    {
                        UnityEngine.Object obj = loader.LoadAsset<T>(name);
                        m_AssetDic[fullPath] = obj;
                        if (onComplete != null)
                        {
                            //进行回调
                            onComplete(obj as T);
                        }
                        //todu 进行xlua的回调
                        if (OnCreate != null)
                        {
                            OnCreate(obj as GameObject);
                        }
                    }
                }
                #endregion
            });
        }
#endif
    }



    /// <summary>
    /// 检查依赖项
    /// </summary>
    /// <param name="index"></param>
    /// <param name="arrDps"></param>
    /// <param name="onComplete"></param>
    private void CheckDps(int index, string[] arrDps, System.Action onComplete)
    {
        lock (this)
        {
            if (arrDps == null || arrDps.Length == 0)
            {
                if (onComplete != null) onComplete();
                return;
            }

            string fullPath = LocalFileMgr.Instance.LocalFilePath + arrDps[index];
        
            if (!File.Exists(fullPath))
            {
                //如果文件不存在 需要下载
                DownloadDataEntity entity = DownloadMgr.Instance.GetServerData(arrDps[index]);
                if (entity != null)
                {
                    AssetBundleDownload.Instance.StartCoroutine(AssetBundleDownload.Instance.DownloadData(entity,
                        (bool isSuccess) =>
                        {
                            index++;
                            if (index == arrDps.Length)
                            {
                                if (onComplete != null) onComplete();
                                return;
                            }

                            CheckDps(index, arrDps, onComplete);
                        }));
                }

            }
            else
            {
                index++;
                
                if (index == arrDps.Length)
                {
                    if (onComplete != null) onComplete();
                    return;
                }

                CheckDps(index, arrDps, onComplete);
            }
        }
    }





    /// <summary>
    /// 加载镜像
    /// </summary>
    /// <param name="ptah">资源路径</param>
    /// <param name="name">资源名称</param>
    /// <returns></returns>
    public GameObject Load(string path, string name)
    {
#if DISABLE_ASSETBUNDLE
        return UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/{0}", path.Replace("assetbundle", "prefab")));
#else
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            return loader.LoadAsset<GameObject>(name);
        }
#endif

    }


    /// <summary>
    /// 同步加载
    /// </summary>
    /// <param name="path">资源路径</param>
    /// <param name="name">资源名称</param>
    /// <returns></returns>
    public GameObject LoadClone(string path, string name)
    {
#if DISABLE_ASSETBUNDLE
        GameObject obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(string.Format("Assets/{0}", path.Replace("assetbundle", "prefab")));
        return UnityEngine.Object.Instantiate(obj);
#else
        using (AssetBundleLoader loader = new AssetBundleLoader(path))
        {
            GameObject obj= loader.LoadAsset<GameObject>(name);
            return UnityEngine.Object.Instantiate(obj);
        }
#endif

    }


    
    /// <summary>
    /// 异步加载
    /// </summary>
    /// <param name="path">加载路径</param>
    /// <param name="name">资源名称</param>
    /// <returns></returns>
    public AssetBundleLoaderAsync LoadAsync(string path, string name)
    {
        //实例化一个游戏对象
        GameObject obj = new GameObject("AssetBundleLoadAsync");

        //如果obj 没有这个脚本  就添加这个脚本
        AssetBundleLoaderAsync async = obj.GetOrAddComponent<AssetBundleLoaderAsync>();
        
        //初始化路径和名称
        async.Init(path, name);

        return async;

    }


    /// <summary>
    /// 卸载依赖项
    /// </summary>
    public void UnloadDpsAssetBundle()
    {
        foreach (var item in m_AssetBundleDic)
        {
            item.Value.Dispose();
        }
        m_AssetBundleDic.Clear();

        m_AssetDic.Clear();
    }


}

