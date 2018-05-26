using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 下载器
/// </summary>
public class AssetBundleDownloadRoutine : MonoBehaviour 
{

    /// <summary>
    /// 这个下载器需要下载的文件列表
    /// </summary>
    private List<DownloadDataEntity> m_List = new List<DownloadDataEntity>();

    /// <summary>
    /// 当前正在下载的数据
    /// </summary>
    private DownloadDataEntity m_CurrDownLoadData;

    /// <summary>
    /// 需要下载的数量
    /// </summary>
    public int NeedDownloadCount
    {
        get;
        private set;
    }

    /// <summary>
    /// 已经下载完成的数量
    /// </summary>
    public int CompleteCoun
    {
        get;
        private set;
    }

    /// <summary>
    /// 已经下载好的文件的总大小
    /// </summary>
    private int m_DownloadSize;

    /// <summary>
    /// 当前下载文件的大小
    /// </summary>
    private int m_CurrDownloadSiz;

    /// <summary>
    /// 这个下载器已经下载的大小
    /// </summary>
    public int DownloadSize
    {
        get { return m_DownloadSize + m_CurrDownloadSiz; }
    }


    /// <summary>
    /// 是否开始下载
    /// </summary>
    public bool IsStartDownload
    {
        get;
        private set;
    }




    /// <summary>
    /// 添加下载对象
    /// </summary>
    /// <param name="entity"></param>
    public void AddDownload(DownloadDataEntity entity)
    {
        m_List.Add(entity);
    }

    /// <summary>
    /// 开始下载
    /// </summary>
    public void StartDownload()
    {
        IsStartDownload = true;
        NeedDownloadCount = m_List.Count;
    }

    void Update()
    {
        if (IsStartDownload)
        {
            IsStartDownload = false;
            StartCoroutine(DownloadData());
        }
    }

    private IEnumerator DownloadData()
    {
        if (NeedDownloadCount == 0) yield break;
        m_CurrDownLoadData = m_List[0];
        //资源下载路径
        string dataUrl = DownloadMgr.Instance.DownLoadUrl + m_CurrDownLoadData.FullName;
        int lastIndex = m_CurrDownLoadData.FullName.LastIndexOf('\\');//大于-1 说明路径包含了文件夹
        if (lastIndex > -1)
        {
            //文件夹短路径 用于创建文件夹
            string path = m_CurrDownLoadData.FullName.Substring(0, lastIndex);
            //得到本地路径
            string localFilePath = DownloadMgr.Instance.LocalFilePath + path;

            if (!Directory.Exists(localFilePath))
            {
                Directory.CreateDirectory(localFilePath);
            }
        }
        WWW www = new WWW(dataUrl);
        float timeOut = Time.time;
        float progress = www.progress;

        while (www != null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;

                m_CurrDownloadSiz = (int)(m_CurrDownLoadData.Size * progress);
            }

            if ((Time.time - timeOut) > AppConst.DownLoadTimeOut)
            {
                Debug.LogError("下载失败 超时");
                yield break;
            }

            yield return null;
        }

        yield return www;

        if (www != null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadMgr.Instance.LocalFilePath + m_CurrDownLoadData.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);
            }
        }

        //下载成功

        m_CurrDownloadSiz = 0;
        m_DownloadSize += m_CurrDownLoadData.Size;
        
        //写入本地文件
        DownloadMgr.Instance.ModifyLocalData(m_CurrDownLoadData);
        
        m_List.RemoveAt(0);
        CompleteCoun++;
        if (m_List.Count == 0)
        {
            m_List.Clear();
        }
        else
        {
            IsStartDownload = true;
        }
    }
}

    


