using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
/// <summary>
/// AssetBundle窗口管理
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    /// <summary>
    /// AssetBundle 数据读取类
    /// </summary>
    private AssetBundleDAL dal;

    /// <summary>
    /// AssetBundle数据实体
    /// </summary>
    private List<AssetBundleEntity> m_Lst;

    /// <summary>
    /// 是否选中
    /// </summary>
    private Dictionary<string, bool> m_Dic;

    /// <summary>
    /// 资源标签
    /// </summary>
    private string[] arrTag = { "All", "Scene", "Role", "Effect", "Audio", "UI","XLuaCode","Prefab","Config", "None" };

    /// <summary>
    /// 标记的索引
    /// </summary>
    private int tagIndex = 0;

    /// <summary>
    /// 选中的标记的索引
    /// </summary>
    private int selectTagIndex = -1;

    /// <summary>
    /// 平台
    /// </summary>
    private string[] arrBuilTarget = { "Windows", "Android", "iOS" };

    /// <summary>
    /// 选中的打包平台索引
    /// </summary>
    private int selectBuildTargetIndex = -1;
    
    private Vector2 pos;
    
#if UNITY_STANDALONE_WIN

    /// <summary>
    /// 平台
    /// </summary>
    private BuildTarget target = BuildTarget.StandaloneWindows;

    /// <summary>
    /// 索引
    /// </summary>
    private int buildTargetIndex = 0;

#elif UNITY_ANDROID

    /// <summary>
    /// 平台
    /// </summary>
    private BuildTarget target = BuildTarget.Android;

    /// <summary>
    /// 索引
    /// </summary>
    private int buildTargetIndex = 1;

#elif UNITY_IPHONE

    /// <summary>
    /// 平台
    /// </summary>
    private BuildTarget target = BuildTarget.iOS;

    /// <summary>
    /// 索引
    /// </summary>
    private int buildTargetIndex = 2;

#endif




    void OnEnable()
    {
       
        //Xml表路径
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";

        //实例化Xml数据管理类
        dal = new AssetBundleDAL(xmlPath);

        //获取Xml数据
        m_Lst = dal.GetList();

        m_Dic = new Dictionary<string, bool>();

        for (int i = 0; i < m_Lst.Count; i++)
        {
            m_Dic[m_Lst[i].Key] = true;
        }
    }
    
    /// <summary>
    /// 绘制窗口
    /// </summary>
    void OnGUI()
    {

        if (m_Lst == null) return;

        #region 按钮行
        GUILayout.BeginHorizontal("box");

        selectTagIndex = EditorGUILayout.Popup(tagIndex, arrTag, GUILayout.Width(100));
        if (selectTagIndex != tagIndex)
        {
            tagIndex = selectTagIndex;
            EditorApplication.delayCall = OnSelectTagCallBack;
        }
        
        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuilTarget, GUILayout.Width(100));
        if (selectBuildTargetIndex != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }
        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }
        if (GUILayout.Button("保存设置", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnSaveAssetBundleCallBack;
        }
        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }
        if (GUILayout.Button("拷贝数据表", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCopyDataTableCallBack;
        }
        if (GUILayout.Button("生成版本文件", GUILayout.Width(150)))
        {
            EditorApplication.delayCall = OnCreateVersionTextCallBack;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();

        #endregion
        
        #region Lodbel文字行

        GUILayout.BeginHorizontal("box");

        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("文件夹", GUILayout.Width(200));
        GUILayout.Label("初始资源", GUILayout.Width(200));

        GUILayout.EndHorizontal();

        #endregion
        
        GUILayout.BeginVertical();

        pos= EditorGUILayout.BeginScrollView(pos);

        for (int i = 0; i < m_Lst.Count; i++)
        {
            AssetBundleEntity entity = m_Lst[i];
            GUILayout.BeginHorizontal("box");

            m_Dic[entity.Key] = GUILayout.Toggle(m_Dic[entity.Key], "", GUILayout.Width(20));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(200));

            GUILayout.EndHorizontal();


            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");

                GUILayout.Space(40);
                GUILayout.Label(path);

                GUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();

        GUILayout.EndVertical();
    }

    /// <summary>
    /// 选定标签 回调
    /// </summary>
    private void OnSelectTagCallBack()
    {
        switch (tagIndex)
        {
            case 0:  //全选
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = true;
                }
                break;

            case 1:  //场景 Scene
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 2:  //角色 Role
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 3:  //特效 Effect
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 4:  //声音 Audio
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 5:  //图片 UI
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("UI", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 6:  //XLua代码 XLuaCode
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("XLuaCode", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 7:  //预制体 Prefab
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Prefab", StringComparison.CurrentCultureIgnoreCase);
                }
                break;
            case 8:  //预制体 Prefab
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = entity.Tag.Equals("Config", StringComparison.CurrentCultureIgnoreCase);
                }
                break;

            case 9:  //空 None
                foreach (AssetBundleEntity entity in m_Lst)
                {
                    m_Dic[entity.Key] = false;
                }
                break;
        }
    }
    
    /// <summary>
    /// 选定平台 回调
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0:  //PC平台
                target = BuildTarget.StandaloneWindows64;
                break;
            case 1:  //a安卓平台
                target = BuildTarget.Android;
                break;
            case 2:  //ios平台
                target = BuildTarget.iOS;
                break;
        }
    }
    
    /// <summary>
    /// 保存设置
    /// </summary>
    private void OnSaveAssetBundleCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();
        //便利Xml表所有对象
        foreach (AssetBundleEntity entity in m_Lst)
        {
            if (m_Dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }
        CopyXLuaCodeFolder(lst);
        //循环设置文件夹包括子文件夹里面的项
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];
            if (entity.IsFolder)
            {
                //如果 这个节点配置的是一个文件夹，那么需要遍历文件夹
                //需要把路径变成绝对路径
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSettings(folderArr, !entity.IsChecked);
            }
            else
            {
                //如果不是文件夹，只需要设置里边的项
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], !entity.IsChecked);
                }
                
            }
        }
    }

    private void CopyXLuaCodeFolder(List<AssetBundleEntity> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];
            if (entity.Tag.Equals(AppConst.XLuaCodeFolderTag,StringComparison.CurrentCultureIgnoreCase))
            {
                //可以不写循环 XLuaCode为根路径 所有需要热更的XLua代码都要放在该路径下 entity.PathList[0]
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    string fromPath = Application.dataPath + "/" + entity.PathList[j];
                    string toPath = fromPath+ "Txt";
                    if (Directory.Exists(toPath))
                    {
                        Directory.Delete(toPath, true);
                    }
                    FileUtil.CopyFileOrDirectory(fromPath,toPath);
                    string[] filesPath = Directory.GetFiles(fromPath, "*.lua", SearchOption.AllDirectories);
                    for (int k = 0; k < filesPath.Length; k++)
                    {
                        filesPath[k] = filesPath[k].Replace('/', '\\');
                        string[] splitStrs = { AppConst.XLuaCodeFolderName };
                        string[] tempStrs = filesPath[k].Split(splitStrs, StringSplitOptions.None);
                        File.Copy(filesPath[k],tempStrs[0]+AppConst.XLuaCodeTxtFolderName+tempStrs[1]+".txt",true);
                    }
                    for (int k = 0; k < filesPath.Length; k++)
                    {
                        filesPath[k] = filesPath[k].Replace('/', '\\');
                        string[] splitStrs = { AppConst.XLuaCodeFolderName };
                        string[] tempStrs = filesPath[k].Split(splitStrs, StringSplitOptions.None);
                        File.Delete(tempStrs[0] + AppConst.XLuaCodeTxtFolderName + tempStrs[1]);
                        File.Delete(tempStrs[0] + AppConst.XLuaCodeTxtFolderName + tempStrs[1] + ".meta");
                    }
                    entity.PathList[j] = entity.PathList[j] + "Txt";
                }
            }
        }
        AssetDatabase.Refresh();
    }

    /// <summary>
    /// 保存文件夹设置
    /// </summary>
    /// <param name="folderArr">文件夹数组</param>
    /// <param name="isSetNull"></param>
    private void SaveFolderSettings(string[] folderArr, bool isSetNull)
    {
        foreach (string folderPath in folderArr)
        {
            //1.先看这个文件夹下的文件
            string[] arrFuile = Directory.GetFiles(folderPath);  //文件夹下的文件

            //2.对文件进行设置
            foreach (string filePath in arrFuile)
            {
                //进行设置
                SaveFileSetting(filePath, isSetNull);
            }

            //3.看这个文件下的子文件夹
            string[] arrFolder = Directory.GetDirectories(folderPath);
            SaveFolderSettings(arrFolder, isSetNull);
        }
    }


    private void SaveFileSetting(string filePath, bool isSetNull)
    {
        FileInfo file = new FileInfo(filePath);
        if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
        {
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
            //路径
            string newPath = filePath.Substring(index);

            //文件名
            string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");

            //后缀
            string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity3D" : "assetbundle";


            AssetImporter import = AssetImporter.GetAtPath(newPath);
            import.SetAssetBundleNameAndVariant(fileName, variant);

            if (isSetNull)
            {
                import.SetAssetBundleNameAndVariant(null, null);
            }
            import.SaveAndReimport();
        }
        else
            return;

        Debug.Log("保存设置成功");
    }


    /// <summary>
    /// 打AssetBundle包 回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {

        //打包路径
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex];
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }

        //打包
        BuildPipeline.BuildAssetBundles(toPath, BuildAssetBundleOptions.None, target);

        Debug.Log("打包完毕");
        if (Directory.Exists(Application.dataPath+"/Download/"+AppConst.XLuaCodeTxtFolderName))
        {
            Directory.Delete(Application.dataPath + "/Download/" + AppConst.XLuaCodeTxtFolderName,true);
        }
        AssetDatabase.Refresh();
    }


    /// <summary>
    /// 清空AssetBundle包
    /// </summary>
    private void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex];

        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }

        Debug.Log("清空完毕");
    }


    /// <summary>
    /// 拷贝数据表
    /// </summary>
    private void OnCopyDataTableCallBack()
    {
        string fromPath = Application.dataPath + "/Download/DataTable";
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex] + "/Download/DataTable";
        IOUtil.CopyDirectory(fromPath, toPath);
        Debug.Log("拷贝数据表完毕");
    }


    /// <summary>
    /// 生成版本文件
    /// </summary>
    private void OnCreateVersionTextCallBack()
    {
        string Path = Application.dataPath + "/../AssetBundles/" + arrBuilTarget[buildTargetIndex];

        if (!Directory.Exists(Path))
        {
            Directory.CreateDirectory(Path);
        }

        //版本文件路径
        string strVersionFilePath = Path + "/VersionFile.txt";
        //如果版本文件存在 则删除
        IOUtil.DeleteFile(strVersionFilePath);

        StringBuilder sbContent = new StringBuilder();

        DirectoryInfo directory = new DirectoryInfo(Path);
        //返回文件目录包括子文件夹所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);
        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName;//全名 包含路径扩展名

            //相对路径
            string name = fullName.Substring(fullName.IndexOf(arrBuilTarget[buildTargetIndex]) + arrBuilTarget[buildTargetIndex].Length + 1);
            //获取文件的md5
            string md5 = EncryptUtil.GetFileMD5(fullName);
            if (md5 == null) continue;

            //文件大小
            string size = Math.Ceiling(file.Length / 1024f).ToString();

            //是否初始数据
            bool isFirstData = true;

            //是否跳出循环
            bool isBreak = false;

            for (int j = 0; j < m_Lst.Count; j++)
            {
                foreach (string xmlPath in m_Lst[j].PathList)
                {
                    string tempPtah = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)
                    {
                        tempPtah = xmlPath.Substring(0, xmlPath.IndexOf("."));
                    }

                    if (name.IndexOf(tempPtah, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        isFirstData = m_Lst[j].IsFirstData;
                        isBreak = true;
                        break;
                    }
                }

                if (isBreak) break;
            }

            if (name.IndexOf("DataTable") != -1)
            {
                isFirstData = true;
            }

            string strLine = string.Format("{0} {1} {2} {3}", name, md5, size, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());
        Debug.Log("创建版本文件成功");
    }

}

