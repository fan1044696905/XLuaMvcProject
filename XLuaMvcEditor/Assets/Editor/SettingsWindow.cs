using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



/// <summary>
/// 宏定义工具类
/// </summary>
public class SettingsWindow :EditorWindow
{
    /// <summary>
    /// 宏定义列表
    /// </summary>
    private List<MacorItem> m_List = new List<MacorItem>();

    /// <summary>
    /// 
    /// </summary>
    private Dictionary<string, bool> m_Dic = new Dictionary<string, bool>();

    /// <summary>
    /// 设置PlayerSettings定义的值
    /// </summary>
    private string m_Macor = null;



    void OnEnable()
    {
        m_Macor = PlayerSettings.GetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android);
        m_List.Clear();
        m_List.Add(new MacorItem() { Name = "DEBUG_MODEL", DisplayName = "调试模式", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_LOG", DisplayName = "打印日志", IsDebug = true, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "SATA_TD", DisplayName = "开启统计", IsDebug = false, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "DEBUG_ROLESTATE", DisplayName = "调试角色状态", IsDebug = false, IsRelease = true });
        m_List.Add(new MacorItem() { Name = "DISABLE_ASSETBUNDLE", DisplayName = "禁用AssetBundle", IsDebug = false, IsRelease = false });
        m_List.Add(new MacorItem() { Name = "HOTFIX_ENABLE", DisplayName = "热补丁", IsDebug = false, IsRelease = true });
        for (int i = 0; i < m_List.Count; i++)
        {
            if (!string.IsNullOrEmpty(m_Macor) && m_Macor.IndexOf(m_List[i].Name) != -1)
            {
                m_Dic[m_List[i].Name] = true;
            }
            else
            {
                m_Dic[m_List[i].Name] = false;
            }
        }
    }




    /// <summary>
    /// 绘制UI
    /// </summary>
    void OnGUI()
    {
        for (int i = 0; i < m_List.Count; i++)
        {
            //开启一行
            EditorGUILayout.BeginHorizontal("box");


            m_Dic[m_List[i].Name] = GUILayout.Toggle(m_Dic[m_List[i].Name], m_List[i].DisplayName);
            
            //结束一行
            EditorGUILayout.EndHorizontal();
        }



        //开启一行
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("保存", GUILayout.Width(100)))
        {
            SaveMacor();
        }



        if (GUILayout.Button("调试模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsDebug;
            }
            SaveMacor();
        }

        if (GUILayout.Button("发布模式", GUILayout.Width(100)))
        {
            for (int i = 0; i < m_List.Count; i++)
            {
                m_Dic[m_List[i].Name] = m_List[i].IsRelease;
            }
            SaveMacor();
        }

        //结束一行
        EditorGUILayout.EndHorizontal();


    }


    /// <summary>
    /// 保存宏
    /// </summary>
    private void SaveMacor()
    {
        m_Macor = string.Empty;
        foreach (var item in m_Dic)
        {
            if (item.Value)
            {
                m_Macor += string.Format("{0};", item.Key);
            }

            if (item.Key.Equals("DISABLE_ASSETBUNDLE", System.StringComparison.CurrentCultureIgnoreCase))
            {
                //如果禁用AssetBundle  就是让DownLoad下的场景生效
                EditorBuildSettingsScene[] arrScenes = EditorBuildSettings.scenes;
                for (int i = 0; i < arrScenes.Length; i++)
                {
                    if (arrScenes[i].path.IndexOf("Download", System.StringComparison.CurrentCultureIgnoreCase) > -1)
                    {
                        arrScenes[i].enabled = item.Value;
                    }
                }

                EditorBuildSettings.scenes = arrScenes;
            }
        }

        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iOS, m_Macor);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Standalone, m_Macor);
    }

	
	
}

/// <summary>
/// 宏项目
/// </summary>
public class MacorItem
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 显示的名称
    /// </summary>
    public string DisplayName;

    /// <summary>
    /// 是否调试项
    /// </summary>
    public bool IsDebug;

    /// <summary>
    /// 是否发布项
    /// </summary>
    public bool IsRelease;
}

