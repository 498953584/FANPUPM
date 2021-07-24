namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class StuffSupplyAction
    {
        private static SupplyPlanMainCollection CreatMainBillCollectionFromTable(DataTable tbl)
        {
            SupplyPlanMainCollection mains = new SupplyPlanMainCollection();
            if (tbl != null)
            {
                foreach (DataRow row in tbl.Rows)
                {
                    SupplyPlanMain main = new SupplyPlanMain {
                        PlanID = Convert.ToInt32(row["ItemID"]),
                        PlanCode = row["SupplyPlanCode"].ToString(),
                        PlanMaker = row["PlanMaker"].ToString(),
                        IsChecked = Convert.ToInt32(row["HasCheck"]),
                        Reason = row["SupplyReason"].ToString(),
                        Remark = row["Remark"].ToString(),
                        SupplyDate = Convert.ToDateTime(row["LimitDate"]),
                        CreateDate = Convert.ToDateTime(row["PlanCreatTime"]),
                        Type = row["SupplyType"].ToString(),
                        ContractCode = row["ContractCode"].ToString(),
                        AuditInfo = row["AuditInfo"].ToString(),
                        AuditMan = row["AuditMan"].ToString()
                    };
                    try
                    {
                        main.FlowGuid = (Guid) row["FlowGuid"];
                    }
                    catch
                    {
                        main.FlowGuid = Guid.Empty;
                    }
                    if (row["AuditResult"].ToString().Length != 0)
                    {
                        main.AuditResult = Convert.ToInt32((row["AuditResult"].ToString().Length == 0) ? "0" : row["AuditResult"]);
                        main.AuditDate = Convert.ToDateTime((row["AuditDate"] == DBNull.Value) ? "1900-01-01" : row["AuditDate"]);
                    }
                    else
                    {
                        main.AuditResult = 3;
                        main.AuditDate = DateTime.Now;
                    }
                    mains.Add(main);
                }
            }
            return mains;
        }

        private static SupplyPlanMainCollection CreatSupplanMainCollection(string comtext)
        {
            return CreatMainBillCollectionFromTable(publicDbOpClass.DataTableQuary(comtext));
        }

        public static void CreatSupplyPlanMain(SupplyPlanMain oneplan)
        {
            string sqlString = string.Format("insert into EPM_Stuff_SupplyPlan_Main(SupplyPlanCode,PlanCreatTime,LimitDate,PlanMaker,HasCheck,SupplyReason,Remark,SupplyType,ContractCode,AuditResult) values('{0}',{1},'{2}','{3}',{4},'{5}','{6}','{7}','{8}','{9}')", new object[] { oneplan.PlanCode, "getdate()", oneplan.SupplyDate.ToString(), oneplan.PlanMaker, oneplan.IsChecked, oneplan.Reason, oneplan.Remark, oneplan.Type, oneplan.ContractCode, oneplan.AuditResult });
            try
            {
                publicDbOpClass.NonQuerySqlString(sqlString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static bool DelItemFromSupplyDetail(int planId, string resourceCode)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select ApplyUniqueCode from EPM_Stuff_ApplyDetails where PlanID=" + planId.ToString() + " and ResourceCode = '" + resourceCode + "'");
            str = " begin ";
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    str = str + " update EPM_Stuff_ApplyBill set IsDoOver=0 where ApplyUniqueCode='" + table.Rows[i]["ApplyUniqueCode"].ToString() + "' ";
                }
            }
            string str2 = str;
            string str3 = str2 + " update EPM_Stuff_ApplyDetails set PlanID=0 where PlanID=" + planId.ToString() + " and ResourceCode='" + resourceCode + "'";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from EPM_Stuff_SupplyPlan_Detail where SupplyPlanID=" + planId.ToString() + " and StuffCode='" + resourceCode + "'") + " end ");
        }

        public static void DellItemFromSupplyDetail(int itemid)
        {
            string sqlString = string.Format("delete from EPM_Stuff_SupplyPlan_Detail where ItemID={0}", itemid);
            try
            {
                publicDbOpClass.NonQuerySqlString(sqlString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static void DelSupplyPlan(string supplancode)
        {
            StringBuilder builder = new StringBuilder();
            string sqlString = string.Format("delete from EPM_Stuff_SupplyPlan_Main where SupplyPlanCode='{0}'", supplancode);
            builder.Append(sqlString + ";");
            publicDbOpClass.NonQuerySqlString(sqlString);
        }

        public static bool EditSplanSum(SupplyPlanDetailCollection details)
        {
            bool flag = false;
            StringBuilder builder = new StringBuilder();
            builder.Append("begin ");
            if (details.Count > 0)
            {
                int count = details.Count;
                for (int i = 0; i < count; i++)
                {
                    builder.Append(string.Format("update EPM_Stuff_SupplyPlan_Detail set StuffCount={0},PrePrice={1},FactPrice={2},ArrivalDate='{3}' where ItemID={4}", new object[] { details[i].StuffCount, details[i].StuffPrice, details[i].FactPrice, details[i].ArrivalDate, details[i].ItemID }) + ";");
                }
                builder.Append(" end");
                try
                {
                    flag = publicDbOpClass.NonQuerySqlString(builder.ToString());
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
            return flag;
        }

        public static void EditSupplyPlanMain(SupplyPlanMain oneplan)
        {
            string sqlString = string.Format("update EPM_Stuff_SupplyPlan_Main set SupplyPlanCode='{0}',LimitDate='{1}',PlanMaker='{2}',SupplyReason='{3}',Remark='{4}',SupplyType='{5}',ContractCode='{6}' where ItemID={7}", new object[] { oneplan.PlanCode, oneplan.SupplyDate.ToString(), oneplan.PlanMaker, oneplan.Reason, oneplan.Remark, oneplan.Type, oneplan.ContractCode, oneplan.PlanID });
            try
            {
                publicDbOpClass.NonQuerySqlString(sqlString);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public static SupplyPlanMain FindOnePlanByCode(string plancode)
        {
            SupplyPlanMainCollection mains = CreatSupplanMainCollection(string.Format("select * from EPM_Stuff_SupplyPlan_Main where SupplyPlanCode='{0}'", plancode));
            if (mains != null)
            {
                return mains[0];
            }
            return null;
        }

        public static SupplyPlanMain FindOnePlanByID(int planid)
        {
            SupplyPlanMainCollection mains = CreatSupplanMainCollection(string.Format("select * from EPM_Stuff_SupplyPlan_Main where ItemID={0}", planid));
            if (mains != null)
            {
                return mains[0];
            }
            return null;
        }

        public static SupplyPlanMain FindOnePlanByWLid(string wlid)
        {
            SupplyPlanMainCollection mains = CreatSupplanMainCollection(string.Format("select * from EPM_Stuff_SupplyPlan_Main where flowguid='" + wlid + "'", new object[0]));
            if (mains != null)
            {
                return mains[0];
            }
            return null;
        }

        public static int getItemidForGuid(string wfguid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select itemid  from EPM_Stuff_SupplyPlan_Main where flowguid='" + wfguid + "'");
            if (table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0].ToString());
            }
            return 0;
        }

        public static DataTable GetLessResource(Guid ProjectCode)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " select * from EPM_Stuff_ApplyDetails a join EPM_Stuff_ApplyBill b on a.ApplyUniqueCode=b.ApplyUniqueCode left join EPM_V_Res_ResourceBasic c on a.ResourceCode=c.ResourceCode where b.ProjectCode='", ProjectCode, "'and b.ApplyType=2 and AuditResult = 1" }));
        }

        public static SupplyPlanDetailCollection GetPlanSelectSum(int planid, bool isSum)
        {
            string str;
            if (isSum)
            {
                str = string.Format("select splan.ItemID,splan.StuffCode,splan.StuffCount,splan.SupplyPlanID,splan.PrePrice StuffPrice,splan.FactPrice FactPrice, splan.ArrivalDate,res.resourceName StuffName,res.specification StuffSpecs,res.UnitName StuffUnit from EPM_Stuff_SupplyPlan_Detail splan,EPM_V_Res_ResourceBasic res where SupplyPlanID={0} and  splan.StuffCode=res.resourceCode ", planid);
            }
            else
            {
                str = string.Format("select StuffName, StuffSpecs,StuffName,sum(StuffCount)  StuffCount from V_StuffSupply_Details_Temp  where SupplyPlanID={0} group by StuffCode, StuffName,StuffSpecs", planid);
            }
            DataTable table = new DataTable();
            table = publicDbOpClass.DataTableQuary(str);
            SupplyPlanDetailCollection details = new SupplyPlanDetailCollection();
            if (table != null)
            {
                foreach (DataRow row in table.Rows)
                {
                    SupplyPlanDetail detail = new SupplyPlanDetail {
                        StuffCode = row["StuffCode"].ToString(),
                        StuffName = row["StuffName"].ToString(),
                        StuffSpecs = row["StuffSpecs"].ToString(),
                        StuffCount = Convert.ToDecimal(row["StuffCount"]),
                        StuffPrice = Convert.ToDecimal(row["StuffPrice"]),
                        FactPrice = Convert.ToDecimal(row["FactPrice"]),
                        StuffUnit = row["StuffUnit"].ToString(),
                        ItemID = Convert.ToInt32(row["ItemID"]),
                        ArrivalDate = (DateTime) row["ArrivalDate"]
                    };
                    details.Add(detail);
                }
            }
            return details;
        }

        public static bool MergePlanResource(int planid, string sessioinCode, string userCode)
        {
            string str2 = ("" + " begin " + " insert into EPM_Stuff_SupplyPlan_Detail ") + " select ResourceCode,0," + planid.ToString() + ",0,0,getdate() from EPM_Res_TempResource ";
            string str3 = (str2 + " where SessionCode='" + sessioinCode + "' and UserCode='" + userCode + "' and ") + " ResourceCode not in (select StuffCode from EPM_Stuff_SupplyPlan_Detail where supplyplanid=" + planid.ToString() + ")";
            return publicDbOpClass.NonQuerySqlString((str3 + " delete from EPM_Res_TempResource where SessionCode='" + sessioinCode + "' and UserCode='" + userCode + "' ") + " end ");
        }

        public static DataTable QueryApplyRecive(int planid)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@planid", planid) };
            return publicDbOpClass.ExecuteDataTable("EPM_P_Stuff_QueryApplyRecive", commandParameters);
        }

        public static SupplyPlanMainCollection QureySupplyCollection(string code, string stime, string etime)
        {
            DataTable table = new DataTable();
            return CreatMainBillCollectionFromTable(publicDbOpClass.GetPageData(" SupplyPlanCode like '" + code + "%' and PlanCreatTime>='" + stime + "' and PlanCreatTime <= '" + etime + "'", "EPM_Stuff_SupplyPlan_Main", "ItemID DESC"));
        }

        public static SupplyPlanMainCollection QureySupplyCollection(string stime, string etime, string plandecode, int checkinfo)
        {
            string str;
            if (checkinfo == -1)
            {
                str = string.Format("select * from EPM_Stuff_SupplyPlan_Main where SupplyPlanCode like'{0}%' and  LimitDate<'{1}' and LimitDate>'{2}' ORDER BY itemID DESC", plandecode, etime, stime);
            }
            else
            {
                str = string.Format("select * from EPM_Stuff_SupplyPlan_Main where SupplyPlanCode like'{0}%' and  LimitDate<'{1}' and LimitDate>'{2}'and HasCheck={3}  ORDER BY itemID DESC", new object[] { plandecode, etime, stime, checkinfo });
            }
            return CreatSupplanMainCollection(str);
        }

        public static SupplyPlanMainCollection QureySupplyCollection(string code, string stime, string etime, string strWhere)
        {
            DataTable table = new DataTable();
            return CreatMainBillCollectionFromTable(publicDbOpClass.GetPageData((" SupplyPlanCode like '" + code + "%' and PlanCreatTime>='" + stime + "' and PlanCreatTime <= '" + etime + "' ") + strWhere, "EPM_Stuff_SupplyPlan_Main", "ItemID DESC"));
        }

        public static void SelectPlan()
        {
        }

        public static void SelectStuffFromStock(ArrayList stuffinfolist, int planid)
        {
            if (stuffinfolist.Count > 0)
            {
                StringBuilder builder = new StringBuilder();
                builder.Append("begin  ");
                foreach (object obj2 in stuffinfolist)
                {
                    ResourceInfo info = (ResourceInfo) obj2;
                    builder.Append(string.Format("if(not exists(select ItemID from EPM_Stuff_SupplyPlan_Detail where StuffCode='{0}' and SupplyPlanID={1}))  begin insert into EPM_Stuff_SupplyPlan_Detail(SupplyPlanID,StuffCode,StuffCount) values({1},'{0}',0) end", info.ResourceNo, planid, 0) + ";");
                }
                builder.Append("  end");
                try
                {
                    publicDbOpClass.NonQuerySqlString(builder.ToString());
                }
                catch (Exception exception)
                {
                    throw exception;
                }
            }
        }

        public static int SupplyPlanAdulit(int itemid, SupplyPlanMain spmc)
        {
            object obj2 = " update EPM_Stuff_SupplyPlan_Main set AuditInfo = '" + spmc.AuditInfo + "' ,AuditMan='" + spmc.AuditMan + "',";
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " AuditDate = '", spmc.AuditDate, "',AuditResult='", spmc.AuditResult, "' where ItemID =", itemid }));
        }

        public static void SupplyPlanCheck(string supplyPlanCode)
        {
            publicDbOpClass.NonQuerySqlString(string.Format("update EPM_Stuff_SupplyPlan_Main set HasCheck=1 where SupplyPlanCode='{0}'", supplyPlanCode));
        }
    }
}

