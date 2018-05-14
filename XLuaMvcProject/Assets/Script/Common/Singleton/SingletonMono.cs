using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// 继承MonoBehaviour的单例模式
/// </summary>
public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    
    #region 单例

    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject(typeof(T).Name);
                DontDestroyOnLoad(obj);
                instance = obj.GetOrAddComponent<T>();
            }
            return instance;
        }
    }

    #endregion



    void Awake()
    {
        OnAwake();
    }

    void Start()
    {
        OnStart();
    }

    void Update()
    {
        OnUpdate();
    }

    void OnDestroy()
    {
        BeforeOnDestroy();
    }


    /// <summary>
    /// OnAwake子类重写
    /// </summary>
    protected virtual void OnAwake() { }

    /// <summary>
    /// OnStart子类重写
    /// </summary>
    protected virtual void OnStart() { }

    /// <summary>
    /// OnUpdate子类重写
    /// </summary>
    protected virtual void OnUpdate() { }

    /// <summary>
    /// OnDestroy子类重写
    /// </summary>
    protected virtual void BeforeOnDestroy() { }

}

