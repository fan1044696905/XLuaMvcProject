--UIRoot控制器
require "GameView/UIRootView"

UIRootCtrl = {};
local this = UIRootCtrl;

local root;
local transform;
local gameObject;


function UIRootCtrl.New()
	return this;
end

function UIRootCtrl.Awake()
	
	print('主界面 启动了');
	--克隆UIRoot
	--CS.LuaHelper.Instance.UISceneCtrl:LoadSceneUI("UISceneView/UIRootView",this.OnCreate,this.OnLoadComplete);
	CS.LuaHelper.Instance.UISceneCtrl:LoadSceneUI("UIRootView",this.OnCreate);
	--CS.LuaHelper.Instance.UISceneCtrl:LoadSceneUI(1,"UIRootView",this.OnCreate);
end

--启动事件--
function UIRootCtrl.OnCreate(obj)
	print("进入了回调");
	
	local btnOpenMessage = UIRootView.btnOpenMessage;
	btnOpenMessage.onClick:AddListener(this.OpenMessageClick);
end
function UIRootCtrl.OnLoadComplete(obj)
	print("加载完成");
end
--单击事件--
function UIRootCtrl.OpenMessageClick()
	
	print("点击了打开消息按钮");	
	GameInit.LoadView(CtrlNames.MessageCtrl);
end