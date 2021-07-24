namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class MaterialApplyAction
    {
        public static bool AddMaterialFromStock(Guid billCode, Guid pageSession, string usercode)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " insert into EPM_Stuff_ApplyDetails_Temp (ApplyUniqueCode,ResourceCode,ResourceStyle,Quantity,UnitPrice,Supply,ArrivalDate,IsShow,IsDoOver,PlanID,UserCode) ";
            string str3 = str2 + " (select '" + billCode.ToString() + "',ResourceCode,ResourceStyle,0,0,1,getdate(),1,0,0,'" + usercode + "' ";
            string str4 = str3 + " from EPM_Res_TempResource where SessionCode='" + pageSession.ToString() + "' and userCode='" + usercode + "' and ResourceCode not in ";
            string str5 = str4 + " (select ResourceCode from EPM_Stuff_ApplyDetails_Temp where SessionCode='" + pageSession.ToString() + "' and userCode='" + usercode + "')) ";
            return publicDbOpClass.NonQuerySqlString((str5 + " delete from EPM_Res_TempResource where SessionCode='" + pageSession.ToString() + "' and userCode='" + usercode + "' ") + " end ");
        }

        public static bool AddMaterialFromStock(Guid billCode, Guid pageSession, string usercode, string prjcode)
        {
            string str = "";
            str = " begin ";
            string str2 = str + " insert into EPM_Stuff_ApplyDetails_Temp (ApplyUniqueCode,ResourceCode,ResourceStyle,Quantity,UnitPrice,Supply,ArrivalDate,IsShow,IsDoOver,PlanID,UserCode) ";
            string str3 = str2 + " (select '" + billCode.ToString() + "',ResourceCode,ResourceStyle,0,0,1,getdate(),1,0,0,'" + usercode + "' ";
            string str4 = str3 + " from EPM_Res_TempResource where SessionCode='" + pageSession.ToString() + "' and userCode='" + usercode + "' and ResourceCode not in ";
            string str5 = str4 + " (select ResourceCode from EPM_Stuff_ApplyDetails_Temp where SessionCode='" + pageSession.ToString() + "' and userCode='" + usercode + "')) ";
            return publicDbOpClass.NonQuerySqlString((str5 + " delete from EPM_Res_TempResource where SessionCode='" + pageSession.ToString() + "' and userCode='" + usercode + "' ") + " end ");
        }

        public static bool AuditApplyBill(MaterialApplyBill bill)
        {
            return publicDbOpClass.NonQuerySqlString(("update EPM_Stuff_ApplyBill set AuditInfo='" + bill.AuditInfo + "',AuditMan='" + bill.AuditMan + "',AuditDate='" + bill.AuditDate.ToShortDateString() + "',AuditResult=" + (bill.AuditResult ? "1" : "0") + ",IsAudit=1 ") + " where ApplyUniqueCode='" + bill.ApplyUniqueCode.ToString() + "'");
        }

        public static bool CheckMaterialIsOut(Guid ProjectCode, string strResourceCode)
        {
            bool flag = false;
            decimal num = 0M;
            decimal num2 = 0M;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select isnull(sum(isnull(Quantity,0)),0) as Quantity from EPM_Task_Resource where ProjectCode='", ProjectCode, "' and ResourceCode='", strResourceCode, "' " }));
            if (table.Rows.Count > 0)
            {
                num = Convert.ToDecimal(table.Rows[0]["Quantity"].ToString());
            }
            DataTable table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select isnull(sum(isnull(b.Quantity,0)),0) as Quantity from EPM_Stuff_ApplyBill a join EPM_Stuff_ApplyDetails b on a.ApplyUniqueCode=b.ApplyUniqueCode where a.ProjectCode='", ProjectCode, "' and b.ResourceCode='", strResourceCode, "' " }));
            if (table2.Rows.Count > 0)
            {
                num2 = Convert.ToDecimal(table2.Rows[0]["Quantity"].ToString());
            }
            if (num2 > num)
            {
                flag = true;
            }
            return flag;
        }

        public static string CheckMaterialNum(Guid ProjectCode, MaterialApplyResourceCollection mrc)
        {
            string str = string.Empty;
            decimal num = 0M;
            decimal num2 = 0M;
            foreach (MaterialApplyResource resource in mrc)
            {
                DataTable table = publicDbOpClass.DataTableQuary(" select ResourceType,ResourceName from EPM_V_Res_ResourceBasic where ResourceStyle=2 and ResourceCode='" + resource.ResourceCode + "' ");
                if ((table.Rows.Count > 0) && (((int) table.Rows[0]["ResourceType"]) == 1))
                {
                    DataTable table2 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select isnull(sum(isnull(Quantity,0)),0) as Quantity from EPM_Task_Resource where ProjectCode='", ProjectCode, "' and ResourceCode='", resource.ResourceCode, "' " }));
                    if (table2.Rows.Count > 0)
                    {
                        num = Convert.ToDecimal(table2.Rows[0]["Quantity"].ToString());
                    }
                    DataTable table3 = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select isnull(sum(isnull(b.Quantity,0)),0) as Quantity from EPM_Stuff_ApplyBill a join EPM_Stuff_ApplyDetails b on a.ApplyUniqueCode=b.ApplyUniqueCode where a.ProjectCode='", ProjectCode, "' and b.ResourceCode='", resource.ResourceCode, "' " }));
                    if (table3.Rows.Count > 0)
                    {
                        num2 = Convert.ToDecimal(table3.Rows[0]["Quantity"].ToString());
                    }
                    if (num2 > num)
                    {
                        str = str + resource.ResourceCode + table.Rows[0]["ResourceName"].ToString() + ",超出预算数量!";
                    }
                }
            }
            decimal num3 = 0M;
            decimal num4 = 0M;
            DataTable table4 = publicDbOpClass.DataTableQuary(" select isnull(sum(isnull(Quantity,0)*isnull(UnitPrice,0)),0) as sumPrice from EPM_Task_Resource a join EPM_V_Res_ResourceBasic b on a.ResourceCode=b.ResourceCode where a.ResourceStyle=2 and b.ResourceType=2 and a.ProjectCode='" + ProjectCode + "' ");
            if (table4.Rows.Count > 0)
            {
                num3 = Convert.ToDecimal(table4.Rows[0]["sumPrice"].ToString());
            }
            DataTable table5 = publicDbOpClass.DataTableQuary(" select isnull(sum(isnull(b.Quantity,0)*isnull(b.UnitPrice,0)),0) as sumPrice from EPM_Stuff_ApplyBill a join EPM_Stuff_ApplyDetails b on a.ApplyUniqueCode=b.ApplyUniqueCode and b.ResourceStyle=2 join EPM_V_Res_ResourceBasic c on b.ResourceCode=c.ResourceCode and c.ResourceType=2 where a.ProjectCode='" + ProjectCode + "' ");
            if (table5.Rows.Count > 0)
            {
                num4 = Convert.ToDecimal(table5.Rows[0]["sumPrice"].ToString());
            }
            if (num4 > num3)
            {
                str = str + "辅材申请总额超出辅材预算总额!";
            }
            return str;
        }

        public static bool DeleteDetailsResTemp(string strUserCode)
        {
            return publicDbOpClass.NonQuerySqlString("delete from EPM_Stuff_ApplyDetails_Temp where UserCode = '" + strUserCode + "'");
        }

        public static bool DelMaterial(Guid billCode, string resourceCode, string UserCode)
        {
            return publicDbOpClass.NonQuerySqlString("delete from EPM_Stuff_ApplyDetails_Temp where ApplyUniqueCode='" + billCode.ToString() + "' and ResourceCode='" + resourceCode + "' and UserCode = '" + UserCode + "'");
        }

        public static bool DelMaterialApply(Guid billCode)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.NonQuerySqlString(((str + " delete from EPM_Stuff_ApplyDetails where ApplyUniqueCode = '" + billCode.ToString() + "'") + " delete from EPM_Stuff_ApplyBill where ApplyUniqueCode = '" + billCode.ToString() + "'") + " end ");
        }

        public static decimal GetMaterialAmountOfYuSuan(Guid ProjectCode, string strResourceCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { " select isnull(sum(isnull(Quantity,0)),0) as Quantity from EPM_Task_Resource where ProjectCode='", ProjectCode, "' and ResourceCode='", strResourceCode, "' " }));
            if (table.Rows.Count > 0)
            {
                return Convert.ToDecimal(table.Rows[0]["Quantity"].ToString());
            }
            return 0M;
        }

        public static string GetWhere(string projectCode, int type)
        {
            string str = "";
            switch (type)
            {
                case -1:
                    return (" projectcode='" + projectCode.ToString() + "' and AuditResult=-1");

                case 0:
                    return (" projectcode='" + projectCode.ToString() + "' and (AuditResult=0)");

                case 1:
                    return (" projectcode='" + projectCode.ToString() + "' and AuditResult=1 ");

                case 2:
                case 3:
                case 4:
                    return str;

                case 5:
                    return (" projectcode='" + projectCode.ToString() + "'");
            }
            return str;
        }

        public static bool InsertDetailsResTemp(Guid ApplyUniqueCode, string strUserCode)
        {
            string str = "insert into EPM_Stuff_ApplyDetails_Temp (ApplyUniqueCode,ResourceCode,ResourceStyle,Quantity,UnitPrice,Supply,ArrivalDate,IsShow,IsDoOver,PlanID,UserCode) ";
            object obj2 = str + " (select ApplyUniqueCode,ResourceCode,ResourceStyle,Quantity,UnitPrice,Supply,ArrivalDate,IsShow,IsDoOver,PlanID,'" + strUserCode + "' from EPM_Stuff_ApplyDetails ";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj2, " where ApplyUniqueCode = '", ApplyUniqueCode, "')" }));
        }

        public static int QueryApplyCount(string sql)
        {
            return publicDbOpClass.GetRecordCount("v_stuff_applybill", sql);
        }

        public static int QueryApplyCount(string projectCode, int type)
        {
            string where = GetWhere(projectCode, type);
            return publicDbOpClass.GetRecordCount("v_stuff_applybill", where);
        }

        public static DataTable QueryApplyDetails(Guid billCode)
        {
            return publicDbOpClass.DataTableQuary("select b.ResourceCode,b.ResourceName,b.Specification,b.UnitName,a.* from EPM_Stuff_ApplyDetails a left join EPM_V_Res_ResourceBasic b on a.ResourceCode=b.ResourceCode and a.ResourceStyle=b.ResourceStyle where a.ApplyUniqueCode='" + billCode.ToString() + "'AND a.Quantity<>0");
        }

        public static DataTable QueryApplyDetailsResTemp(Guid pageGuid, string strUserCode)
        {
            string str = "";
            str = "select b.ResourceCode,b.ResourceName,b.Specification,b.UnitName,a.* from EPM_Stuff_ApplyDetails_Temp a left join EPM_V_Res_ResourceBasic b ";
            return publicDbOpClass.DataTableQuary((str + " on a.ResourceCode=b.ResourceCode and a.ResourceStyle=b.ResourceStyle where ") + " a.UserCode = '" + strUserCode + "'");
        }

        public static DataTable QueryApplyDetailss(Guid billCode)
        {
            return publicDbOpClass.DataTableQuary("select a.resourceCode,a.resourceName,a.specification,a.UnitName,b.quantity,b.unitprice,b.SumPrice,b.Factory,b.FactoryDate from EPM_Stuff_MaterialInDetial b left join v_Res_Resource a on b.resourceCode = a.resourceCode where b.stockincode='" + billCode.ToString() + "'");
        }

        public static DataTable QueryApplyList(string projectCode)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_Stuff_ApplyBill where projectcode='" + projectCode.ToString() + "' and (AuditResult=1 or ApplyType=2)  order by AuditDate desc");
        }

        public static DataTable QueryApplyList(string sql, int pageSize, int pageIndex)
        {
            return publicDbOpClass.GetRecordFromPage("v_stuff_applybill", "ApplyDate", pageSize, pageIndex, 1, sql);
        }

        public static DataTable QueryApplyLists(string projectCode, string sdate, string edate, string code)
        {
            string str2 = "";
            if ((sdate != null) && (edate != null))
            {
                string str3 = str2;
                str2 = str3 + " and c.InDate BETWEEN '" + sdate + "' AND '" + edate + "' ";
            }
            else if (sdate != null)
            {
                str2 = str2 + " and c.InDate='" + sdate + "' ";
            }
            else if (edate != null)
            {
                str2 = str2 + " and c.InDate='" + edate + "' ";
            }
            if (code != null)
            {
                str2 = str2 + " and StockInBillCode='" + code + "'";
            }
            return publicDbOpClass.DataTableQuary("select * from EPM_Stuff_MaterialIn as c inner join EPM_Stuff_ISop  as c1 on c.StockInCode=c1.guidcode where c.ProjectCode='" + projectCode + "'" + str2);
        }

        public static DataTable QueryApplyListZZ(string projectCode, int tt)
        {
            return publicDbOpClass.GetPageData(GetWhere(projectCode, tt), "v_stuff_applybill", "ApplyDate desc");
        }

        public static DataTable QueryOneApplyBill(Guid billCode)
        {
            return publicDbOpClass.DataTableQuary("select * from v_stuff_applybill where ApplyUniqueCode='" + billCode.ToString() + "'");
        }

        public static bool SaveMaterialApply(MaterialApplyBill bill, MaterialApplyResourceCollection resourceList, string UserCode)
        {
            string str = "";
            string format = " insert into EPM_Stuff_ApplyBill(ApplyUniqueCode,ApplyBillCode,ProjectCode,ApplyDate,ApplyMan,Remark,AuditInfo,AuditMan,AuditDate,AuditResult,IsAudit,IsDoOver,ApplyType) values('{0}','{1}','{2}','{3}','{4}','{5}','','',getdate(),-1,{6},0,{7}) ";
            string str3 = " update EPM_Stuff_ApplyBill set ApplyBillCode='{0}',ApplyDate='{1}',ApplyMan='{2}',Remark='{3}',IsAudit=0 where ApplyUniqueCode='{4}'";
            string str4 = " update EPM_Stuff_ApplyDetails_Temp set Quantity={0},UnitPrice={1},Supply={2},ArrivalDate='{3}',IsShow={4} where ApplyUniqueCode='{5}' and ResourceCode='{6}' and ResourceStyle={7} and UserCode ='{8}'";
            str = " begin ";
            object obj2 = str;
            str = (string.Concat(new object[] { obj2, " if not exists(select 1 from EPM_Stuff_ApplyBill where ApplyUniqueCode='", bill.ApplyUniqueCode, "')" }) + string.Format(format, new object[] { bill.ApplyUniqueCode, bill.ApplyBillCode, bill.ProjectCode, bill.ApplyDate, bill.ApplyMan, bill.Remark, bill.IsAudit ? 1 : 0, bill.ApplyType })) + " else " + string.Format(str3, new object[] { bill.ApplyBillCode, bill.ApplyDate, bill.ApplyMan, bill.Remark, bill.ApplyUniqueCode });
            for (int i = 0; i < resourceList.Count; i++)
            {
                MaterialApplyResource resource = resourceList[i];
                str = str + string.Format(str4, new object[] { resource.Quantity, resource.UnitPrice, resource.Supply, resource.ArrivalDate, resource.IsShow ? "1" : "0", bill.ApplyUniqueCode, resource.ResourceCode, resource.ResourceStyle, UserCode });
            }
            object obj3 = str;
            object obj4 = string.Concat(new object[] { obj3, " delete from EPM_Stuff_ApplyDetails where ApplyUniqueCode = '", bill.ApplyUniqueCode, "'" }) + " insert into EPM_Stuff_ApplyDetails (ApplyUniqueCode,ResourceCode,ResourceStyle,Quantity,UnitPrice,Supply,ArrivalDate,IsShow,IsDoOver,PlanID) ";
            return publicDbOpClass.NonQuerySqlString(string.Concat(new object[] { obj4, " (select '", bill.ApplyUniqueCode, "',ResourceCode,ResourceStyle,Quantity,UnitPrice,Supply,ArrivalDate,IsShow,IsDoOver,PlanID from EPM_Stuff_ApplyDetails_Temp where ApplyUniqueCode = '", bill.ApplyUniqueCode, "' and UserCode = '", UserCode, "')" }) + " end ");
        }

        public static bool UpdateMaterialApplyAuditResult(Guid flowGuid)
        {
            string str = "";
            str = " begin ";
            return publicDbOpClass.NonQuerySqlString((str + " update EPM_Stuff_ApplyBill set AuditResult=1 where FlowGuid='" + flowGuid.ToString() + "' ") + " end ");
        }

        public static DataTable WFApplyBill(Guid WFGUID)
        {
            return publicDbOpClass.DataTableQuary("select * from v_stuff_applybill where applyuniquecode='" + WFGUID.ToString() + "'");
        }
    }
}

