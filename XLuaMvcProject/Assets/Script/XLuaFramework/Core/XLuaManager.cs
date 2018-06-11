using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;
/// <summary>
/// xLua环境管理类
/// </summary>
public class XLuaManager : SingletonMono<XLuaManager>
{
    /// <summary>
    /// 全局的xLua引擎
    /// </summary>
    public static LuaEnv LuaEnv;


    protected override void OnAwake()
    {
        base.OnAwake();

        LuaEnv = new LuaEnv();
        LuaEnv.AddLoader(MyLoader);

        //TODO 此处尚需优化 现在是把lua代码又保存到了本地
#if DEBUG_MODEL==false && DISABLE_ASSETBUNDLE==false
        List<string> tempList = LuaHelper.Instance.luaList;
        tempList.Clear();
        AssetBundleMgr.Instance.LoadOrDownloadLuaCode(string.Format("{0}.lua.assetbundle", "Core"), (TextAsset ta) =>
        {
            string filePath = Application.persistentDataPath + AppConst.XLuaCodeDownloadPath;
            System.IO.FileInfo file = new System.IO.FileInfo(filePath + "Core.lua");
            file.Directory.Create();
            File.WriteAllText(filePath +"Core.lua", ta.text);
            //LuaEnv.DoString(ta.text);
        });
        LuaEnv.DoString("require 'Core'");
        LuaFunction fun = LuaEnv.Global.Get<LuaFunction>("AddPreload");
        fun.Call();
        for (int i = 0; i <tempList.Count ; i++)
        {
            AssetBundleMgr.Instance.LoadOrDownloadLuaCode(string.Format("{0}.lua.assetbundle", tempList[i]), (TextAsset ta) => 
            {
                string filePath = Application.persistentDataPath + AppConst.XLuaCodeDownloadPath;
                System.IO.FileInfo file = new System.IO.FileInfo(filePath + tempList[i]+".lua");
                file.Directory.Create();
                File.WriteAllText(filePath + tempList[i] + ".lua", ta.text);
            });
        }
#endif

        //这里相当于初始化路径 也就是 Application.dataPath 文件夹下 .lua的文件都会被初始化加载
        //LuaEnv.DoString(string.Format("package.path = '{0}/?.lua'", Application.dataPath));
        //LuaEnv.DoString(string.Format("package.path = '{0}/?.lua'", Application.persistentDataPath));
    }


    /// <summary>
    /// 执行lua脚本
    /// </summary>
    /// <param name="str"></param>
    public void DoString(string str)
    {
        LuaEnv.DoString(str);      
    }



    protected override void OnUpdate()
    {
        base.OnUpdate();
        //luaEnv.GC(); //时刻回收
    }


    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        //luaEnv.Dispose(); //释放
    }

    public byte[] MyLoader(ref string filePath)
    {
        //调试模式加载本地代码
#if DEBUG_MODEL
        string absPath = AppConst.XLuaCodePath + filePath + ".lua";
#else
        string absPath = Application.persistentDataPath + "/" + AppConst.XLuaCodePath + filePath + ".lua";
#endif
        return System.Text.Encoding.UTF8.GetBytes(File.ReadAllText(absPath));
    }
    

   
    /// <summary>
    /// 执行lua脚本中的方法
    /// </summary>
    /// <param name="fileName">Lua脚本名</param>
    /// <param name="luaFun">方法名</param>
    /// <param name="arr"></param>
    /// <returns></returns>
    public object[] PlayLua(string fileName,string luaFun,params object[] arr)
    {

        return null;
    }

    
}

