using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 初始化场景UI
/// </summary>
public class UISceneInitView : MonoBehaviour {

    [SerializeField]
    private Text txt_Load;

    [SerializeField]
    private Slider slider_Load;

    private Image bg;

    public static UISceneInitView Instance;

    private Text valueText;
    void Awake()
    {
        Instance = this;
        bg = transform.GetComponent<Image>("Image");
        valueText = transform.GetComponent<Text>("Slider_Load/ValueText");
#if DISABLE_ASSETBUNDLE
        
#else

#endif
    }


    /// <summary>
    /// 设置进度条的值
    /// </summary>
    /// <param name="text"></param>
    /// <param name="value"></param>
    public void SetProgress(string text, float value)
    {
        txt_Load.text = text;
        slider_Load.value = value;
        valueText.text = (value * 100).ToString("0.00") + "%";
        if (value==1)
        {
            valueText.text = "100%";
        }
    }
}
