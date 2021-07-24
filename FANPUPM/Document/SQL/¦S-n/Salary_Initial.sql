
--给公司表添加初始化数据
INSERT INTO Sam_Formula(FormulaID,FormulaName,Formula,AddDate) VALUES(N'39a037fc-9562-465d-ad01-d3085b2a06c8',N'承包组',N'[DayLaborersPrice]x{ActualDays}','2010-08-02 10:57:40.440')
INSERT INTO Sam_Formula(FormulaID,FormulaName,Formula,AddDate) VALUES(N'7dbd26ea-f6cd-4f89-86a4-369302808a76',N'工人工式',N'([DayLaborersPrice]+[LivingSubsidy])x{ActualDays}','2010-08-02 10:56:41.687')
INSERT INTO Sam_Formula(FormulaID,FormulaName,Formula,AddDate) VALUES(N'd070d58e-1ca5-4fa1-9441-6ee4f037669b',N'管理人员工式',N'([DayLaborersPrice]x{Days})+([PhoneSubsidies]+[LivingSubsidy])x{ActualDays}','2010-08-02 10:55:29.000')
