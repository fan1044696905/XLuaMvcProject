using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// 本地配置初始化时下载到本地(assetbundle)
/// 服务器配置初始化时把配置加载到内存
/// </summary>
public class XMLManager : SingletonMono<XMLManager> {
    
    private string suffixPath = ".xml";
    
    private Dictionary<string, Dictionary<int, Dictionary<string, object>>> allConfigDic;
    private Dictionary<int, Dictionary<string, object>> oneConfigDic;

    List<string> xmlNameList;
    Dictionary<string,int> xmlNameDic;

    private Action<string> complateAction;

    private int totalSize;
    public int TotalSize
    {
        get
        {
            return totalSize / 1024;
        }
        private set { totalSize = value; }
    }
    public bool isStartDownload
    {
        get;
        private set;
    }
    public int TotalCount
    {
        get;
        private set;
    }
    public int TotalCompleteCount
    {
        get;
        private set;
    }

    public int DownloadSize { get { return (m_DownloadSize + m_CurrDownloadSiz)/1024; } }
    private int m_CurrDownloadSiz;//当前文件已经下载大小
    private int m_DownloadSize;//已下载好的文件的总大小
    public bool IsDownloadFinish
    {
        get;
        private set;
    }
    protected override void OnAwake()
    {
        allConfigDic = new Dictionary<string, Dictionary<int, Dictionary<string, object>>>();
        xmlNameList = new List<string>();
        xmlNameDic = new Dictionary<string, int>();
        #region ----- 预加载文件 -----



        #endregion
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (isStartDownload)
        {
            isStartDownload = false;
            StartCoroutine(LoadXmlByName(xmlNameList[0]));
        }

    }
    public void StartDownload()
    {
        StartCoroutine(SplitXmlTxt());
    }

    private Dictionary<int, Dictionary<string, object>> GetOrAddFile(string fileName)
    {
        if (!allConfigDic.ContainsKey(fileName))
        {
            Debuger.LogError("cant find config:" + fileName);
        }
        Dictionary<int, Dictionary<string, object>> dic;
        if (allConfigDic.TryGetValue(fileName,out dic))
        {
            return dic;
        }
        return null;
    }

    #region ------ 获取服务器xml配置中id所对应的字段 ------

    public int GetInt(string fileName, int id, string typeName)
    {
        return int.Parse(GetString(fileName, id, typeName));
    }

    public float GetFloat(string fileName, int id, string typeName)
    {
        return Convert.ToSingle(GetString(fileName, id, typeName));
    }

    /// <summary>
    /// 获取服务器Xml配置中的string字段调用这个方法
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="id"></param>
    /// <param name="typeName"></param>
    /// <returns></returns>
    public string GetString(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFile(fileName);
        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                return o.ToString();
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return null;
    }
    public string[] GetStringArray(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFile(fileName);
        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                string tempStr = o.ToString();
                tempStr = tempStr.Substring(1, tempStr.Length - 2);
                string[] strArray = tempStr.Split(',');
                return strArray;
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
        }
        Debuger.LogError(fileName + " 中不存在 " + "id" + id);
        return null;
    }
    public int[] GetIntArray(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFile(fileName);
        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                string tempStr = o.ToString();
                tempStr = tempStr.Substring(1, tempStr.Length - 2);
                string[] strArray = tempStr.Split(',');
                int[] intArray = new int[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    intArray[i] = int.Parse(strArray[i]);
                }
                return intArray;
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
        }
        Debuger.LogError(fileName + " 中不存在 " + "id" + id);
        return null;
    }

    public float[] GetFloatArray(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFile(fileName);
        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                string tempStr = o.ToString();
                tempStr = tempStr.Substring(1, tempStr.Length - 2);
                string[] strArray = tempStr.Split(',');
                float[] floatArray = new float[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    floatArray[i] = Convert.ToSingle(strArray[i]);
                }
                return floatArray;
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
        }
        Debuger.LogError(fileName + " 中不存在 " + "id" + id);
        return null;
    }

    public Dictionary<string, object> GetOneLine(string fileName,int id)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFile(fileName);
        if (dic1.ContainsKey(id))
        {
            return dic1[id];
        }
        Debuger.LogError(fileName + " 中不存在 " + "id" + id);
        return null;
    }

    #endregion

    

    public IEnumerator SplitXmlTxt()
    {
        WWW www = new WWW(AppConst.XmlTxtPath);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            totalSize = www.size;
            string xmlTxtStr = www.text;
            string[] xmlNames = xmlTxtStr.Split(',');
            TotalCount = xmlNames.Length - 1;
            for (int i = 0; i < xmlNames.Length-1; i++)
            {
                string[] xmlMsgs = xmlNames[i].Split('_');
                xmlNameList.Add(xmlMsgs[0]);
                xmlNameDic.Add(xmlMsgs[0], Int32.Parse(xmlMsgs[1]));
                totalSize += Int32.Parse(xmlMsgs[1]);
            }
        }
        isStartDownload = true;
    }

    public IEnumerator LoadXmlByName(string xmlName)
    {
        string url = string.Format(AppConst.XmlPath, xmlName);
        WWW www = new WWW(url);
        int size = xmlNameDic[xmlName];
        float timeOut = Time.time;
        float progress = www.progress;
        while (www != null && !www.isDone)
        {
            if (progress < www.progress)
            {
                timeOut = Time.time;
                progress = www.progress;

                m_CurrDownloadSiz = (int)(size * progress);
            }

            if ((Time.time - timeOut) > AppConst.DownLoadTimeOut)
            {
                Debug.LogError("下载失败 超时");
                yield break;
            }

            yield return null;
        }

        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            string str = www.text;
            AddXmlConfig(xmlName.Split('.')[0], str);
            //下载成功
            xmlNameList.RemoveAt(0);
            if (xmlNameList.Count == 0)
            {
                xmlNameList.Clear();
                xmlNameDic.Clear();
                IsDownloadFinish = true;
            }
            else
            {
                isStartDownload = true;
            }
            m_CurrDownloadSiz = 0;
            m_DownloadSize += www.size;
            TotalCompleteCount++;
        }
        else
        {
            Debuger.LogError("The url is error:  " + url);
        }
    }


    private Dictionary<int, Dictionary<string, object>> GetOrAddFileByBundle(string fileName)
    {
        if (!allConfigDic.ContainsKey(fileName))
        {
            string path = string.Format("Download/Config/Xml/{0}.assetbundle", fileName);
            AssetBundleMgr.Instance.LoadOrDownload<TextAsset>(path, fileName, (TextAsset ta) =>
            {
                AddXmlConfig(fileName, ta.text);
            });
        }
        Dictionary<int, Dictionary<string, object>> dic;
        if (allConfigDic.TryGetValue(fileName, out dic))
        {
            return dic;
        }
        return null;
    }

    #region ------ 获取本地xml配置中id所对应的字段 ------
    /// <summary>
    /// 获取本地的Xml文件(被打包成assetbundle的xml)配置中的string字段调用这个方法
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="id">获取的id(行)</param>
    /// <param name="typeName">获取的类型(列)</param>
    public string GetBundleString(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFileByBundle(fileName);

        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                return o.ToString();
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
            return null;
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return null;
    }

    public int GetBundleInt(string fileName, int id, string typeName)
    {
        return int.Parse(GetBundleString(fileName, id, typeName));
    }

    public float GetBundleFloat(string fileName, int id, string typeName)
    {
        return Convert.ToSingle(GetBundleString(fileName, id, typeName));
    }

    public string[] GetBundleStringArray(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFileByBundle(fileName);

        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                string tempStr = o.ToString();
                tempStr = tempStr.Substring(1, tempStr.Length - 2);
                string[] strArray = tempStr.Split(',');
                return strArray;
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
            return null;
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return null;
    }
    public int[] GetBundleIntArray(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFileByBundle(fileName);

        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                string tempStr = o.ToString();
                tempStr = tempStr.Substring(1, tempStr.Length - 2);
                string[] strArray = tempStr.Split(',');
                int[] intArray = new int[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    intArray[i] = int.Parse(strArray[i]);
                }
                return intArray;
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
            return null;
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return null;
    }

    public float[] GetBundleFloatArray(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFileByBundle(fileName);

        if (dic1.ContainsKey(id))
        {
            Dictionary<string, object> dic = dic1[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                string tempStr = o.ToString();
                tempStr = tempStr.Substring(1, tempStr.Length - 2);
                string[] strArray = tempStr.Split(',');
                float[] floatArray = new float[strArray.Length];
                for (int i = 0; i < strArray.Length; i++)
                {
                    floatArray[i] = Convert.ToSingle(strArray[i]);
                }
                return floatArray;
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
            return null;
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return null;
    }

    public Dictionary<string, object> GetBundleOneLine(string fileName,int id)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFileByBundle(fileName);

        if (dic1.ContainsKey(id))
        {
            return dic1[id];
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return null;
    }
    #endregion
    /// <summary>
    /// 添加xml
    /// </summary>
    /// <param name="fileName">xml文件名</param>
    /// <param name="str">xml内容</param>
    public void AddXmlConfig(string fileName, string str)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(str);
        XmlNodeList nodeList = xml.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes;

        int columnCount = 0;
        for (int i = 0; i < nodeList.Count; i++)
        {
            //子对象个数为0表示没有对象 开始索引+1
            if (nodeList[i].ChildNodes.Count == 0)
            {
                columnCount++;
            }
            else
            {
                //该行有数据 即从该行开始读
                break;
            }
        }
        //这三行在每个xml配置表必须存在 否则会报错或者读到的数据不正常
        //beginIndex 是类型名那一行所在的索引  beginIndex-1 是英文类型   beginIndex-2 是中文类型
        int beginIndex = columnCount + 2;
        List<string> typeList = new List<string>();

        for (int i = 0; i < nodeList[beginIndex].ChildNodes.Count; i++)
        {
            if (nodeList[beginIndex].ChildNodes[i].InnerText != null)
            {
                typeList.Add(nodeList[beginIndex].ChildNodes[i].InnerText);
            }
        }

        oneConfigDic = new Dictionary<int, Dictionary<string, object>>();
        for (int i = beginIndex + 1; i < nodeList.Count; i++)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            XmlNodeList tempNodeList = nodeList[i].ChildNodes;
            //tempNodeList.Count==0 表示这一行数据为空(id不连续是会出现该情况 例如：1 2 3 5 7 8)
            if (tempNodeList.Count == 0)
            {
                continue;
            }
            for (int j = 0; j < tempNodeList.Count; j++)
            {
                dic.Add(typeList[j], tempNodeList[j].InnerText);
            }
            oneConfigDic.Add(int.Parse(nodeList[i].ChildNodes[0].InnerText), dic);
        }
        allConfigDic.Add(fileName, oneConfigDic);
    }
}
