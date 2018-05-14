using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;


/// <summary>
/// 游戏工具类
/// </summary>
public static class GameObjectUtil
{
    /// <summary>
    /// 设置父对象
    /// </summary>
    /// <param name="obj">需要设置的对象</param>
    /// <param name="Parent">父对象</param>
    public static void SetParent(this GameObject obj, Transform Parent)
    {
        obj.transform.parent = Parent;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.localEulerAngles = Vector3.zero;
    }


    /// <summary>
    /// 设置为空
    /// </summary>
    /// <param name="arr"></param>
    public static void SetNull(this MonoBehaviour[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    /// <summary>
    /// 设置为空
    /// </summary>
    /// <param name="arr"></param>
    public static void SetNull(this Transform[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    /// <summary>
    /// 设置为空
    /// </summary>
    /// <param name="arr"></param>
    public static void SetNull(this Sprite[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }

    /// <summary>
    /// 设置为空
    /// </summary>
    /// <param name="arr"></param>
    public static void SetNull(this GameObject[] arr)
    {
        if (arr != null)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = null;
            }
            arr = null;
        }
    }



    //--------------------- UI扩展 -----------------------------

    /// <summary>
    /// 设置Text的值
    /// </summary>
    /// <param name="textObj"></param>
    /// <param name="text"></param>
    public static void SetText(this Text txtObj, string text, bool isAnimation = false, float duration = 0.2f, ScrambleMode scrambleMode = ScrambleMode.None)
    {
        if (txtObj != null)
        {
            if (isAnimation)
            {
                txtObj.text = "";
                txtObj.DOText(text, duration, scrambleMode: scrambleMode);
            }
            else
            {
                txtObj.text = text;
            }
        }
    }


    /// <summary>
    /// 设置Slider的值
    /// </summary>
    /// <param name="textObj"></param>
    /// <param name="text"></param>
    public static void SetSliderValue(this Slider sliderObj, float value)
    {
        if (sliderObj != null)
        {
            sliderObj.value = value;
        }
    }


    /// <summary>
    /// 设置Image的值
    /// </summary>
    /// <param name="textObj"></param>
    /// <param name="text"></param>
    public static void SetImage(this Image ImageObj, Sprite str)
    {
        if (ImageObj != null)
        {
            ImageObj.overrideSprite = str;
        }
    }


}