print('启动GameInit.lua')
require "CtrlManager"
require "Data/DBModelMgr"

GameInit = {};
local this = GameInit;

--初始化
function GameInit.Init()
	this.InitViews();
	CtrlManager.Init();
	DBModelMgr.Init();
	--GameInit.LoadView(CtrlNames.UIRootCtrl);
end

--初始化视图
function GameInit.InitViews()
	for i=1, #ViewNames do
		require('GameView/'..tostring(ViewNames[i]));
	end
end

--加载视图
function GameInit.LoadView(type)
	
	local ctrl = CtrlManager.GetCtrl(type);
	if ctrl ~= nil then
		ctrl.Awake();
	end
end
