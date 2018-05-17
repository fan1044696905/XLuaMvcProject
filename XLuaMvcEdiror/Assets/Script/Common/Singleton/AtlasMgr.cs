using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtlasMgr : Singleton<AtlasMgr> {

    private Dictionary<string, Dictionary<string,Sprite>> atlasDic = new Dictionary<string, Dictionary<string, Sprite>>();

    public Dictionary<string, Dictionary<string, Sprite>> AtlasDic
    {
        get
        {
            return atlasDic;
        }
    }

    public void Add()
    {

    }
}

public class AtlasItem
{
    public string ABPath;
    public string AtlasName;
    public string ItemName;
    public Sprite SP;

}
