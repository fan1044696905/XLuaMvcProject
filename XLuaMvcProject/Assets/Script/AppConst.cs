using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppConst  {

    #region ------ 场景名字 ------

    public const string Loading = "Loading";
    public const string LogOn = "LogOn";

    #endregion


    public const string ServerUrl = "http://192.168.1.102:8080/Web/";// 服务器地址(DownloadMgr)
    public const string DownloadPath = "D:/Documents/XLuaMvcProject/XLuaMvcEditor/Assets/";//调试模式下Download路径

#if DEBUG_MODEL
    public const bool DebugMode = true;// 是否开启调试模式
    //调试模式加载XLuaMvcEditor工程的代码
    public const string XLuaCodePath = DownloadPath + "Download/XLuaCode/";
#else
    public const bool DebugMode = false;// 是否开启调试模式
    public const string XLuaCodePath = "Download/XLuaCode/";//非调试模式加载沙盒路径代码
#endif
    
    public const int DownLoadRountineNum = 5;// 下载器的数量(DownloadMgr)
    public const int DownLoadTimeOut = 5;// 超时时间(DownloadMgr)
    public const string VersionFileName = "VersionFile.txt";// 版本文件名称(DownloadMgr)
    public const int OperateAlertCount = 5;//初始化操作警报个数(DownloadMgr)
    public const int AudioSourceCount = 5;//初始化操作警报个数(DownloadMgr)
    public const string OperateAlertParentPath = "Canvas";//操作警报父对象路径
    public const string CloseButtonName = "BtnClose";//Window窗口关闭界面按钮名字
    public const string XLuaCodeTxtPath = "Download/XLuaCodeTxt/";//AssetBundle代码所在的短路径

    public const string XLuaCodeDownloadPath = @"\Download\XLuaCode\";//AssetBundle代码所在的短路径
    public const string UIViewPath = "Download/UIPrefab/UISceneView/{0}.assetbundle";//UIView路径
    public const string UIWindowPath = "Download/UIPrefab/UIWindows/{0}.assetbundle";//UIWindows路径
    public const string UISourcePath = "Download/UISource/{0}.assetbundle";//UISource路径
}
