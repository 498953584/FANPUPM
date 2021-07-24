namespace com.jwsoft.pm.entpm.action
{
    using BonZe.DataAccess;
    using com.jwsoft.common.data;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class WaitingJob
    {
        private string _mkdm;

        public WaitingJob(string MKDM)
        {
            this._mkdm = MKDM;
        }

        public void Delete()
        {
            string commandText = "delete from prj_waitingJob where mkdm='" + this._mkdm + "'";
            SqlHelper.ExecuteNonQuery(this.cn.SqlConnectionSystem(), CommandType.Text, commandText);
        }

        public static DataTable GetAgentWorks(string UserCode)
        {
            string spName = "prj_sp_getWaitingJobs";
            SqlParameter[] commandParameters = new SqlParameter[] { new SqlParameter("@yhdm", UserCode) };
            return publicDbOpClass.ExecuteDataTable(spName, commandParameters);
        }

        public static DataTable GetAgentWorkSetting(string mkdm)
        {
            return publicDbOpClass.DataTableQuary("select mkdm,url,title,sql,memo,isValid from prj_waitingJob where mkdm='" + mkdm + "'");
        }

        public static string GetFullModuleName(string mkdm)
        {
            return (string) publicDbOpClass.ExecuteScalar("declare @str varchar(500) ; set @str = ''  select @str=@str+' ->'+v_mkmc from pt_mk where patindex(c_mkdm+'%','" + mkdm + "')>0 order by c_mkdm  select substring(@str,4,len(@str)-3)");
        }

        public void Insert(params SqlParameter[] myparms)
        {
            string format = "insert into prj_waitingJob(mkdm,url,title,sql,memo,isvalid) values('{0}',@url,@title,@sql,@memo,@isvalid)";
            format = string.Format(format, this._mkdm);
            SqlHelper.ExecuteNonQuery(this.cn.SqlConnectionSystem(), CommandType.Text, format, myparms);
        }

        public void Update(params SqlParameter[] myparms)
        {
            string commandText = "update prj_waitingJob set title=@title,sql=@sql,memo=@memo,isvalid=@isvalid where mkdm='" + this._mkdm + "'";
            SqlHelper.ExecuteNonQuery(this.cn.SqlConnectionSystem(), CommandType.Text, commandText, myparms);
        }

        private Conn cn
        {
            get
            {
                return new Conn();
            }
        }
    }
}

