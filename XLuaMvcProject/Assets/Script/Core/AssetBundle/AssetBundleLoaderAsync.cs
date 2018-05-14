using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 异步加载资源包
/// </summary>
public class AssetBundleLoaderAsync : MonoBehaviour
{
    
    /// <summary>
    /// 资源完整路径
    /// </summary>
    private string m_FullPath;

    /// <summary>
    /// 资源名称
    /// </summary>
    private string m_Name;

    /// <summary>
    /// 资源包异步创建请求
    /// </summary>
    private AssetBundleCreateRequest request;

    /// <summary>
    /// 资源包
    /// </summary>
    private AssetBundle bundle;

    /// <summary>
    /// 加载完成委托
    /// </summary>
    public Action<UnityEngine.Object> OnLoadComplete;








	void Start ()
	{
        //开启协程
        StartCoroutine(Load());
	}


    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="path"></param>
    /// <param name="name"></param>
    public void Init(string path, string name)
    {
        m_FullPath = LocalFileMgr.Instance.LocalFilePath + path;
        m_Name = name;
    }


    
    /// <summary>
    /// 加载资源
    /// </summary>
    /// <returns></returns>
    private IEnumerator Load()
    {
        request = AssetBundle.LoadFromMemoryAsync(LocalFileMgr.Instance.GetBuffer(m_FullPath));
        yield return request;
        bundle = request.assetBundle;


        if (OnLoadComplete != null)
        {
            Debug.Log("加载资源完成");

            OnLoadComplete(bundle.LoadAsset(m_Name));
            DestroyImmediate(gameObject);

        }
    }



    /// <summary>
    /// 销毁时执行
    /// </summary>
    void OnDestroy()
    {
        //卸载所有包含在bundle中的对象。 
        if (bundle != null) bundle.Unload(false);
        Debug.Log("卸载资源");
        m_FullPath = null;
        m_Name = null;
    }


	
}

