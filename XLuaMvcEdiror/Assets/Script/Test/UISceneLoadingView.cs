using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISceneLoadingView : UISceneViewBase
{
    /// <summary>
    /// 进度条
    /// </summary>
    [SerializeField]
    private Slider m_Slider;

    /// <summary>
    /// 进度条上的文本
    /// </summary>
    [SerializeField]
    private Text m_TxtProgress;

    protected override void OnStart()
    {
        base.OnStart();
        m_Slider.value = 0;
    }

    /// <summary>
    /// 设置进度条的值
    /// </summary>
    /// <param name="value"></param>
    public void SetProgressValue(float value)
    {
        if (m_Slider == null || m_TxtProgress == null) return;
        m_Slider.value = value;
        m_TxtProgress.text = string.Format("{0}%", (int)(value * 100));
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        m_Slider = null;
        m_TxtProgress = null;
    }

}
