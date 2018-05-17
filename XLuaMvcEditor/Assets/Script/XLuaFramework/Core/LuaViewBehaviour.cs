using System;
using UnityEngine;
using XLua;


[LuaCallCSharp]
public class LuaViewBehaviour : MonoBehaviour
{
    [CSharpCallLua]
    public delegate void delLuaAwake(GameObject obj);
    LuaViewBehaviour.delLuaAwake luaAwake;

    [CSharpCallLua]
    public delegate void delLuaStart();
    LuaViewBehaviour.delLuaStart luaStart;

    [CSharpCallLua]
    public delegate void delLuaUpdate();
    LuaViewBehaviour.delLuaUpdate luaUpdate;

    [CSharpCallLua]
    public delegate void delLuaOnDestroy();
    LuaViewBehaviour.delLuaOnDestroy luaOnDestroy;

    private LuaTable scriptEnv;
    private LuaEnv luaEnv;


    void Awake()
    {
        luaEnv = LuaManager.LuaEnv; //此处要从LuaManager上获取 全局只有一个

        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        string prefabName = name;
        if (prefabName.Contains("(Clone)"))
        {
            prefabName = prefabName.Split(new string[] { "(Clone)" }, StringSplitOptions.RemoveEmptyEntries)[0];
        }

        prefabName = prefabName.Replace("Pan_", "");

        luaAwake = scriptEnv.GetInPath<LuaViewBehaviour.delLuaAwake>(prefabName + ".awake");
        luaStart = scriptEnv.GetInPath<LuaViewBehaviour.delLuaStart>(prefabName + ".start");
        luaUpdate = scriptEnv.GetInPath<LuaViewBehaviour.delLuaUpdate>(prefabName + ".update");
        luaOnDestroy = scriptEnv.GetInPath<LuaViewBehaviour.delLuaOnDestroy>(prefabName + ".ondestroy");

        scriptEnv.Set("self", this);
        if (luaAwake != null)
        {
            luaAwake(gameObject);
        }
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    void Destroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaUpdate = null;
        luaStart = null;
        luaAwake = null;
    }

}
