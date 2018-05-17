using UnityEngine;
using System.Collections;
using System;


/// <summary>
/// ������
/// </summary>
/// <typeparam name="T">����</typeparam>
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

    public virtual void Dispose()
    {
        
    }
}