using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 读取服务器Lua配置
/// </summary>
public class LuaManager:Singleton<LuaManager> {

    Dictionary<string, LuaItem> LuaConfigDic = new Dictionary<string, LuaItem>();
    public override void Init()
    {
        base.Init();
    }
}

public class LuaItem
{
    public string name { get; set; }


}
