using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// 场景跳转管理
/// </summary>
public class SceneMgr : Singleton<SceneMgr>
{
    /// <summary>
    /// 当前场景类型
    /// </summary>
    public string CurrentSceneType { get; private set; }

    /// <summary>
    /// 当前玩法类型
    /// </summary>
    public E_PlayType E_CurrPlayType
    {
        get;
        private set;
    }


    public SceneMgr()
    {
        //服务器返回角色进入世界地图场景消息
        
    }
    
    /// <summary>
    /// 加载场景
    /// </summary>
    /// <param name="sceneName">场景名字</param>
    public void LoadScene(string sceneName)
    {
        CurrentSceneType = sceneName;
        SceneManager.LoadScene(AppConst.Loading);
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
