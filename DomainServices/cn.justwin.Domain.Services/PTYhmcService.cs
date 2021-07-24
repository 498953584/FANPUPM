namespace cn.justwin.Domain.Services
{
    using cn.justwin.Domain.Entities;
    using cn.justwin.Domain.Repositories;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;
    using System.Text;
    using WcfNHibernate;

    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall), AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed), NHibernateContext, ServiceContract]
    public class PTYhmcService : Repository<PTyhmc>
    {
        public Hashtable GetAllUser()
        {
            Hashtable hashtable = new Hashtable();
            foreach (PTyhmc tyhmc in this)
            {
                hashtable.Add(tyhmc.v_yhdm, tyhmc.v_xm);
            }
            return hashtable;
        }

        [WebGet(UriTemplate="/PTYhmc/{id}", ResponseFormat=WebMessageFormat.Json), OperationContract]
        public PTyhmc GetById(string id)
        {
            return (from y in this
                where y.v_yhdm == id
                select y).FirstOrDefault<PTyhmc>();
        }

        public IList<PTyhmc> GetChargeMan(string id)
        {
            int bmdm = Convert.ToInt32(id);
            return (from y in this
                where (y.i_bmdm == bmdm) && y.IsChargeMan
                select y).ToList<PTyhmc>();
        }

        public DataTable GetEmployees(string userCode, string userName, string roleTypeName, int? bmdm, int? state, int pageSize, int pageIndex)
        {
            if (pageSize == 0)
            {
                pageSize = this.GetEmployeesCount(userCode, userName, roleTypeName, bmdm, state);
            }
            if (pageIndex == 0)
            {
                pageIndex = 1;
            }
            DataTable table = new DataTable();
            List<SqlParameter> list = new List<SqlParameter>();
            StringBuilder builder = new StringBuilder();
            if (bmdm.HasValue)
            {
                builder.Append(" AND y.i_bmdm=@bmdm  ");
                list.Add(new SqlParameter("@bmdm", bmdm.Value));
            }
            if (state.HasValue)
            {
                builder.Append(" AND State=@state  ");
                list.Add(new SqlParameter("@state", state.Value));
            }
            if (!string.IsNullOrEmpty(roleTypeName))
            {
                builder.Append(" AND DutyName LIKE '%'+@roleTypeName+'%' ");
                list.Add(new SqlParameter("@roleTypeName", roleTypeName));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @userCode nvarchar(50),@userName nvarchar(50),@roleTypeName nvarchar(50),\r\n\t\t\t-- @bmdm int,@state int,@pageSize int,@pageIndex int\r\n\t\t\t--SET @userCode=''\r\n\t\t\t--SET @userName=''\r\n\t\t\t--SET @roleTypeName='' \n\t\t\t--SET @state=1\n\t\t\t--SET @bmdm=124\n\t\t\t--SET @pageSize=500\r\n\t\t\t--SET @pageIndex=1\n\t\t\tSELECT TOP(@pageSize) Num,v_yhdm,UserCode,v_xm,RoleTypeName,EnterCorpDate,LeaveDate,\n\t\t\tWorkingdates,StateName,MobilePhoneCode FROM ( \n\t\t\t\tSELECT Row_Number()OVER(ORDER BY PT_d_bm.i_xh, y.I_DUTYID) AS Num , \n\t\t\t\ty.v_yhdm,ISNULL(y.UserCode,'') UserCode,v_xm,DutyName RoleTypeName,\n\t\t\t\tEnterCorpDate,LeaveDate,y.c_sfyx,\n\t\t\t\t(case when LeaveDate is null then DateDiff(dd,EnterCorpDate,GetDate()) \n\t\t\t\t\twhen LeaveDate is not null then DateDiff(dd,EnterCorpDate,LeaveDate) end) Workingdates,\n\t\t\t\t(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName,\n\t\t\t\tMobilePhoneCode\n\t\t\t\tFROM PT_yhmc AS y \n\t\t\t\tLEFT JOIN PT_d_bm ON y.i_bmdm=PT_d_bm.i_bmdm  \n\t\t\t\tLEFT JOIN PT_DUTY ON y.I_DUTYID = PT_DUTY.I_DUTYID  \n\t\t\t\tWHERE y.c_sfyx='y' AND PT_d_bm.c_sfyx='y' \n\t\t\t\tAND UserCode LIKE '%'+@userCode+'%' AND v_xm LIKE '%'+@userName+'%'";
            cmdText = cmdText + builder.ToString() + " ) Employees WHERE Num>@pageSize*(@pageIndex-1) ";
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@userName", userName));
            list.Add(new SqlParameter("@pageSize", pageSize));
            list.Add(new SqlParameter("@pageIndex", pageIndex));
            return base.ExecuteQuery(cmdText, list.ToArray());
        }

        public int GetEmployeesCount(string userCode, string userName, string roleTypeName, int? bmdm, int? state)
        {
            int num = 0;
            StringBuilder builder = new StringBuilder();
            List<SqlParameter> list = new List<SqlParameter>();
            if (bmdm.HasValue)
            {
                builder.Append(" AND y.i_bmdm=@bmdm  ");
                list.Add(new SqlParameter("@bmdm", bmdm.Value));
            }
            if (state.HasValue)
            {
                builder.Append(" AND State=@state  ");
                list.Add(new SqlParameter("@state", state.Value));
            }
            if (!string.IsNullOrEmpty(roleTypeName))
            {
                builder.Append(" AND DutyName LIKE '%'+@roleTypeName+'%' ");
                list.Add(new SqlParameter("@roleTypeName", roleTypeName));
            }
            string cmdText = "\r\n\t\t\t--DECLARE @userCode nvarchar(50),@userName nvarchar(50),@roleTypeName nvarchar(50),\r\n\t\t\t--@state int,@bmdm int\r\n\t\t\t--SET @userCode=''\r\n\t\t\t--SET @userName=''\r\n\t\t\t--SET @roleTypeName='' \n\t\t\t--SET @state=2\n\t\t\t--SET @bmdm=124\n\t\t\tSELECT COUNT(*) FROM ( \n\t\t\t\tSELECT y.v_yhdm,ISNULL(y.UserCode,'') UserCode,v_xm,DutyName RoleTypeName,\n\t\t\t\tEnterCorpDate,LeaveDate,y.c_sfyx,\n\t\t\t\t(case when LeaveDate is null then DateDiff(dd,EnterCorpDate,GetDate()) \n\t\t\t\t\twhen LeaveDate is not null then DateDiff(dd,EnterCorpDate,LeaveDate) end) Workingdates,\n\t\t\t\t(case State when 0 then '试用' when 1 then '在职' when 2 then '离职' end ) as StateName\n\t\t\t\tFROM PT_yhmc AS y \n\t\t\t\tLEFT JOIN PT_d_bm ON y.i_bmdm=PT_d_bm.i_bmdm  \n\t\t\t\tLEFT JOIN PT_DUTY ON y.I_DUTYID = PT_DUTY.I_DUTYID \n\t\t\t\tWHERE y.c_sfyx='y' AND UserCode LIKE '%'+@userCode+'%' AND v_xm LIKE '%'+@userName+'%' ";
            cmdText = cmdText + builder.ToString() + ") Employees";
            list.Add(new SqlParameter("@userCode", userCode));
            list.Add(new SqlParameter("@userName", userName));
            DataTable table = base.ExecuteQuery(cmdText, list.ToArray());
            if (table != null)
            {
                num = Convert.ToInt32(table.Rows[0][0]);
            }
            return num;
        }

        public string GetName(string code)
        {
            return (from y in this
                where y.v_yhdm == code
                select y.v_xm).FirstOrDefault<string>();
        }

        public IList<string> GetNameList(IList<string> codeList)
        {
            List<string> list = new List<string>();
            foreach (string str in codeList)
            {
                list.Add(this.GetName(str));
            }
            return list;
        }
    }
}

