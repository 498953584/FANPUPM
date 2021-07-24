namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class PlanAction
    {
        public DataTable GetTaskBid(Guid ProjectCode, int WorkLayer)
        {
            string str = "";
            object obj2 = str;
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " select TaskCode,TaskName from EPM_Task_TaskList where ProjectCode='", ProjectCode, "' and WorkLayer = ", WorkLayer }));
        }

        public DataTable GetTaskBid(Guid ProjectCode, string TaskCode)
        {
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select *,isnull(Quantity,0) as TotalQuantity,isnull(Quantity,0)-isnull(CompleteCount,0) as LeavlQuantity from EPM_Task_TaskList where ProjectCode = '", ProjectCode, "' and TaskCode='", TaskCode, "' and WbsType=1 " }));
        }

        public bool IsDateTime(WBSBidTask wbid)
        {
            bool flag = false;
            object obj2 = string.Concat(new object[] { "select * from EPM_Task_TaskList where ProjectCode='", wbid.ProjectCode, "' and TaskCode ='", wbid.ParentTaskCode, "'" });
            if (publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, "and datediff(dd,StartDate,'", wbid.StartDate, "')>0 and datediff(dd,EndDate,'", wbid.EndDate, "')<0" })).Rows.Count > 0)
            {
                flag = true;
            }
            return flag;
        }

        public DataTable Resource(Guid prjCode, string taskCode, int style)
        {
            string sqlString = "";
            string str2 = sqlString + "select b.ResourceCode,b.wastage,b.unitprice,c.ResourceName,c.Specification,c.UnitName,c.ResourceStyle ";
            sqlString = (str2 + " from (select a.ResourceCode,sum(a.Wastage) as Wastage,sum(a.unitprice) as unitprice  from EPM_Task_Resource a where a.ProjectCode='" + prjCode.ToString() + "' and a.TaskCode = '" + taskCode + "' group by ResourceCode) b") + " left join epm_v_res_resourcebasic c on b.ResourceCode = c.ResourceCode where (1=1)";
            switch (style)
            {
                case 0:
                    sqlString = sqlString + " and c.ResourceType=1";
                    break;

                case 1:
                case 2:
                case 3:
                    sqlString = sqlString + " and c.ResourceStyle=" + style.ToString();
                    break;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable Resource(Guid prjCode, string taskCode, int style, string StepCode)
        {
            string sqlString = "";
            if (StepCode == "")
            {
                string str2 = sqlString + "select b.ResourceCode,b.Quantity,c.ResourceName,c.Specification,c.UnitName,c.Price,c.ResourceStyle ";
                sqlString = (str2 + " from (select a.ResourceCode,sum(a.Wastage) as Quantity  from EPM_Task_Resource a where a.ProjectCode='" + prjCode.ToString() + "' and a.TaskCode like '" + taskCode + "%' group by ResourceCode) b") + " left join EPM_Res_PublicResource c on b.ResourceCode = c.ResourceCode where (1=1)";
            }
            else
            {
                string str3 = sqlString + "select b.ResourceCode,b.Quantity,c.ResourceName,c.Specification,c.UnitName,c.Price,c.ResourceStyle ";
                sqlString = (str3 + " from (select a.ResourceCode,sum(a.Wastage) as Quantity  from EPM_Task_Resource a where a.ProjectCode='" + prjCode.ToString() + "' and a.StepCode like '" + StepCode + "%' group by ResourceCode) b") + " left join EPM_Res_PublicResource c on b.ResourceCode = c.ResourceCode where (1=1)";
            }
            switch (style)
            {
                case 0:
                    sqlString = sqlString + " and c.ResourceType=1";
                    break;

                case 1:
                case 2:
                case 3:
                    sqlString = sqlString + " and c.ResourceStyle=" + style.ToString();
                    break;
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int TaskBidUP(WBSBidTask wbid)
        {
            return publicDbOpClass.ExecSqlString(string.Concat(new object[] { " update EPM_Task_TaskList set StartDate='", wbid.StartDate, "',EndDate='", wbid.EndDate, "'" }) + " where NoteID = " + wbid.NoteID);
        }

        public int TaskRationDel(int id)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(str + "delete EPM_Task_Ration where NoteID = " + id);
        }

        public DataTable TaskRationDT(Guid prjCode, string taskCode)
        {
            string str = "";
            object obj2 = str + "select a.NoteID,a.RationItem,a.ItemUnit ,a.Quantity,b.ItemName , b.WorkContent from " + " EPM_Task_Ration a left join EPM_Ration_PrivateItem b on a.RationItem = b.ItemCode";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " where a.ProjectCode = '", prjCode, "' and a.TaskCode = '", taskCode, "'" }));
        }
    }
}

