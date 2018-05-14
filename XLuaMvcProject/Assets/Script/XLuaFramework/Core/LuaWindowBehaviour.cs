using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;


[LuaCallCSharp]
public class LuaWindowBehaviour : UIWindowViewBase
{
    [CSharpCallLua]
    public delegate void delLuaAwake(GameObject obj);
    delLuaAwake luaAwake;

    [CSharpCallLua]
    public delegate void delLuaStart();
    delLuaStart luaStart;

    [CSharpCallLua]
    public delegate void delLuaUpdate();
    delLuaUpdate luaUpdate;

    [CSharpCallLua]
    public delegate void delLuaOnDestroy();
    delLuaOnDestroy luaOnDestroy;

    private LuaTable scriptEnv;
    private LuaEnv luaEnv;

    protected override void OnAwake()
    {
        base.OnAwake();
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
        luaAwake = scriptEnv.GetInPath<delLuaAwake>(prefabName + ".awake");
        luaStart = scriptEnv.GetInPath<delLuaStart>(prefabName + ".start");
        luaUpdate = scriptEnv.GetInPath<delLuaUpdate>(prefabName + ".update");
        luaOnDestroy = scriptEnv.GetInPath<delLuaOnDestroy>(prefabName + ".ondestroy");

        scriptEnv.Set("self", this);
        if (luaAwake != null)
        {
            luaAwake(gameObject);
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        if (luaStart != null)
        {
            luaStart();
        }
    }

    protected override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();

        //备注 调用销毁的话，经常会造成Unity崩溃
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
