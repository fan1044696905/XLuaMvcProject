using System;
using UnityEngine;
using UnityEngine.UI;
using XLua;
[LuaCallCSharp]
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
        return GetOrAddComponent(go, typeof(T), path) as T;
    }

    public static T GetOrAddComponent<T>(this Transform trans, string path = "") where T : Component
    {
        return trans.gameObject.GetOrAddComponent<T>(path);
    }

    public static T GetOrAddComponent<T>(this MonoBehaviour mono, string path = "") where T : Component
    {
        return mono.gameObject.GetOrAddComponent<T>(path);
    }

    public static Component GetOrAddComponent(this GameObject go,System.Type type,string path="")
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
        Component ret = t.gameObject.GetComponent(type);
        if (ret == null)
        {
            ret = t.gameObject.AddComponent(type);
        }
        return ret;
    }
    public static Component GetOrAddComponent(this Transform trans, System.Type type, string path = "")
    {
        return trans.gameObject.GetOrAddComponent(type, path);
    }
    public static Component GetOrAddComponent(this MonoBehaviour mono, System.Type type, string path = "")
    {
        return mono.gameObject.GetOrAddComponent(type, path);
    }

    public static Component GetOrAddComponent(this GameObject go, string typeStr, string path = "")
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
        System.Type type = System.Type.GetType(typeStr);
        Component ret = t.gameObject.GetComponent(type);
        if (ret == null)
        {
            ret = t.gameObject.AddComponent(type);
        }
        return ret;
    }

    public static Component GetOrAddComponent(this Transform trans, string typeStr, string path = "")
    {
        return trans.gameObject.GetOrAddComponent(typeStr, path);
    }
    public static Component GetOrAddComponent(this MonoBehaviour mono, string typeStr, string path = "")
    {
        return mono.gameObject.GetOrAddComponent(typeStr, path);
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

    /// <summary>
    /// 设置图片
    /// </summary>
    /// <param name="image"></param>
    /// <param name="atlasName">图集名字</param>
    /// <param name="spriteName">小图片名</param>
    public static void SetAtlasSprite(this Image image, string atlasName, string spriteName)
    {
        Sprite sp = AtlasManager.Instance.GetAtlasSprite(atlasName, spriteName);
        image.sprite = sp;
    }
    /// <summary>
    /// 设置背景图片(在目录UISource/BackGround目录下)
    /// </summary>
    /// <param name="image"></param>
    /// <param name="bgName">背景图片名</param>
    public static void SetBgSprite(this Image image, string bgName)
    {
        Sprite sp = AssetBundleMgr.Instance.LoadBgSprite(string.Format(AppConst.UIBgPath, bgName), bgName);
        image.sprite = sp;
    }

    /// <summary>
    /// 设置背景图片(在目录UISource/BackGround/XXX目录下)
    /// </summary>
    /// <param name="image"></param>
    /// <param name="bgPath">背景图片路径</param>
    /// <param name="bgName">背景图片名</param>
    public static void SetBgSprite(this Image image, string bgPath,string bgName)
    {
        Sprite sp = AssetBundleMgr.Instance.LoadBgSprite(string.Format(AppConst.UIBgPath, bgPath), bgName);
        image.sprite = sp;
    }

    #endregion
}

