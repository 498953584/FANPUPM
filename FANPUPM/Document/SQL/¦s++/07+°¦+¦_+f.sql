DELETE FROM plus_progress_privilege
GO
DELETE FROM plus_BackProject
GO
DELETE FROM Plus_task
GO
DELETE FROM plus_progress_version
GO
DELETE FROM plus_progress
GO
--清空关于进度的提醒信息
DELETE FROM PT_Warning WHERE RelationsTable='plus_progress_version' OR
WarningTitle LIKE '%进度预警%' AND WarningContent LIKE '%实际日期与计划日期不符%'