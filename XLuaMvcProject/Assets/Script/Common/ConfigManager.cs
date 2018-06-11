using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager> {

    public bool IsDownloadFinish
    {
        get;
        set;
    }

    public int TotalCount
    {
        get
        {
            return XMLManager.Instance.TotalCount;
        }
    }
    public int TotalCompleteCount
    {
        get
        {
            return XMLManager.Instance.TotalCompleteCount;
        }
    }

    public int DownloadSize
    {
        get
        {
            return XMLManager.Instance.DownloadSize;
        }
    }

    public int TotalSize
    {
        get
        {
            return XMLManager.Instance.TotalSize;
        }
    }

    public void StartDownload()
    {
        XMLManager.Instance.StartDownload();
    }

    #region ------ 读取服务器Xml配置 ------
    public string GetXSString(string fileName,int id,string typeName)
    {
        return XMLManager.Instance.GetString(fileName, id, typeName);
    }
    public int GetXSInt(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetInt(fileName, id, typeName);
    }
    public float GetXSFloat(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetFloat(fileName, id, typeName);
    }
    public string[] GetXSStringArray(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetStringArray(fileName, id, typeName);
    }
    public int[] GetXSIntArray(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetIntArray(fileName, id, typeName);
    }
    public float[] GetXSFloatArray(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetFloatArray(fileName, id, typeName);
    }
    public Dictionary<string, object> GetXSOneLine(string fileName, int id)
    {
        return XMLManager.Instance.GetOneLine(fileName, id);
    }

    #endregion

    #region ------ 读取本地Xml配置 ------
    public string GetXLString(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetBundleString(fileName, id, typeName);
    }
    public int GetXLInt(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetBundleInt(fileName, id, typeName);
    }
    public float GetXLFloat(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetBundleFloat(fileName, id, typeName);
    }
    public string[] GetXLStringArray(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetBundleStringArray(fileName, id, typeName);
    }
    public int[] GetXLIntArray(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetBundleIntArray(fileName, id, typeName);
    }
    public float[] GetXLFloatArray(string fileName, int id, string typeName)
    {
        return XMLManager.Instance.GetBundleFloatArray(fileName, id, typeName);
    }
    public Dictionary<string, object> GetXLOneLine(string fileName, int id)
    {
        return XMLManager.Instance.GetBundleOneLine(fileName, id);
    }
    #endregion

    #region ------ 读取服务器Json配置 ------



    #endregion

    #region ------ 读取本地Json配置 ------



    #endregion

    #region ------ 读取服务器Lua配置 ------



    #endregion

    #region ------ 读取本地Lua配置 ------



    #endregion
}
