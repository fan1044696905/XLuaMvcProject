

LuaHelper = CS.LuaHelper.Instance;
function AddPreload()
    LuaHelper:AddPreload("Main");
    LuaHelper:AddPreload("GameInit");
    LuaHelper:AddPreload("CtrlManager");
    LuaHelper:AddPreload("Core");

    LuaHelper:AddPreload("Common/Define");

    LuaHelper:AddPreload("Data/Create/MessageDBModel");
    LuaHelper:AddPreload("Data/Create/MessageEntity");
    LuaHelper:AddPreload("Data/DBModelMgr");
    LuaHelper:AddPreload("Data/MessageDBModelExt");

    LuaHelper:AddPreload("GameCtrl/MessageCtrl");
    LuaHelper:AddPreload("GameCtrl/UIRootCtrl");

    LuaHelper:AddPreload("GameView/MessageView");
    LuaHelper:AddPreload("GameView/UIRootView");

    LuaHelper:AddPreload("Proto/Message_SearchTaskProto");
    LuaHelper:AddPreload("Proto/Message_SearchTaskReyurnProto");

    LuaHelper:AddPreload("Template/CtrlTemplate");
    LuaHelper:AddPreload("Template/ViewTemplate");
end