require "Data/Create/MessageEntity"

MessageDBModel = {};
local this = MessageDBModel;

local taskTable = {}; --定义表格

function MessageDBModel.New()
	return this;
end

function MessageDBModel.Init()
	print("-- MessageDBModel.Init --")
	
	--这里从c#代码中获取一个数据数组
	local gameDataTable = CS.LuaHelper.Instance:GetData("Task.data")
	--表格的前三行是表头，所以去数据的时候，要从3开始
	for i=3, gameDataTable.Row-1, 1 do
		taskTable[#taskTable+1] = MessageEntity.New(tonumber(gameDataTable.Data[i][0]),gameDataTable.Data[i][1],tonumber(gameDataTable.Data[i][2]),gameDataTable.Data[i][3])
	end

end

function MessageDBModel.GetList()
	return taskTable;
end