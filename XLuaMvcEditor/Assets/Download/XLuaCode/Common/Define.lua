--全局定义
print('启动了Define.lua')

--控制器名称枚举
CtrlNames={
	UIRootCtrl = "UIRootCtrl",
	MessageCtrl = "MessageCtrl"
}

--视图名称枚举
ViewNames={
	"UIRootView",
	"MessageView"
}

DBModelNames = {
	MessageDBModel = "MessageDBModel",
	MessageDBModelExt = "MessageDBModelExt"
}

--这里要把常用的引擎类型都加入进来
WWW = CS.UnityEngine.WWW;
GameObject = CS.UnityEngine.GameObject;
Color = CS.UnityEngine.Color;
Vector3 = CS.UnityEngine.Vector3;
LuaManager = CS.LuaManager.Instance;
AudioManager = CS.LuaHelper.Instance.AudioManager;
AtlasManager = CS.LuaHelper.Instance.AtlasManager;
FrameTimerManager = CS.LuaHelper.Instance.FrameTimerManager;