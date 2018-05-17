using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 所有场景UI基类
/// </summary>
public class UISceneViewBase : UIViewBase
{

    /// <summary>
    /// 容器_居中
    /// </summary>
    [SerializeField]
    public Transform Container_Center;

  
    /// <summary>
    /// 加载完毕
    /// </summary>
    public Action OnLoadComplete;

}

