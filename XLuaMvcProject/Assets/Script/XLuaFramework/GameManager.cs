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
        //启动的时候 在自身挂上 XLuaManager 脚本
        gameObject.AddComponent<XLuaManager>();
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (DelegateDefine.Instance.OnSceneLoadOK != null)
        {
            DelegateDefine.Instance.OnSceneLoadOK();
        }
        XLuaManager.Instance.DoString("require'Main'");//执行lua Main脚本
        LuaHelper.Instance.LoadLuaView("UIRootCtrl");//创建窗体
        //LuaHelper.Instance.AudioManager.PlayBgmAudio("tianxiawushuang");
    }
}

