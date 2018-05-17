-- MessageEntity 实体类

MessageEntity = {Id = 0 , Title = "", Status = 0 , Content = ""}

-- 这句是重定义元表的索引(就是说有了这句，才是一个类)
MessageEntity.__index  = MessageEntity;

function MessageEntity.New(Id, Title, Status, Content)
	local self = {}; -- 初始化self	
	setmetatable(self, MessageEntity);  -- 将self的元表设定为class
	
	self.Id = Id;
	self.Title = Title;
	self.Status = Status;
	self.Content = Content;
	
	return self;
end