--[[

lua config manager
所有的lua配置文件均放在AllConfig文件夹下

--]]
Debuger = CS.Debuger;
LuaManager = {};
local self = LuaManager;

self.m_LuaConfigDic = {};
self.m_LuaConfigName = nil;
self.m_LuaTable = {};

function LuaManager.LoadConfig(configName)
	local luaTable = require("AllConfig/"..configName);
	self.m_LuaTable = luaTable;
	self.m_LuaConfigDic[configName] = luaTable;
end

function LuaManager.SetLuaConfig(configName)
	self.m_LuaConfigName = configName;
	if self.m_LuaConfigDic[configName] ~= nil then
		self.m_LuaTable = self.m_LuaConfigDic[configName];
	else
		self.LoadConfig(configName)
	end
end

--获取某个配置中编号(id)为index,类型为type的值
function LuaManager.GetObject(index,type)
	if self.m_LuaTable[index] == nil then
		Debuger.LogError("can‘t find index:"..index.." by the config:"..self.m_LuaConfigName);
		return nil;
	else
		if self.m_LuaTable[index][type] == nil then
			Debuger.LogError("can‘t find type:"..type.." by the config:"..self.m_LuaConfigName);
			return nil;
		end
	end
	return self.m_LuaTable[index][type];
end

--获取一个完整的配置
function LuaManager.GetOneConfig(configName)
	if self.m_LuaConfigDic[configName] ~= nil then
		self.m_LuaTable = self.m_LuaConfigDic[configName];
	else
		self.LoadConfig(configName)
	end
	return self.m_LuaTable;
end