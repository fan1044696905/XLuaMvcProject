--UIRoot视图

UIRootView = {}
local this = UIRootView;

local transform;
local gameObject;

function UIRootView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.InitView();
	print('UIRootView awake');
end

--初始化面板--
function UIRootView.InitView()
	this.btnOpenMessage = transform:Find("ContainerBottomRight/btnOpenMessage"):GetComponent("UnityEngine.UI.Button");
	--this.btnJumpScene = transform:Find("ContainerBottomRight/btnJumpScene"):GetComponent("UnityEngine.UI.Button");
	print("初始化面板完毕")
end

function UIRootView.start()
	print('UIRootView start');
end

function UIRootView.update()
end

function UIRootView.ondestroy()
end