
MessageView = {}
local this = MessageView;

local transform;
local gameObject;

function MessageView.awake(obj)
	gameObject = obj;
	transform = obj.transform;
	
	this.InitView();
	print('MessageView awake');
end

--初始化面板--
function MessageView.InitView()
	
	--查找UI组件
	--容器
	this.content = transform:Find("BackGround/ScrollView/Viewport/Content");
	this.detailContainer = transform:Find("BackGround/detailContainer");
	this.detailContainer.gameObject:SetActive(false);
	--Text
	this.txtTaskId = transform:Find("BackGround/detailContainer/txtTaskId"):GetComponent("UnityEngine.UI.Text");
	this.txtTaskName = transform:Find("BackGround/detailContainer/txtTaskName"):GetComponent("UnityEngine.UI.Text");
	this.txtTaskStatus = transform:Find("BackGround/detailContainer/txtTaskStatus"):GetComponent("UnityEngine.UI.Text");
	this.txtTaskContent = transform:Find("BackGround/detailContainer/txtTaskContent"):GetComponent("UnityEngine.UI.Text");

end

function MessageView.start()
	print("MessageView.start")
	MessageCtrl.OnStart();
end

function MessageView.update()
end

function MessageView.ondestroy()
end