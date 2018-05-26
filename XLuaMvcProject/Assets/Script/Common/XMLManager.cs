using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System;
using System.IO;
public class XMLManager : Singleton<XMLManager> {

    private string prefixPath = Application.dataPath + "/Resources/";
    private string suffixPath = ".xml";
    // Use this for initialization
    private Dictionary<string, Dictionary<int, Dictionary<string, object>>> allConfigDic;
    private Dictionary<int, Dictionary<string, object>> oneConfigDic;

    List<string> xmlNameList; 

    public override void Init()
    {
        allConfigDic = new Dictionary<string, Dictionary<int, Dictionary<string, object>>>();
        xmlNameList = new List<string>();
        #region ----- 预加载文件 -----



        #endregion
    }

    private Dictionary<int, Dictionary<string, object>> GetOrAddFile(string fileName)
    {
        if (!allConfigDic.ContainsKey(fileName))
        {
            //Add(fileName);
            GlobalInit.Instance.StartCoroutine(LoadXml(fileName));
        }
        Dictionary<int, Dictionary<string, object>> dic;
        if (allConfigDic.TryGetValue(fileName,out dic))
        {
            return dic;
        }
        return null;
    }
    private void Add(string fileName)
    {
        string path = prefixPath + fileName + suffixPath;
        XmlDocument xml = new XmlDocument();
        try
        {
            //xml.Load(path);
            //xml.Load()
            xml.LoadXml(fileName);
        }
        catch
        {
            Debuger.LogError("路径错误 "+path);
        }
        XmlNodeList nodeList = xml.ChildNodes[2].ChildNodes[4].ChildNodes[0].ChildNodes;
        int columnCount = 0;
        for (int i = 0; i < nodeList.Count; i++)
        {
            if (nodeList[i].ChildNodes.Count == 0)
            {
                columnCount++;
            }
        }
        int beginIndex = columnCount + 2;
        List<string> typeList = new List<string>();
        
        for (int i = 0; i < nodeList[beginIndex].ChildNodes.Count; i++)
        {
            if (nodeList[beginIndex].ChildNodes[i].InnerText!=null)
            {
                typeList.Add(nodeList[beginIndex].ChildNodes[i].InnerText);
            }
        }

        oneConfigDic = new Dictionary<int, Dictionary<string, object>>();
        for (int i = beginIndex+1; i < nodeList.Count; i++)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            XmlNodeList tempNodeList = nodeList[i].ChildNodes;
            for (int j = 0; j < tempNodeList.Count; j++)
            {
                //Debuger.Log(typeList[j] + "     "+ tempNodeList[j].InnerText);
                dic.Add(typeList[j],tempNodeList[j].InnerText);
            }
            oneConfigDic.Add(int.Parse(nodeList[i].ChildNodes[0].InnerText),dic);
        }
        allConfigDic.Add(fileName, oneConfigDic);
    }

    #region ------ Get ------

    public int GetInt(string fileName, int id, string typeName)
    {
        GetOrAddFile(fileName);
        if (oneConfigDic.ContainsKey(id))
        {
            Dictionary<string, object> dic = oneConfigDic[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                return int.Parse(o.ToString());
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return -1;
    }

    public float GetFloat(string fileName, int id, string typeName)
    {
        GetOrAddFile(fileName);
        if (oneConfigDic.ContainsKey(id))
        {
            Dictionary<string, object> dic = oneConfigDic[id];
            if (dic.ContainsKey(typeName))
            {
                object o;
                bool b = dic.TryGetValue(typeName, out o);
                return Convert.ToSingle(o.ToString());
            }
            Debuger.LogError(fileName + " 中不存在字段 " + typeName);
        }
        Debuger.LogError(fileName + " 中不存在id " + id);
        return -1;
    }
    public string GetString(string fileName, int id, string typeName)
    {
        Dictionary<int, Dictionary<string, object>> dic1 = GetOrAddFile(fileName);
        
        //return null;
        if (dic1 == null)
        {
            Debuger.LogError("cant find xml:" + fileName + " by fileDic");
        }
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
        GetOrAddFile(fileName);
        if (oneConfigDic.ContainsKey(id))
        {
            Dictionary<string, object> dic = oneConfigDic[id];
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
        GetOrAddFile(fileName);
        if (oneConfigDic.ContainsKey(id))
        {
            Dictionary<string, object> dic = oneConfigDic[id];
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

    #endregion 
    

    public IEnumerator LoadXml(string fileName)
    {
        string url = string.Format(AppConst.XmlPath, fileName);
        WWW www = new WWW(url);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            string str = www.text;
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
                if (tempNodeList.Count==0)
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
        else
        {
            Debuger.LogError("The url is error:  " + url);
        }
    }



    public IEnumerator ReadXmlTxt()
    {
        WWW www = new WWW(AppConst.XmlTxtPath);
        yield return www;
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            string str = www.text;
            string[] allXmlName = str.Split('\n');
            for (int i = 0; i < allXmlName.Length; i++)
            {
                Debug.Log(allXmlName[i]);
            }
        }
    }
}
