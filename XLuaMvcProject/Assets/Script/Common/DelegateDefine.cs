using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 委托定义
/// </summary>
public class DelegateDefine :Singleton<DelegateDefine>
{
    /// <summary>
    /// 场景加载完毕委托
    /// </summary>
    public Action OnSceneLoadOK;
}
