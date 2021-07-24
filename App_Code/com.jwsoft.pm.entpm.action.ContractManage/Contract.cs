namespace com.jwsoft.pm.entpm.action.ContractManage
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.action;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.pm.entpm.Model;
    using System;
    using System.Data;
    using System.Text;

    public class Contract
    {
        public bool Add(ContInfo model)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_Cont_ContInfo", "ContractID")) + 1;
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            builder.Append(string.Concat(new object[] { " update pm_cont_contInfo set isbottom = 0 where contCode like '", model.ContCode.Substring(0, model.ContCode.Length - 3), "%' and contclassId=", model.ContClassID }));
            builder.Append(" insert into pm_Cont_ContInfo(");
            builder.Append("ContractID,ContClassID,ContName,ContNumber,ConstituteDate,GivenDate,FirstParty,SecondParty,ContMoney,CenewContMoney,BeginExecuteDate,EndExecuteDate,SecondPartyType,SecondPartyID,ProjectName,ProjectId,ResidenceCommunity,BuildingNumber,RoomNumber,GroundNumber,AuditState,Remark,GivenTranPerson,UserCode,RecordDate,RelationInfoID,ConClassAttr,contCode,parentidcoll");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(num.ToString() + ",");
            builder.Append(model.ContClassID + ",");
            builder.Append(model.ContName + ",");
            builder.Append(model.ContNumber + ",");
            builder.Append("'" + model.ConstituteDate + "',");
            builder.Append("'" + model.GivenDate + "',");
            builder.Append(model.FirstParty + ",");
            builder.Append(model.SecondParty + ",");
            builder.Append(model.ContMoney + ",");
            builder.Append(model.CenewContMoney + ",");
            builder.Append("'" + model.BeginExecuteDate + "',");
            builder.Append("'" + model.EndExecuteDate + "',");
            builder.Append("'" + model.SecondPartyType + "',");
            builder.Append(model.SecondPartyID + ",");
            builder.Append(model.ProjectName + ",");
            builder.Append(model.ProjectId + ",");
            builder.Append("'" + model.ResidenceCommunity + "',");
            builder.Append("'" + model.BuildingNumber + "',");
            builder.Append("'" + model.RoomNumber + "',");
            builder.Append("'" + model.GroundNumber + "',");
            builder.Append(model.AuditState + ",");
            builder.Append(model.Remark + ",");
            builder.Append("'" + model.GivenTranPerson + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append(model.RelationInfoID + ",");
            builder.Append("'" + model.ConClassAttr + "',");
            builder.Append("'" + model.ContCode + "',");
            builder.Append("'" + model.Parentidcoll + "'");
            builder.Append(")");
            if (model.Parentidcoll != "")
            {
                builder.Append(string.Concat(new object[] { " update pm_cont_contInfo set ChildNum=ChildNum+1 where contCode='", model.ContCode.Substring(0, model.ContCode.Length - 3), "' and contclassId=", model.ContClassID }));
            }
            builder.Append(" update pm_Cont_ContClause set Flag = '1', ContractID =" + num.ToString() + " where ContractID = 0 and Flag = '0' and UserCode ='" + model.UserCode + "'");
            builder.Append(" end ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool addAudiEssentialInfo(string ContractID, string EssentialName, string AdvertProceeding, string Remark)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_ContEssential (ContractID,EssentialName,AdvertProceeding,Remark) values (" + ContractID + "," + EssentialName + "," + AdvertProceeding + "," + Remark + ")");
        }

        public bool addContAffairInfo(string ContractID, string AffairName, string AffairDate, string TransactResult, string DutyPerson)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_Affair (ContractID,  AffairName,  AffairDate,  TransactResult,  DutyPerson) values (" + ContractID + "," + AffairName + ",'" + AffairDate + "'," + TransactResult + "," + DutyPerson + ")");
        }

        public bool addContAlterationInfo(string ContractID, string AlterationDate, string AlterationMoney, string AlterationCause, string Remark, string GivenDate, string GivenTranPerson)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_ContAlteration (ContractID,  AlterationDate,  AlterationMoney,  AlterationCause,  Remark,GivenDate,GivenTranPerson) values (" + ContractID + ",'" + AlterationDate + "'," + AlterationMoney + "," + AlterationCause + "," + Remark + ",'" + GivenDate + "'," + GivenTranPerson + ")");
        }

        public bool AddContMoney(string contractid, string money, string sdate, string remark)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_PaymentPlan(contractid,pstime,paymentmoney,remark) values('" + contractid + "','" + sdate + "'," + money + ",'" + remark + "')");
        }

        public bool addContWbs(string contractid, string taskid)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_cont_wbs(contractid,taskid) values('" + contractid + "','" + taskid + "')");
        }

        public bool AddMateExamResult(string MaterialRecordID, string ExamineResult, string Remark, string QCContent, string QCStandard, string QCBound)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_MateExamResult(MaterialRecordID,ExamineResult,Remark,QCContent,QCStandard,QCBound)  values (" + MaterialRecordID + "," + ExamineResult + "," + Remark + "," + QCContent + "," + QCStandard + "," + QCBound + ")");
        }

        public bool AddMaterialArrive(string MaterialRecordID, string ArriveTime, string Buyer, string ArriveScalar, string ProviderId)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_Cont_MaterialArrive", "MaterialArriveID")) + 1;
            string str2 = " begin";
            string str3 = str2 + " insert into pm_Cont_MaterialArrive (MaterialArriveID,MaterialRecordID,ArriveTime,Buyer,ArriveScalar,ProviderId)  values (" + num.ToString() + "," + MaterialRecordID + ",'" + ArriveTime + "'," + Buyer + "," + ArriveScalar + "," + ProviderId + ")";
            return publicDbOpClass.NonQuerySqlString((str3 + " insert into pm_Cont_MaterialExamine (MaterialArriveID,QCContent,QCStandard,QCBound)  select " + num.ToString() + ", QCContent,QCStandard,QCBound from pm_Cont_MateExamResult  where MaterialRecordID =" + MaterialRecordID) + " end");
        }

        public bool AddMaterialExamine(string MaterialArriveID, string ExamineResult, string Remark, string QCContent, string QCStandard, string QCBound)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_MaterialExamine(MaterialArriveID,ExamineResult,Remark,QCContent,QCStandard,QCBound)  values (" + MaterialArriveID + "," + ExamineResult + "," + Remark + "," + QCContent + "," + QCStandard + "," + QCBound + ")");
        }

        public bool addPaymentCycInfo(string ContractID, string FundName, string TaskId, string SpaceDay, string PayCondition2, string PaymentScale, string Remark)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_PaymentCyc (ContractID,FundName,TaskId,SpaceDay,PayCondition2,PaymentScale,Remark) values (" + ContractID + "," + FundName + "," + TaskId + "," + SpaceDay + ",'" + PayCondition2 + "'," + PaymentScale + "," + Remark + ")");
        }

        public bool addPaymentNoteInfo(string ContractID, string PaymentTime, string PaymentMoney, string Transactor, string Remark, string TaskId)
        {
            return publicDbOpClass.NonQuerySqlString("insert into pm_Cont_PaymentNote (ContractID,  PaymentTime,  PaymentMoney,  Transactor,  Remark,TaskId) values (" + ContractID + ",'" + PaymentTime + "'," + PaymentMoney + "," + Transactor + "," + Remark + "," + TaskId + ")");
        }

        public bool addTempContClause(string OP, string ClauseItemIDs, string ContractID, string UserCode)
        {
            string sqlString = "";
            string str2 = OP.ToLower();
            if (str2 != null)
            {
                if (!(str2 == "add"))
                {
                    if (str2 == "upd")
                    {
                        sqlString = "insert into pm_Cont_ContClause (ContractID,ClauseItemName,DataType,SerialNumber,UserCode,Flag)  select " + ContractID + ",ClauseItemName,DataType,SerialNumber,'" + UserCode + "','0' from pm_Cont_ClauseItem where ClauseItemID in (" + ClauseItemIDs + ")";
                    }
                }
                else
                {
                    sqlString = "insert into pm_Cont_ContClause (ContractID,ClauseItemName,DataType,SerialNumber,UserCode,Flag)  select 0,ClauseItemName,DataType,SerialNumber,'" + UserCode + "','0' from pm_Cont_ClauseItem where ClauseItemID in (" + ClauseItemIDs + ")";
                }
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool AddUrgencyPlan(string ContractID, DateTime RemindTime, string RemindPersonCode, string RemindContent)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "insert into pm_Cont_UrgencyPlan (ContractID,RemindTime,RemindPersonCode,RemindState,RemindContent) values('", ContractID, "','", RemindTime, "','", RemindPersonCode, "',0,'", RemindContent, "')" }));
        }

        public bool CheckContNumber(string ContractID, string ContNumber, string OpType)
        {
            string sqlString = "";
            if (OpType.Trim() == "add")
            {
                sqlString = "select count(1) from pm_Cont_ContInfo where ContNumber ='" + ContNumber + "'";
            }
            else
            {
                sqlString = "select count(1) from pm_Cont_ContInfo where ContractID <> " + ContractID + " and  ContNumber ='" + ContNumber + "'";
            }
            return (((int) publicDbOpClass.ExecuteScalar(sqlString)) != 0);
        }

        public string CheckContractApply(string ContractID)
        {
            string str = "";
            int num = 0;
            int num2 = 0;
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_ContClause where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在合同条款信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_ContMaterial where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在材料清单信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_PaymentPlan where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在付款计划信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_Affair where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在合同事务信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_PaymentNote where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在付款记录信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_ContAlteration where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在合同变更记录；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_ContEssential where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在审核要点信息；";
            }
            num = (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_Cont_PaymentCyc where ContractID=" + ContractID);
            if (num > 0)
            {
                num2++;
                str = str + @"\n\r" + num2.ToString() + "、存在付款周期信息；";
            }
            if (!(str != ""))
            {
                return "0";
            }
            return str;
        }

        public DataTable ContAffairList(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_Affair where ContractID = " + ContractID);
        }

        public DataTable ContAlterationList(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ContAlteration where ContractID = " + ContractID);
        }

        public DataTable ContEssentialList(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ContEssential where ContractID = " + ContractID);
        }

        public bool ContGiven(string ContractID, string GivenDate, string GivenTranPerson)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_ContInfo set GivenDate = '" + GivenDate + "',GivenTranBS = '1',GivenTranPerson =" + GivenTranPerson + " where ContractID =" + ContractID);
        }

        public DataTable ContList(string sqlWhere)
        {
            return publicDbOpClass.DataTableQuary("select * ,SumMoney = (isnull(ContMoney,0) + isnull(CenewContMoney,0)) ,AuditStateName =  ( case AuditState  when -1 then '未启动' when 0 then '流转中' when 1 then '审核通过' when -2 then '驳回' end ) ,SecondPartyTypeName =  ( case SecondPartyType  when '0' then '其它' when '1' then '供应商' when '2' then '客户' end )  , planpaymoney=(select sum(paymentmoney) from pm_cont_paymentplan  a where a.ContractID = pm_Cont_ContInfo.ContractID and AuditState=1)  , okpaymoney=(select sum(paymoney) from pm_cont_paymentplan  a where a.ContractID = pm_Cont_ContInfo.ContractID and AuditState=1)  , wfje=((isnull(ContMoney,0) + isnull(CenewContMoney,0))-(select sum(paymoney) from pm_cont_paymentplan  a where a.ContractID = pm_Cont_ContInfo.ContractID and AuditState=1)) , ContClassID as ContClassID,contCode,(len([contCode])/3) as tree_level,isbottom ,parentidcoll,childnum,(case when isnull(i_pschildnum,0)!=0  then isnull(i_pschildnum,0) when isnull(i_pschildnum,0)=0 then isnull(childnum,0) end)  as direct_child_num from pm_Cont_ContInfo  " + sqlWhere + " order by pm_Cont_ContInfo.contnumber, pm_Cont_ContInfo.contCode asc, ConstituteDate desc");
        }

        public DataTable ContPaymentCycList(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.TaskName,PayCondition2Name = ( case a.PayCondition2 when '0' then '合同签订' when '1' then '节点完成' end)   from pm_Cont_PaymentCyc a  left join pm_wbs b on a.TaskId = b.TaskId where a.ContractID = " + ContractID);
        }

        public bool delAudiEssentialInfo(string RecordID)
        {
            return publicDbOpClass.NonQuerySqlString(" delete pm_Cont_ContEssential where RecordID=" + RecordID);
        }

        public bool delContAffairInfo(string RecordID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_Affair where RecordID =" + RecordID);
        }

        public bool delContAlterationInfo(string AlterationRecordID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_ContAlteration where AlterationRecordID =" + AlterationRecordID);
        }

        public bool delContClauseInfo(string ContClauseRecordID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_ContClause where ContClauseRecordID =" + ContClauseRecordID);
        }

        public bool DelContMaterial(string MaterialRecordID, string Flag)
        {
            string str = " begin ";
            if (Flag == "1")
            {
                str = str + " update pm_resources set HeadwayState = '2' where MdResourceId in (select MdResourceId from PM_Purc_DemandPlanDetail where DemandPlanDetailID in (select DemandPlanDetailID from PM_Purc_PurcPlanDetail where PurcPlanDetailID in (select PurcPlanDetailID from pm_Cont_ContMaterial where  MaterialRecordID =" + MaterialRecordID + ")))";
            }
            return publicDbOpClass.NonQuerySqlString((((((str + " delete pm_Cont_MaterialExamine where MaterialArriveID in (select MaterialArriveID from pm_Cont_MaterialArrive where MaterialRecordID =" + MaterialRecordID + ")") + " delete pm_Cont_MaterialArrive where MaterialRecordID =" + MaterialRecordID) + " delete pm_Cont_MateExamResult where MaterialRecordID =" + MaterialRecordID) + " delete pm_Cont_ContMaterial_P where MaterialRecordID =" + MaterialRecordID) + " delete pm_Cont_ContMaterial where MaterialRecordID =" + MaterialRecordID) + " end ");
        }

        public bool DelContMoney(string pid)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pm_Cont_PaymentPlan where payplanrecordid=" + pid);
        }

        public bool DelContractInfo(string ContractID, string ContCode, int ContClassId)
        {
            object obj2 = "delete pm_Cont_ContInfo where ContractID =" + ContractID;
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, " update pm_cont_continfo set ChildNum=ChildNum-1 where contCode='", ContCode.Substring(0, ContCode.Length - 3), "' and contclassid=", ContClassId }));
        }

        public static bool DelContResSpare(string recordID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from PM_Cont_SparePart where RecordID='" + recordID + "'");
        }

        public bool delContWbs(string pkcontwbsid)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_cont_wbs where pkcontwbsid='" + pkcontwbsid + "'");
        }

        public bool DelGarbageData(string ContractID)
        {
            string str = " begin";
            return publicDbOpClass.NonQuerySqlString((((str + " delete pm_Cont_ContMaterial_P where MaterialRecordID in (select MaterialRecordID from pm_Cont_ContMaterial where Flag = '0' and ContractID =" + ContractID + ")") + " delete pm_Cont_MateExamResult where MaterialRecordID in (select MaterialRecordID from pm_Cont_ContMaterial where Flag = '0' and ContractID =" + ContractID + ")") + " delete pm_Cont_ContMaterial where Flag = '0' and ContractID =" + ContractID) + " end");
        }

        public bool DelMateExamResult(string RecordID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_MateExamResult where RecordID =" + RecordID);
        }

        public bool DelMaterialArrive(string MaterialArriveID)
        {
            string str = " begin";
            return publicDbOpClass.NonQuerySqlString(((str + " delete pm_Cont_MaterialExamine where MaterialArriveID =" + MaterialArriveID) + " delete pm_Cont_MaterialArrive where MaterialArriveID =" + MaterialArriveID) + " end");
        }

        public bool DelMaterialExamine(string MaterialExamineID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_MaterialExamine where MaterialExamineID =" + MaterialExamineID);
        }

        public bool delPaymentCycInfo(string PaymCycRecordID)
        {
            return publicDbOpClass.NonQuerySqlString(" delete pm_Cont_PaymentCyc where PaymCycRecordID=" + PaymCycRecordID);
        }

        public bool delPaymentNoteInfo(string RecordID)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_PaymentNote where PaymentNoteID =" + RecordID);
        }

        public static bool DelUrgencyPlan(string UrgencyPlanID)
        {
            return publicDbOpClass.NonQuerySqlString("delete from pm_Cont_UrgencyPlan where UrgencyPlanID=" + UrgencyPlanID);
        }

        public bool delUserTempContClause(string UserCode)
        {
            return publicDbOpClass.NonQuerySqlString("delete pm_Cont_ContClause where Flag = '0' and UserCode ='" + UserCode + "'");
        }

        public bool EssentialAdhibition(string ContClassID, string ContractID)
        {
            string str2 = (" begin " + " delete pm_Cont_ContEssential where ContractID =" + ContractID) + " insert into pm_Cont_ContEssential (ContractID,EssentialName,AdvertProceeding,Remark) ";
            return publicDbOpClass.NonQuerySqlString((str2 + " select " + ContractID + ",EssentialName,AdvertProceeding,Remark from pm_Cont_AudiEssential where ContClassID =" + ContClassID) + " end ");
        }

        public DataTable GetBuildingNumber(string ProjectId)
        {
            return publicDbOpClass.DataTableQuary("select  isnull(BuildingNumber,'') from pm_buildings where ProjectId =" + ProjectId);
        }

        public string GetConClassAttr(string ContractID)
        {
            string str = "0";
            DataTable table = publicDbOpClass.DataTableQuary("select isnull(ConClassAttr,'0') as ConClassAttr from pm_Cont_ContInfo where ContractID =" + ContractID);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["ConClassAttr"].ToString();
            }
            return str;
        }

        public DataTable GetContAffairInfo(string RecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_Affair where RecordID = " + RecordID);
        }

        public DataTable GetContAlterationInfo(string AlterationRecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ContAlteration where AlterationRecordID = " + AlterationRecordID);
        }

        public string GetContClassID(string ContractID)
        {
            return publicDbOpClass.QueryDataRow("select contclassid from pm_Cont_ContInfo   where ContractID = " + ContractID)["contclassid"].ToString();
        }

        public DataTable GetContClause(string ContractID, string Flag)
        {
            string sqlString = "";
            if (Flag == "")
            {
                sqlString = "select * from pm_Cont_ContClause where ContractID =" + ContractID + " order by SerialNumber";
            }
            else
            {
                sqlString = "select * from pm_Cont_ContClause where ContractID =" + ContractID + " and Flag = '1' order by SerialNumber";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetContEssentialInfo(string RecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ContEssential where RecordID = " + RecordID);
        }

        public DataTable GetContFastItem(string ContClassID)
        {
            return publicDbOpClass.DataTableQuary("select *  from pm_Cont_FastItem where ContClassID=" + ContClassID);
        }

        public DataTable GetContList(string SqlWhere)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ContInfo where 1=1 and AuditState=1" + SqlWhere);
        }

        public DataTable GetContMaterialList(string ContractID, string Flag)
        {
            string sqlString = "";
            if (Flag == "")
            {
                sqlString = "select a.*,c.ResName,c.Specification ,d.UnitName from pm_Cont_ContMaterial a left join PM_Prj_Res_Item c on a.MaterialId = c.MaterialId left join PM_Prj_Res_Unit d on c.UnitID = d.UnitID  where a.ContractID = " + ContractID + " order by a.MaterialId,a.MaterialRecordID ";
            }
            else
            {
                sqlString = "select a.* ,c.ResName,c.Specification,d.UnitName from pm_Cont_ContMaterial a left join PM_Prj_Res_Item c on a.MaterialId = c.MaterialId left join PM_Prj_Res_Unit d on c.UnitID = d.UnitID where a.Flag = '1' and  a.ContractID = " + ContractID + " order by a.MaterialId,a.MaterialRecordID ";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetContPaymentCycInfo(string PaymCycRecordID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.TaskName from pm_Cont_PaymentCyc a  left join pm_wbs b on a.TaskId = b.TaskId where a.PaymCycRecordID = " + PaymCycRecordID);
        }

        public static DataTable GetContPurcPart(string materialRecordID)
        {
            string sqlString = "SELECT a.*,b.UnitName as UnitName FROM PM_Cont_SparePart a left outer join dbo.PM_Prj_Res_Unit b on a.UnitID=b.UnitID";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public string GetContractID(string Guid)
        {
            return publicDbOpClass.ExecuteScalar("select ContractID from pm_Cont_ContInfo where RecordID = " + Guid).ToString().Trim();
        }

        public DataTable GetExamineInfo(string id, string opType)
        {
            string sqlString = "";
            if (opType == "MateExamResult")
            {
                sqlString = "select * from pm_Cont_MateExamResult where RecordID =" + id;
            }
            if (opType == "MaterialExamine")
            {
                sqlString = "select * from pm_Cont_MaterialExamine where MaterialExamineID =" + id;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetExamineList(string id, string opType)
        {
            string sqlString = "";
            if (opType == "MateExamResult")
            {
                sqlString = "select * from pm_Cont_MateExamResult where MaterialRecordID =" + id;
            }
            if (opType == "MaterialExamine")
            {
                sqlString = "select *,MaterialExamineID as RecordID from pm_Cont_MaterialExamine where MaterialArriveID =" + id;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetMaterialArriveInfo(string MaterialArriveID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_MaterialArrive where MaterialArriveID =" + MaterialArriveID);
        }

        public DataTable GetMaterialArriveList(string MaterialRecordID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,isnull(b.ProviderName,'') as ProviderName from pm_Cont_MaterialArrive a left join pm_provider_info b on a.ProviderId=b.ProviderId  where a.MaterialRecordID =" + MaterialRecordID);
        }

        public ContInfo GetModel(int ContractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  ");
            builder.Append(" ContractID,ContClassID,ContName,ContNumber,ConstituteDate,GivenDate,FirstParty,SecondParty,ContMoney,CenewContMoney,BeginExecuteDate,EndExecuteDate,SecondPartyType,SecondPartyID,ProjectName,ProjectId,ResidenceCommunity,BuildingNumber,RoomNumber,GroundNumber,AuditState,Remark,GivenTranPerson,UserCode,RecordDate,RelationInfoID,ConClassAttr ");
            builder.Append(" from pm_Cont_ContInfo ");
            builder.Append(" where ContractID=" + ContractID.ToString());
            ContInfo info = new ContInfo();
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            if (table.Rows.Count <= 0)
            {
                return null;
            }
            if (table.Rows[0]["ContractID"].ToString() != "")
            {
                info.ContractID = int.Parse(table.Rows[0]["ContractID"].ToString());
            }
            if (table.Rows[0]["ContClassID"].ToString() != "")
            {
                info.ContClassID = int.Parse(table.Rows[0]["ContClassID"].ToString());
            }
            info.ContName = table.Rows[0]["ContName"].ToString();
            info.ContNumber = table.Rows[0]["ContNumber"].ToString();
            if (table.Rows[0]["ConstituteDate"].ToString() != "")
            {
                info.ConstituteDate = DateTime.Parse(table.Rows[0]["ConstituteDate"].ToString());
            }
            if (table.Rows[0]["GivenDate"].ToString() != "")
            {
                info.GivenDate = DateTime.Parse(table.Rows[0]["GivenDate"].ToString());
            }
            info.FirstParty = table.Rows[0]["FirstParty"].ToString();
            info.SecondParty = table.Rows[0]["SecondParty"].ToString();
            if (table.Rows[0]["ContMoney"].ToString() != "")
            {
                info.ContMoney = decimal.Parse(table.Rows[0]["ContMoney"].ToString());
            }
            if (table.Rows[0]["CenewContMoney"].ToString() != "")
            {
                info.CenewContMoney = decimal.Parse(table.Rows[0]["CenewContMoney"].ToString());
            }
            if (table.Rows[0]["BeginExecuteDate"].ToString() != "")
            {
                info.BeginExecuteDate = DateTime.Parse(table.Rows[0]["BeginExecuteDate"].ToString());
            }
            if (table.Rows[0]["EndExecuteDate"].ToString() != "")
            {
                info.EndExecuteDate = DateTime.Parse(table.Rows[0]["EndExecuteDate"].ToString());
            }
            info.SecondPartyType = table.Rows[0]["SecondPartyType"].ToString();
            if (table.Rows[0]["SecondPartyID"].ToString() != "")
            {
                info.SecondPartyID = int.Parse(table.Rows[0]["SecondPartyID"].ToString());
            }
            info.ProjectName = table.Rows[0]["ProjectName"].ToString();
            if (table.Rows[0]["ProjectId"].ToString() != "")
            {
                info.ProjectId = int.Parse(table.Rows[0]["ProjectId"].ToString());
            }
            info.ResidenceCommunity = table.Rows[0]["ResidenceCommunity"].ToString();
            info.BuildingNumber = table.Rows[0]["BuildingNumber"].ToString();
            info.RoomNumber = table.Rows[0]["RoomNumber"].ToString();
            info.GroundNumber = table.Rows[0]["GroundNumber"].ToString();
            if (table.Rows[0]["AuditState"].ToString() != "")
            {
                info.AuditState = int.Parse(table.Rows[0]["AuditState"].ToString());
            }
            info.Remark = table.Rows[0]["Remark"].ToString();
            info.GivenTranPerson = table.Rows[0]["GivenTranPerson"].ToString();
            info.UserCode = table.Rows[0]["UserCode"].ToString();
            if (table.Rows[0]["RecordDate"].ToString() != "")
            {
                info.RecordDate = DateTime.Parse(table.Rows[0]["RecordDate"].ToString());
            }
            if (table.Rows[0]["RelationInfoID"].ToString() != "")
            {
                info.RelationInfoID = int.Parse(table.Rows[0]["RelationInfoID"].ToString());
            }
            info.ConClassAttr = table.Rows[0]["ConClassAttr"].ToString();
            return info;
        }

        public static string GetNewTaskCode(string code)
        {
            string str = "";
            int num = code.Length + 3;
            StringBuilder builder = new StringBuilder();
            builder.Append(string.Concat(new object[] { " select max(contCode) from pm_cont_contInfo where contCode like '", code, "%' and len(contCode)='", num, "'" }));
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = (string) obj2;
                str = Convert.ToString((int) (int.Parse(str.Substring(str.Length - 3, 3)) + 1)).PadLeft(3, '0');
                return (code.Trim() + str);
            }
            return (code.Trim() + "001");
        }

        public static DataTable GetParentTaskCBS(string contcode)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary((str + "select isnull(a.parentidcoll,'') +','+Convert(nvarchar(100),a.contcode)+'' as parentidcoll from pm_cont_contInfo a  ") + " where  a.contcode = '" + contcode + "'");
        }

        public DataTable GetPaymentNoteInfo(string RecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_PaymentNote where PaymentNoteID = " + RecordID);
        }

        public DataTable GetProjectID(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select ProjectId from pm_Cont_ContInfo   where ContractID = " + ContractID);
        }

        public DataTable GetProviderName(string MaterialRecordID)
        {
            return publicDbOpClass.DataTableQuary("select isnull(ProviderName,'') as ProviderName,ProviderId from pm_provider_info  where ProviderId in (select top 1 isnull(SecondPartyID,0) from pm_Cont_ContInfo where ContractID = (select  ContractID from pm_Cont_ContMaterial where MaterialRecordID =" + MaterialRecordID + "))");
        }

        public DataTable GetRoomNumber(string BuildingNumber)
        {
            return publicDbOpClass.DataTableQuary("select  isnull(RoomNumber,'') from pm_building_rooms where BuildingId =(select BuildingId from pm_buildings where BuildingNumber =" + BuildingNumber.Trim() + ")");
        }

        public DataTable GetTaskList(string ProviderId, string ProjectId)
        {
            return publicDbOpClass.DataTableQuary("select a.TaskId,(''+replicate('&nbsp;',(len(a.TaskCode)/3-1)*2) + isnull(a.TaskName,'')) as TaskName from pm_wbs a  left join PM_WBS_Builder b  on a.TaskId = b.TaskId  where a.ProjectId = " + ProjectId + " and b.BuilderID = " + ProviderId + " order by a.taskcode ");
        }

        public DataTable GetTempContClause(string UserCode)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_ContClause where UserCode ='" + UserCode + "' and Flag ='0' order by SerialNumber");
        }

        public static DataTable GetUrgencyPlanInfo(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.v_xm from pm_Cont_UrgencyPlan a left join pt_yhmc b on a.RemindPersonCode = b.v_yhdm where a.ContractID=" + ContractID);
        }

        public static DataTable GetUrgencyPlanInfoOnUrgencyPlanID(string UrgencyPlanID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,b.v_xm from pm_Cont_UrgencyPlan a left join pt_yhmc b on a.RemindPersonCode=b.v_yhdm where a.UrgencyPlanID=" + UrgencyPlanID);
        }

        public DataTable GetUrgencyPlanList(string UrgencyPlanID)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Cont_UrgencyPlan where UrgencyPlanID =" + UrgencyPlanID);
        }

        public DataTable GetWbsList(string strwhere)
        {
            string sqlString = "select a.*,c.ProjectId from pm_cont_wbs a  left join pm_Cont_ContInfo c on a.contractid = c.contractid ";
            if (strwhere != "")
            {
                sqlString = sqlString + strwhere;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public bool IsGivenTran(string ContractID)
        {
            bool flag = false;
            string str = "0";
            DataTable table = publicDbOpClass.DataTableQuary("select isnull(GivenTranBS,'0') as GivenTranBS from pm_Cont_ContInfo where ContractID =" + ContractID);
            if (table.Rows.Count > 0)
            {
                str = table.Rows[0]["GivenTranBS"].ToString().Trim();
            }
            if (str == "1")
            {
                flag = true;
            }
            return flag;
        }

        public bool MakePaymentPlan(string ContractID)
        {
            return publicDbOpClass.NonQuerySqlString("begin delete pm_Cont_PaymentPlan where ContractID =" + ContractID + " exec p_Prj_MakeContPaymentPlan " + ContractID + " end");
        }

        public DataTable PaymentNoteList(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,isnull(b.TaskName,'') as TaskName  from pm_Cont_PaymentNote a left join pm_wbs b on a.TaskId = b.TaskId where a.ContractID = " + ContractID);
        }

        public DataTable PaymentPlanList(string ContractID)
        {
            return publicDbOpClass.DataTableQuary("select c.ResName,b.TaskName, convert(varchar(10),pstime,120) as pstime, PayConditionName =  ( case a.PayCondition  when '0' then '合同签订' when  '1' then '节点完成' end) ,AuditState= ( case a.AuditState  when '-1' then '未启动' when '0' then '流转中' when '1' then '审核通过' when '-2' then '驳回' end ) ,a.*  from pm_Cont_PaymentPlan a left join pm_wbs b on a.TaskId = b.TaskId left join PM_Prj_Res_Item c on a.MaterialId = c.MaterialId where a.ContractID = " + ContractID);
        }

        public DataTable PaymentPlanListAtProid(string pid, string wsql)
        {
            return publicDbOpClass.DataTableQuary("select payplanrecordid,convert(varchar(10),pstime,120) as pstime ,PaymentMoney,auditstate,remark from pm_cont_paymentplan where (contractid in(select contractid from pm_cont_continfo where projectid=" + pid + ")) " + wsql + " order by pstime asc");
        }

        public DataTable PayPlanList(string ContractID, string str)
        {
            return publicDbOpClass.DataTableQuary(("select a.*,c.ResName,b.TaskName , PayConditionName =  ( case a.PayCondition  when '0' then '合同签订' when  '1' then '节点完成' end) ,AuditState= ( case a.AuditState  when '-1' then '未启动' when '0' then '流转中' when '1' then '审核通过' when '-2' then '驳回' end ) from pm_Cont_PaymentPlan a left join pm_wbs b on a.TaskId = b.TaskId left join PM_Prj_Res_Item c on a.MaterialId = c.MaterialId where a.ContractID = " + ContractID) + " and a.AuditState=1 " + str);
        }

        public static bool PurcAddContResSpare(string MaterialRecordID, string SparePartName, string Specification, int UnitID, string Remark, string unitPrice, string amount)
        {
            return publicDbOpClass.NonQuerySqlString(" insert into PM_Cont_SparePart (RecordID,MaterialRecordID,SparePartName, Specification, UnitID, Remark,UnitPrice,Amount)values (newid()," + MaterialRecordID + ",'" + SparePartName + "','" + Specification + "'," + UnitID.ToString() + ",'" + Remark + "'," + unitPrice + "," + amount + ")");
        }

        public static DataTable PurcGetContResSpare(string RecordID)
        {
            return publicDbOpClass.DataTableQuary("SELECT a.*,b.UnitName as UnitName FROM PM_Cont_SparePart a left outer join dbo.PM_Prj_Res_Unit b on a.UnitID=b.UnitID where a.RecordID ='" + RecordID + "' ");
        }

        public static bool PurcUpdResContSpare(string RecordID, string SparePartName, string Specification, int UnitID, string Remark, string unitPrice, string amount)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { " update PM_Cont_SparePart set SparePartName='", SparePartName, "',Specification='", Specification, "',UnitID=", UnitID, ",Remark='", Remark, "',UnitPrice=", unitPrice, ",Amount=", amount, "   where RecordID='", RecordID, "'" }));
        }

        public bool SaveContMaterial(string UserCode, string ContractID, string PurcPlanDetailIDs)
        {
            string str = "";
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_Cont_ContMaterial", "MaterialRecordID")) + 1;
            string[] strArray = PurcPlanDetailIDs.Split(new char[] { ',' });
            str = " begin";
            for (int i = 0; i < strArray.Length; i++)
            {
                num += i;
                string str2 = str;
                string str3 = str2 + " insert into pm_Cont_ContMaterial  (ContractID,MaterialRecordID,Flag,PurcPlanDetailID,UserCode,MaterialId,UnitPrice,Scalar)  select " + ContractID + "," + num.ToString() + ",'0'," + strArray[i] + ",'" + UserCode + "',MaterialId ,Price = (  case PurcMode when 0 then isnull((select ProviderPrice from PM_Purc_BidPlanDetail where PurcPlanDetailID =" + strArray[i] + "),0.00) when 1 then isnull((select Price from PM_Purc_InquiryProvPrice where IsPurchaseMoney = '1' and PurcPlanDetailID =" + strArray[i] + "),0.00) end ),Scalar from PM_Purc_PurcPlanDetail  where PurcPlanDetailID =" + strArray[i];
                string str4 = str3 + " insert into pm_Cont_ContMaterial_P (MaterialRecordID,Name,Remark) select " + num.ToString() + ", Name,Remark from PM_Purc_PurcPlanDetail_P where  PurcPlanDetailID =" + strArray[i];
                str = str4 + " insert into pm_Cont_MateExamResult(MaterialRecordID,QCContent,QCStandard,QCBound)  select " + num.ToString() + " ,QCContent,QCStandard,QCBound from PM_Prj_Res_QC where MaterialId in (select top 1 MaterialId from PM_Purc_PurcPlanDetail where PurcPlanDetailID =" + strArray[i] + ")";
            }
            return publicDbOpClass.NonQuerySqlString(str + " end");
        }

        public bool SaveContMaterial2(string UserCode, string ContractID, string MaterialIds)
        {
            string str = "";
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_Cont_ContMaterial", "MaterialRecordID")) + 1;
            string[] strArray = MaterialIds.Split(new char[] { ',' });
            str = " begin";
            for (int i = 0; i < strArray.Length; i++)
            {
                num += i;
                string str2 = str;
                string str3 = str2 + " insert into  pm_Cont_ContMaterial (ContractID,MaterialRecordID,Flag,PurcPlanDetailID,UserCode,MaterialId,UnitPrice,Scalar) select " + ContractID + "," + num.ToString() + ",'0',0,'" + UserCode + "',MaterialId,0,0 from PM_Prj_Res_Item where MaterialId = " + strArray[i];
                string str4 = str3 + " insert into pm_Cont_ContMaterial_P (MaterialRecordID,Name,Remark) select " + num.ToString() + ", Name,Remark from PM_Prj_Res_Technology where IsValid = '1' and MaterialId = " + strArray[i];
                str = str4 + " insert into pm_Cont_MateExamResult(MaterialRecordID,QCContent,QCStandard,QCBound)  select " + num.ToString() + " ,QCContent,QCStandard,QCBound from PM_Prj_Res_QC where MaterialId =" + strArray[i];
            }
            return publicDbOpClass.NonQuerySqlString(str + " end");
        }

        public bool ScaleIsGreaterThan1(string ContractID, string TaskId, string PaymCycRecordID, string PaymentScale)
        {
            bool flag = false;
            if (double.Parse(publicDbOpClass.ExecuteScalar("select isnull(Sum(PaymentScale),0)+" + PaymentScale + " as ScaleSum from pm_Cont_PaymentCyc  where TaskId = " + TaskId + " and  ContractID = " + ContractID + " and  PaymCycRecordID <> " + PaymCycRecordID).ToString()) > 1.0)
            {
                flag = true;
            }
            return flag;
        }

        public DataTable SelectContMoney(string pid)
        {
            return publicDbOpClass.DataTableQuary("select pstime,paymentmoney,remark from pm_Cont_PaymentPlan where payplanrecordid=" + pid);
        }

        public DataTable SelectContMoneyAtGuid(string pid)
        {
            return publicDbOpClass.DataTableQuary("select a.*,convert(varchar(10),b.pstime,120) as pstime,b.paymentmoney,b.remark as payremark,c.altmoney from pm_cont_continfo a,pm_cont_paymentPlan b,(select sum(AlterationMoney) as altmoney  from pm_Cont_ContAlteration where contractid=(select contractid from pm_cont_paymentPlan where recordid='" + pid + "'))c where a.contractid=b.contractid and b.recordid='" + pid + "'");
        }

        public string SelOverProjectID(string pid)
        {
            return Convert.ToString(publicDbOpClass.ExecuteScalar("select projectid from pm_cont_continfo where contractid=(select contractid from pm_cont_paymentplan where recordid='" + pid + "')"));
        }

        public bool SetInvocationState(string UrgencyPlanID)
        {
            bool flag = false;
            DataTable table = publicDbOpClass.DataTableQuary("select * from pm_Cont_UrgencyPlan where UrgencyPlanID =" + UrgencyPlanID);
            if (table.Rows.Count > 0)
            {
                PTDBSJ model = new PTDBSJ {
                    V_LXBM = "018",
                    I_XGID = UrgencyPlanID,
                    DTM_DBSJ = DateTime.Parse(table.Rows[0]["RemindTime"].ToString()),
                    V_Content = userManageDb.GetCurrentUserInfo().UserName + "给您发送了合同材料催交提醒。",
                    V_DBLJ = "?rid=" + UrgencyPlanID,
                    V_YHDM = table.Rows[0]["RemindPersonCode"].ToString()
                };
                if (publicDbOpClass.NonQuerySqlString("update pm_Cont_UrgencyPlan set UserCode='" + userManageDb.GetCurrentUserInfo().UserCode + "', RemindState = '1' where UrgencyPlanID =" + UrgencyPlanID))
                {
                    PublicInterface.SendSysMsg(model);
                    flag = true;
                }
            }
            return flag;
        }

        public bool SetInvocationState2(string UrgencyPlanID)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_UrgencyPlan set RemindState ='2' where UrgencyPlanID=" + UrgencyPlanID);
        }

        public bool SetPay(string RecordID, string paymoney)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_PaymentPlan set PayMoney=" + paymoney + ", Paytime='" + DateTime.Now.ToString() + "'  where PayPlanRecordID =" + RecordID);
        }

        public bool SetPay0(string RecordID)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_PaymentPlan set PayMoney=0, Paytime='" + DateTime.Now.ToString() + "'  where PayPlanRecordID =" + RecordID);
        }

        public bool Update(ContInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin ");
            builder.Append(" update pm_Cont_ContInfo set ");
            builder.Append("ContName=" + model.ContName + ",");
            builder.Append("ContNumber=" + model.ContNumber + ",");
            builder.Append("ConstituteDate='" + model.ConstituteDate.ToString() + "',");
            builder.Append("GivenDate='" + model.GivenDate.ToString() + "',");
            builder.Append("FirstParty=" + model.FirstParty + ",");
            builder.Append("SecondParty=" + model.SecondParty + ",");
            builder.Append("ContMoney=" + model.ContMoney.ToString() + ",");
            builder.Append("CenewContMoney=" + model.CenewContMoney + ",");
            builder.Append("BeginExecuteDate='" + model.BeginExecuteDate.ToString() + "',");
            builder.Append("EndExecuteDate='" + model.EndExecuteDate.ToString() + "',");
            builder.Append("SecondPartyType='" + model.SecondPartyType + "',");
            builder.Append("SecondPartyID=" + model.SecondPartyID.ToString() + ",");
            builder.Append("ProjectName=" + model.ProjectName + ",");
            builder.Append("ProjectId=" + model.ProjectId.ToString() + ",");
            builder.Append("ResidenceCommunity='" + model.ResidenceCommunity + "',");
            builder.Append("BuildingNumber='" + model.BuildingNumber + "',");
            builder.Append("RoomNumber='" + model.RoomNumber + "',");
            builder.Append("GroundNumber='" + model.GroundNumber + "',");
            builder.Append("AuditState=" + model.AuditState.ToString() + ",");
            builder.Append("Remark=" + model.Remark + ",");
            builder.Append("GivenTranPerson='" + model.GivenTranPerson + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate.ToString() + "',");
            builder.Append("RelationInfoID=" + model.RelationInfoID.ToString() + ",");
            builder.Append("ConClassAttr='" + model.ConClassAttr + "'");
            builder.Append(" where ContractID=" + model.ContractID.ToString());
            builder.Append(" update pm_Cont_ContClause set Flag = '1' where ContractID = " + model.ContractID.ToString());
            builder.Append(" end ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public void updateCenewContMoney(string ContractID)
        {
            publicDbOpClass.NonQuerySqlString("update pm_Cont_ContInfo set CenewContMoney = isnull((select sum(isnull(AlterationMoney,0)) from pm_Cont_ContAlteration where ContractID =" + ContractID + "),0) where ContractID =" + ContractID);
        }

        public bool UpdateContMaterialFlag(string ContractID)
        {
            string str = " begin ";
            return publicDbOpClass.NonQuerySqlString(((str + " update pm_resources set HeadwayState = '3' where MdResourceId in (select MdResourceId from PM_Purc_DemandPlanDetail where DemandPlanDetailID in (select DemandPlanDetailID from PM_Purc_PurcPlanDetail where PurcPlanDetailID in (select PurcPlanDetailID from pm_Cont_ContMaterial where Flag = '0' and ContractID =" + ContractID + ")))") + " update pm_Cont_ContMaterial set Flag = '1' where Flag = '0' and  ContractID =" + ContractID) + " end ");
        }

        public bool UpdateContMoney(string pid, string contractid, string money, string sdate, string remark)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_PaymentPlan set contractid='" + contractid + "',pstime='" + sdate + "',paymentmoney='" + money + "',remark='" + remark + "' where payplanrecordid=" + pid);
        }

        public static bool UpdateUrgencyPlan(string UrgencyPlanID, DateTime RemindTime, string RemindPersonCode, string RemindContent)
        {
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { "update pm_Cont_UrgencyPlan set RemindTime='", RemindTime, "',RemindPersonCode='", RemindPersonCode, "',RemindContent='", RemindContent, "' where UrgencyPlanID=", UrgencyPlanID }));
        }

        public bool updAudiEssentialInfo(string EssentialID, string EssentialName, string AdvertProceeding, string Remark)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_ContEssential set EssentialName =" + EssentialName + ",AdvertProceeding =" + AdvertProceeding + ",Remark =" + Remark + " where RecordID =" + EssentialID);
        }

        public bool updContAffairInfo(string RecordID, string AffairName, string AffairDate, string TransactResult, string DutyPerson)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_Affair set AffairName =" + AffairName + ",AffairDate ='" + AffairDate + "',TransactResult =" + TransactResult + ",DutyPerson =" + DutyPerson + " where RecordID =" + RecordID);
        }

        public bool updContAlterationInfo(string AlterationRecordID, string AlterationDate, string AlterationMoney, string AlterationCause, string Remark, string GivenDate, string GivenTranPerson)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_ContAlteration set AlterationDate ='" + AlterationDate + "',AlterationMoney =" + AlterationMoney + ",AlterationCause =" + AlterationCause + ",Remark =" + Remark + ",GivenDate ='" + GivenDate + "',GivenTranPerson =" + GivenTranPerson + " where AlterationRecordID =" + AlterationRecordID);
        }

        public bool updContClauseInfo(string bs, string id, string TValue)
        {
            string sqlString = "";
            string str2 = bs;
            if (str2 != null)
            {
                if (!(str2 == "2"))
                {
                    if (str2 == "1")
                    {
                        sqlString = "update pm_Cont_ContClause set ClauseContent = " + SqlStringConstructor.GetQuotedString(TValue) + " where ContClauseRecordID =" + id;
                    }
                }
                else
                {
                    sqlString = "update pm_Cont_ContClause set SerialNumber = " + TValue + " where ContClauseRecordID =" + id;
                }
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool updContMaterialFieldValue(string bs, string id, string TValue)
        {
            string sqlString = "";
            string str2 = bs;
            if (str2 != null)
            {
                if (!(str2 == "0"))
                {
                    if (str2 == "1")
                    {
                        sqlString = "update pm_Cont_ContMaterial set Scalar = " + TValue + " where MaterialRecordID =" + id;
                    }
                    else if (str2 == "2")
                    {
                        sqlString = "update pm_Cont_ContMaterial set UnitPrice = " + TValue + " where MaterialRecordID =" + id;
                    }
                    else if (str2 == "3")
                    {
                        sqlString = "update pm_Cont_ContMaterial set ArriveDate = '" + TValue + "' where MaterialRecordID =" + id;
                    }
                    else if (str2 == "4")
                    {
                        sqlString = "update pm_Cont_ContMaterial set MaterialTexture = '" + TValue + "' where MaterialRecordID =" + id;
                    }
                    else if (str2 == "5")
                    {
                        sqlString = "update pm_Cont_ContMaterial set Standard = '" + TValue + "' where MaterialRecordID =" + id;
                    }
                }
                else
                {
                    sqlString = "update pm_Cont_ContMaterial set CapabilityRequest = '" + TValue + "' where MaterialRecordID =" + id;
                }
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public bool updContWbs(string pkcontwbsid, string taskid)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_cont_wbs set taskid ='" + taskid + "' where pkcontwbsid ='" + pkcontwbsid + "'");
        }

        public bool UpdMateExamResult(string RecordID, string ExamineResult, string Remark, string QCContent, string QCStandard, string QCBound)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_MateExamResult set ExamineResult =" + ExamineResult + ",Remark =" + Remark + ",QCContent =" + QCContent + ",QCStandard =" + QCStandard + ",QCBound =" + QCBound + " where RecordID =" + RecordID);
        }

        public bool UpdMaterialArrive(string MaterialArriveID, string ArriveTime, string Buyer, string ArriveScalar)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_MaterialArrive set ArriveTime ='" + ArriveTime + "',Buyer =" + Buyer + ",ArriveScalar =" + ArriveScalar + ",MaterialArriveID =" + MaterialArriveID);
        }

        public bool UpdMaterialExamine(string MaterialExamineID, string ExamineResult, string Remark, string QCContent, string QCStandard, string QCBound)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_MaterialExamine set ExamineResult =" + ExamineResult + ",Remark =" + Remark + ",QCContent =" + QCContent + ",QCStandard =" + QCStandard + ",QCBound =" + QCBound + " where MaterialExamineID =" + MaterialExamineID);
        }

        public bool updPaymentCycInfo(string PaymCycRecordID, string FundName, string TaskId, string SpaceDay, string PayCondition2, string PaymentScale, string Remark)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_PaymentCyc set FundName =" + FundName + ",TaskId =" + TaskId + ",SpaceDay =" + SpaceDay + ",PayCondition2 ='" + PayCondition2 + "',PaymentScale =" + PaymentScale + ",Remark =" + Remark + " where PaymCycRecordID =" + PaymCycRecordID);
        }

        public bool updPaymentNoteInfo(string PaymentNoteID, string PaymentTime, string PaymentMoney, string Transactor, string Remark, string TaskId)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Cont_PaymentNote set PaymentTime ='" + PaymentTime + "',PaymentMoney =" + PaymentMoney + ",Transactor =" + Transactor + ",Remark =" + Remark + ",TaskId =" + TaskId + " where PaymentNoteID =" + PaymentNoteID);
        }
    }
}

