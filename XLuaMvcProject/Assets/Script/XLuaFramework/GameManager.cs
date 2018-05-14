using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/// <summary>
/// xLua入口
/// </summary>
public class GameManager : MonoBehaviour
{

    void Awake()
    {
        //启动的时候 在自身挂上 LuaManager 脚本
        gameObject.AddComponent<LuaManager>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (DelegateDefine.Instance.OnSceneLoadOK != null)
        {
            DelegateDefine.Instance.OnSceneLoadOK();
        }
        //开启调试模式执行本地Lua代码，否则执行服务器代码
//#if DEBUG_MODEL
        LuaManager.Instance.DoString("require'Main'");//执行lua Main脚本
        LuaHelper.Instance.LoadLuaView("UIRootCtrl");//创建窗体
        //#else
        //AssetBundleMgr.Instance.LoadOrDownload<TextAsset>("Download/XLuaCodeTxt/GameInit.lua.assetbundle", "GameInit.lua", (TextAsset ta) =>
        //{
        //    //Debuger.Log(ta.text);
        //    //string filePath = Application.persistentDataPath + @"\Download\XLuaCode2\Common\";
        //    //string fileName = "GameInit.lua";
        //    ////将lua文件下载到本地沙盒路径
        //    //if (!Directory.Exists(filePath))
        //    //{
        //    //    System.IO.FileInfo file = new System.IO.FileInfo(filePath+fileName);
        //    //    file.Directory.Create();
        //    //}
        //    //File.WriteAllText(filePath + fileName, "123");
        //});
        //AssetBundleMgr.Instance.LoadOrDownload<TextAsset>("Download/XLuaCodeTxt/Main.lua.assetbundle", "Main.lua", (TextAsset ta) =>
        //{
        //    //Debuger.Log(ta.text);
        //    //LuaManager.Instance.DoString(ta.text);
        //    //LuaHelper.Instance.LoadLuaView("UIRootCtrl");//创建窗体
        //});
        
        //
//#endif
    }
}

