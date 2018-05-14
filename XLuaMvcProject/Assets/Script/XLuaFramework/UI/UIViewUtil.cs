using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 窗口UI的管理
/// 功能：窗口的打开、动画、销毁管理
/// </summary>
public class UIViewUtil : Singleton<UIViewUtil>
{

    /// <summary>
    /// 存放UI窗口的字典
    /// </summary>
    private Dictionary<string, UIWindowViewBase> m_DicWindow = new Dictionary<string, UIWindowViewBase>();

    /// <summary>
    /// 已经打开的窗口数量
    /// </summary>
    public int OpenWindowCount { get { return m_DicWindow.Count; } }




    /// <summary>
    /// 关闭所有窗口
    /// </summary>
    public void CloseAllWindow()
    {
        if (m_DicWindow != null)
        {
            m_DicWindow.Clear();
        }
    }


    #region LoadWindow 打开窗口
  
    public void LoadWindowForLua(string viewName, XLuaCustomExport.OnCreate OnCreate = null, string path = null)
    {
        LoadWindow(viewName, null, null, OnCreate, path);
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <param name="type">窗口类型</param>
    /// <returns></returns>
    public void LoadWindow(string viewName, Action<GameObject> onComplete, Action OnShow = null, XLuaCustomExport.OnCreate OnCreate = null, string path = null)
    {
        if (UIRoot.Instance == null) return;
        //如果窗口不存在则
        if (!m_DicWindow.ContainsKey(viewName) || m_DicWindow[viewName] == null)
        {
            string newPath = string.Empty;

            if (string.IsNullOrEmpty(path))
            {
                newPath = string.Format("Download/Prefab/UI/UIPrefab/UIWindows/Pan_{0}.assetbundle", viewName);
            }
            else
            {
                newPath = path;
            }
         
            AssetBundleMgr.Instance.LoadOrDownload(newPath, string.Format("Pan_{0}", viewName), (GameObject obj) => 
            {
                obj = UnityEngine.Object.Instantiate(obj);

                UIWindowViewBase windowBase = obj.GetComponent<UIWindowViewBase>();
                if (windowBase == null) return ;

                if (OnShow != null)
                {
                    windowBase.OnShow = OnShow;
                }

                m_DicWindow[viewName] = windowBase;
                windowBase.ViewName = viewName;

                //Transform transParent = null;

                //switch (windowBase.containerType)
                //{
                //    case WindowUIContainerType.Center:
                //        transParent = UISceneCtrl.Instance.CurrentUIScene.Container_Center;
                //        break;
                //}
                
                //obj.transform.parent = UIRoot.Instance.gameObject.transform;
                obj.transform.SetParent(UIRoot.Instance.gameObject.transform);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
                obj.gameObject.SetActive(false);

                //层级管理
                LayerUIMgr.Instance.SetLayer(obj);

                StartShowWindow(windowBase, true);

                if (onComplete != null)
                {
                    onComplete(obj);
                }

                if (OnCreate != null)
                {
                    OnCreate(obj);
                }

            });
        }
        else
        {
            if (onComplete != null)
            {
                GameObject obj = m_DicWindow[viewName].gameObject;
                //层级管理
                LayerUIMgr.Instance.SetLayer(obj);

                onComplete(obj);
            }
        }
    }


    #endregion

    #region CloseWindow 关闭窗口

    /// <summary>
    /// 关闭窗口
    /// </summary>
    /// <param name="viewName">窗口的名称</param>
    public void CloseWindow(string viewName)
    {
        if (m_DicWindow.ContainsKey(viewName))
        {
            StartShowWindow(m_DicWindow[viewName], false);
        }
    }

    #endregion

    #region StartShowWindow 开始打开窗口

    /// <summary>
    /// 开始打开窗口
    /// </summary>
    /// <param name="windowBase">窗口的名称</param>
    /// <param name="isOpen">是否打开</param>
    private void StartShowWindow(UIWindowViewBase windowBase, bool isOpen)
    {
        switch (windowBase.EshowStyle)
        {
            
            case E_WindowShowStyle.Normal:         //正常打开
                ShowNormal(windowBase, isOpen);
                break;
            case E_WindowShowStyle.CenterToBig:    //从中间放大
                ShowCenterToBig(windowBase, isOpen);
                break;            
            case E_WindowShowStyle.FromTop:        //从上往下
                ShowFromDir(windowBase, 0, isOpen);
                break;
            case E_WindowShowStyle.FromDown:       //从下往上
                ShowFromDir(windowBase, 1, isOpen);
                break;
            case E_WindowShowStyle.FromLeft:       //从左向右
                ShowFromDir(windowBase, 2, isOpen);
                break;
            case E_WindowShowStyle.FromRight:      //从右向左
                ShowFromDir(windowBase, 3, isOpen);
                break;

            default:
                break;
        }
    }

    #endregion

    #region 各种打开效果

    
    /// <summary>
    /// 正常打开
    /// </summary>
    /// <param name="windowBase">窗口的名称</param>
    /// <param name="isOpen">是否打开</param>
    private void ShowNormal(UIWindowViewBase windowBase, bool isOpen)
    {
        if (isOpen)
        {
            windowBase.gameObject.SetActive(true);
        }
        else
        {
            DestroyWindow(windowBase);
        }
    }



    /// <summary>
    /// 中间变大
    /// </summary>
    /// <param name="windowBase">窗口的名称</param>
    /// <param name="isOpen">是否打开</param>
    private void ShowCenterToBig(UIWindowViewBase windowBase, bool isOpen)
    {
        windowBase.gameObject.SetActive(true);                                 //显示UI
        windowBase.transform.localScale = Vector3.zero;                        //设置局部缩放为0
        windowBase.transform.DOScale(Vector3.one, windowBase.duration)         //设置局部缩放动画(最终值，持续时间)
            .SetAutoKill(false)                                                //设置自动销毁为false
            .SetEase(windowBase.Eease)                     //设置动画曲线
            .Pause().OnRewind(() =>                                            //Pause暂停动画,OnRewind反向播放时执行
            {
                DestroyWindow(windowBase);
            });


        if (isOpen)
            windowBase.transform.DOPlayForward();                              //向前播放
        else
            windowBase.transform.DOPlayBackwards();                            //反向播放
    }


    /// <summary>
    /// 从不同的方向加载
    /// </summary>
    /// <param name="windowBase">窗口的名称</param>
    /// <param name="dirType">0=从上 1=从下 2=从左 3=从右</param>
    /// <param name="isOpen">是否打开</param>
    private void ShowFromDir(UIWindowViewBase windowBase, int dirType, bool isOpen)
    {
        windowBase.gameObject.SetActive(true);
        Vector3 from = Vector3.zero;
        switch (dirType)
        {
            case 0:
                from = new Vector3(0, 1000, 0);
                break;
            case 1:
                from = new Vector3(0, -1000, 0);
                break;
            case 2:
                from = new Vector3(-1400, 0, 0);
                break;
            case 3:
                from = new Vector3(1400, 0, 0);
                break;
        }

        windowBase.transform.localPosition = from;                             //获得局部坐标
        windowBase.transform.DOLocalMove(Vector3.zero, windowBase.duration)
            .SetAutoKill(false)
            .SetEase(windowBase.Eease)
            .Pause().OnRewind(() =>
            {
                DestroyWindow(windowBase);
            });

        if (isOpen)
            windowBase.transform.DOPlayForward();
        else
            windowBase.transform.DOPlayBackwards();


    }





    #endregion

    #region DestroyWindow 销毁窗口

    /// <summary>
    /// 销毁窗口
    /// </summary>
    /// <param name="windowBase">窗口名称</param>
    private void DestroyWindow(UIWindowViewBase windowBase)
    {
        m_DicWindow.Remove(windowBase.ViewName);
        UnityEngine.Object.Destroy(windowBase.gameObject);
    }

    #endregion


}

