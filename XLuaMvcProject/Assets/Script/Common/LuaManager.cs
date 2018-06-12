using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
/// <summary>
/// 读取Lua配置
/// </summary>
public class LuaManager:Singleton<LuaManager> {

    private string m_LuaConfigName = "";

    [CSharpCallLua]
    public delegate void D_SetLuaConfig(string str);
    D_SetLuaConfig setLuaConfig;

    [CSharpCallLua]
    public delegate object D_GetObject(int index,string type);
    D_GetObject getObject;

    [CSharpCallLua]
    public delegate string[] D_GetStringArray(int index, string type);
    D_GetStringArray getStringArray;

    [CSharpCallLua]
    public delegate int[] D_GetIntArray(int index, string type);
    D_GetIntArray getIntArray;

    [CSharpCallLua]
    public delegate float[] D_GetFloatArray(int index, string type);
    D_GetFloatArray getFloatArray;

    [CSharpCallLua]
    public delegate double[] D_GetDoubleArray(int index, string type);
    D_GetDoubleArray getDoubleArray;

    private LuaTable scriptEnv = null;
    private LuaEnv luaEnv;

    public LuaManager()
    {
        XLuaManager.Instance.DoString("require'LuaManager'");//执行Lua LuaManager脚本
    }

    public override void Init()
    {
        luaEnv = XLuaManager.LuaEnv;
        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        setLuaConfig = scriptEnv.GetInPath<D_SetLuaConfig>("LuaManager.SetLuaConfig");
        getObject = scriptEnv.GetInPath<D_GetObject>("LuaManager.GetObject");
        getStringArray = scriptEnv.GetInPath<D_GetStringArray>("LuaManager.GetObject");
        getIntArray = scriptEnv.GetInPath<D_GetIntArray>("LuaManager.GetObject");
        getFloatArray = scriptEnv.GetInPath<D_GetFloatArray>("LuaManager.GetObject");
        getDoubleArray = scriptEnv.GetInPath<D_GetDoubleArray>("LuaManager.GetObject");
    }

    /// <summary>
    /// 设置当前配置名 读一个配置之前必须先调用该函数
    /// </summary>
    /// <param name="configName">配置名</param>
    public void SetLuaConfig(string configName)
    {
        m_LuaConfigName = configName;
        setLuaConfig(configName);
    }

    public object GetObject(int index, string type)
    {
        return getObject(index, type);
    }

    public string GetString(int index, string type)
    {
        return GetObject(index, type).ToString();
    }
    public int GetInt(int index, string type)
    {
        return int.Parse(GetString(index, type));
    }
    public float GetFloat(int index, string type)
    {
        return float.Parse(GetString(index, type));
    }
    public double GetDouble(int index, string type)
    {
        return double.Parse(GetString(index, type));
    }

    public string[] GetStringArray(int index, string type)
    {
        if (getStringArray(index,type) == null)
        {
            Debuger.LogError("can't find index:" + index + " or type:" + type + " by the config:" + m_LuaConfigName);
        }
        return getStringArray(index, type);
    }
    public int[] GetIntArray(int index, string type)
    {
        if (getIntArray(index, type) == null)
        {
            Debuger.LogError("can't find index:" + index + " or type:" + type + " by the config:" + m_LuaConfigName);
        }
        return getIntArray(index, type);
    }
    public float[] GetFloatArray(int index, string type)
    {
        if (getFloatArray(index, type) == null)
        {
            Debuger.LogError("can't find index:" + index + " or type:" + type + " by the config:" + m_LuaConfigName);
        }
        return getFloatArray(index, type);
    }
    public double[] GetDoubleArray(int index, string type)
    {
        if (getDoubleArray(index, type) == null)
        {
            Debuger.LogError("can't find index:" + index + " or type:" + type + " by the config:" + m_LuaConfigName);
        }
        return getDoubleArray(index, type);
    }

}

