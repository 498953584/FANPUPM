namespace cn.justwin.opm.action
{
    using cn.justwin.opm.model;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class Business_DataAction
    {
        public bool Add(Business_DataModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" insert into opm_business_data(uid,btype,prjid,bcode,bname,codeid,dutyuser,designer,flowstate,begindate,  ");
            builder.Append(" enddate,cause,remark,adduser,addtime,isvalid) ");
            builder.Append(" values( ");
            builder.Append(" '" + model.UID + "', ");
            builder.Append(" '" + model.BType + "', ");
            builder.Append(" '" + model.PrjId + "', ");
            builder.Append(" '" + model.BCode + "', ");
            builder.Append(" '" + model.BName + "', ");
            builder.Append(" " + model.CodeId + ", ");
            builder.Append(" '" + model.DutyUser + "', ");
            builder.Append(" '" + model.Designer + "',");
            builder.Append(" " + model.FlowState + ", ");
            builder.Append(" '" + model.BeginDate + "', ");
            if (model.EndDate != null)
            {
                builder.Append(" '" + model.EndDate + "', ");
            }
            else
            {
                builder.Append(" NULL, ");
            }
            builder.Append(" '" + model.Cause + "', ");
            builder.Append(" '" + model.Remark + "', ");
            builder.Append(" '" + model.AddUser + "', ");
            builder.Append(" '" + model.AddTime + "', ");
            builder.Append(" '" + model.IsValid + "' )");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public string ChkFlowState(string codeId, string prjid)
        {
            string sqlString = "select * from opm_business_data where flowstate=1 ";
            if (!string.IsNullOrEmpty(codeId))
            {
                sqlString = sqlString + " and codeid='" + codeId + "'";
            }
            if (!string.IsNullOrEmpty(prjid))
            {
                sqlString = sqlString + " and prjid='" + prjid + "'";
            }
            if (publicDbOpClass.DataTableQuary(sqlString).Rows.Count > 0)
            {
                return "1";
            }
            return "0";
        }

        public bool Delete(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin  ");
            builder.Append(" delete from opm_business_data where uid in (" + uid + ")");
            builder.Append(" delete from OPM_Business_DataItem where CodeID in (" + uid + ") ");
            builder.Append(" end ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public DataTable GetListByBusType(string bType, string pc, string codeId, string sqlWhere, bool isAll)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from ( select  *,(select top 1 parentcodelist from XPM_Basic_CodeList join XPM_Basic_CodeType on XPM_Basic_CodeList.TypeID=XPM_Basic_CodeType.TypeID where ");
            builder.Append(" (SignCode='" + bType + "') and (XPM_Basic_CodeList.IsVisible=1) and codeid=a.codeid ) parentcodelist,(select top 1 operator from wf_instance where id =(select top(1) id from wf_instance_main where instanceCode=a.uid) order by theorder desc) as Operator from opm_business_data a ");
            builder.Append(" where btype= '" + bType + "' and prjid='" + pc + "' ");
            if ((sqlWhere != null) && (sqlWhere != ""))
            {
                builder.Append(sqlWhere);
            }
            builder.Append(" and isvalid='1' ) as newtable  ");
            if (isAll)
            {
                builder.Append("  where  parentcodelist like '%," + codeId + ",%' ");
            }
            builder.Append("  order by EndDate desc  ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetModel(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select *  from opm_business_data ");
            builder.Append(" where uid= '" + uid + "' and isvalid=1 ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public bool Update(Business_DataModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" update opm_business_data set  ");
            builder.Append(" btype='" + model.BType + "',");
            builder.Append(" BCode='" + model.BCode + "',");
            builder.Append(" BName='" + model.BName + "',");
            builder.Append(" DutyUser='" + model.DutyUser + "',");
            builder.Append(" Designer='" + model.Designer + "',");
            builder.Append(" BeginDate='" + model.BeginDate + "',");
            if (model.EndDate != null)
            {
                builder.Append(" EndDate='" + model.EndDate + "',");
            }
            else
            {
                builder.Append(" EndDate=NULL,");
            }
            builder.Append(" Cause='" + model.Cause + "',");
            builder.Append(" Remark='" + model.Remark + "'");
            builder.Append(" where UID='" + model.UID + "'");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

