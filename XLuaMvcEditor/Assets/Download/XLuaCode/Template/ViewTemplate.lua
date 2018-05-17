--视图模板

ViewTemplate = {}
local this = ViewTemplate;

local transform;
local gameObject;

function ViewTemplate.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.InitView();
	print('ViewTemplate awake');
end

--初始化面板--
function ViewTemplate.InitView()
	--查找UI组件

end

function ViewTemplate.start()
	print('ViewTemplate start');
end

function ViewTemplate.update()
end

function ViewTemplate.ondestroy()
end