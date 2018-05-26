using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConst  {

    #region ------ 场景名字 ------

    public const string Loading = "Loading";
    public const string LogOn = "LogOn";

    #endregion

#if DEBUG_MODEL
    public const bool DebugMode = true;// 是否开启调试模式
#else
    public const bool DebugMode = false;// 是否开启调试模式
#endif
    public const string ServerUrl = "http://192.168.1.105:8080/Web/";// 服务器地址(DownloadMgr)
    public const int DownLoadRountineNum = 5;// 下载器的数量(DownloadMgr)
    public const int DownLoadTimeOut = 5;// 超时时间(DownloadMgr)
    public const string VersionFileName = "VersionFile.txt";// 版本文件名称(DownloadMgr)
    public const int OperateAlertCount = 5;//初始化操作警报个数(DownloadMgr)
    public const string OperateAlertParentPath = "Canvas";//操作警报父对象路径
    public const string XLuaCodeFolderTag = "XLuaCode";//XLua代码标签
    public const string XLuaCodeFolderName = "XLuaCode";//XLua文件夹名字  路径：Application.dataPath+"/Download/XLuaCode"
    public const string XLuaCodeTxtFolderName = "XLuaCodeTxt";//拷贝的XLua代码文件夹名(文件格式为txt，标记AB创建，打包AB删除)
    public const string CloseButtonName = "BtnClose";//Window窗口关闭界面按钮名字
    public const string XLuaCodePath = "Download/XLuaCode/";//代码短路径 Application.dataPath(persistentDataPath) + "/Download/XLuaCode/" +"xxxxxxxxx.lua"
    public const string XLuaCodeTxtPath = "Download/XLuaCodeTxt/";//AssetBundle代码所在的短路径
    public const string XLuaCodeDownloadPath = @"\Download\XLuaCode\";//AssetBundle代码所在的短路径
    public const string UIViewPath = "Download/UIPrefab/UISceneView/{0}.assetbundle";//UIView路径
    public const string UIWindowPath = "Download/UIPrefab/UIWindows/{0}.assetbundle";//UIWindows路径
    public const string UISourcePath = "Download/UISource/{0}.assetbundle";//UISource路径
}
