using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


/// <summary>
/// lua助手
/// </summary>
[LuaCallCSharp]
public class LuaHelper : Singleton<LuaHelper>
{
    /// <summary>
    /// UIRoot管理
    /// </summary>
    public UISceneCtrl UISceneCtrl
    {
        get { return UISceneCtrl.Instance; }
    }

    /// <summary>
    /// 视图管理
    /// </summary>
    public UIWindowsUtil UIWindowsUtil
    {
        get { return UIWindowsUtil.Instance; }
    }

    /// <summary>
    /// AssetBundleMgr下载 and 加载
    /// </summary>
    public AssetBundleMgr AssetBundleMgr
    {
        get { return AssetBundleMgr.Instance; }
    }

    public AtlasManager AtlasManager
    {
        get { return AtlasManager.Instance; }
    }

    public AudioManager AudioManager
    {
        get { return AudioManager.Instance; }
    }
    public ConfigManager ConfigManager
    {
        get { return ConfigManager.Instance; }
    }
    public FrameTimerManager FrameTimerManager
    {
        get { return FrameTimerManager.Instance; }
    }
    /// <summary>
    /// 读取数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public GameDataTableToLua GetData(string path)
    {
        GameDataTableToLua data = new GameDataTableToLua();

#if DISABLE_ASSETBUNDLE
        path = Application.dataPath + "/Download/DataTable/" + path;
#else
        path = Application.persistentDataPath + "/Download/DataTable/" + path;
#endif

        using (GameDataTableParser parse = new GameDataTableParser(path))
        {
            data.Row = parse.Row;
            data.Column = parse.Column;

            //实例化交叉数组
            data.Data = new string[data.Row][];

            //这里相当于把二维数组转换成交叉数组
            for (int i = 0; i < data.Row; i++)
            {
                string[] arr = new string[data.Column];
                for (int j = 0; j < data.Column; j++)
                {
                    arr[j] = parse.GameData[i, j];
                }
                data.Data[i] = arr;
            }
        }

        return data;
    }


    /// <summary>
    /// 创建一个MemoryStream
    /// </summary>
    /// <returns></returns>
    public MMO_MemoryStream CreateMemoryStream()
    {
        return new MMO_MemoryStream();
    }

    /// <summary>
    /// 创建带有buffer的MemoryStream
    /// </summary>
    /// <param name="buffer"></param>
    /// <returns></returns>
    public MMO_MemoryStream CreateMemoryStream(byte[] buffer)
    {
        return new MMO_MemoryStream(buffer);
    }

    /// <summary>
    /// 发送协议
    /// </summary>
    /// <param name="buffer"></param>
    public void SendProto(byte[] buffer)
    {
        NetWorkSocket.Instance.SendMsg(buffer);     
    }


    /// <summary>
    /// 添加Lua的监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="callBack"></param>
    public void AddEventListtener(ushort protoCode, SocketDispatcher.OnActionHandler callBack)
    {
        SocketDispatcher.Instance.AddEventListener(protoCode, callBack);
    }

    /// <summary>
    /// 移除Lua监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="callBack"></param>
    public void RemoveEventListener(ushort protoCode, SocketDispatcher.OnActionHandler callBack)
    {
        SocketDispatcher.Instance.RemoveEventListener(protoCode, callBack);
    }


    #region LoadLuaView

    [CSharpCallLua]
    public delegate void delLuaLoadView(string ctrlName);
    LuaHelper.delLuaLoadView luaLoadView;

    private LuaTable scriptEnv;
    private LuaEnv luaEnv;

    /// <summary>
    /// 调用lua中的view
    /// </summary>
    /// <param name="ctrName">控制器名称</param>
    public void LoadLuaView(string ctrName)
    {
        luaEnv = XLuaManager.LuaEnv;
        if (luaEnv == null) return;

        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        luaLoadView = scriptEnv.GetInPath<LuaHelper.delLuaLoadView>("GameInit.LoadView");
        if (luaLoadView != null)
        {
            luaLoadView(ctrName);
        }

        scriptEnv = null;
    }

    #endregion


    #region 自动加载图片

    /// <summary>
    /// 
    /// </summary>
    /// <param name="go"></param>
    /// <param name="imgPath"></param>
    /// <param name="imgName"></param>
    public void AutoLoadTexture(GameObject go, string imgPath, string imgName)
    {
        AutoLoadTexture component = go.GetOrAddComponent<AutoLoadTexture>();
        if (component != null)
        {
            component.ImgPath = imgPath;
            component.ImgName = imgName;
        }
    }

    #endregion


    public List<string> luaList = new List<string>();
    /// <summary>
    /// 预加载Lua脚本
    /// </summary>
    /// <param name="codeName">脚本名(Download/XLuaCode路径下，如果还有子目录，需要加上目录名,例：Common\Define)</param>
    public void AddPreload(string codeName)
    {
        luaList.Add(codeName);
    }
}
