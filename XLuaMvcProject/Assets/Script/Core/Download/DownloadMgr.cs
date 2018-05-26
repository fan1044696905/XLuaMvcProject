using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;


/// <summary>
/// 下载资源管理器
/// </summary>
public class DownloadMgr : Singleton<DownloadMgr>
{


#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    /// <summary>
    /// 资源下载路径
    /// </summary>
    public string DownLoadUrl = AppConst.ServerUrl + "Windows/";
#elif UNITY_ANDROID
    /// <summary>
    /// 资源下载路径
    /// </summary>
    public string DownLoadUrl = AppConst.ServerUrl + "Android/";
#elif UNITY_IPHONE
    /// <summary>
    /// 资源下载路径
    /// </summary>
    public string DownLoadUrl = AppConst.ServerUrl + "iOS/";
#endif

    /// <summary>
    /// 本地资源路径
    /// </summary>
    public string LocalFilePath = Application.persistentDataPath + "/";

    /// <summary>
    /// 需要下载的资源数据列表
    /// </summary>
    private List<DownloadDataEntity> m_NeedDownLoadDataList = new List<DownloadDataEntity>();

    /// <summary>
    /// 本地资源数据列表
    /// </summary>
    private List<DownloadDataEntity> m_LocalDataList = new List<DownloadDataEntity>();

    /// <summary>
    /// 服务器资源数据列表
    /// </summary>
    private List<DownloadDataEntity> m_ServerDataList;
    
    /// <summary>
    /// 本地的版本文件
    /// </summary>
    private string m_LocalVersionPath;
    
    /// <summary>
    /// StreamingAssets 路径
    /// </summary>
    private string m_StreamingAssetsPath;
    
    /// <summary>
    /// 初始化完毕委托
    /// </summary>
    public Action OnInitComplete;

    
    /// <summary>
    /// 第一步： 初始化资源
    /// </summary>
    public void InitStreamingAssets(Action onInitComplete)
    {
        OnInitComplete = onInitComplete;

        //本地版本文件路径
        m_LocalVersionPath = LocalFilePath + AppConst.VersionFileName;

        //判读本地是否已经有资源
        if (File.Exists(m_LocalVersionPath))
        {
            //如果有资源则 检查更新
            InitCheckVersion();
        }
        else
        {
            //如果没有资源 执行初始化,然后再检查更新
            m_StreamingAssetsPath = "file:///" + Application.streamingAssetsPath + "/AssetBundles/";
#if UNITY_ANDROID && !UNITY_EDITOR
            m_StreamingAssetsPath = Application.streamingAssetsPath + "/AssetBundles/";
#endif

            string versionFileUrl = m_StreamingAssetsPath + AppConst.VersionFileName;
            
            GlobalInit.Instance.StartCoroutine(ReadStreamingAssetVersionFile(versionFileUrl, OnReadStreaminAssetOver));
        }
    }

    /// <summary>
    /// 读取初始资源目录的版本文件
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="onReadStreaminAssetOver"></param>
    /// <returns></returns>
    private IEnumerator ReadStreamingAssetVersionFile(string fileUrl, Action<string> onReadStreaminAssetOver)
    {

        UISceneInitView.Instance.SetProgress("正在准备进行资源初始化", 0);
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                if (onReadStreaminAssetOver != null)
                {
                    onReadStreaminAssetOver(Encoding.UTF8.GetString(www.bytes));
                }
            }
            else
            {
                onReadStreaminAssetOver("");
            }
        }
    }


    /// <summary>
    /// 读取版本文件完毕
    /// </summary>
    /// <param name="obj"></param>
    private void OnReadStreaminAssetOver(string content)
    {
        GlobalInit.Instance.StartCoroutine(InitStreamingAssetList(content));
    }

    /// <summary>
    /// 初始化资源清单
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private IEnumerator InitStreamingAssetList(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            InitCheckVersion();
            yield break;
        }

        string[] arr = content.Split('\n');
        //循环解压
        for (int i = 0; i < arr.Length; i++)
        {
            string[] arrInfo = arr[i].Split(' ');
            string filUrl = arrInfo[0]; //短路经

            yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(m_StreamingAssetsPath + filUrl,
              LocalFilePath + filUrl
             ));

            float value = (i + 1) / (float)arr.Length;
            UISceneInitView.Instance.SetProgress(string.Format("初始化资源不消耗流量{0}/{1}", i + 1, arr.Length), value);
        }

        //解压版本文件
        yield return GlobalInit.Instance.StartCoroutine(AssetLoadToLocal(m_StreamingAssetsPath + AppConst.VersionFileName,
                LocalFilePath + AppConst.VersionFileName
                ));

        //检查更新
        InitCheckVersion();
    }

    /// <summary>
    /// 解压某个文件到本地
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="toPath"></param>
    /// <returns></returns>
    private IEnumerator AssetLoadToLocal(string fileUrl, string toPath)
    {
        using (WWW www = new WWW(fileUrl))
        {
            yield return www;
            if (www.error == null)
            {
                int lastIndexof = toPath.LastIndexOf('\\');
                if (lastIndexof != -1)
                {
                    //除去文件名以外的路径
                    string localPath = toPath.Substring(0, lastIndexof);
                    if (!Directory.Exists(localPath))
                    {
                        Directory.CreateDirectory(localPath);
                    }
                }

                using (FileStream fs = File.Create(toPath, www.bytes.Length))
                {
                    fs.Write(www.bytes, 0, www.bytes.Length);
                    fs.Close();
                }
            }
        }
    }




    /// <summary>
    /// 初始化 检查版本文件
    /// </summary>
    public void InitCheckVersion()
    {
        UISceneInitView.Instance.SetProgress("正在检查版本更新", 0);

        //资源版本文件路径
        string strVersionUrl = DownLoadUrl + AppConst.VersionFileName;

        //读取版本文件
        AssetBundleDownload.Instance.InitServerVersion(strVersionUrl, OnInitVersionCallBack);
    }


    /// <summary>
    /// 初始化版本文件回调
    /// </summary>
    /// <param name="obj"></param>
    private void OnInitVersionCallBack(List<DownloadDataEntity> serverDownloadData)
    {
        //得到服务端数据列表
        m_ServerDataList = serverDownloadData;
        
        if (File.Exists(m_LocalVersionPath))
        {
            //如果本地存在版本文件 则和服务器端的进行对比

            //服务器端的版本文件字典
            Dictionary<string, string> serverDic = PackDownloadDataDic(serverDownloadData);


            //客户端版本文件
            string content = IOUtil.GetFileText(m_LocalVersionPath);
            Dictionary<string, string> clientDic = PackDownloadDataDic(content);

            m_LocalDataList = PackDownLoadData(content);

            //1.查找新加的初始资源
            for (int i = 0; i < serverDownloadData.Count; i++)
            {
                //Debug.LogError("name==>" + serverDownloadData[i].FullName);

                if (serverDownloadData[i].IsFirstData && !clientDic.ContainsKey(serverDownloadData[i].FullName))
                {
                    //加入下载列表
                    m_NeedDownLoadDataList.Add(serverDownloadData[i]);
                }
            }

            //2.对比已经下载过的 但是有更新的资源
            foreach (var item in clientDic)
            {
                //如果Md5不一致
                if (serverDic.ContainsKey(item.Key) && serverDic[item.Key] != item.Value)
                {
                    DownloadDataEntity entity = GetDownLoadData(item.Key, serverDownloadData);
                    if (entity != null)
                    {
                        m_NeedDownLoadDataList.Add(entity);
                    }
                }
            }
        }
        else
        {
            //如果不存在 则初始资源 就是需要下载的文件
            for (int i = 0; i < serverDownloadData.Count; i++)
            {
                if (serverDownloadData[i].IsFirstData)
                {
                    m_NeedDownLoadDataList.Add(serverDownloadData[i]);
                }
            }
        }

        if (m_NeedDownLoadDataList.Count == 0)
        {
            UISceneInitView.Instance.SetProgress("资源更新完毕", 1);
            if (OnInitComplete != null)
            {
                OnInitComplete();
            }
            return;
        }
        //拿到下载列表 m_NeedDownLoadDataList 进行下载 
        AssetBundleDownload.Instance.DownloadFiles(m_NeedDownLoadDataList);


    }


    /// <summary>
    /// 根据资源名称获取资源实体
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="lst"></param>
    /// <returns></returns>
    private DownloadDataEntity GetDownLoadData(string fullName, List<DownloadDataEntity> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].FullName.Equals(fullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return lst[i];
            }
        }

        return null;
    }


    /// <summary>
    /// 封装字典
    /// </summary>
    /// <param name="lst"></param>
    /// <returns></returns>
    public Dictionary<string, string> PackDownloadDataDic(List<DownloadDataEntity> lst)
    {
        Dictionary<string, string> Dic = new Dictionary<string, string>();

        for (int i = 0; i < lst.Count; i++)
        {
            Dic[lst[i].FullName] = lst[i].MD5;
        }

        return Dic;
    }

    /// <summary>
    /// 封装字典
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public Dictionary<string, string> PackDownloadDataDic(string content)
    {
        Dictionary<string, string> Dic = new Dictionary<string, string>();

        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                Dic[arrData[0]] = arrData[1];
            }
        }

        return Dic;
    }


    /// <summary>
    /// 根据文本内容 封装下载数据列表
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public List<DownloadDataEntity> PackDownLoadData(string content)
    {
        List<DownloadDataEntity> lst = new List<DownloadDataEntity>();

        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                DownloadDataEntity entity = new DownloadDataEntity();
                entity.FullName = arrData[0];
                entity.MD5 = arrData[1];
                entity.Size = arrData[2].ToInt();
                entity.IsFirstData = arrData[3].ToInt() == 1;
                lst.Add(entity);
            }
        }

        return lst;

    }


    /// <summary>
    /// 修改本地文件
    /// </summary>
    /// <param name="entity"></param>
    public void ModifyLocalData(DownloadDataEntity entity)
    {
        if (m_LocalDataList == null) return;
        bool isExists = false;
        
        for (int i = 0; i < m_LocalDataList.Count; i++)
        {
            if (m_LocalDataList[i].FullName.Equals(entity.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                m_LocalDataList[i].MD5 = entity.MD5;
                m_LocalDataList[i].Size = entity.Size;
                m_LocalDataList[i].IsFirstData = entity.IsFirstData;
                isExists = true;
                break;
            }
        }

        if (!isExists)
        {
            m_LocalDataList.Add(entity);
        }

        SaveLocalVersion();
    }


    /// <summary>
    /// 保存本地版本文件
    /// </summary>
    private void SaveLocalVersion()
    {
        StringBuilder sbConetn = new StringBuilder();
        for (int i = 0; i < m_LocalDataList.Count; i++)
        {
            sbConetn.AppendLine(string.Format("{0} {1} {2} {3}", m_LocalDataList[i].FullName, m_LocalDataList[i].MD5, m_LocalDataList[i].Size, m_LocalDataList[i].IsFirstData ? 1 : 0));
        }

        IOUtil.CreateTextFile(m_LocalVersionPath, sbConetn.ToString());
    }



    /// <summary>
    /// 从服务器获取资源地址
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public DownloadDataEntity GetServerData(string path)
    {
        if (m_ServerDataList == null) return null;

        for (int i = 0; i < m_ServerDataList.Count; i++)
        {
            if (path.Replace("/", "\\").Equals(m_ServerDataList[i].FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return m_ServerDataList[i];
            }
        }

        return null;
    }





}
