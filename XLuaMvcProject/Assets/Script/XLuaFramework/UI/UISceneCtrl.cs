using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CustomSpace;


/// <summary>
/// 场景UI控制器
/// </summary>
public class UISceneCtrl : Singleton<UISceneCtrl>
{
    
    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneViewBase CurrentUIScene;


    /// <summary>
    /// 加载场景UI Lua调用
    /// </summary>
    /// <param name="viewName"></param>
    /// <param name="OnCreate">Lua回调</param>
    public void LoadSceneUI(string viewName, XLuaCustomExport.OnCreate OnCreate)
    {
        LoadSceneUI(viewName, null, OnCreate);
    }
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="viewName">UI名</param>
    /// <param name="OnComplete">C#回调</param>
    /// <param name="OnCreate">Lua回调</param>
    public void LoadSceneUI(string viewName, Action<GameObject> OnComplete, XLuaCustomExport.OnCreate OnCreate = null)
    {
        if (UIRoot.Instance == null) return;
        string newPath = string.Format(AppConst.UIViewPath, viewName);
        
        AssetBundleMgr.Instance.LoadOrDownload(newPath, viewName, (GameObject obj) =>
        {
            obj = UnityEngine.Object.Instantiate(obj, UIRoot.Instance.gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            //此时表示 是从C#中加载的
            if (OnComplete != null)
            {
                OnComplete(obj);
            }
            //此时表示 是从lua中加载的
            if (OnCreate != null)
            {               
                obj.GetOrAddComponent<LuaViewBehaviour>();
                OnCreate(obj);
            }
        });
    }
}

