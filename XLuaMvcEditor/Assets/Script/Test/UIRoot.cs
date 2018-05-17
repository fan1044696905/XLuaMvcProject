using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRoot : MonoBehaviour
{
    public static UIRoot Instance;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

}
