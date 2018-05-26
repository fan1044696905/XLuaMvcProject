using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 语言工具类
/// </summary>
public class LangUtil {

    private static string language = "Chinese";
    public static string Language
    {
        get
        {
            return language;
        }
        set
        {
            language = value;
            Debug.Log(language);
        }
    }
    private static Dictionary<int, string> langDic = new Dictionary<int, string>();

    private static Dictionary<string, Dictionary<int, string>> language2 = new Dictionary<string, Dictionary<int, string>>();
    private static Dictionary<int, string> languageDic = new Dictionary<int, string>();
    public static string GetValue(int key)
    {
        if (langDic.ContainsKey(key))
        {
            return langDic[key];
        }



        return null;
    }

    /// <summary>
    /// 获取游戏语言
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string GetLanguage(int key)
    {

        return null;
    }
}
