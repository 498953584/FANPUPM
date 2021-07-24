--添加是否有效字段
ALTER TABLE WF_Template ADD IsValid bit DEFAULT 1
GO
UPDATE WF_Template SET IsValid = 1 

--添加节点过期后处理方式
ALTER TABLE WF_TemplateNode ADD DueMode VARCHAR(2) DEFAULT 'N'
GO
UPDATE WF_TemplateNode SET DueMode = 'N'
