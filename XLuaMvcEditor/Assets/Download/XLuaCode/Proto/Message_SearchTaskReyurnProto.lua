--服务器返回任务列表信息

--定义任务项
TaskTtem = {Id = 0 , Name = "", Status = 0, Content = ""}
TaskTtem.__index = TaskTtem;

function TaskTtem.New()
	local self = {}; 
	setmetatable(self,TaskTtem);
	return self;
end


--协议主体
Message_SearchTaskReyurnProto = { ProtoCode = 15002 , TaskCount = 0, TaskTable = {} };

-- 这句是重定义元表的索引(就是说有了这句，才是一个类)
Message_SearchTaskReyurnProto.__index  = Message_SearchTaskReyurnProto;

function Message_SearchTaskReyurnProto.New()
	local self = {}; -- 初始化self	
	setmetatable(self, Message_SearchTaskReyurnProto);  -- 将self的元表设定为class
	return self;
end

--发送协议
function Message_SearchTaskReyurnProto.SendProto(proto)
	local ms = CS.LuaHelper:CreateMemoryStream();
	ms:WriteUShort(proto.ProtoCode);
	
	ms:WriteInt(proto.TaskCount);
	for i=1, proto.TaskCount, 1 do
		ms:WriteInt(proto.TaskTable[i].Id);
		ms:WriteUTF8String(proto.TaskTable[i].Name);
		ms:WriteInt(proto.TaskTable[i].Status);
		ms:WriteUTF8String(proto.TaskTable[i].Content);
		
	end
	
	CS.LuaHelper.Instance:SendProto(ms:ToArray());
	ms:Dispose();
end

--解析协议
function Message_SearchTaskReyurnProto.GetProto(buffer)
	local proto = Message_SearchTaskProto.New();
	local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);
	proto.TaskCount = ms:ReadInt();
	for i = 1, proto.TaskCount, 1 do
		local taskItem = TaskTtem.New();
		taskItem.Id = ms:ReadInt();
		taskItem.Name = ms:ReadUTF8String();
		taskItem.Status = ms:ReadInt();
		taskItem.Content = ms:ReadUTF8String();
		
		proto.TaskTable[#proto.TaskTable+1] = taskItem;
	end
	
	ms:Dispose();	
	return proto;
end