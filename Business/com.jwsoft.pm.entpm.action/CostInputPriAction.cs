namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using com.jwsoft.web.WebControls;
    using System;
    using System.Data;

    public class CostInputPriAction
    {
        public static int deleteCostInputPri(string RecordID)
        {
            string str = "begin ";
            return publicDbOpClass.ExecSqlString(((str + " delete from EPM_CostImport where RecordID = '" + RecordID + "'") + " delete from EPM_CostImportChild where RecordID = '" + RecordID + "'") + " end ");
        }

        public static DataTable GetPageData(object PageControl, int iPageSize, string strWhere)
        {
            int num = 1;
            PaginationControl control = (PaginationControl) PageControl;
            num = ((publicDbOpClass.GetRecordCount("EPM_V_CostImport", strWhere) - 1) / iPageSize) + 1;
            control.PageCount = num;
            return publicDbOpClass.GetRecordFromPage("EPM_V_CostImport", "ID", iPageSize, control.CurrentPageIndex, 1, strWhere);
        }

        public static DataTable getSingleCostInputPriTable(Guid RecordID)
        {
            return publicDbOpClass.DataTableQuary("select * from EPM_CostImport where RecordID= '" + RecordID.ToString() + "'");
        }

        public static bool insertCostInput(CostInputPri objInfo, DataTable dt, string opType)
        {
            string str = "begin ";
            if (opType == "add")
            {
                object obj2 = str + " insert into EPM_CostImport(RecordID,PrjCode,CostItemName,HappenUnit,HappenDate,FillPeople,TouchMan) values('";
                str = string.Concat(new object[] { obj2, objInfo.RecordID.ToString(), "','", objInfo.PrjCode, "' ,'", objInfo.CostItemName, "' ,'", objInfo.HappenUnit, "','", objInfo.HappenDate, "','", objInfo.FillPeople, "','", objInfo.TouchMan, "')" });
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string str2 = str + " insert into EPM_CostImportChild (RecordID,ItemName,Price,CostCode,Remark) values ('";
                    string str3 = str2 + dt.Rows[i]["RecordID"].ToString() + "','" + dt.Rows[i]["ItemName"].ToString() + "',cast('";
                    str = (str3 + dt.Rows[i]["Price"].ToString() + "' as money),'" + dt.Rows[i]["CostCode"].ToString() + "','") + dt.Rows[i]["Remark"].ToString() + "')";
                }
            }
            else if (opType == "edit")
            {
                string str4 = str;
                object obj3 = str4 + " update EPM_CostImport set CostItemName = '" + objInfo.CostItemName + "',HappenUnit = '" + objInfo.HappenUnit + "',";
                str = string.Concat(new object[] { obj3, " HappenDate = '", objInfo.HappenDate, "',FillPeople = '", objInfo.FillPeople, "',TouchMan = '", objInfo.TouchMan, "'" }) + " where RecordID = '" + objInfo.RecordID.ToString() + "'";
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (dt.Rows[j]["ChildID"].ToString() == "0")
                    {
                        string str5 = str + " insert into EPM_CostImportChild (RecordID,ItemName,Price,CostCode,Remark) values ('";
                        string str6 = str5 + dt.Rows[j]["RecordID"].ToString() + "','" + dt.Rows[j]["ItemName"].ToString() + "',cast('";
                        str = (str6 + dt.Rows[j]["Price"].ToString() + "' as money),'" + dt.Rows[j]["CostCode"].ToString() + "','") + dt.Rows[j]["Remark"].ToString() + "')";
                    }
                    else
                    {
                        string str7 = str + " update EPM_CostImportChild set ItemName = '" + dt.Rows[j]["ItemName"].ToString() + "',";
                        string str8 = str7 + " Price = cast('" + dt.Rows[j]["Price"].ToString() + "' as money),CostCode = '" + dt.Rows[j]["CostCode"].ToString() + "',";
                        str = str8 + " Remark = '" + dt.Rows[j]["Remark"].ToString() + "' where ChildID = " + dt.Rows[j]["ChildID"].ToString();
                    }
                }
            }
            return publicDbOpClass.NonQuerySqlString(str + " end");
        }

        public static int updateCostJudgePri(CostInputPri objInfo)
        {
            object obj2 = string.Concat(new object[] { "update EPM_CostImport set AuditPeople = '", objInfo.AuditPeople, "', AuditDate = '", objInfo.AuditDate, "', AuditResult = ", objInfo.AuditResult, ", Remark = '", objInfo.Remark, "'" });
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { obj2, " where RecordID = '", objInfo.RecordID, "'" }));
        }
    }
}

