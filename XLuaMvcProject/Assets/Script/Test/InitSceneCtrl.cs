using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 初始化场景管理
/// </summary>
public class InitSceneCtrl : MonoBehaviour {
    
    void Start()
    {
#if DISABLE_ASSETBUNDLE
        SceneMgr.Instance.LoadScene("LogOn");
#else
        //获取服务器下发的资源服务器地址
        //DownloadMgr.DownLoadBaseUrl = GlobalInit.Instance.CurrChannelInitConfig.SourceUrl;

        //TODO 读取服务器配置

        ConfigManager.Instance.StartDownload();

        //启动协程
        //DownloadMgr.Instance.InitStreamingAssets(OnInitComplete);
#endif
    }

    private void Update()
    {
        if (ConfigManager.Instance.TotalCount>0 && !ConfigManager.Instance.IsDownloadFinish)
        {
            string str = string.Format("正在加载服务器配置{0}/{1}     {2}Kb/{3}Kb", ConfigManager.Instance.TotalCompleteCount, ConfigManager.Instance.TotalCount, ConfigManager.Instance.DownloadSize, ConfigManager.Instance.TotalSize);
            string strProgress = string.Format("加载配置进度={0}", ConfigManager.Instance.DownloadSize / (float)ConfigManager.Instance.TotalSize);

            UISceneInitView.Instance.SetProgress(str, ConfigManager.Instance.DownloadSize / (float)ConfigManager.Instance.TotalSize);
            
            if (ConfigManager.Instance.TotalCompleteCount == ConfigManager.Instance.TotalCount)
            {
                ConfigManager.Instance.IsDownloadFinish = true;
                DownloadMgr.Instance.InitStreamingAssets(OnInitComplete);
            }
        }
    }

    void OnInitComplete()
    {
        //启动登陆场景协程
        StartCoroutine(LoadLogOn());
    }

    


    /// <summary>
    /// 加载登陆场景
    /// </summary>
    /// <returns></returns>
    private IEnumerator LoadLogOn()
    {
        yield return new WaitForSeconds(0.3f);
        SceneMgr.Instance.LoadScene(AppConst.LogOn);
    }
    
}
