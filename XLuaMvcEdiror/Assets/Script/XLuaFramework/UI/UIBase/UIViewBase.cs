using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 所以UI视图的基类
/// </summary>
public class UIViewBase : MonoBehaviour
{
    
    /// <summary>
    /// 显示委托
    /// </summary>
    public Action OnShow;


    void Awake()
    {
        OnAwake();

    }

		
	void Start ()
	{
        //获取子对象组件列表 包括隐藏的
        Button[] btnArr = GetComponentsInChildren<Button>(true);
        for (int i = 0; i < btnArr.Length; i++)
        {
            if (btnArr[i].name.Equals(AppConst.CloseButtonName,StringComparison.CurrentCultureIgnoreCase))
            {
                EventTriggerListener.Get(btnArr[i]).onClick += BtnClick;
            }
        }

        OnStart();

        if (OnShow != null) OnShow();

    }


    void OnDestroy()
    {
        BeforeOnDestroy();
    }

    
    /// <summary>
    /// 点击检测
    /// </summary>
    /// <param name="go">点击对象</param>
    private void BtnClick(GameObject go)
    {
        OnBtnClick(go);
    }


    //=========== 虚方法 =================

    protected virtual void OnAwake() { }
    protected virtual void OnStart() { }
    protected virtual void BeforeOnDestroy() { }
    protected virtual void OnBtnClick(GameObject go) { }



}

