--向服务器请求 查询任务列表协议

Message_SearchTaskProto = { ProtoCode = 15001 };

-- 这句是重定义元表的索引(就是说有了这句，才是一个类)
Message_SearchTaskProto.__index  = Message_SearchTaskProto;

function Message_SearchTaskProto.New()
	local self = {}; -- 初始化self	
	setmetatable(self, Message_SearchTaskProto);  -- 将self的元表设定为class
		
	return self;
end

--发送协议
function Message_SearchTaskProto.SendProto(proto)
	local ms = CS.LuaHelper.Instance:CreateMemoryStream();
	ms:WriteUShort(proto.ProtoCode);
	CS.LuaHelper.Instance:SendProto(ms:ToArray());
	ms:Dispose();
end

--解析协议
function Message_SearchTaskProto.GetProto(buffer)
	local proto = Message_SearchTaskProto.New();
	local ms = CS.LuaHelper.Instance:CreateMemoryStream(buffer);
	ms:Dispose();
	
	return proto;
end