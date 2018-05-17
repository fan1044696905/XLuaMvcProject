--���ݱ���չ
require "Data/Create/MessageEntity"


MessageDBModelExt = {};
local this = MessageDBModelExt;


function MessageDBModelExt.New()
	return this;
end

--����״̬��ȡ����
function MessageDBModelExt.GetListByStatus(status)
	local taskTable = DBModelMgr.GetDBModel(DBModelNames.MessageDBModel).GetList();
	--Ҫ���صı�
	local retTable = {};
	for i=1,#taskTable,1 do
		if(taskTable[i].Status == status) then
			retTable[#retTable+1] = taskTable[i];
		end
	end
	
	return retTable;
end