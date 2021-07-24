namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class SalaryTAction
    {
        public int Add(SalaryTModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into HR_Salary_Templet(");
            builder.Append("TempletName,UseState,UserCode,RecordDate,ClassCode,BeginDate,EndDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.TempletName + "',");
            builder.Append(model.UseState + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.ClassCode + "',");
            builder.Append("'" + model.BeginDate + "',");
            builder.Append("'" + model.EndDate + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int TempletID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete HR_Salary_Templet ");
            builder.Append(" where TempletID=" + TempletID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int ExecTempletTable(int tempid)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select count(*) from sysobjects where id = object_id('hxerp.dbo.HR_Salary_EST_" + tempid + "')");
            if (table.Rows.Count > 0)
            {
                return Convert.ToInt32(table.Rows[0][0]);
            }
            return 0;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select TempletID,TempletName,UseState,UserCode,RecordDate,ClassCode,BeginDate,EndDate ");
            builder.Append(" FROM HR_Salary_Templet ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public SalaryTModel GetModel(int TempletID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" TempletID,TempletName,UseState,UserCode,RecordDate,ClassCode,BeginDate,EndDate ");
            builder.Append(" from HR_Salary_Templet ");
            builder.Append(" where TempletID=" + TempletID + " ");
            SalaryTModel model = new SalaryTModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["TempletID"].ToString() != "")
            {
                model.TempletID = int.Parse(set.Tables[0].Rows[0]["TempletID"].ToString());
            }
            model.TempletName = set.Tables[0].Rows[0]["TempletName"].ToString();
            if (set.Tables[0].Rows[0]["UseState"].ToString() != "")
            {
                model.UseState = new int?(int.Parse(set.Tables[0].Rows[0]["UseState"].ToString()));
            }
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString()));
            }
            model.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            if (set.Tables[0].Rows[0]["BeginDate"].ToString() != "")
            {
                model.BeginDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["BeginDate"].ToString()));
            }
            if (set.Tables[0].Rows[0]["EndDate"].ToString() != "")
            {
                model.EndDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["EndDate"].ToString()));
            }
            return model;
        }

        public string strClassName(string ClassCode)
        {
            DataTable table = publicDbOpClass.DataTableQuary(" SELECT [ClassID], [ClassName], [Remark], [IsValid], [ClassCode], [CorpCode], [ClassTypeCode] FROM [PT_SingleClasses]    WHERE [ClassTypeCode] =" + " (select [ClassTypeCode] from PT_D_SingleClass where FilterField = 'HumanType') and ClassCode='" + ClassCode + "'");
            if (table.Rows.Count > 0)
            {
                return table.Rows[0]["ClassName"].ToString();
            }
            return "";
        }

        public int TempletUse(int tempid)
        {
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@templetid", SqlDbType.Int, 4), new SqlParameter("@i_Ret", SqlDbType.Int, 4) };
            commandParameters[0].Value = tempid;
            commandParameters[1].Direction = ParameterDirection.Output;
            publicDbOpClass.ExecuteProcedure("HR_Salary_UseTemplate", commandParameters);
            return Convert.ToInt32(commandParameters[1].Value);
        }

        public int Update(SalaryTModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update HR_Salary_Templet set ");
            builder.Append("TempletName='" + model.TempletName + "',");
            builder.Append("UseState=" + model.UseState + ",");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("ClassCode='" + model.ClassCode + "',");
            builder.Append("BeginDate='" + model.BeginDate + "',");
            builder.Append("EndDate='" + model.EndDate + "'");
            builder.Append(" where TempletID=" + model.TempletID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

