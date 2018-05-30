--UIRoot控制器
require "GameView/UIRootView"

UIRootCtrl = {};
local self = UIRootCtrl;

local root;
local transform;
local gameObject;


function UIRootCtrl.New()
	return self;
end

function UIRootCtrl.Awake()
	
	print('主界面 启动了');
	CS.LuaHelper.Instance.UISceneCtrl:LoadSceneUI("UIRootView",self.OnCreate);
end

--启动事件--
function UIRootCtrl.OnCreate(obj)
	print("进入了回调");
	local btnOpenMessage = UIRootView.btnOpenMessage;
	btnOpenMessage.onClick:AddListener(self.OpenMessageClick);
end
function UIRootCtrl.OnLoadComplete(obj)
	print("加载完成");
end
--单击事件--
function UIRootCtrl.OpenMessageClick()
	
	print("点击了打开消息按钮");	
	GameInit.LoadView(CtrlNames.MessageCtrl);

	local index = math.random(1,4);
	audioList = {"fight","tianxiawushuang","juejun","jieyangshan"};
	AudioManager:PlayBgmAudio(audioList[index]);
	local image = UIRootView.image;
	
	--image.sprite = AtlasManager:GetAtlasSprite("item1", "200000015");
	
	image:SetAtlasSprite("item1","200000015");

	--image.gameObject:GetOrAddComponent("CS.UnityEngine.Button");
	--image.sprite = AtlasManager:GetAtlasSprite("fishsss", "0");
	--FrameTimerManager:Add("Test",0.2,self.Test,-1);
end
function UIRootCtrl.Test()
	print("发哥666666");
end