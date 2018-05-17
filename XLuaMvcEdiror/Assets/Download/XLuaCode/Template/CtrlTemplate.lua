--控制器模板

CtrlTemplate = {};
local this = CtrlTemplate;

local transform;
local gameObject;


function CtrlTemplate.New()
	return this;
end

function CtrlTemplate.Awake()	
	print('主界面 启动了');


end

--启动事件--
function CtrlTemplate.OnCreate(obj)
	print("进入了回调");
	
end