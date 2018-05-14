using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 异步加载控制
/// 功能: 异步加载进度
/// </summary>
public class LoadingSceneCtrl : MonoBehaviour
{
    /// <summary>
    /// UI场景控制器
    /// </summary>
    [SerializeField]
    private UISceneLoadingView m_UILoadingView;

    /// <summary>
    /// 异步操作协同程序
    /// </summary>
    private AsyncOperation m_Async = null;

    /// <summary>
    /// 当前进度
    /// </summary>
    private int m_CurrProgress = 0;

    /// <summary>
    /// 资源包异步创建请求
    /// </summary>
    private AssetBundleCreateRequest request;

    /// <summary>
    /// 资源包
    /// </summary>
    private AssetBundle bundle;



    void Start()
    {

        AssetBundleMgr.Instance.UnloadDpsAssetBundle();
        GC.Collect();
        Resources.UnloadUnusedAssets();
        DelegateDefine.Instance.OnSceneLoadOK += OnSceneLoadOk;
        //重置层级关系
        LayerUIMgr.Instance.Reset();
        //启动协程
        StartCoroutine(LoadingScene());
        UIViewUtil.Instance.CloseAllWindow();

    }

    /// <summary>
    /// 加载完成销毁LodingUI
    /// </summary>
    private void OnSceneLoadOk()
    {
        if (m_UILoadingView != null)
        {
            Destroy(m_UILoadingView.gameObject);
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// 异步加载场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadingScene()
    {
        //场景名称
        string strSceneName = SceneMgr.Instance.CurrentSceneType;

        //如果目标场景名称是空，则直接返回
        if (string.IsNullOrEmpty(strSceneName))
        {
            yield break;
        }
#if DISABLE_ASSETBUNDLE
        //加载
        m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
        //设置允许场景激活为false
        m_Async.allowSceneActivation = false;
        yield return m_Async;
#else
        StartCoroutine(Load(string.Format("Download/Scene/{0}.unity3d", strSceneName), strSceneName));
#endif
    }


    private IEnumerator Load(string path, string strSceneName)
    {
#if DISABLE_ASSETBUNDLE
        yield return null;
        path = path.Replace(".unity3d", "");
        m_Async = SceneManager.LoadSceneAsync(strSceneName, LoadSceneMode.Additive);
        //设置允许场景激活为false
        m_Async.allowSceneActivation = false;
#else
        //获取资源完整路径
        string fullPath = LocalFileMgr.Instance.LocalFilePath + path;
        
        //如果路径不存在 就要进行下载
        if (!File.Exists(fullPath))
        {
            //得到服务器资源下载地址数据
            DownloadDataEntity entity = DownloadMgr.Instance.GetServerData(path);

            if (entity != null)
            {
                StartCoroutine(AssetBundleDownload.Instance.DownloadData(entity,
                    (bool isSuccess) =>
                    {
                        if (isSuccess) //如果下载成功
                        {
                            StartCoroutine(LoadScene(fullPath, strSceneName));
                        }
                    }));
            }
        }
        else //如果路径存在
        {
            StartCoroutine(LoadScene(fullPath, strSceneName));
        }

        yield return null;
#endif
    }


    private IEnumerator LoadScene(string fullPath, string sceneName)
    {
        //异步从内存创建资源
        request = AssetBundle.LoadFromMemoryAsync(LocalFileMgr.Instance.GetBuffer(fullPath));

        yield return request;

        bundle = request.assetBundle;

        m_Async = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        //设置允许场景激活为false
        m_Async.allowSceneActivation = false;
    }



    void Update()
    {
        if (m_Async == null) return;

        //目标进度
        int toProgress = 0;

        //如果进度小于0.9
        if (m_Async.progress < 0.9f)
        {
            toProgress = Mathf.Clamp((int)m_Async.progress * 100, 1, 100);
        }
        else
        {
            toProgress = 100;
        }

        if (m_CurrProgress < toProgress || m_CurrProgress < 100)
        {
            m_CurrProgress++;
        }
        else
        {
            m_Async.allowSceneActivation = true;
        }

        m_UILoadingView.SetProgressValue(m_CurrProgress * 0.01f);
    }

    void OnDestroy()
    {
        DelegateDefine.Instance.OnSceneLoadOK -= OnSceneLoadOk;

        if (bundle != null)
        {
            //卸载bundle
            bundle.Unload(false);
        }

        request = null;
        bundle = null;
    }

}
