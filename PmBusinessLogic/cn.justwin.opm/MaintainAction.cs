namespace cn.justwin.opm
{
    using cn.justwin.opm.Maintain;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class MaintainAction
    {
        public bool Addmaintains(MaintainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OPM_Maintain(maintainID, maintaincode,prjid,UrgencyDegree,MaintainItem,MaintainType,ApplyFee,ApplyDate,ProblemStatement,Tel,");
            builder.Append("MaintainMan,ApplyDptDutyMan,AdminDptMan, StoreManager,Dept,AddUser,AddTime,Remark,i_xh,IsValid,FlowState)");
            builder.Append(" values(");
            builder.Append("'" + model.MaintainId + "',");
            builder.Append("'" + model.MaintainCode + "',");
            builder.Append("'" + model.Prjid + "',");
            builder.Append("'" + model.UrgencyDegree + "',");
            builder.Append("'" + model.MaintainItem + "',");
            builder.Append("'" + model.MaintainType + "',");
            builder.Append(model.ApplyFee + ",");
            builder.Append("'" + model.ApplyDate + "',");
            builder.Append("'" + model.ProblemStatement + "',");
            builder.Append("'" + model.Tel + "',");
            builder.Append("'" + model.MaintainMan + "',");
            builder.Append("'" + model.ApplyDptDutyMan + "',");
            builder.Append("'" + model.AdminDptMan + "',");
            builder.Append("'" + model.StoreManager + "',");
            builder.Append("'" + model.Dept + "',");
            builder.Append("'" + model.Adduser + "',");
            builder.Append("'" + model.Addtime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append(model.I_xh + ",");
            builder.Append("'" + model.Isvalid + "',");
            builder.Append(model.Flowstate);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public int DeleteMaintains(Guid InUid)
        {
            return publicDbOpClass.ExecSqlString("delete from OPM_maintain where maintainid='" + InUid + "'");
        }

        public string GetDocTypeByID(int typeid, int codeID)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select CodeName from XPM_Basic_CodeList where typeID=", typeid, " and codeID=", codeID }));
            string str2 = string.Empty;
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            return str2;
        }

        public DataTable getMaintain_ByID(Guid maintainID)
        {
            string str = "select OPM_Maintain.* ,pt_prjinfo.prjname,pt_prjinfo.prjplace from OPM_Maintain join pt_prjinfo ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, "on pt_prjinfo.prjguid=OPM_Maintain.prjid where OPM_Maintain.maintainId='", maintainID, "'" }));
        }

        public DataTable getMaintains(Guid prjid)
        {
            string str = "select OPM_Maintain.* ,pt_prjinfo.prjname,pt_prjinfo.prjplace from OPM_Maintain join pt_prjinfo ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, "on pt_prjinfo.prjguid=OPM_Maintain.prjid where OPM_Maintain.prjid='", prjid, "'" }) + "order by ApplyDate");
        }

        public string GetStoreCodeByID(Guid prjID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("select Owner from PT_PrjInfo where PrjGuid='" + prjID + "'");
            string str2 = string.Empty;
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            return str2;
        }

        public int IsmaintainCode(string maintainCode)
        {
            return publicDbOpClass.DataTableQuary("select * from OPM_maintain where maintainCode='" + maintainCode + "'").Rows.Count;
        }

        public DataTable SearchMaintains(string maintainCode, string prjid)
        {
            string str = "select OPM_Maintain.* ,pt_prjinfo.prjname,pt_prjinfo.prjplace from OPM_Maintain join pt_prjinfo ";
            str = str + "on pt_prjinfo.prjguid=OPM_Maintain.prjid where 1=1 ";
            if (prjid != "")
            {
                str = str + " and OPM_Maintain.prjid='" + prjid + "'";
            }
            return publicDbOpClass.DataTableQuary(str + "and maintainCode  like '%" + maintainCode.Trim() + "%'");
        }

        public DataTable SearchMaintains(string maintainCode, string prjid, string prjplace, DateTime startDate, DateTime endDate)
        {
            string str = "select OPM_Maintain.* ,pt_prjinfo.prjname,pt_prjinfo.prjplace from OPM_Maintain join pt_prjinfo ";
            str = str + "on pt_prjinfo.prjguid=OPM_Maintain.prjid where 1=1 ";
            if (prjid != "")
            {
                str = str + " and OPM_Maintain.prjid='" + prjid + "'";
            }
            object obj2 = (str + "and maintainCode  like '%" + maintainCode.Trim() + "%'") + " and prjplace like '%" + prjplace + "%' ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " and applydate  between '", startDate, "' and '", endDate, "'" }));
        }

        public int UpdateMaintains(MaintainModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_maintain SET");
            builder.Append(" MaintainCode=");
            builder.Append("'" + model.MaintainCode + "',");
            builder.Append(" Prjid=");
            builder.Append("'" + model.Prjid + "',");
            builder.Append(" UrgencyDegree=");
            builder.Append("'" + model.UrgencyDegree + "',");
            builder.Append(" MaintainItem=");
            builder.Append("'" + model.MaintainItem + "',");
            builder.Append(" MaintainType=");
            builder.Append("'" + model.MaintainType + "',");
            builder.Append(" ApplyFee=");
            builder.Append(model.ApplyFee + ",");
            builder.Append(" ApplyDate =");
            builder.Append("'" + model.ApplyDate + "',");
            builder.Append(" ProblemStatement=");
            builder.Append("'" + model.ProblemStatement + "',");
            builder.Append(" Tel=");
            builder.Append("'" + model.Tel + "',");
            builder.Append(" MaintainMan=");
            builder.Append("'" + model.MaintainMan + "',");
            builder.Append(" ApplyDptDutyMan=");
            builder.Append("'" + model.ApplyDptDutyMan + "',");
            builder.Append("AdminDptMan=");
            builder.Append("'" + model.AdminDptMan + "',");
            builder.Append("StoreManager=");
            builder.Append("'" + model.StoreManager + "',");
            builder.Append("Dept=");
            builder.Append("'" + model.Dept + "',");
            builder.Append("FlowState=");
            builder.Append("'" + model.Flowstate + "',");
            builder.Append("IsValid=");
            builder.Append("'" + model.Isvalid + "',");
            builder.Append("AddUser=");
            builder.Append("'" + model.Adduser + "',");
            builder.Append("AddTime=");
            builder.Append("'" + model.Addtime + "',");
            builder.Append("Remark=");
            builder.Append("'" + model.Remark + "',");
            builder.Append("I_xh=");
            builder.Append(model.I_xh);
            builder.Append(" where ");
            builder.Append("maintainID=");
            builder.Append("'" + model.MaintainId + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

