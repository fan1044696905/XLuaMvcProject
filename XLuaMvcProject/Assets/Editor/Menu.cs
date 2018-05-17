/**********************************************************
*    主题： 自定义菜单工具
*    功能： 添加自定义工具菜单
*    方法： [MenuItem("Tools/Settings")] 添加方法
***********************************************************/
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



/// <summary>
/// 自定义菜单工具
/// </summary>
public class Menu 
{

    /// <summary>
    /// 全局设置
    /// </summary>
    [MenuItem("Tools/全局设置")]
    public static void Settings()
    {
        SettingsWindow win = (SettingsWindow)EditorWindow.GetWindow(typeof(SettingsWindow));
        //SettingsWindow win = EditorWindow.GetWindow<SettingsWindow>();
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }
    
}

