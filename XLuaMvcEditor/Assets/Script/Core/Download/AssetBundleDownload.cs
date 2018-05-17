using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 资源 主下载器
/// </summary>
public class AssetBundleDownload : SingletonMono<AssetBundleDownload>
{

    //版本信息资源地址
    private string m_VersionUrl;
    //初始化服务器版本信息委托
    private Action<List<DownloadDataEntity>> m_OnInitVersion;

    /// <summary>
    /// 下载器数组
    /// </summary>
    private AssetBundleDownloadRoutine[] m_Routine = new AssetBundleDownloadRoutine[AppConst.DownLoadRountineNum];

    /// <summary>
    /// 下载器索引
    /// </summary>
    private int m_RoutineIndex = 0;

    /// <summary>
    /// 是否下载完成
    /// </summary>
    private bool m_IsDownloadOver = false;



    protected override void OnStart()
    {
        base.OnStart();

        //真正的运行
        StartCoroutine(DownLoadVersion(m_VersionUrl));
    }


    //采样时间
    private float m_Time = 2;
    //已经下载的时间
    private float m_AlreadyTime = 0f;
    //剩余的时间
    private float m_NeedTime = 0f;
    //下载速度
    private float m_Speed = 0f;

    protected override void OnUpdate()
    {
        base.OnUpdate();

        //如果需要下载的数量大于0 并且还没有下载完成
        if (TotalCount > 0 && !m_IsDownloadOver)
        {
            //当前下载的数量
            int totalCompleteCount = CurrCompleteTotalCount();
            totalCompleteCount = totalCompleteCount == 0 ? 1 : totalCompleteCount;

            //当前下载的大小
            int totalCompleteSize = CurrCompleteTotalSize();

            //已经下载的时间
            m_AlreadyTime += Time.deltaTime;
            if (m_AlreadyTime > m_Time && m_Speed == 0)
            {
                //速度
                m_Speed = totalCompleteSize / m_Time;
            }

            //计算下载剩余时间 = （总大小 - 已经下载的大小） / 速度
            if (m_Speed > 0)
            {
                m_NeedTime = (TotalSize - totalCompleteSize) / m_Speed;
            }

            string str = string.Format("正在下载{0}/{1}", totalCompleteCount, TotalCount);
            string strProgress = string.Format("下载进度={0}", totalCompleteSize / (float)TotalSize);

            UISceneInitView.Instance.SetProgress(str, totalCompleteCount / (float)TotalCount);

            if (m_NeedTime > 0)
            {
                string strTime = string.Format("剩余时间={0}秒", m_NeedTime);
                //AppDebug.Log(strTime);
            }
          
            if (totalCompleteCount == TotalCount)
            {
                m_IsDownloadOver = true;
                UISceneInitView.Instance.SetProgress("资源更新完毕", 1);
                if (DownloadMgr.Instance.OnInitComplete != null)
                {
                    DownloadMgr.Instance.OnInitComplete();
                }

            }
        }
    }






    /// <summary>
    /// 初始化服务器版本信息
    /// </summary>
    /// <param name="url">资源地址</param>
    /// <param name="onInitVersion">初始化服务器版本信息委托</param>
    public void InitServerVersion(string url, Action<List<DownloadDataEntity>> onInitVersion)
    {
        m_VersionUrl = url;
        m_OnInitVersion = onInitVersion;
    }

    /// <summary>
    /// 加载版本信息文件
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    private IEnumerator DownLoadVersion(string url)
    {
        WWW www = new WWW(url);

        float timeOut = Time.time;
        float progress = www.progress;

        while (www != null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;
            }

            if ((Time.time - timeOut) > AppConst.DownLoadTimeOut)
            {
                Debug.Log("下载超时");
                yield break;
            }
        }

        yield return www;

        if (www != null && www.error == null)
        {
            string conten = www.text;
            if (m_OnInitVersion != null)
            {
                m_OnInitVersion(DownloadMgr.Instance.PackDownLoadData(conten));
            }
        }
        else
        {
            Debug.Log("下载失败 原因：" + www.error);
        }
    }


    /// <summary>
    /// 总大小
    /// </summary>
    public int TotalSize
    {
        get;
        private set;
    }
    /// <summary>
    /// 当前已经下载的文件的总大小
    /// </summary>
    /// <returns></returns>
    public int CurrCompleteTotalSize()
    {
        int completeTotalSize = 0;

        for (int i = 0; i < m_Routine.Length; i++)
        {
            if (m_Routine[i] == null) continue;
            completeTotalSize += m_Routine[i].DownloadSize;
        }

        return completeTotalSize;
    }



    /// <summary>
    /// 总数量
    /// </summary>
    public int TotalCount
    {
        get;
        private set;
    }
    /// <summary>
    /// 当前已经下载的文件的总数量
    /// </summary>
    /// <returns></returns>
    public int CurrCompleteTotalCount()
    {
        int completeTotalCount = 0;

        for (int i = 0; i < m_Routine.Length; i++)
        {
            if (m_Routine[i] == null) continue;
            completeTotalCount += m_Routine[i].CompleteCoun;
        }

        return completeTotalCount;
    }



    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="downloadList"></param>
    public void DownloadFiles(List<DownloadDataEntity> downloadList)
    {
        TotalSize = 0;
        TotalCount = 0;

        //初始化下载器
        for (int i = 0; i < m_Routine.Length; i++)
        {
            if (m_Routine[i] == null)
            {
                m_Routine[i] = gameObject.AddComponent<AssetBundleDownloadRoutine>();
            }
        }

        //循环的给下载器分配下载任务
        for (int i = 0; i < downloadList.Count; i++)
        {
            //0-4
            m_RoutineIndex = m_RoutineIndex % m_Routine.Length;

            //其中的一个下载器 给它分配一个文件
            m_Routine[m_RoutineIndex].AddDownload(downloadList[i]);

            m_RoutineIndex++;
            TotalSize += downloadList[i].Size;
            TotalCount++;
        }

        //让下载器开始下载
        for (int i = 0; i < m_Routine.Length; i++)
        {
            if (m_Routine[i] == null) continue;
            m_Routine[i].StartDownload();
        }


    }


    /// <summary>
    /// 动态下载更新
    /// </summary>
    /// <param name="currDownloadData">当前需要下载的文件</param>
    /// <param name="onComplete">是否下载完成</param>
    /// <returns></returns>
    public IEnumerator DownloadData(DownloadDataEntity currDownloadData,Action<bool> onComplete)
    {

        //资源下载路径
        string dataUrl = DownloadMgr.Instance.DownLoadUrl + currDownloadData.FullName;
        Debug.Log("dataUrl         " + dataUrl);
        //短路径 用于创建文件夹
        string path = currDownloadData.FullName.Substring(0, currDownloadData.FullName.LastIndexOf('\\'));

        //得到本地路径
        string localFilePath = DownloadMgr.Instance.LocalFilePath + path;

        if (!Directory.Exists(localFilePath))
        {
            Directory.CreateDirectory(localFilePath);
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
            }

            if ((Time.time - timeOut) > AppConst.DownLoadTimeOut)
            {
                Debug.LogError("下载失败 超时");
                if (onComplete != null)
                {
                    onComplete(false);
                }
                yield break;
            }

            yield return null;
        }

        yield return www;

        if (www != null && www.error == null)
        {
            using (FileStream fs = new FileStream(DownloadMgr.Instance.LocalFilePath + currDownloadData.FullName, FileMode.Create, FileAccess.ReadWrite))
            {
                fs.Write(www.bytes, 0, www.bytes.Length);
            }
        }
   
        //写入本地文件
        DownloadMgr.Instance.ModifyLocalData(currDownloadData);

        if (onComplete != null)
        {
            onComplete(true);
        }
    }

}
