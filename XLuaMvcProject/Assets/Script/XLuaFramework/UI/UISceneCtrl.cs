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
    #region SceneUIType 场景UI类型

    /// <summary>
    /// 场景UI类型
    /// </summary>
    public enum SceneUIType
    {
        /// <summary>
        /// 未定义
        /// </summary>
        None=0,
        /// <summary>
        /// 登录
        /// </summary>
        LogOn,
        /// <summary>
        /// 加载
        /// </summary>
        Loading,
        /// <summary>
        /// 选人场景
        /// </summary>
        SelectRole,
        /// <summary>
        /// 主城
        /// </summary>
        MainCity,
    }
    #endregion


    /// <summary>
    /// 当前场景UI
    /// </summary>
    public UISceneViewBase CurrentUIScene;


    #region LoadSceneUI 加载场景UI
    public void LoadSceneUI(string path, XLuaCustomExport.OnCreate OnCreate = null, Action<GameObject> OnLoadComplete = null)
    {
        if (UIRoot.Instance == null)
        {
            Debuger.LogError("UIRoot.Instance == null");
            return;
        }
        string newPath = string.Empty;
        //newPath = string.Format("Download/UIPerfab/{0}.assetbundle", path);
        newPath = string.Format("Download/UIPerfab/{0}.assetbundle", path);
        AssetBundleMgr.Instance.LoadOrDownload(newPath, "", (GameObject obj) =>
        {
            obj = UnityEngine.Object.Instantiate(obj, UIRoot.Instance.gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            //CurrentUIScene = obj.GetComponent<UISceneViewBase>();
            if (OnLoadComplete != null)
            {
                OnLoadComplete(obj);
            }

            //此时表示 是从lua中加载的
            if (OnCreate != null)
            {
                obj.GetOrAddComponent<LuaViewBehaviour>();
                OnCreate(obj);
            }
        });
    }

    
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public void LoadSceneUI(int type, string path, XLuaCustomExport.OnCreate OnCreate)
    {
        LoadSceneUI(type, null, OnCreate, path);
    }
    /// <summary>
    /// 加载场景UI
    /// </summary>
    /// <param name="type">场景UI类型</param>
    /// <returns></returns>
    public void LoadSceneUI(int type, Action<GameObject> OnLoadComplete, XLuaCustomExport.OnCreate OnCreate = null, string path = null)
    {
        if (UIRoot.Instance == null) return;
        string strUIName = string.Empty;
        string newPath = string.Empty;

        switch (type)
        {
            case 1:
                strUIName = "UIRootView";
                break;                
        }
        newPath = string.Format("Download/UIPerfab/UISceneView/{0}.assetbundle", strUIName);

        //if (type != SceneUIType.None)
        //{
        //    switch (type)
        //    {
        //        case SceneUIType.LogOn:
        //            strUIName = "UI_Root_LogOn";
        //            break;
        //        case SceneUIType.Loading:
        //            break;
        //        case SceneUIType.SelectRole:
        //            strUIName = "UI_Root_SelectRole";
        //            break;
        //        case SceneUIType.MainCity:
        //            strUIName = "UI_Root_MainCity";
        //            break;
        //    }

        //    newPath = string.Format("Download/Prefab/UI/UIPrefab/UIScene/{0}.assetbundle", strUIName);
        //}
        //else
        //{
        //    newPath = path;
        //}


        AssetBundleMgr.Instance.LoadOrDownload(newPath, strUIName, (GameObject obj) =>
        {
            obj = UnityEngine.Object.Instantiate(obj);
           
            //obj.transform.parent= UIRoot.Instance.gameObject.transform;
            obj.transform.SetParent(UIRoot.Instance.gameObject.transform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;

            //CurrentUIScene = obj.GetComponent<UISceneViewBase>();
            if (OnLoadComplete != null)
            {
                OnLoadComplete(obj);
            }

            //此时表示 是从lua中加载的
            if (OnCreate != null)
            {               
                obj.GetOrAddComponent<LuaViewBehaviour>();
                OnCreate(obj);
            }
        });
    }
    #endregion


}

