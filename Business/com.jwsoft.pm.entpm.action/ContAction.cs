namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ContAction
    {
        public int AddContract(ContractMainInfo ci)
        {
            string str = "";
            object obj2 = str + " begin " + " insert into EPM_Con_ContractMain(ProjectCode, ContractType, ContractCode, ContractName,   SignAddr, SignDate,  PayMode,  SumMoney, Begindate, EndDate, WorkDay, MainWork,QualitySafety, Remark,   ContractOwner, ContractOwnerAddr, ContractOwnerMan,  ContractOwnerSpokesman,ContractOwnerTel, ContractOwnerFax,   ContractOwnerZipcode, ContractOwnerBack, ContractOwnerAccount,ContractOther,  ContractOtherAddr, ContractOtherMan, ContractOtherSpokesman,  ContractOtherTel,  ContractOtherFax, ContractOtherZipcode,  ContractOtherBack, ContractOtherAccount,FromType) ";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { 
                obj2, " values('", ci.ProjectCode.ToString(), "','", ci.ContractType.ToString(), "','", ci.ContractCode, "','", ci.ContractName, "', '", ci.SignAddr.ToString(), "',", (ci.SignDate == DateTime.MinValue) ? "null" : ("'" + ci.SignDate.ToShortDateString() + "'"), ", '", ci.PayMode, "',", 
                Convert.ToInt32(ci.SumMoney.ToString()).ToString(), ",", (ci.BeginDate == DateTime.MinValue) ? "null" : ("'" + ci.BeginDate.ToShortDateString() + "'"), ",", (ci.EndDate == DateTime.MinValue) ? "null" : ("'" + ci.EndDate.ToShortDateString() + "'"), ",    ", ci.WorkDay, ",'", ci.MainWork, "','", ci.QualitySafety, "','", ci.Remark, "','", ci.ContractOwner, "','", 
                ci.ContractOwnerAddr, "','", ci.ContractOwnerMan, "', '", ci.ContractOwnerSpokesman, "','", ci.ContractOwnerTel, "','", ci.ContractOwnerFax, "', '", ci.ContractOwnerZipcode, "','", ci.ContractOwnerBack, "','", ci.ContractOwnerAccount, "', '", 
                ci.ContractOther, "','", ci.ContractOtherAddr, "','", ci.ContractOtherMan, "', '", ci.ContractOtherSpokesman, "','", ci.ContractOtherTel, "','", ci.ContractOtherFax, "', '", ci.ContractOtherZipcode, "','", ci.ContractOtherBack, "','", 
                ci.ContractOtherAccount, "',", ci.FromType, ") "
             }) + " end");
        }

        public int AddContractAskPay(string prjcode, string ContractType, string contractcode)
        {
            string str2 = (" begin " + "delete from EPM_Con_ContractAskPay where contractcode='" + contractcode + "' and (AskPayCode is null or AskPayCode='');") + "INSERT INTO EPM_Con_ContractAskPay ( ProjectCode, ContractType, ContractCode )";
            return publicDbOpClass.ExecSqlString((str2 + "values('" + prjcode + "','" + ContractType + "', '" + contractcode + "') ") + " end ");
        }

        public static bool AddContractCount(ContractCountInfo ci, string taskCodes)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            str = str2 + " insert into EPM_Con_ContractCount values('" + ci.RecordID.ToString() + "','" + ci.ProjectCode.ToString() + "','" + ci.ContractCode.ToString() + "','" + ci.CountDate.ToString() + "','" + ci.ReportUser.ToString() + "','" + ci.Remark + "') ";
            if (taskCodes.Length > 0)
            {
                string str3 = str + " insert into EPM_Con_ContractCountDetail(CountRecordID,TaskCode,TaskName,QuantityUnit,Quantity) ";
                str = str3 + " select '" + ci.RecordID.ToString() + "',TaskCode,TaskName,QuantityUnit,isnull(quantity,0) from EPM_Task_TaskList where projectcode='" + ci.ProjectCode.ToString() + "' and taskcode in (" + taskCodes + ") ";
            }
            return publicDbOpClass.NonQuerySqlString(str + " end ");
        }

        public int AddContractStockBill(ContractStockBillInfo ci)
        {
            string str = "";
            object obj2 = str + "INSERT INTO EPM_Con_ContractStockBill ( ProjectCode, ContractType, ContractCode , ResourceCode,ResourceName,Specification,Unit,Price)";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { 
                obj2, "values('", ci.ProjectCode, "','", ci.ContractType, "', '", ci.ContractCode, "',  '", ci.ResourceCode, "','", ci.ResourceName, "',  ", (ci.Specification.ToString() == "&nbsp;") ? "null" : ("'" + ci.Specification.ToString() + "'"), ",  '", ci.Unit, "',", 
                ci.Price, " ) "
             }));
        }

        public int AddContractTask(ContractTaskInfo ci)
        {
            string str = "";
            string str2 = str + "INSERT INTO EPM_Con_ContractTask ( ProjectCode, ContractType, ContractCode, TaskCode, TaskName,  QuantityUnit,Quantity, ContractPrice, subtotal )";
            string str3 = str2 + "values('" + ci.ProjectCode.ToString() + "','" + ci.ContractType.ToString() + "', '" + ci.ContractCode.ToString() + "',";
            object obj2 = str3 + " '" + ci.TaskCode.ToString() + "','" + ci.TaskName.ToString() + "', '" + ci.QuantityUnit.ToString() + "',";
            object obj3 = string.Concat(new object[] { obj2, " ", ci.Quantity, ", " });
            object obj4 = string.Concat(new object[] { obj3, " ", ci.ContractPrice, ", " });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj4, " ", ci.subtotal, ") " }));
        }

        public int AddContractTaskNew(string contractcode, string project, string taskcode, string ContractType)
        {
            string str = "begin ";
            string str2 = str + "INSERT INTO EPM_Con_ContractTask ( ProjectCode, TaskCode, TaskName,  QuantityUnit,Quantity, ContractPrice, subtotal ,ContractCode,ContractType)";
            string str3 = str2 + "SELECT distinct ProjectCode, TaskCode, TaskName,QuantityUnit, Quantity,  SynthPrice, SumPrice,'" + contractcode + "'," + ContractType + " FROM dbo.EPM_Task_TaskList ";
            return publicDbOpClass.ExecSqlString((str3 + " where wbstype=1 and ProjectCode='" + project + "' and TaskCode in (" + taskcode + ") and TaskCode not in (select TaskCode  from EPM_Con_ContractTask where ContractCode='" + contractcode + "')   ") + " end ");
        }

        public int AddContractUpdate(string prjcode, string ContractType, string contractcode)
        {
            string str2 = (" begin " + "if not exists( select 1 from EPM_Con_ContractUpdate where contractcode='" + contractcode + "' and (UpdateCode is null or UpdateCode='')) ") + "INSERT INTO EPM_Con_ContractUpdate ( ProjectCode, ContractType, ContractCode )";
            return publicDbOpClass.ExecSqlString((str2 + "values('" + prjcode + "','" + ContractType + "', '" + contractcode + "') ") + "end");
        }

        public int DelContract(string conttype, string contcode)
        {
            string str = " begin ";
            try
            {
                if ((conttype == "1") || (conttype == "2"))
                {
                    str = ((((((((str + " delete from EPM_Con_ContractTask   ") + " where ContractCode = '" + contcode + "'  and  ") + " ContractType='" + conttype + "'") + " delete from EPM_Con_ContractUpdate   ") + " where ContractCode = '" + contcode + "'  and  ") + " ContractType='" + conttype + "'") + " delete from EPM_Con_ContractAskPay   ") + " where ContractCode = '" + contcode + "'  and  ") + " ContractType='" + conttype + "'";
                }
                return publicDbOpClass.ExecSqlString((((str + " delete from EPM_Con_ContractMain   ") + " where ContractCode = '" + contcode + "'  and  ") + " ContractType='" + conttype + "'") + " end");
            }
            catch
            {
                return 0;
            }
        }

        public int DelContractAskPay(int ID)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + " delete from EPM_Con_ContractAskPay  where  ID=" + ID);
        }

        public int DelContractCount(string RecordID)
        {
            string str = "begin";
            return publicDbOpClass.ExecSqlString(((str + " delete from EPM_Con_ContractCount  where  RecordID='" + RecordID + "'") + " delete from EPM_Con_ContractCountDetail  where  CountRecordID='" + RecordID + "'") + " end");
        }

        public int DelContractStockBill(int ID)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + " delete from EPM_Con_ContractStockBill  where  ID=" + ID);
        }

        public int DelContractTask(string conttype, string contcode, string taskcode)
        {
            string str2 = (" begin " + " delete from EPM_Con_ContractTask   ") + " where ContractCode = '" + contcode + "'  and  ";
            return publicDbOpClass.ExecSqlString((str2 + " ContractType='" + conttype + "' and  TaskCode='" + taskcode + "'") + " end");
        }

        public int DelContractUpdate(int ID)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + " delete from EPM_Con_ContractUpdate  where  ID=" + ID);
        }

        public static bool getcbconNum(string pc)
        {
            if (publicDbOpClass.DataTableQuary("select count(*) from EPM_V_Fund_ContListForPay where ProjectCode='" + pc + "' and ContractType=1 and AuditState=1").Rows[0][0].ToString().Equals("0"))
            {
                return false;
            }
            return true;
        }

        public static DataTable GetContAskPayList(string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT *  FROM EPM_Con_ContractAskPay  WHERE (ContractCode ='" + contcode + "' )  ");
        }

        public static DataTable GetContAskPayList(string conttype, string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT *  FROM EPM_Con_ContractAskPay  WHERE (ContractCode ='" + contcode + "' )  ");
        }

        public static string GetcontcodeByWFID(string WFID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("Select ContractCode from EPM_Con_ContractMain where FlowGuid='" + WFID + "'  ");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public static int GetContcodeCount(string conttype, string contcode)
        {
            return publicDbOpClass.DataTableQuary("Select * from EPM_Con_ContractMain where ContractCode='" + contcode + "'  ").Rows.Count;
        }

        public int GetContCount(string strWhere)
        {
            return publicDbOpClass.GetRecordCount("EPM_Con_ContractMain", strWhere);
        }

        public DataTable GetContCountDetailList(Guid RecordID, string Type)
        {
            string sqlString = "SELECT b.*, a.ContractCode as ContCode FROM dbo.EPM_Con_ContractCount a INNER JOIN ";
            sqlString = sqlString + " EPM_Con_ContractCountDetail b ON a.RecordID = b.CountRecordID WHERE b.CountRecordID = '" + RecordID.ToString() + "'";
            DataTable dt = publicDbOpClass.DataTableQuary(sqlString);
            this.SetContCurrCount(dt);
            if (Type == "1")
            {
                sqlString = "SELECT a.ContractCode, b.ID, b.CountRecordID, b.TaskCode, b.TaskName, b.QuantityUnit, b.Quantity, b.CompleteQuantity, b.CurrentQuantity, ";
                sqlString = (sqlString + " c.ContractPrice FROM dbo.EPM_Con_ContractCount a INNER JOIN dbo.EPM_Con_ContractCountDetail b ON a.RecordID = b.CountRecordID INNER JOIN " + " dbo.EPM_Task_TaskList c ON a.ProjectCode = c.ProjectCode AND b.TaskCode = c.TaskCode") + " WHERE b.CountRecordID = '" + RecordID.ToString() + "' and c.ChildNum = 0";
            }
            else if (Type == "2")
            {
                sqlString = "SELECT a.ContractCode, b.ID, b.CountRecordID, b.TaskCode, b.TaskName, b.QuantityUnit, ";
                sqlString = ((sqlString + "b.Quantity, b.CompleteQuantity, b.CurrentQuantity, c.ContractPrice FROM dbo.EPM_Con_ContractCount a INNER JOIN ") + "dbo.EPM_Con_ContractCountDetail b ON a.RecordID = b.CountRecordID INNER JOIN dbo.EPM_Con_ContractTask c ON a.ProjectCode = c.ProjectCode AND " + "b.TaskCode = c.TaskCode AND a.ContractCode = c.ContractCode INNER JOIN dbo.EPM_Task_TaskList d ON c.TaskCode = d.TaskCode AND ") + "c.ProjectCode = d.ProjectCode WHERE b.CountRecordID = '" + RecordID.ToString() + "' and d.ChildNum = 0";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable GetContCountList(string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM EPM_Con_ContractCount WHERE contractCode = '" + contcode + "' order by CountDate desc");
        }

        public static DataTable GetContList(string where)
        {
            string str = "select * from EPM_V_Fund_ContListForPay ";
            return publicDbOpClass.DataTableQuary((str + " " + where) + "  order by SignDate desc");
        }

        public static DataTable getContListAudit(int FlowID, string yhdm, int Audit)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_V_Fund_ContListForPay where  AuditState=" + Audit + " ");
        }

        public static DataTable GetContListForPay(string where)
        {
            return publicDbOpClass.DataTableQuary("Select * from EPM_V_Fund_ContListForPay " + where);
        }

        public static DataTable GetContPageList(int intPageSize, int intCurentPage, string strWhere)
        {
            return publicDbOpClass.GetRecordFromPage("EPM_Con_ContractMain", "ID", intPageSize, intCurentPage, 0, strWhere);
        }

        public ContractMainInfo GetContractRow(string conttype, string contcode)
        {
            DataRow row = publicDbOpClass.DataTableQuary("Select * from EPM_Con_ContractMain where ContractCode='" + contcode + "'  ").Rows[0];
            return new ContractMainInfo { 
                ContractCode = row["ContractCode"].ToString(), ProjectCode = row["ProjectCode"].ToString(), ContractType = row["ContractType"].ToString(), ContractName = row["ContractName"].ToString(), SignAddr = row["SignAddr"].ToString(), SignDate = (row["SignDate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime) row["SignDate"]), PayMode = row["PayMode"].ToString(), SumMoney = (decimal) row["SumMoney"], BeginDate = (row["BeginDate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime) row["BeginDate"]), EndDate = (row["EndDate"] == DBNull.Value) ? DateTime.MinValue : ((DateTime) row["EndDate"]), WorkDay = (decimal) row["WorkDay"], MainWork = row["MainWork"].ToString(), QualitySafety = row["QualitySafety"].ToString(), Remark = row["Remark"].ToString(), ContractOther = row["ContractOther"].ToString(), ContractOtherMan = row["ContractOtherMan"].ToString(), 
                ContractOtherSpokesman = row["ContractOtherSpokesman"].ToString(), ContractOtherZipcode = row["ContractOtherZipcode"].ToString(), ContractOtherAddr = row["ContractOtherAddr"].ToString(), ContractOtherBack = row["ContractOtherBack"].ToString(), ContractOtherFax = row["ContractOtherFax"].ToString(), ContractOtherTel = row["ContractOtherTel"].ToString(), ContractOtherAccount = row["ContractOtherAccount"].ToString(), ContractOwner = row["ContractOwner"].ToString(), ContractOwnerMan = row["ContractOwnerMan"].ToString(), ContractOwnerSpokesman = row["ContractOwnerSpokesman"].ToString(), ContractOwnerZipcode = row["ContractOwnerZipcode"].ToString(), ContractOwnerAddr = row["ContractOwnerAddr"].ToString(), ContractOwnerBack = row["ContractOwnerBack"].ToString(), ContractOwnerFax = row["ContractOwnerFax"].ToString(), ContractOwnerTel = row["ContractOwnerTel"].ToString(), ContractOwnerAccount = row["ContractOwnerAccount"].ToString(), 
                BalanceRemark = row["BalanceRemark"].ToString(), SumBalance = (decimal) row["SumBalance"], FromType = (row["FromType"] == DBNull.Value) ? "0" : row["FromType"].ToString()
             };
        }

        public WBSBidTaskCollection GetContractTaskList(Guid projectCode, string contractCode)
        {
            WBSBidTaskCollection tasks = new WBSBidTaskCollection();
            DataTable table = publicDbOpClass.DataTableQuary("SELECT * FROM EPM_Contract_CountSelTask where ProjectCode='" + projectCode.ToString() + "' and ContractCode = '" + contractCode + "'");
            for (int i = 0; i < table.Rows.Count; i++)
            {
                tasks.Add(this.GetFromDataRowOfNew(table.Rows[i]));
            }
            return tasks;
        }

        public static DataTable GetContStockBillList(string conttype, string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM EPM_Con_ContractStockBill   WHERE ContractType = '" + conttype + "' AND contractCode = '" + contcode + "'    order by ID");
        }

        public static DataTable GetContTaskList(string conttype, string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM EPM_Contract_ContractTask WHERE (ContractCode ='" + contcode + "' ) AND (ContractType = '" + conttype + "') ");
        }

        public static string GetContType(string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT ContractType  FROM EPM_Con_ContractUpdate  WHERE (ContractCode ='" + contcode + "' ) ").Rows[0][0].ToString();
        }

        public static DataTable GetContUpdateList(string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT *  FROM EPM_Con_ContractUpdate  WHERE (ContractCode ='" + contcode + "' ) ");
        }

        public static DataTable GetContUpdateList(string conttype, string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT *  FROM EPM_Con_ContractUpdate  WHERE (ContractCode ='" + contcode + "' ) ");
        }

        private WBSBidTask GetFromDataRowOfNew(DataRow dr)
        {
            return new WBSBidTask { NoteID = Convert.ToInt32(dr["ID"]), TaskCode = dr["TaskCode"].ToString(), TaskName = dr["TaskName"].ToString(), Quantity = (dr["Quantity"] == DBNull.Value) ? 0M : ((decimal) dr["Quantity"]), QuantityUnit = dr["QuantityUnit"].ToString(), WorkLayer = (int) dr["WorkLayer"], ParentTaskCode = dr["ParentTaskCode"].ToString(), ChildNum = (int) dr["ChildNum"], StartDate = (DateTime) dr["StartDate"], EndDate = (DateTime) dr["EndDate"], SynthPrice = (decimal) dr["ContractPrice"] };
        }

        public string GetNameByCode(string code)
        {
            return publicDbOpClass.ExecuteScalar("select prjName from pt_PrjInfo where prjguid='" + code + "'").ToString();
        }

        public string GetPCByContCode(string code)
        {
            return publicDbOpClass.ExecuteScalar("select ProjectCode from EPM_Con_ContractMain where ContractCode='" + code + "'").ToString();
        }

        public string GetPnByPc(string code)
        {
            return publicDbOpClass.ExecuteScalar("SELECT  PNode FROM dbo.v_Prj_PrjTree where prjguid='" + code + "'").ToString();
        }

        public static DataTable GetResourceList(string conttype, string contcode)
        {
            return publicDbOpClass.DataTableQuary("SELECT * FROM PT_V_resource  WHERE  resourceTypeID='" + conttype + "'and  (ResourceNo NOT IN  (SELECT ResourceCode  FROM EPM_Con_ContractStockBill   WHERE contractCode = '" + contcode + "'))  order by ResourceNo");
        }

        public DataTable GetResourceTypeByStyle()
        {
            return publicDbOpClass.DataTableQuary(string.Format("select * from EPM_Res_Category where VersionCode='896431D1-F875-47EC-8164-CED63F6E65F2' and resourceStyle={0}", Convert.ToInt32(ResourceTypeStyle.Material)));
        }

        public DataTable GetResourceTypeByStyle(int restype)
        {
            return publicDbOpClass.DataTableQuary(string.Format("select * from EPM_Res_Category where VersionCode='896431D1-F875-47EC-8164-CED63F6E65F2' and resourceStyle={0}", 3));
        }

        public static string GetSubPrjCode(string conttype, string contcode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT pt_PrjInfo.PrjName,pt_PrjInfo.Prjcode  FROM pt_PrjInfo INNER JOIN     EPM_Con_ContractMain ON   pt_PrjInfo.prjguid =  EPM_Con_ContractMain.ProjectCode  WHERE (dbo.EPM_Con_ContractMain.ContractCode ='" + contcode + "' ) AND  (dbo.EPM_Con_ContractMain.ContractType = '" + conttype + "') ");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][1].ToString();
            }
            return "";
        }

        public static string GetSubPrjName(string contcode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT pt_PrjInfo.PrjName,pt_PrjInfo.Prjguid  FROM pt_PrjInfo INNER JOIN     EPM_Con_ContractMain ON   pt_PrjInfo.prjguid =  EPM_Con_ContractMain.ProjectCode  WHERE (dbo.EPM_Con_ContractMain.ContractCode ='" + contcode + "' )");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public static string GetSubPrjName(string conttype, string contcode)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT pt_PrjInfo.PrjName,pt_PrjInfo.Prjguid  FROM pt_PrjInfo INNER JOIN     EPM_Con_ContractMain ON   pt_PrjInfo.prjguid =  EPM_Con_ContractMain.ProjectCode  WHERE (dbo.EPM_Con_ContractMain.ContractCode ='" + contcode + "' )");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0][0].ToString();
            }
            return "";
        }

        public static string GetSubPrjNamecontcode(string pc)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT * FROM PT_PrjInfo  WHERE (PrjGuid ='" + pc + "' )");
            if (table.Rows.Count > 0)
            {
                string str2 = table.Rows[0]["PrjCode"].ToString();
                string str3 = publicDbOpClass.DataTableQuary("SELECT count(*) FROM  EPM_Con_ContractMain  where ContractCode like '" + str2 + "%' ").Rows[0][0].ToString();
                return (str2 + str3.PadLeft(3, '0'));
            }
            return "";
        }

        public static string GetSubPrjNamecontcode(string pc, int col)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT PrjCode, StartDate, EndDate,PrjPlace,Owner,'0' as GoalCost,DATEDIFF(d, StartDate, EndDate) AS workday, PrjCost, PrjName from Pt_prjInfo WHERE (prjguid ='" + pc + "' )");
            if (table.Rows.Count <= 0)
            {
                return "";
            }
            string str2 = table.Rows[0][col].ToString();
            if ((col != 1) && (col != 2))
            {
                return str2;
            }
            return Convert.ToDateTime(str2).ToString("yyyy-MM-dd");
        }

        public static DataTable QueryOneRecord(Guid CountRecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_Con_ContractCount where RecordID='" + CountRecordID.ToString() + "'");
        }

        public static DataTable QueryOutMoney(Guid projectCode, int corp, DateTime dtMonth)
        {
            string sqlString = "";
            string str2 = "";
            sqlString = "select ContractCode,ConfirmMoney,PayMode,PayDate,PayCompany,";
            str2 = "select sum(ConfirmMoney) from EPM_FUND_ContractPay where prjCode='" + projectCode.ToString() + "' ";
            if (corp != 0)
            {
                str2 = str2 + " and PayCompany=" + corp.ToString();
            }
            if (dtMonth != DateTime.MinValue)
            {
                str2 = str2 + " and PayDate<'" + dtMonth.ToShortDateString() + "'";
            }
            string str3 = sqlString;
            sqlString = str3 + "(" + str2 + ") as sumMoney from EPM_FUND_ContractPay where PrjCode='" + projectCode.ToString() + "' ";
            if (corp != 0)
            {
                sqlString = sqlString + " and PayCompany=" + corp.ToString();
            }
            if (dtMonth != DateTime.MinValue)
            {
                sqlString = sqlString + " and PayDate<'" + dtMonth.ToShortDateString() + "'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public static DataTable QueryTaskList(Guid CountRecordID)
        {
            return publicDbOpClass.DataTableQuary("select a.*,c.TaskName,c.ChildNum from EPM_Con_ContractCountDetail a left join EPM_Con_ContractCount b on a.CountRecordID=b.RecordID left join EPM_Task_TaskList c on a.TaskCode=c.TaskCode and b.ProjectCode=c.ProjectCode where a.CountRecordID='" + CountRecordID.ToString() + "'");
        }

        public static DataTable QueryTaskList(string taskCodes, Guid projectCode)
        {
            string sqlString = "";
            sqlString = "select TaskCode,TaskName from epm_task_tasklist where ProjectCode='" + projectCode.ToString() + "' and childNum=0";
            if (taskCodes.Length == 0)
            {
                sqlString = sqlString + " and (1=0)";
            }
            else
            {
                sqlString = sqlString + " and taskCode in (" + taskCodes + ")";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int SelectContractAskPay(ContractAskPayInfo ci)
        {
            if (publicDbOpClass.DataTableQuary("select * from EPM_Con_ContractAskPay    where ID=" + ci.ID.ToString() + "   ").Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public int SelectContractStockBill(ContractStockBillInfo ci)
        {
            if (publicDbOpClass.DataTableQuary("select * from EPM_Con_ContractStockBill    where ID=" + ci.ID.ToString() + "   ").Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public int SelectContractTask(ContractTaskInfo ci)
        {
            if (publicDbOpClass.DataTableQuary("select * from EPM_Con_ContractTask    where ContractType='" + ci.ContractType.ToString() + "' and    ContractCode ='" + ci.ContractCode.ToString() + "' and   TaskCode='" + ci.TaskCode.ToString() + "'").Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public int SelectContractUpdate(ContractUpdateInfo ci)
        {
            if (publicDbOpClass.DataTableQuary("select * from EPM_Con_ContractUpdate    where ID=" + ci.ID.ToString() + "   ").Rows.Count > 0)
            {
                return 1;
            }
            return 0;
        }

        public void SetContCurrCount(DataTable dt)
        {
            string sqlString = " ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string str2 = (sqlString + " update EPM_Con_ContractCountDetail set CompleteQuantity = " + " (select sum(isnull(CurrentQuantity,0)) from EPM_Con_ContractCountDetail where ") + " CountRecordID in (select RecordID from EPM_Con_ContractCount where ContractCode = '" + dt.Rows[i]["ContCode"].ToString() + "')";
                sqlString = str2 + " and TaskCode = '" + dt.Rows[i]["TaskCode"].ToString() + "') where ID = " + dt.Rows[i]["ID"].ToString();
            }
            publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public int UpdateBidBalance(ContractMainInfo ci)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString((((str + " update EPM_Con_ContractMain  set ") + " BalanceRemark='" + ci.BalanceRemark + "',") + " SumBalance=" + ci.SumBalance.ToString() + " ") + " where ContractCode = '" + ci.ContractCode.ToString() + "'  ");
        }

        public int UpdateContract(ContractMainInfo ci)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString((((((((((((((((((((((((((((((((((str + " begin " + " update EPM_Con_ContractMain  set ") + " ContractName='" + ci.ContractName + "',") + " SignAddr='" + ci.SignAddr + "',") + " SignDate=" + ((ci.SignDate == DateTime.MinValue) ? "null" : ("'" + ci.SignDate.ToShortDateString() + "'")) + ",") + " PayMode='" + ci.PayMode + "',") + " SumMoney=" + ci.SumMoney.ToString() + ",") + " BeginDate=" + ((ci.BeginDate == DateTime.MinValue) ? "null" : ("'" + ci.BeginDate.ToShortDateString() + "'")) + ",") + " EndDate=" + ((ci.EndDate == DateTime.MinValue) ? "null" : ("'" + ci.EndDate.ToShortDateString() + "'")) + ",") + " WorkDay=" + ci.WorkDay.ToString() + ",") + " MainWork='" + ci.MainWork + "',") + " QualitySafety='" + ci.QualitySafety + "',") + " Remark='" + ci.Remark + "',") + " FromType='" + ci.FromType + "',") + " ContractOwner='" + ci.ContractOwner + "',") + " ContractOwnerAddr='" + ci.ContractOwnerAddr + "',") + " ContractOwnerMan='" + ci.ContractOwnerMan + "',") + " ContractOwnerSpokesman='" + ci.ContractOwnerSpokesman + "',") + " ContractOwnerTel='" + ci.ContractOwnerTel + "',") + " ContractOwnerFax='" + ci.ContractOwnerFax + "',") + " ContractOwnerZipcode='" + ci.ContractOwnerZipcode + "',") + " ContractOwnerBack='" + ci.ContractOwnerBack + "',") + " ContractOwnerAccount='" + ci.ContractOwnerAccount + "',") + " ContractOther='" + ci.ContractOther + "',") + " ContractOtherAddr='" + ci.ContractOtherAddr + "',") + " ContractOtherMan='" + ci.ContractOtherMan + "',") + " ContractOtherSpokesman='" + ci.ContractOtherSpokesman + "',") + " ContractOtherTel='" + ci.ContractOtherTel + "',") + " ContractOtherFax='" + ci.ContractOtherFax + "',") + " ContractOtherZipcode='" + ci.ContractOtherZipcode + "',") + " ContractOtherBack='" + ci.ContractOtherBack + "',") + " ContractOtherAccount='" + ci.ContractOtherAccount + "'") + " where ContractCode = '" + ci.ContractCode.ToString() + "'  and  ") + " ContractType='" + ci.ContractType.ToString() + "'") + " end");
        }

        public int UpdateContractAskPay(ContractAskPayInfo ci)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { "Update [EPM_Con_ContractAskPay]  SET AskPayCode='", ci.AskPayCode, "',  AskPayType='", ci.AskPayType, "',  AskPayIdea='", ci.AskPayIdea, "',  AskPayContent='", ci.AskPayContent, "',  AskPayDate='", ci.AskPayDate, "'   where  ID=", ci.ID.ToString() }));
        }

        public static bool UpdateContractCount(ContractCountInfo ci, string taskCodes)
        {
            string str = "";
            str = " begin ";
            string str2 = str;
            str = (str2 + " update EPM_Con_ContractCount set CountDate='" + ci.CountDate.ToString() + "',Remark='" + ci.Remark + "' where RecordID='" + ci.RecordID.ToString() + "' ") + " delete from EPM_Con_ContractCountDetail where CountRecordID='" + ci.RecordID.ToString() + "' ";
            if (taskCodes.Length > 0)
            {
                string str3 = str + " insert into EPM_Con_ContractCountDetail (CountRecordID,TaskCode,TaskName,QuantityUnit,Quantity)";
                str = str3 + " select '" + ci.RecordID.ToString() + "',TaskCode,TaskName,QuantityUnit,isnull(quantity,0) from EPM_Task_TaskList where projectcode='" + ci.ProjectCode.ToString() + "' and taskcode in (" + taskCodes + ") ";
            }
            return publicDbOpClass.NonQuerySqlString(str + " end ");
        }

        public static bool UpdateContractCountCompleteQuantity(DataTable dt)
        {
            string str = " begin ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                str = (str + " update EPM_Con_ContractCountDetail set CurrentQuantity = '" + dt.Rows[i]["CurrentQuantity"].ToString() + "'") + " where ID = " + dt.Rows[i]["ID"].ToString();
            }
            return publicDbOpClass.NonQuerySqlString(str + " end ");
        }

        public int UpdateContractStockBill(ContractStockBillInfo ci)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { "UPDATE [EPM_Con_ContractStockBill]  SET Specification='", ci.Specification, "',  logo='", ci.logo, "',  producer='", ci.producer, "',  LimitTime='", ci.LimitTime, "',  Remark='", ci.Remark, "',  Num=", ci.Num, ",  Price=", ci.Price, "   where  ID=", ci.ID.ToString() }));
        }

        public int UpdateContractTask(ContractTaskInfo ci)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { "UPDATE [EPM_Con_ContractTask]  SET Quantity=", ci.Quantity, ",  ContractPrice=", ci.ContractPrice, ",  subtotal=", ci.subtotal, "   where ContractType='", ci.ContractType.ToString(), "'and   ContractCode ='", ci.ContractCode.ToString(), "' and   TaskCode='", ci.TaskCode.ToString(), "'" }));
        }

        public int UpdateContractUpdate(ContractUpdateInfo ci)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { "UPDATE [EPM_Con_ContractUpdate]  SET UpdateCode='", ci.UpdateCode, "',  UpdateType='", ci.UpdateType, "',  UpdateContent='", ci.UpdateContent, "',  UpdateMoney='", ci.UpdateMoney, "',  UpdateDate='", ci.UpdateDate, "'   where  ID=", ci.ID.ToString() }));
        }
    }
}

