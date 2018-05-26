using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 图集管理器
/// </summary>
public class AtlasManager : Singleton<AtlasManager> {

    private Dictionary<string, AtlasItem> atlasDic = new Dictionary<string, AtlasItem>();

    public void AddAtlasItem(string atlasName)
    {
        string fullPath = LocalFileMgr.Instance.LocalFilePath + string.Format(AppConst.UIAtlasPath, atlasName);
        AssetBundle bundle = AssetBundle.LoadFromMemory(LocalFileMgr.Instance.GetBuffer(fullPath));
        AtlasItem item = new AtlasItem(bundle);
        atlasDic.Add(atlasName, item);
    }

    public Sprite GetAtlasSprite(string atlasName,string spriteName)
    {
        if (!atlasDic.ContainsKey(atlasName))
        {
            AddAtlasItem(atlasName);
        }
        AtlasItem item = null;
        if (atlasDic.TryGetValue(atlasName, out item))
        {
            return item.GetSprite(spriteName);
        }
        else
        {
            Debuger.LogError("can not find atlasName:" + atlasName + " by atlasDic");
            return null;
        }
    }

    public override void Dispose()
    {
        atlasDic.Clear();
    }
}

public class AtlasItem
{
    private AssetBundle bundle;
    private int count = -1;
    private UnityEngine.Object[] objs;
    public AtlasItem(AssetBundle assetBundle)
    {
        bundle = assetBundle;
    }

    public Sprite GetSprite(string name)
    {
        //奇怪  用这个api获取不到图集里面的小图片 LoadAllAssets 然后遍历可以获取
        //return bundle.LoadAsset<Sprite>(name);
        if (objs == null)
        {
            objs = bundle.LoadAllAssets();
            Dispose();
        }
        //objs[0] 是Texture2D类型的图集
        for (int i = 1; i < objs.Length; i++)
        {
            if (objs[i].name.Equals(name))
            {
                return objs[i] as Sprite;
            }
        }
        Debuger.LogError("cant find sprite:"+name+" by bundle:"+bundle.name);
        return null;
    }

    public void Dispose()
    {
        bundle.Unload(false);
    }
}

