namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class BindBudgetAction
    {
        public static bool Add(BindBudget model)
        {
            string str = "";
            str = "insert into BindBudget(";
            str = str + "PrjCode,Templatescode,TaskCode)" + " values ('";
            return (publicDbOpClass.ExecSqlString(string.Concat(new object[] { str, model.PrjCode, "','", model.Templatescode, "','", model.TaskCode, "')" })) > 0);
        }

        public static bool Delete(string templeatesCode)
        {
            string str = "";
            str = "delete from BindBudget ";
            return (publicDbOpClass.ExecSqlString(str + " where Templatescode=" + templeatesCode) > 0);
        }

        public static decimal getAllccTotal(string PrjCode, string Templatescode)
        {
            decimal num = 0.00M;
            DataSet set = new DataSet();
            set = publicDbOpClass.DataSetQuary(string.Concat(new object[] { "select isnull(cast(sum(book.Quantity*book.UnitPrice)AS decimal(18, 2)),0) as cctotal from EPM_Book_Resource as book inner join  BindBudget as bind on book.ProjectCode=Bind.PrjCode and book.TaskCode=Bind.TaskCode where substring(Bind.Templatescode,1,", Templatescode.Length, ") ='", Templatescode, "' and Bind.PrjCode='", PrjCode, "'" }));
            if (set.Tables[0].Rows.Count > 0)
            {
                num = Convert.ToDecimal(set.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        public static DataSet getAllNews(string PrjCode, string Templatescode)
        {
            DataSet set = new DataSet();
            return publicDbOpClass.DataSetQuary("select Task.Quantity,Task.TaskName,Task.SumPrice,Bind.*,(ISNULL((select cast(sum(isnull(quantity,0)*isnull(UnitPrice,0))AS decimal(18, 2)) from EPM_Task_Resource where Projectcode=Bind.PrjCode  and TaskCode =Bind.TaskCode),0)) as total, (isnull((select sum(TodayComplete) as SumCompleteQuantity from EPM_Book_ConstructTask where ProjectCode=Bind.PrjCode and TaskCode=Bind.TaskCode group by ProjectCode,TaskCode ),0)) as SumCompleteQuantity,(isnull((select cast(sum(Quantity*UnitPrice)AS decimal(18, 2)) from EPM_Book_Resource where ProjectCode=Bind.PrjCode and TaskCode=Bind.TaskCode group by Projectcode,TaskCode ),0)) as cctotal from EPM_Task_TaskList as Task inner join BindBudget as Bind on Task.ProjectCode=Bind.PrjCode and Task.TaskCode=Bind.TaskCode where Bind.PrjCode='" + PrjCode + "' and Bind.Templatescode='" + Templatescode + "'");
        }

        public static decimal getAlltotal(string PrjCode, string Templatescode)
        {
            decimal num = 0.00M;
            DataSet set = new DataSet();
            set = publicDbOpClass.DataSetQuary(string.Concat(new object[] { "select isnull(cast(sum(isnull(res.quantity,0)*isnull(res.UnitPrice,0))AS decimal(18, 2)),0) as total from EPM_Task_Resource as res inner join  BindBudget as bind on res.Projectcode=Bind.PrjCode  and res.TaskCode =Bind.TaskCode where substring(Bind.Templatescode,1,", Templatescode.Length, ") ='", Templatescode, "' and Bind.PrjCode='", PrjCode, "'" }));
            if (set.Tables[0].Rows.Count > 0)
            {
                num = Convert.ToDecimal(set.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        public static DataSet GetList(string strWhere)
        {
            string sqlString = "";
            sqlString = "select NoteId,PrjCode,Templatescode,TaskCode ";
            sqlString = sqlString + " FROM BindBudget ";
            if (strWhere.Trim() != "")
            {
                sqlString = sqlString + " where " + strWhere;
            }
            return publicDbOpClass.DataSetQuary(sqlString);
        }

        public BindBudget GetModel(int NoteId)
        {
            string sqlString = "";
            sqlString = "select NoteId,PrjCode,Templatescode,TaskCode from BindBudget ";
            sqlString = sqlString + " where NoteId=" + NoteId;
            BindBudget budget = new BindBudget();
            DataSet set = publicDbOpClass.DataSetQuary(sqlString);
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["NoteId"].ToString() != "")
            {
                budget.NoteId = Convert.ToInt32(set.Tables[0].Rows[0]["NoteId"].ToString());
            }
            if (set.Tables[0].Rows[0]["PrjCode"].ToString() != "")
            {
                budget.PrjCode = new Guid(set.Tables[0].Rows[0]["PrjCode"].ToString());
            }
            budget.Templatescode = set.Tables[0].Rows[0]["Templatescode"].ToString();
            budget.TaskCode = set.Tables[0].Rows[0]["TaskCode"].ToString();
            return budget;
        }

        public static DataTable getNewsreturnTable(string PrjCode, string Templatescode)
        {
            DataTable table = new DataTable();
            return publicDbOpClass.DataTableQuary("select Bind.TaskCode,Task.TaskName,Bind.TemplatesCode as ParentTaskCode,Task.Quantity, (isnull((select sum(TodayComplete) as SumCompleteQuantity from EPM_Book_ConstructTask where ProjectCode=Bind.PrjCode and TaskCode=Bind.TaskCode group by ProjectCode,TaskCode ),0)) as SumCompleteQuantity,(ISNULL((select cast(sum(isnull(quantity,0)*isnull(UnitPrice,0))AS decimal(18, 2)) from EPM_Task_Resource where Projectcode=Bind.PrjCode  and TaskCode =Bind.TaskCode),0)) as total,(isnull((select cast(sum(Quantity*UnitPrice)AS decimal(18, 2)) from EPM_Book_Resource where ProjectCode=Bind.PrjCode and TaskCode=Bind.TaskCode group by Projectcode,TaskCode ),0)) as cctotal from EPM_Task_TaskList as Task inner join BindBudget as Bind on Task.ProjectCode=Bind.PrjCode and Task.TaskCode=Bind.TaskCode where Bind.PrjCode='" + PrjCode + "' and Bind.Templatescode='" + Templatescode + "'");
        }

        public static decimal getTotalQuantity(string PrjCode, string Templatescode)
        {
            decimal num = 0.00M;
            DataSet set = new DataSet();
            set = publicDbOpClass.DataSetQuary(string.Concat(new object[] { "select isnull(sum(Task.Quantity),0) as Quantity from EPM_Task_TaskList as Task inner join BindBudget as Bind on Task.ProjectCode=Bind.PrjCode and Task.TaskCode=Bind.TaskCode where Bind.PrjCode='", PrjCode, "' and substring(Bind.Templatescode,1,", Templatescode.Length, ") ='", Templatescode, "'" }));
            if (set.Tables[0].Rows.Count > 0)
            {
                num = Convert.ToDecimal(set.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        public static decimal gettotalSumCompleteQuantity(string PrjCode, string Templatescode)
        {
            decimal num = 0.00M;
            DataSet set = new DataSet();
            set = publicDbOpClass.DataSetQuary(string.Concat(new object[] { "select isnull(sum(task.TodayComplete),0) as SumCompleteQuantity from EPM_Book_ConstructTask as task inner join  BindBudget as bind on task.ProjectCode=Bind.PrjCode and task.TaskCode=Bind.TaskCode where substring(Bind.Templatescode,1,", Templatescode.Length, ") ='", Templatescode, "' and Bind.PrjCode='", PrjCode, "'" }));
            if (set.Tables[0].Rows.Count > 0)
            {
                num = Convert.ToDecimal(set.Tables[0].Rows[0][0].ToString());
            }
            return num;
        }

        public static bool ishave(string PrjCode, string Templatescode, string taskCode)
        {
            string sqlString = "";
            sqlString = "select NoteId,PrjCode,Templatescode,TaskCode from BindBudget ";
            sqlString = sqlString + " where PrjCode='" + PrjCode + "' and Templatescode='" + Templatescode + "' and TaskCode='" + taskCode + "'";
            new BindBudget();
            return (publicDbOpClass.DataSetQuary(sqlString).Tables[0].Rows.Count > 0);
        }

        public bool Update(BindBudget model)
        {
            string str = "";
            str = "update BindBudget set ";
            return (publicDbOpClass.ExecSqlString(((string.Concat(new object[] { str, "PrjCode='", model.PrjCode, "'," }) + "Templatescode='" + model.Templatescode + "',") + "TaskCode='" + model.TaskCode + "'") + " where NoteId=" + model.NoteId) > 0);
        }
    }
}

