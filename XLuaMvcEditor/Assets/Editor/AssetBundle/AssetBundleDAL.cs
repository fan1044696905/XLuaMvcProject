using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;



/// <summary>
/// AssetBundle配置数据读取
/// </summary>
public class AssetBundleDAL 
{

    /// <summary>
    /// Xml路径
    /// </summary>
    private string m_Path;

    /// <summary>
    /// 数据集合
    /// </summary>
    private List<AssetBundleEntity> m_List = null;




    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path">外部传递的Xml路径</param>
    public AssetBundleDAL(string path)
    {
        m_Path = path;
        m_List = new List<AssetBundleEntity>();
    }



    /// <summary>
    /// 获取Xml数据集合
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        //1.清空数据集合
        m_List.Clear();

        //2.读取Xml 把数据添加到m_List里
        XDocument xDoc = XDocument.Load(m_Path);                               //根据路径读取Xml文件
        XElement root = xDoc.Root;                                             //获取Xml文件根节点
        XElement assetBundleNode = root.Element("AssetBundle");                //获取指定节点

        IEnumerable<XElement> lst = assetBundleNode.Elements("Item");          //

        int index = 0;
        foreach (XElement item in lst)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "Key" + ++index;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.IsFolder = item.Attribute("IsFolder").Value.Equals("True", System.StringComparison.CurrentCultureIgnoreCase);
            entity.IsFirstData = item.Attribute("IsFirstData").Value.Equals("True", System.StringComparison.CurrentCultureIgnoreCase);

            IEnumerable<XElement> pathList = item.Elements("Path");
            foreach (XElement path in pathList)
            {
                entity.PathList.Add(path.Attribute("Value").Value);
            }

            m_List.Add(entity);
        }


        return m_List;
    }





}

