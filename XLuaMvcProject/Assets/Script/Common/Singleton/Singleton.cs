using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// µ¥ÀýÀà
/// </summary>
/// <typeparam name="T">·ºÐÍ</typeparam>
public class Singleton<T> where T :class,new()
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    protected Singleton()
    {
        Init();
    }

    public virtual void Dispose()
    {
        
    }

    public virtual void Init() { }
}