namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;

    public class ContractAlertAction
    {
        public static bool DelInfo(int AlertCode)
        {
            string format = "delete prj_AlertMessage_Contract where AlertCode = {0}";
            return publicDbOpClass.NonQuerySqlString(string.Format(format, AlertCode));
        }

        public static DataTable GetAlertInfosByContractCode(string ContractCode)
        {
            string format = "Select * from prj_AlertMessage_Contract where ContractCode='{0}'";
            return publicDbOpClass.DataTableQuary(string.Format(format, ContractCode));
        }

        public static DataTable GetContractAlertMessageByUser(string UserCode)
        {
            return publicDbOpClass.DataTableQuary("select *,(select PrjName from pt_PrjInfo Project,EPM_Con_ContractMain Contract where Project.Prjguid = Contract.ProjectCode and Contract.contractCode = prj_AlertMessage_Contract.contractCode) as PrjName ,dateadd(day,ValidDate-BeforeDate,HappenDate) as FAILUREDate from prj_AlertMessage_Contract where charindex('" + UserCode + "',AlertTo)>0\tand (dateadd(day,-beforedate,happendate)<=getdate()\tand dateadd(day,ValidDate,dateadd(day,-beforedate,happendate))>=getdate())");
        }

        public static string GetUserNames(string UserCodes)
        {
            string format = "select * from pt_yhmc where charindex(v_yhdm,'{0}')>0 ";
            format = string.Format(format, UserCodes);
            string str2 = "";
            foreach (DataRow row in publicDbOpClass.DataTableQuary(format).Rows)
            {
                str2 = str2 + row["v_xm"].ToString() + ",";
            }
            return ((str2.Length > 0) ? str2.Remove(str2.Length - 1, 1) : "");
        }

        public static bool SaveInfo(ContractAlertInfo info)
        {
            string format = "";
            if (info.AlertCode == 0)
            {
                format = "insert into prj_AlertMessage_Contract(ContractCode,HappenDate,StageAmount,AlertTo,BeforeDate,ValidDate) values ('{0}','{1}',{2},'{3}',{4},{5})";
                format = string.Format(format, new object[] { info.ContractCode, info.HappenDate, info.StageAmount, info.AlertTo, info.BeforeDate, info.ValidDate });
            }
            else
            {
                format = "update prj_AlertMessage_Contract set HappenDate='{0}',StageAmount={1},AlertTo='{2}',BeforeDate={3},ValidDate={4} where AlertCode = {5}";
                format = string.Format(format, new object[] { info.HappenDate, info.StageAmount, info.AlertTo, info.BeforeDate, info.ValidDate, info.AlertCode });
            }
            return publicDbOpClass.NonQuerySqlString(format);
        }
    }
}

