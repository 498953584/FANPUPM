namespace cn.justwin.contractDAL
{
    using cn.justwin.contractModel;
    using cn.justwin.DAL;
    using cn.justwin.stockDAL;
    using cn.justwin.XPMBasicContactCorp;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class IncometContract
    {
        public int Add(IncometContractModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into Con_Incomet_Contract(");
            builder.Append("ContractID,ContractCode,ContractName,ContractPrice,TypeID,Party,Second,SignedAddress,BalanceMode,PayMode,SignedTime,StartDate,EndDate,Annex,Subscriber,RegisterTime,Project,Remark,FCode,IsArchived,isFContract,FileTime,MainProvision,CllectionCondition,UserCodes,sign,flowState,SignPeople,QualityPeriod,ReFundDate,CParty)");
            builder.Append(" values (");
            builder.Append("@ContractID,@ContractCode,@ContractName,@ContractPrice,@TypeID,@Party,@Second,@SignedAddress,@BalanceMode,@PayMode,@SignedTime,@StartDate,@EndDate,@Annex,@Subscriber,@RegisterTime,@Project,@Remark,@FCode,@IsArchived,@isFContract,@FileTime,@MainProvision,@CllectionCondition,@UserCodes,@sign,@flowState,@SignPeople,@QualityPeriod,@ReFundDate,@CParty)");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@ContractCode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@ContractName", SqlDbType.NVarChar, 500),
                new SqlParameter("@ContractPrice", SqlDbType.Decimal, 9),
                new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Party", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Second", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@SignedAddress", SqlDbType.NVarChar, 500),
                new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@SignedTime", 4),
                new SqlParameter("@StartDate", 4),
                new SqlParameter("@EndDate", 4),
                new SqlParameter("@Annex", SqlDbType.NVarChar, 200),
                new SqlParameter("@Subscriber", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@RegisterTime", 4),
                new SqlParameter("@Project", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Remark", SqlDbType.NVarChar),
                new SqlParameter("@FCode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@IsArchived", SqlDbType.Bit, 1),
                new SqlParameter("@isFContract", SqlDbType.Bit, 1),
                new SqlParameter("@FileTime", 4),
                new SqlParameter("@MainProvision", 12),
                new SqlParameter("@CllectionCondition", SqlDbType.NVarChar, 200),
                new SqlParameter("@UserCodes", 12),
                new SqlParameter("@sign", 8),
                new SqlParameter("@flowState", 8),
                new SqlParameter("@SignPeople", 12),
                new SqlParameter("@QualityPeriod", 12),
                new SqlParameter("@ReFundDate", 4),
                new SqlParameter("@CParty", 12)
            };
            list[0].Value = model.ContractID;
            list[1].Value = model.ContractCode;
            list[2].Value = model.ContractName;
            list[3].Value = model.ContractPrice;
            list[4].Value = model.TypeID.TypeID;
            list[5].Value = model.Party.CorpID + "," + model.Party.CorpName;
            list[6].Value = model.Second;
            list[7].Value = model.SignedAddress;
            list[8].Value = model.BalanceMode;
            list[9].Value = model.PayMode;
            list[10].Value = model.SignedTime;
            list[11].Value = model.StartDate;
            list[12].Value = model.EndDate;
            list[13].Value = model.Annex;
            list[14].Value = model.Subscriber;
            list[15].Value = model.RegisterTime;
            list[0x10].Value = model.Project.PrjGuid.ToString();
            list[0x11].Value = model.Remark;
            list[0x12].Value = model.FCode;
            list[0x13].Value = model.IsArchived;
            list[20].Value = model.isFContract;
            list[0x15].Value = model.FileTime;
            list[0x16].Value = model.MainProvision;
            list[0x17].Value = model.CllectionCondition;
            list[0x18].Value = model.UserCodes;
            list[0x19].Value = model.Sign;
            list[0x1a].Value = model.FlowState;
            if (model.SignPepole != null)
            {
                list[0x1b].Value = model.SignPepole;
            }
            else
            {
                list[0x1b].Value = DBNull.Value;
            }
            list[0x1c].Value = model.QualityPeriod;
            if (model.ReFundDate.HasValue)
            {
                list[0x1d].Value = model.ReFundDate;
            }
            else
            {
                list[0x1d].Value = DBNull.Value;
            }
            list[30].Value = model.CParty;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public int Delete(SqlTransaction trans, string ContractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from Con_Incomet_Contract ");
            builder.Append(" where ContractID=@ContractID ");
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@ContractID", SqlDbType.NVarChar, 50) };
            commandParameters[0].Value = ContractID;
            return SqlHelper.ExecuteNonQuery(trans, CommandType.Text, builder.ToString(), commandParameters);
        }

        public int GetCountByParam(string strWhere, SqlParameter[] parms)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n\t        SELECT COUNT(*)\r\n\t        FROM   dbo.Con_Incomet_Contract AS p1 \r\n\t\t           LEFT JOIN dbo.PT_PrjInfo AS p2 \r\n\t\t\t         ON p1.project = p2.prjGuid \r\n                   LEFT JOIN PT_PrjInfo_ZTB \r\n                     ON p1.project =PT_PrjInfo_ZTB.prjGuid\r\n\t\t           INNER JOIN dbo.Con_ContractType AS p3 \r\n\t\t\t         ON p1.TypeId = p3.typeId \r\n\t\t           INNER JOIN dbo.XPM_Basic_CodeList AS p4 \r\n\t\t\t         ON p1.BalanceMode = p4.NoteID \r\n\t\t           INNER JOIN dbo.XPM_Basic_CodeList AS p5 \r\n\t\t\t         ON p1.PayMode = p5.NoteID \r\n\t\t           LEFT JOIN dbo.PT_yhmc \r\n\t\t\t         ON SignPeople = v_yhdm \r\n\t        WHERE  \r\n\t\t\t        p1.Project != '' \r\n\t\t\t        AND IsArchived = 'false'");
            builder.Append(strWhere);
            object obj2 = SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), parms);
            return ((obj2 == null) ? 0 : Convert.ToInt32(obj2.ToString()));
        }

        public DataTable GetInfoByParam(string contractId, string tbName, string userCodes, string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            if (tbName == "Con_Income_PaymentApply")
            {
                builder.Append("select *,ISNULL(v_xm,'') as SignPeopleName,p2.FlowState AS ApplyFlowState from dbo.Con_Incomet_Contract as p1");
            }
            else
            {
                builder.Append("select *,ISNULL(v_xm,'') as SignPeopleName from dbo.Con_Incomet_Contract as p1");
            }
            builder.Append(" inner join " + tbName + " as p2 on p1.contractId=p2.contractID ");
            builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
            builder.Append(" where p1.userCodes like '%" + userCodes + "%'");
            builder.Append(" and p2.contractId='" + contractId + "'");
            if (!string.IsNullOrEmpty(strWhere))
            {
                builder.Append(strWhere);
            }
            else
            {
                builder.Append(" and p1.IsArchived='false'");
            }
            builder.Append(" order by InputDate desc");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public List<IncometContractModel> GetListArray(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ContractID,ContractCode,ContractName,ContractPrice,TypeID,Party,Second,SignedAddress,BalanceMode,PayMode,\r\n                SignedTime,StartDate,EndDate,Annex,Subscriber,RegisterTime,Project,Remark,FCode,IsArchived,isFContract,FileTime,MainProvision,CllectionCondition,\r\n                UserCodes,sign,conState,SignPeople,QualityPeriod,ReFundDate,CParty ");
            builder.Append(" FROM Con_Incomet_Contract ");
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            List<IncometContractModel> list = new List<IncometContractModel>();
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[0]))
            {
                while (reader.Read())
                {
                    list.Add(this.ReaderBind(reader));
                }
            }
            return list;
        }

        public IncometContractModel GetModel(string ContractID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ContractID,ContractCode,ContractName,ContractPrice,TypeID,Party,Second,SignedAddress,BalanceMode,PayMode,SignedTime,\r\n            StartDate,EndDate,Annex,Subscriber,RegisterTime,Project,Remark,FCode,IsArchived,isFContract,FileTime,MainProvision,CllectionCondition,UserCodes,sign,conState,\r\n            SignPeople,QualityPeriod,ReFundDate,CParty from Con_Incomet_Contract ");
            builder.Append(" where ContractID=@ContractID ");
            IncometContractModel model = null;
            using (IDataReader reader = SqlHelper.ExecuteReader(CommandType.Text, builder.ToString(), new SqlParameter[] { new SqlParameter("@ContractID", ContractID) }))
            {
                if (reader.Read())
                {
                    model = this.ReaderBind(reader);
                }
            }
            return model;
        }

        public DataTable GetModifyDetail(string prjId, string sWhere, int pageSize, int pageIndex)
        {
            string cmdText = string.Format(string.Concat(new object[] { "SELECT TOP ", pageSize, " *  FROM(\r\n                           SELECT ROW_NUMBER() OVER(ORDER BY T.OrderNumber)AS NUM, T.TaskCode,T.TaskId,T.ModifyType,T.Unit,T.TaskName,\r\n                            ISNULL((T.Total/NULLIF(T.Quantity,0.0)),0.0) AS UnitPrice,\r\n                            (T.Quantity-SUM(M.Quantity))AS oQty,  --变更前工程量\r\n                            ISNULL(T.Total,0.0)-SUM(M.Total)AS oTotal,   --变更前小计\r\n                            T.Quantity AS CQuantity ,        --变更后工程量\r\n                            T.Total AS CTotal,              --变更后小计\r\n                            SUM(M.Quantity)AS Quantity,\r\n                            SUM(M.Total) AS Total\r\n                            FROM  Bud_ContractTask T\r\n                            RIGHT JOIN \r\n                            Bud_ConModifyTask M\r\n                            ON  T.TaskId=M.TaskId         \r\n                            WHERE T.PrjId='{0}' AND T.ModifyId!='' ", sWhere, "\r\n                            GROUP BY T.OrderNumber,T.TaskCode,T.TaskId,T.ModifyType,T.Unit,T.Quantity,T.Total,T.TaskName\r\n\t                       ) Task WHERE NUM>", pageSize * (pageIndex - 1) }), prjId);
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, null);
        }

        public DataTable GetMonthStatistics(string PrjID, string beginTime, string endTime, string sWhere)
        {
            string cmdText = "WITH Task AS\r\n                            (\r\n                            SELECT  ROW_NUMBER() OVER(ORDER BY T.OrderNumber)AS NUM, T.TaskCode,T.TaskId,T.ModifyType,T.TaskName,T.Unit,T.OrderNumber,\r\n                            (ISNULL(T.Total,0.0)/NULLIF(T.Quantity,0.0)) AS UnitPrice,\r\n                            dbo.fn_GetContractTaskCount(T.TaskId) AS SubCount,\r\n                            (T.Quantity-SUM(ISNULL(M.Quantity,0.0)))AS Quantity,  --变更前工程量\r\n                             ISNULL(T.Total,0.0)-SUM(ISNULL(M.Total,0.0))AS Total,   --变更前小计\r\n                            T.Quantity AS MQuantity ,        --变更后工程量 \r\n                            T.Total AS MTotal             --变更后小计\r\n                            FROM  Bud_ContractTask T\r\n                            LEFT JOIN \r\n                            Bud_ConModifyTask M\r\n                            ON  T.TaskId=M.TaskId\r\n                            WHERE T.PrjId=@prjId \r\n                            GROUP BY T.OrderNumber,T.TaskCode,T.TaskId,T.ModifyType,T.TaskName,T.Unit,T.Quantity,T.Total,T.OrderNumber\r\n                            ),MPT AS\r\n                            (\r\n                            SELECT RptId FROM Bud_ContractConsReport WHERE InputDate>@sDate AND InputDate<@eDate\r\n                            AND PrjId=@prjId AND IsValid=1\r\n                            ),MPI AS\r\n                            (\r\n                            SELECT SUM(Amount) AS cQty,TaskId FROM Bud_ContractConsTask  CTK\r\n                            JOIN MPT ON  CTK.RptId=MPT.RptId\r\n                            GROUP BY TaskId\r\n                            ),TPT AS\r\n                            (\r\n                            SELECT RptId FROM Bud_ContractConsReport WHERE PrjId=@prjId AND IsValid=1\r\n                            ),TPI AS\r\n                            (\r\n                            SELECT SUM(Amount) AS tQty,TaskId FROM Bud_ContractConsTask CTK\r\n                            JOIN TPT ON  CTK.RptId=TPT.RptId\r\n                            GROUP BY TaskId\r\n                            )\r\n                            SELECT Task.NUM,Task.TaskCode,Task.TaskId,Task.ModifyType,Task.TaskName,Task.Unit,Task.UnitPrice,\r\n                            Task.Quantity,Task.Total,Task.MQuantity,Task.MTotal,Task.OrderNumber,Task.SubCount,\r\n                            ISNULL(MPI.cQty,0.0) AS CAmount ,                                 --本月产值\r\n                            ISNULL(TPI.tQty,0.0) AS TAmount                                  --总产值\r\n                            FROM Task LEFT JOIN MPI\r\n                            ON Task.TaskId=MPI.TaskId\r\n                            LEFT JOIN TPI\r\n                            ON Task.TaskId=TPI.TaskId WHERE 1=1 " + sWhere;
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", PrjID), new SqlParameter("@sDate", beginTime), new SqlParameter("@eDate", endTime) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetMonthStatisticsTable(string PrjID, string beginTime, string endTime, string sWhere)
        {
            new DataTable();
            string cmdText = "SELECT T.*, \r\n\t\t\t\t\t\t\tISNULL(ISNULL(SUM(CMT.Quantity), 0.0)/NULLIF(POWER((SELECT COUNT(*)AS ReportCount FROM  Bud_ContractConsReport WHERE PrjId='2ca3e7cd-3c02-4528-a54d-26c4dafb7645'),2),0.0),0.0) + T.Quantity AS MQuantity,\t--变更后工程量\r\n\t\t                    ISNULL(ISNULL(SUM(CMT.Total), 0.0)/NULLIF(POWER((SELECT COUNT(*)AS ReportCount FROM  Bud_ContractConsReport WHERE PrjId='2ca3e7cd-3c02-4528-a54d-26c4dafb7645'),2),0.0),0.0) + T.Total AS MTotal,\t\t\t--变更后小计\r\n\t\t                    ISNULL(ISNULL(SUM(CCT.Amount), 0.0)/NULLIF((SELECT COUNT(*)AS ReportCount FROM  Bud_ContractConsReport WHERE PrjId='2ca3e7cd-3c02-4528-a54d-26c4dafb7645'),0.0),0.0) AS CAmount,\t\t\t\t\t--本月产值（元）\r\n\t\t                    ISNULL(ISNULL(SUM(CCT2.Amount), 0.0)/NULLIF((SELECT COUNT(*)AS ReportCount FROM  Bud_ContractConsReport WHERE PrjId='2ca3e7cd-3c02-4528-a54d-26c4dafb7645'),0.0),0.0) AS TAmount\t\t\t\t\t--累计完成产值（元）\r\n\t\t\t\t\t\t    FROM dbo.ufn_GetAllConTask(@prjId) AS T\r\n\t\t\t\t\t\t    LEFT JOIN Bud_ConModifyTask AS CMT ON CMT.TaskId = T.TaskId AND CMT.ModifyType = 1\r\n\t\t\t\t\t\t    LEFT JOIN Bud_ContractConsReport AS CCR ON CCR.PrjId = @prjId\r\n\t\t\t\t\t\t\tAND CCR.IsValid = 1 AND CCR.FlowState = 1 \r\n\t\t\t\t\t\t\tAND CCR.InputDate >= @sDate AND CCR.InputDate < @eDate\r\n\t\t\t\t\t\t    LEFT JOIN Bud_ContractConsTask AS CCT ON CCT.RptId = CCR.RptId\r\n\t\t\t\t\t\t\tAND CCT.TaskId = t.TaskId\r\n\t\t\t\t\t\t    LEFT JOIN Bud_ContractConsReport AS CCR2 ON CCR2.PrjId = @prjId \r\n\t\t\t\t\t\t\tAND CCR.IsValid = 1 AND CCR.FlowState = 1 \r\n\t\t\t\t\t\t    LEFT JOIN Bud_ContractConsTask AS CCT2 ON CCT2.RptId = CCR2.RptId AND CCT2.TaskId = t.TaskId\r\n                            WHERE 1=1 " + sWhere + "\r\n\t\t\t\t\t\t    GROUP BY T.No, T.ParentId, T.TaskId, T.OrderNumber, T.TaskCode,\r\n\t\t\t\t\t\t\tT.TaskName, T.Unit, T.Quantity, T.UnitPrice, T.StartDate, T.EndDate, \r\n\t\t\t\t\t\t\tT.Note, T.Total, T.ConstructionPeriod, T.FeatureDescription, T.ModifyType ";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@prjId", PrjID), new SqlParameter("@sDate", beginTime), new SqlParameter("@eDate", endTime) };
            return SqlHelper.ExecuteQuery(CommandType.Text, cmdText, commandParameters);
        }

        public DataTable GetReportTb(string contractType, string project, string contractCode, string contractName, string startTime, string endTime, string Party, string tbName, string strWhere, string prjTypeCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append(this.GetSqlByTbName(tbName));
            builder.Append(" where 1=1");
            builder.Append(" and p1.Project != '' and p1.FlowState=1");
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(contractType))
            {
                builder.Append(" and p2.TypeName like @typeId");
                list.Add(new SqlParameter("@typeId", "%" + contractType + "%"));
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append(" and isnull(p3.PrjName,PT_PrjInfo_ZTB.PrjName) LIKE @project");
                list.Add(new SqlParameter("@project", "%" + project + "%"));
            }
            if (!string.IsNullOrEmpty(Party))
            {
                builder.AppendFormat(" and p1.Party like '%{0}%'", Party);
            }
            if (!string.IsNullOrEmpty(contractCode))
            {
                builder.Append(" and p1.contractCode like @contractCode");
                list.Add(new SqlParameter("@contractCode", "%" + contractCode + "%"));
            }
            if (!string.IsNullOrEmpty(contractName))
            {
                builder.Append(" and p1.contractName like @contractName");
                list.Add(new SqlParameter("@contractName", "%" + contractName + "%"));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p1.SignedTime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p1.SignedTime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(prjTypeCode))
            {
                builder.Append(" and p3.PrjKindClass = @prjTypeCode");
                list.Add(new SqlParameter("@prjTypeCode", prjTypeCode));
            }
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.Append(" ) Y \n");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetReportTb(string contractType, string project, string contractCode, string contractName, string startTime, string endTime, string Party, string tbName, string strWhere, string prjTypeCode, int pageIndex, int pageSize)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) as Num,* FROM ( \n");
            builder.Append(this.GetSqlByTbName(tbName));
            builder.Append(" where 1=1");
            builder.Append(" and p1.Project != ''");
            List<SqlParameter> list = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(contractType))
            {
                builder.Append(" and p2.TypeName like @typeId");
                list.Add(new SqlParameter("@typeId", "%" + contractType + "%"));
            }
            if (!string.IsNullOrEmpty(project))
            {
                builder.Append("and p3.PrjName like @project");
                list.Add(new SqlParameter("@project", "%" + project + "%"));
            }
            if (!string.IsNullOrEmpty(Party))
            {
                builder.AppendFormat(" and p1.Party like '%{0}%'", Party);
            }
            if (!string.IsNullOrEmpty(contractCode))
            {
                builder.Append(" and p1.contractCode like @contractCode");
                list.Add(new SqlParameter("@contractCode", "%" + contractCode + "%"));
            }
            if (!string.IsNullOrEmpty(contractName))
            {
                builder.Append(" and p1.contractName like @contractName");
                list.Add(new SqlParameter("@contractName", "%" + contractName + "%"));
            }
            if (!string.IsNullOrEmpty(startTime))
            {
                builder.Append(" and p1.SignedTime>=@startTime");
                list.Add(new SqlParameter("@startTime", startTime));
            }
            if (!string.IsNullOrEmpty(endTime))
            {
                builder.Append(" and p1.SignedTime<=@endTime");
                list.Add(new SqlParameter("@endTime", endTime + " 23:59:59"));
            }
            if (!string.IsNullOrEmpty(prjTypeCode))
            {
                builder.Append(" and p3.PrjKindClass =@prjTypeCode");
                list.Add(new SqlParameter("@prjTypeCode", prjTypeCode));
            }
            if (strWhere.Trim() != "")
            {
                builder.Append(strWhere);
            }
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append(string.Concat(new object[] { "WHERE Num > (", pageIndex, "-1)*", pageSize, " \n" }));
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetSelectContract(string contractCode, string contractName, string userCode, string isAudit)
        {
            new DataTable();
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(contractCode))
            {
                builder.AppendFormat(" AND p1.contractCode LIKE '%{0}%' ", contractCode);
            }
            if (!string.IsNullOrEmpty(contractName))
            {
                builder.AppendFormat(" AND p1.contractName LIKE '%{0}%' ", contractName);
            }
            StringBuilder builder2 = new StringBuilder();
            builder2.AppendLine("select p1.*,case p1.isFContract when 1 then p1.ContractId when 0 then p1.Fcode+p1.ContractId end as Version, ");
            builder2.AppendLine("CASE p1.isFContract WHEN '1' THEN p1.RegisterTime WHEN '0' THEN ( SELECT c2.RegisterTime FROM  ");
            builder2.AppendLine("Con_Incomet_Contract c1 JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID WHERE  ");
            builder2.AppendLine("c1.ContractID = p1.ContractID) END AS Date ");
            builder2.AppendLine("from dbo.Con_Incomet_Contract as p1  ");
            builder2.AppendFormat("where p1.UserCodes like '%{0}%' ", userCode).AppendLine();
            if (isAudit == "1")
            {
                builder2.AppendFormat(" AND p1.FlowState=1 ", new object[0]);
            }
            builder2.AppendLine(builder.ToString());
            builder2.AppendLine(" ORDER BY Date DESC, Version ASC  ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder2.ToString(), null);
        }

        public StringBuilder GetSqlByTbName(string tbName)
        {
            StringBuilder builder = new StringBuilder();
            if (tbName == "Balance")
            {
                builder.Append("select p1.ContractCode,p1.isFContract,p1.conState,ISNULL(v_xm,'') as SignPeopleName,ISNULL(p3.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,Convert(NVARCHAR(10),p1.SignedTime,121) AS SignedTime,p1.ContractName,p1.ContractId,p1.FCode,");
                builder.Append(" (p1.ContractPrice+isnull((select sum(ChangePrice) from ");
                builder.Append(" dbo.Con_Incomet_Modify where ContractId=p1.ContractId),0))");
                builder.Append(" as EndPrice, p2.TypeName,p1.Party,isnull((");
                builder.Append(" select sum(ClearingPrice) from dbo.Con_Incomet_Balance");
                builder.Append(" where ContractId=p1.ContractId),0) as BalancePrice");
                builder.Append(",case p1.isFContract");
                builder.Append(" when 1 then p1.ContractId");
                builder.Append(" when 0 then p1.Fcode+p1.ContractId");
                builder.Append(" end as Version,");
                builder.Append(" CASE p1.isFContract");
                builder.Append(" WHEN '1' THEN p1.RegisterTime");
                builder.Append(" WHEN '0' THEN (");
                builder.Append(" SELECT c2.RegisterTime FROM Con_Incomet_Contract c1");
                builder.Append(" JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID");
                builder.Append(" WHERE c1.ContractID = p1.ContractID) END AS Date,PrjType.CodeName");
                builder.Append(" from dbo.Con_Incomet_Contract as p1 inner join dbo.Con_ContractType");
                builder.Append(" as p2 on p1.TypeID=p2.TypeID");
                builder.Append(" LEFT JOIN dbo.Pt_prjInfo as p3 on p1.project=p3.prjGuid");
                builder.Append(" LEFT JOIN PT_PrjInfo_ZTB ON PT_PrjInfo_ZTB.prjGuid=p1.project");
                builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
                builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
                builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
                builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
                builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON p1.project = PrjType.PrjGuid ").AppendLine();
                return builder;
            }
            if (tbName == "Payment")
            {
                builder.Append("select p1.ContractCode,p1.isFContract,p1.conState,ISNULL(v_xm,'') as SignPeopleName,ISNULL(p3.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,Convert(NVARCHAR(10),p1.SignedTime,121) AS SignedTime,p1.ContractName,p1.ContractId,p1.FCode,");
                builder.Append(" (p1.ContractPrice+isnull((select sum(ChangePrice) from ");
                builder.Append(" dbo.Con_Incomet_Modify where ContractId=p1.ContractId),0))");
                builder.Append(" as EndPrice, p2.TypeName,p1.Party,isnull((");
                builder.Append(" select sum(CllectionPrice) from dbo.Con_Incomet_Payment");
                builder.Append(" where ContractId=p1.ContractId),0) as CllectionPrice");
                builder.Append(",case p1.isFContract");
                builder.Append(" when 1 then p1.ContractId");
                builder.Append(" when 0 then p1.Fcode+p1.ContractId");
                builder.Append(" end as Version,");
                builder.Append(" CASE p1.isFContract");
                builder.Append(" WHEN '1' THEN p1.RegisterTime");
                builder.Append(" WHEN '0' THEN (");
                builder.Append(" SELECT c2.RegisterTime FROM Con_Incomet_Contract c1");
                builder.Append(" JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID");
                builder.Append(" WHERE c1.ContractID = p1.ContractID) END AS Date,PrjType.CodeName");
                builder.Append(" from dbo.Con_Incomet_Contract as p1 inner join dbo.Con_ContractType");
                builder.Append(" as p2 on p1.TypeID=p2.TypeID");
                builder.Append(" left join dbo.Pt_prjInfo as p3 on p1.project=p3.prjGuid");
                builder.Append(" LEFT JOIN PT_PrjInfo_ZTB ON PT_PrjInfo_ZTB.prjGuid=p1.project");
                builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
                builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
                builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
                builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
                builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON p1.project = PrjType.PrjGuid ").AppendLine();
                return builder;
            }
            if (tbName == "Invoice")
            {
                builder.Append("select Notes,p5.CodeName,p1.ContractCode,p1.isFContract,p1.conState,ISNULL(v_xm,'') as SignPeopleName,ISNULL(p3.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,Convert(NVARCHAR(10),p1.SignedTime,121) AS SignedTime,p1.ContractName,p1.ContractId,p1.FCode,");
                builder.Append(" (p1.ContractPrice+isnull((select sum(ChangePrice) from ");
                builder.Append(" dbo.Con_Incomet_Modify where ContractId=p1.ContractId),0))");
                builder.Append(" as EndPrice, p2.TypeName,p1.Party,isnull((");
                builder.Append(" select sum(amount) from dbo.Con_Incomet_Invoice");
                builder.Append(" where ContractId=p1.ContractId),0) as InvoicePrice");
                builder.Append(",case p1.isFContract");
                builder.Append(" when 1 then p1.ContractId");
                builder.Append(" when 0 then p1.Fcode+p1.ContractId");
                builder.Append(" end as Version,");
                builder.Append(" CASE p1.isFContract");
                builder.Append(" WHEN '1' THEN p1.RegisterTime");
                builder.Append(" WHEN '0' THEN (");
                builder.Append(" SELECT c2.RegisterTime FROM Con_Incomet_Contract c1");
                builder.Append(" JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID");
                builder.Append(" WHERE c1.ContractID = p1.ContractID) END AS Date,PrjType.prjTypeName");
                builder.Append(" from dbo.Con_Incomet_Contract as p1 inner join dbo.Con_ContractType");
                builder.Append(" as p2 on p1.TypeID=p2.TypeID");
                builder.Append(" left join dbo.Pt_prjInfo as p3 on p1.project=p3.prjGuid");
                builder.Append(" LEFT JOIN PT_PrjInfo_ZTB ON PT_PrjInfo_ZTB.prjGuid=p1.project");
                builder.Append(" inner join dbo.XPM_Basic_CodeList as p5 on p1.PayMode=p5.NoteID");
                builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
                builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
                builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
                builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') prjTypeName ").AppendLine();
                builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON p1.project = PrjType.PrjGuid ").AppendLine();
                return builder;
            }
            builder.Append("select p1.ContractCode,p1.isFContract,p1.Fcode,p1.ContractID,p1.ContractName,ISNULL(v_xm,'') as SignPeopleName,p1.ContractPrice,p1.conState,Convert(NVARCHAR(10),p1.SignedTime,121) AS SignedTime,");
            builder.Append(" ISNULL(p3.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,(p1.ContractPrice+isnull((select sum(ChangePrice) from ");
            builder.Append(" dbo.Con_Incomet_Modify where ContractId=p1.ContractId),0))");
            builder.Append("  as EndPrice, p2.TypeName,p1.Party,");
            builder.Append(" isnull((select sum(ChangePrice) from dbo.Con_Incomet_Modify ");
            builder.Append(" where ContractId=p1.ContractId),0) as ModifyPrice,");
            builder.Append(" isnull((select sum(ClearingPrice) from dbo.Con_Incomet_Balance ");
            builder.Append(" where ContractId=p1.ContractId),0) as BalancePrice,");
            builder.Append(" isnull((select sum(CllectionPrice) from dbo.Con_Incomet_Payment");
            builder.Append(" where ContractId=p1.ContractId),0) as PaymentPrice, ");
            builder.Append(" isnull((select sum(amount) from dbo.Con_Incomet_Invoice");
            builder.Append(" where ContractId=p1.ContractId),0) as InvoicePrice ");
            builder.Append(",case p1.isFContract");
            builder.Append(" when 1 then p1.ContractId");
            builder.Append(" when 0 then p1.Fcode+p1.ContractId");
            builder.Append(" end as Version,");
            builder.Append(" CASE p1.isFContract");
            builder.Append(" WHEN '1' THEN p1.RegisterTime");
            builder.Append(" WHEN '0' THEN (");
            builder.Append(" SELECT c2.RegisterTime FROM Con_Incomet_Contract c1");
            builder.Append(" JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID");
            builder.Append(" WHERE c1.ContractID = p1.ContractID) END AS Date,PrjType.CodeName");
            builder.Append(" from dbo.Con_Incomet_Contract as p1");
            builder.Append(" inner join dbo.Con_ContractType as p2 on p1.TypeID=p2.TypeID");
            builder.Append(" left join dbo.Pt_prjInfo as p3 on p1.project=p3.prjGuid");
            builder.Append(" LEFT JOIN PT_PrjInfo_ZTB ON PT_PrjInfo_ZTB.prjGuid=p1.project");
            builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
            builder.Append(" LEFT JOIN( SELECT prjGuid,STUFF((select ','+[CodeName] FROM PT_PrjInfo_Kind ").AppendLine();
            builder.Append(" LEFT JOIN XPM_Basic_CodeList on XPM_Basic_CodeList.CodeID=PT_PrjInfo_Kind.PrjKind ").AppendLine();
            builder.Append("WHERE PrjGuid=PrjInfo_PrjKind.PrjGuid AND isvalid=1 AND TypeID=(SELECT TypeID FROM XPM_Basic_CodeType WHERE SignCode='ProjectType') FOR XML PATH('')),1,1,'') CodeName ").AppendLine();
            builder.Append(" FROM PT_PrjInfo_Kind as PrjInfo_PrjKind GROUP by[PrjGuid] ) PrjType ON p1.project = PrjType.PrjGuid ").AppendLine();
            return builder;
        }

        public DataTable GetTbByParam(string strWhere, SqlParameter[] parms, int pageSize, int pageNo)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("\r\n            WITH cteContract AS(\r\n\t            SELECT p1.*, \r\n\t\t               Isnull(v_xm, '') AS SignPeopleName, \r\n\t\t               ISNULL(p2.prjName,PT_PrjInfo_ZTB.prjName) AS prjName,\r\n\t\t               p3.TypeName, \r\n\t\t               p4.CodeName      AS BMode, \r\n\t\t               CASE p1.isFContract \r\n\t\t\t             WHEN 1 THEN p1.ContractId \r\n\t\t\t             WHEN 0 THEN p1.Fcode + p1.ContractId \r\n\t\t               END              AS Version, \r\n\t\t               CASE p1.isFContract \r\n\t\t\t             WHEN '1' THEN p1.RegisterTime \r\n\t\t\t             WHEN '0' THEN (SELECT c2.RegisterTime \r\n\t\t\t\t\t\t\t            FROM   Con_Incomet_Contract c1 \r\n\t\t\t\t\t\t\t\t               JOIN Con_Incomet_Contract c2 \r\n\t\t\t\t\t\t\t\t\t             ON c1.FCode = c2.ContractID \r\n\t\t\t\t\t\t\t            WHERE  c1.ContractID = p1.ContractID) \r\n\t\t               END              AS Date, \r\n\t\t               p5.CodeName      AS PMode\r\n\t            FROM   dbo.Con_Incomet_Contract AS p1 \r\n\t\t               LEFT JOIN dbo.PT_PrjInfo AS p2 \r\n\t\t\t             ON p1.project = p2.prjGuid \r\n                       LEFT JOIN dbo.PT_PrjInfo_ZTB  \r\n\t\t\t             ON p1.project = PT_PrjInfo_ZTB.prjGuid \r\n\t\t               INNER JOIN dbo.Con_ContractType AS p3 \r\n\t\t\t             ON p1.TypeId = p3.typeId \r\n\t\t               INNER JOIN dbo.XPM_Basic_CodeList AS p4 \r\n\t\t\t             ON p1.BalanceMode = p4.NoteID \r\n\t\t               INNER JOIN dbo.XPM_Basic_CodeList AS p5 \r\n\t\t\t             ON p1.PayMode = p5.NoteID \r\n\t\t               LEFT JOIN dbo.PT_yhmc \r\n\t\t\t             ON SignPeople = v_yhdm \r\n\t            WHERE  \r\n\t\t\t            p1.Project != '' \r\n\t\t\t            AND IsArchived = 'false' ");
            builder.Append(strWhere);
            int num = ((pageNo - 1) * pageSize) + 1;
            int num2 = pageNo * pageSize;
            builder.AppendFormat("\r\n            ),cteSortData AS(\r\n\t            SELECT ROW_NUMBER() OVER(ORDER  BY Date DESC,  Version ASC) AS No,* \r\n\t            FROM cteContract \r\n            )\r\n            SELECT * FROM cteSortData WHERE No BETWEEN {0} AND {1}", num, num2);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), parms);
        }

        public DataTable GetTbByParam(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string projectName, string userCodes, string strWhere)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("select p1.*,ISNULL(v_xm,'') as SignPeopleName,(ISNULL(p1.ContractPrice,0.000)+ISNULL(ConModify.ChangePrice,0.00)) AS Price,ISNULL(p2.prjName,PT_PrjInfo_ZTB.PrjName) AS prjName,p3.TypeName,p4.CodeName as BMode,");
            builder.Append(" case p1.isFContract");
            builder.Append(" when 1 then p1.ContractId");
            builder.Append(" when 0 then p1.Fcode+p1.ContractId");
            builder.Append(" end as Version,");
            builder.Append(" CASE p1.isFContract");
            builder.Append(" WHEN '1' THEN p1.RegisterTime");
            builder.Append(" WHEN '0' THEN (");
            builder.Append(" SELECT c2.RegisterTime FROM Con_Incomet_Contract c1");
            builder.Append(" JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID");
            builder.Append(" WHERE c1.ContractID = p1.ContractID) END AS Date,");
            builder.Append(" p5.CodeName as PMode,conState from dbo.Con_Incomet_Contract as p1");
            builder.Append(" LEFT join dbo.PT_PrjInfo as p2 on p1.project=p2.prjGuid");
            builder.Append(" LEFT JOIN PT_PrjInfo_ZTB on p1.project = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append(" LEFT join dbo.Con_ContractType as p3 on p1.TypeId=p3.typeId");
            builder.Append(" LEFT join dbo.XPM_Basic_CodeList as p4 on p1.BalanceMode=p4.NoteID");
            builder.Append(" LEFT join dbo.XPM_Basic_CodeList as p5 on p1.PayMode=p5.NoteID");
            builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
            builder.Append(" LEFT JOIN (SELECT sum(ChangePrice) AS ChangePrice,ContractID FROM Con_Incomet_Modify GROUP BY ContractID) ConModify ON ConModify.ContractID=p1.ContractID");
            builder.Append(" where p1.contractCode like @contractCode ");
            builder.Append(" and p1.contractName like @contractName ");
            builder.Append(" and p1.userCodes like @userCodes");
            builder.Append(" and p1.Project != ''");
            builder.Append(strWhere);
            list.Add(new SqlParameter("@contractCode", "%" + contractCode + "%"));
            list.Add(new SqlParameter("@contractName", "%" + contractName + "%"));
            list.Add(new SqlParameter("@userCodes", "%" + userCodes + "%"));
            if (!string.IsNullOrEmpty(type))
            {
                builder.Append(" and p3.TypeName like @type");
                list.Add(new SqlParameter("@type", "%" + type + "%"));
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                builder.Append(" and p2.PrjName  LIKE @projectName");
                list.Add(new SqlParameter("@projectName", "%" + projectName + "%"));
            }
            if (!string.IsNullOrEmpty(startSignedTime))
            {
                builder.Append(" and p1.SignedTime >=@startSignedTime");
                list.Add(new SqlParameter("@startSignedTime", startSignedTime));
            }
            if (!string.IsNullOrEmpty(endSignedTime))
            {
                builder.Append(" and p1.SignedTime<=@endSignedTime");
                list.Add(new SqlParameter("@endSignedTime", endSignedTime));
            }
            if (!string.IsNullOrEmpty(startContractPrice))
            {
                startContractPrice = startContractPrice.Replace(",", "");
                builder.Append(" and ISNULL(p1.ContractPrice,0.00) + ISNULL(ConModify.ChangePrice,0.00) >=@startContractPrice");
                list.Add(new SqlParameter("@startContractPrice", startContractPrice));
            }
            if (!string.IsNullOrEmpty(endContractPrice))
            {
                endContractPrice = endContractPrice.Replace(",", "");
                builder.Append(" and ISNULL(p1.ContractPrice,0.00) + ISNULL(ConModify.ChangePrice,0.00) <=@endContractPrice");
                list.Add(new SqlParameter("@endContractPrice", endContractPrice));
            }
            builder.Append(" ORDER BY Date DESC, Version ASC ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public DataTable GetTbByParam(string contractCode, string contractName, string type, string startSignedTime, string endSignedTime, string startContractPrice, string endContractPrice, string projectName, string userCodes, string strWhere, int pageIndex, int pageSize)
        {
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            builder.Append("SELECT TOP (" + pageSize + ") * FROM \n");
            builder.Append("( \n");
            builder.Append("SELECT Row_Number() OVER (ORDER BY Date DESC, Version ASC) AS Num,* FROM (");
            builder.Append("select p1.*,ISNULL(v_xm,'') as SignPeopleName,(ISNULL(p1.ContractPrice,0.000)+ISNULL(ConModify.ChangePrice,0.00)) AS Price,ISNULL(p2.prjName,PT_PrjInfo_ZTB.PrjName) AS prjName,p3.TypeName,p4.CodeName as BMode,");
            builder.Append(" case p1.isFContract");
            builder.Append(" when 1 then p1.ContractId");
            builder.Append(" when 0 then p1.Fcode+p1.ContractId");
            builder.Append(" end as Version,");
            builder.Append(" CASE p1.isFContract");
            builder.Append(" WHEN '1' THEN p1.RegisterTime");
            builder.Append(" WHEN '0' THEN (");
            builder.Append(" SELECT c2.RegisterTime FROM Con_Incomet_Contract c1");
            builder.Append(" JOIN Con_Incomet_Contract c2 ON c1.FCode = c2.ContractID");
            builder.Append(" WHERE c1.ContractID = p1.ContractID) END AS Date,");
            builder.Append(" p5.CodeName as PMode,conState as state from dbo.Con_Incomet_Contract as p1");
            builder.Append(" LEFT join dbo.PT_PrjInfo as p2 on p1.project=p2.prjGuid");
            builder.Append(" LEFT JOIN PT_PrjInfo_ZTB on p1.project = PT_PrjInfo_ZTB.PrjGuid").AppendLine();
            builder.Append(" LEFT join dbo.Con_ContractType as p3 on p1.TypeId=p3.typeId");
            builder.Append(" LEFT join dbo.XPM_Basic_CodeList as p4 on p1.BalanceMode=p4.NoteID");
            builder.Append(" LEFT join dbo.XPM_Basic_CodeList as p5 on p1.PayMode=p5.NoteID");
            builder.Append(" LEFT JOIN dbo.PT_yhmc ON SignPeople=v_yhdm ");
            builder.Append(" LEFT JOIN (SELECT sum(ChangePrice) AS ChangePrice,ContractID FROM Con_Incomet_Modify GROUP BY ContractID) ConModify ON ConModify.ContractID=p1.ContractID");
            builder.Append(" where p1.contractCode like @contractCode ");
            builder.Append(" and p1.contractName like @contractName ");
            builder.Append(" and p1.userCodes like @userCodes");
            builder.Append(" and p1.Project != ''");
            builder.Append(strWhere);
            list.Add(new SqlParameter("@contractCode", "%" + contractCode + "%"));
            list.Add(new SqlParameter("@contractName", "%" + contractName + "%"));
            list.Add(new SqlParameter("@userCodes", "%" + userCodes + "%"));
            if (!string.IsNullOrEmpty(type))
            {
                builder.Append(" and p3.TypeName like @type");
                list.Add(new SqlParameter("@type", "%" + type + "%"));
            }
            if (!string.IsNullOrEmpty(projectName))
            {
                builder.Append(" and p2.PrjName LIKE @projectName");
                list.Add(new SqlParameter("@projectName", "%" + projectName + "%"));
            }
            if (!string.IsNullOrEmpty(startSignedTime))
            {
                builder.Append(" and p1.SignedTime >=@startSignedTime");
                list.Add(new SqlParameter("@startSignedTime", startSignedTime));
            }
            if (!string.IsNullOrEmpty(endSignedTime))
            {
                builder.Append(" and p1.SignedTime<=@endSignedTime");
                list.Add(new SqlParameter("@endSignedTime", endSignedTime));
            }
            if (!string.IsNullOrEmpty(startContractPrice))
            {
                startContractPrice = startContractPrice.Replace(",", "");
                builder.Append(" and ISNULL(p1.ContractPrice,0.00) + ISNULL(ConModify.ChangePrice,0.00) >=@startContractPrice");
                list.Add(new SqlParameter("@startContractPrice", startContractPrice));
            }
            if (!string.IsNullOrEmpty(endContractPrice))
            {
                endContractPrice = endContractPrice.Replace(",", "");
                builder.Append(" and ISNULL(p1.ContractPrice,0.00) + ISNULL(ConModify.ChangePrice,0.00) <=@endContractPrice");
                list.Add(new SqlParameter("@endContractPrice", endContractPrice));
            }
            builder.Append(") Y \n");
            builder.Append(") T \n");
            builder.Append("WHERE Num >'" + ((pageIndex - 1) * pageSize) + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }

        public bool IsDel(string ContractID)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@contractId", SqlDbType.NVarChar, 0x40), new SqlParameter("@i_Ret", SqlDbType.Int) };
            commandParameters[0].Value = ContractID;
            commandParameters[1].Direction = ParameterDirection.Output;
            SqlHelper.ExecuteNonQuery(CommandType.StoredProcedure, "p_contract_existchild", commandParameters);
            if (!string.IsNullOrEmpty(commandParameters[1].Value.ToString()))
            {
                return false;
            }
            return true;
        }

        public IncometContractModel ReaderBind(IDataReader dataReader)
        {
            IncometContractModel model = new IncometContractModel {
                ContractID = dataReader["ContractID"].ToString(),
                ContractCode = dataReader["ContractCode"].ToString(),
                ContractName = dataReader["ContractName"].ToString()
            };
            object obj2 = dataReader["ContractPrice"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ContractPrice = new decimal?((decimal) obj2);
            }
            string[] strArray = dataReader["Party"].ToString().Split(new char[] { ',' });
            model.TypeID = new ContractType().GetModel(dataReader["TypeID"].ToString());
            model.Party = new BasicContactCorp().GetModel(Convert.ToInt32(strArray[0]));
            model.Second = dataReader["Second"].ToString();
            model.SignedAddress = dataReader["SignedAddress"].ToString();
            model.BalanceMode = dataReader["BalanceMode"].ToString();
            model.PayMode = dataReader["PayMode"].ToString();
            obj2 = dataReader["SignedTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.SignedTime = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["StartDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.StartDate = new DateTime?((DateTime) obj2);
            }
            obj2 = dataReader["EndDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.EndDate = new DateTime?((DateTime) obj2);
            }
            model.Annex = dataReader["Annex"].ToString();
            model.Subscriber = dataReader["Subscriber"].ToString();
            obj2 = dataReader["RegisterTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.RegisterTime = new DateTime?((DateTime) obj2);
            }
            model.Project = new PrjInfo().GetModelByPrjGuid(dataReader["Project"].ToString());
            model.Remark = dataReader["Remark"].ToString();
            model.FCode = dataReader["FCode"].ToString();
            obj2 = dataReader["IsArchived"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.IsArchived = (bool) obj2;
            }
            obj2 = dataReader["isFContract"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.isFContract = (bool) obj2;
            }
            obj2 = dataReader["FileTime"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.FileTime = new DateTime?((DateTime) obj2);
            }
            model.MainProvision = dataReader["MainProvision"].ToString();
            model.CllectionCondition = dataReader["CllectionCondition"].ToString();
            model.UserCodes = dataReader["UserCodes"].ToString();
            if (dataReader["sign"].ToString() != "")
            {
                model.Sign = Convert.ToInt32(dataReader["sign"].ToString());
            }
            else
            {
                model.Sign = -1;
            }
            model.ConState = Convert.ToInt32(dataReader["conState"]);
            model.SignPepole = dataReader["SignPeople"].ToString();
            model.QualityPeriod = dataReader["QualityPeriod"].ToString();
            obj2 = dataReader["ReFundDate"];
            if ((obj2 != null) && (obj2 != DBNull.Value))
            {
                model.ReFundDate = new DateTime?((DateTime) obj2);
            }
            model.CParty = dataReader["CParty"].ToString();
            return model;
        }

        public void UpConState(string sql, SqlTransaction trans)
        {
            SqlParameter[] commandParameters = null;
            SqlHelper.ExecuteNonQuery(trans, CommandType.Text, sql, commandParameters);
        }

        public int Update(IncometContractModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update Con_Incomet_Contract set ");
            builder.Append("ContractCode=@ContractCode,");
            builder.Append("ContractName=@ContractName,");
            builder.Append("ContractPrice=@ContractPrice,");
            builder.Append("TypeID=@TypeID,");
            builder.Append("Party=@Party,");
            builder.Append("Second=@Second,");
            builder.Append("SignedAddress=@SignedAddress,");
            builder.Append("BalanceMode=@BalanceMode,");
            builder.Append("PayMode=@PayMode,");
            builder.Append("SignedTime=@SignedTime,");
            builder.Append("StartDate=@StartDate,");
            builder.Append("EndDate=@EndDate,");
            builder.Append("Annex=@Annex,");
            builder.Append("Subscriber=@Subscriber,");
            builder.Append("RegisterTime=@RegisterTime,");
            builder.Append("Project=@Project,");
            builder.Append("Remark=@Remark,");
            builder.Append("FCode=@FCode,");
            builder.Append("IsArchived=@IsArchived,");
            builder.Append("isFContract=@isFContract,");
            builder.Append("FileTime=@FileTime,");
            builder.Append("MainProvision=@MainProvision,");
            builder.Append("CllectionCondition=@CllectionCondition,");
            builder.Append("UserCodes=@UserCodes,");
            builder.Append("QualityPeriod=@QualityPeriod,");
            builder.Append("ReFundDate=@ReFundDate,");
            builder.Append("sign=@sign,");
            builder.Append("SignPeople=@SignPeople, ");
            builder.Append("CParty=@CParty ");
            builder.Append(" where ContractID=@ContractID ");
            List<SqlParameter> list = new List<SqlParameter> {
                new SqlParameter("@ContractID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@ContractCode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@ContractName", SqlDbType.NVarChar, 500),
                new SqlParameter("@ContractPrice", SqlDbType.Decimal, 9),
                new SqlParameter("@TypeID", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Party", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Second", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@SignedAddress", SqlDbType.NVarChar, 500),
                new SqlParameter("@BalanceMode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@PayMode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@SignedTime", 4),
                new SqlParameter("@StartDate", 4),
                new SqlParameter("@EndDate", 4),
                new SqlParameter("@Annex", SqlDbType.NVarChar, 200),
                new SqlParameter("@Subscriber", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@RegisterTime", 4),
                new SqlParameter("@Project", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@Remark", SqlDbType.NVarChar),
                new SqlParameter("@FCode", SqlDbType.NVarChar, 0x40),
                new SqlParameter("@IsArchived", SqlDbType.Bit, 1),
                new SqlParameter("@isFContract", SqlDbType.Bit, 1),
                new SqlParameter("@FileTime", 4),
                new SqlParameter("@MainProvision", 12),
                new SqlParameter("@CllectionCondition", SqlDbType.NVarChar, 200),
                new SqlParameter("@UserCodes", 12),
                new SqlParameter("@sign", 8),
                new SqlParameter("@SignPeople", 12),
                new SqlParameter("@QualityPeriod", 12),
                new SqlParameter("@ReFundDate", 4),
                new SqlParameter("@CParty", 12)
            };
            list[0].Value = model.ContractID;
            list[1].Value = model.ContractCode;
            list[2].Value = model.ContractName;
            list[3].Value = model.ContractPrice;
            list[4].Value = model.TypeID.TypeID;
            list[5].Value = model.Party.CorpID + "," + model.Party.CorpName;
            list[6].Value = model.Second;
            list[7].Value = model.SignedAddress;
            list[8].Value = model.BalanceMode;
            list[9].Value = model.PayMode;
            list[10].Value = model.SignedTime;
            list[11].Value = model.StartDate;
            list[12].Value = model.EndDate;
            list[13].Value = model.Annex;
            list[14].Value = model.Subscriber;
            list[15].Value = model.RegisterTime;
            list[0x10].Value = model.Project.PrjGuid.ToString();
            list[0x11].Value = model.Remark;
            list[0x12].Value = model.FCode;
            list[0x13].Value = model.IsArchived;
            list[20].Value = model.isFContract;
            list[0x15].Value = model.FileTime;
            list[0x16].Value = model.MainProvision;
            list[0x17].Value = model.CllectionCondition;
            list[0x18].Value = model.UserCodes;
            list[0x19].Value = model.Sign;
            if (model.SignPepole != null)
            {
                list[0x1a].Value = model.SignPepole;
            }
            else
            {
                list[0x1a].Value = DBNull.Value;
            }
            list[0x1b].Value = model.QualityPeriod;
            if (model.ReFundDate.HasValue)
            {
                list[0x1c].Value = model.ReFundDate;
            }
            else
            {
                list[0x1c].Value = DBNull.Value;
            }
            list[0x1d].Value = model.CParty;
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), list.ToArray());
        }
    }
}

