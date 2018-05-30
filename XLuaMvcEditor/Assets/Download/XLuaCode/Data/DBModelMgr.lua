--require "Common/Define"
require "Data/Create/MessageDBModel"
require "Data/MessageDBModelExt"

DBModelMgr = {};
local this = DBModelMgr;

--�������б�
local DBModelList = {};

--��ʼ�����ݹ�����
function DBModelMgr.Init()
	DBModelList[DBModelNames.MessageDBModel] = MessageDBModel.New();
	DBModelList[DBModelNames.MessageDBModel].Init();
	
	DBModelList[DBModelNames.MessageDBModelExt] = MessageDBModelExt.New();
	return this;
end

--���ݿ����������� ��ȡ������
function DBModelMgr.GetDBModel(DBModelName)
	return DBModelList[DBModelName];
end