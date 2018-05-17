using System;
using UnityEngine;
using UnityEngine.UI;
public static class MonoBehaviourExtended
{
    #region Get Or Add Component By Path
    /// <summary>
    /// 通过路径获取或添加组件(可用于父对象通过路径获取子对象组件)
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="go"></param>
    /// <param name="path">路径(为空表示操作自身，否则表示操作子对象)</param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject go, string path = "") where T : Component
    {
        Transform t;
        if (string.IsNullOrEmpty(path))
        {
            t = go.transform;
        }
        else
        {
            t = go.transform.Find(path);
        }
        if (t == null)
        {
            Debuger.LogError(go.name + " not Find GameObject at Path: " + path);
            return null;
        }
        T ret = t.gameObject.GetComponent<T>();
        if (ret == null)
        {
            ret = t.gameObject.AddComponent<T>();
        }
        return ret;
    }

    public static T GetOrAddComponent<T>(this Transform trans, string path = "") where T : Component
    {
        return trans.gameObject.GetOrAddComponent<T>(path);
    }

    public static T GetOrAddComponent<T>(this MonoBehaviour mono, string path = "") where T : Component
    {
        return mono.gameObject.GetOrAddComponent<T>(path);
    }
    #endregion

    #region Get Component By Path

    /// <summary>
    /// 通过路径获取组件(父对象获取子对象)
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="trans">父对象Treansform</param>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static T GetComponent<T>(this Transform trans, string path) where T : Component
    {
        Transform temp = trans;
        if (string.IsNullOrEmpty(path) == false)
        {
            temp = trans.Find(path);
            if (temp == null)
            {
                Debuger.LogError(trans.name + " not Find Transform at Path: " + path);
                return null;
            }
        }
        return temp.GetComponent<T>();
    }
    /// <summary>
    /// 通过路径获取组件(父对象获取子对象)
    /// </summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="go">父对象</param>
    /// <param name="path">路径</param>
    /// <returns></returns>
    public static T GetComponent<T>(this GameObject go, string path) where T : Component
    {
        return go.transform.GetComponent<T>(path);
    }
    #endregion

    #region ------ Button Extended ------
    public static void AddListener(this Button btn, Action action)
    {
        btn.onClick.AddListener(delegate 
        {
            action();
        });
    }
    #endregion

    #region ------ Set Image Sprite ------

    public static void SetAtlasSprite(this Image image,string atlasPath,string name)
    {
        Sprite sp = AssetBundleMgr.Instance.LoadAtlasSprite(atlasPath, name);
        image.sprite = sp;
    }
    public static void SetBgSprite(this Image image, string bgName)
    {
        Sprite sp = AssetBundleMgr.Instance.LoadBgSprite(bgName);
        image.sprite = sp;
    }

    #endregion
}

