namespace cn.justwin.opm.Fire
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class FireAction
    {
        public bool AddFires(FireModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OPM_Business_Data(UID, BType,PrjID,codeID");
            builder.Append(",BCode,BName,DutyUser,Cause,AddUser,AddTime,Remark,i_xh,IsValid,FlowState)");
            builder.Append(" values(");
            builder.Append("'" + model.UId + "',");
            builder.Append("'" + model.BType + "',");
            builder.Append("'" + model.PrjId + "',");
            builder.Append("'" + model.CodeId + "',");
            builder.Append("'" + model.BCode + "',");
            builder.Append("'" + model.BName + "',");
            builder.Append("'" + model.DutyUser + "',");
            builder.Append("'" + model.Cause + "',");
            builder.Append("'" + model.Adduser + "',");
            builder.Append("'" + model.Addtime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append(model.I_xh + ",");
            builder.Append("'" + model.Isvalid + "',");
            builder.Append(model.Flowstate + " ");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public int DeleteFires(string InUid)
        {
            return publicDbOpClass.ExecSqlString("delete from OPM_Business_Data where uid in (" + InUid + ")");
        }

        public DataTable getByCodeID(int cid)
        {
            return publicDbOpClass.DataTableQuary(("select codeName from XPM_Basic_CodeList where TypeID=211 and codeId='" + cid + "'").ToString());
        }

        public DataTable getByPrjID(Guid id)
        {
            return publicDbOpClass.DataTableQuary(("select PrjName from PT_PrjInfo where PrjGuid='" + id + "'").ToString());
        }

        public DataTable getFires(Guid prjid, int codeId)
        {
            string str = "select * from OPM_Business_Data ";
            if (codeId == 0)
            {
                str = string.Concat(new object[] { str, " where prjid='", prjid, "'" });
            }
            else
            {
                str = string.Concat(new object[] { str, " where prjid='", prjid, "' and codeID=", codeId });
            }
            return publicDbOpClass.DataTableQuary(str + "  and BType='fire'");
        }

        public DataTable getFires_ByID(Guid uid)
        {
            string str = "select * from OPM_Business_Data ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, " where uid='", uid, "'" }));
        }

        public int IsFireCode(string BCode)
        {
            return publicDbOpClass.DataTableQuary("select * from OPM_Business_Data where BCode='" + BCode + "'").Rows.Count;
        }

        public DataTable SearchFires(string prjName, string BCode)
        {
            string sqlString = "select * from OPM_Business_Data inner join pt_prjInfo on OPM_Business_Data.prjid=pt_prjInfo.prjguid";
            sqlString = sqlString + " and btype='fire'";
            if (prjName.Trim() != "")
            {
                sqlString = sqlString + " and prjName like '%" + prjName.Trim() + "%'";
            }
            if (BCode.Trim() != "")
            {
                sqlString = sqlString + " and BCode like '%" + BCode.Trim() + "%'";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int UpdateFires(FireModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_Business_Data SET");
            builder.Append(" Prjid=");
            builder.Append("'" + model.PrjId + "',");
            builder.Append(" codeID=");
            builder.Append(model.CodeId + ",");
            builder.Append(" BCode=");
            builder.Append("'" + model.BCode + "',");
            builder.Append(" BName=");
            builder.Append("'" + model.BName + "',");
            builder.Append(" DutyUser=");
            builder.Append("'" + model.DutyUser + "',");
            builder.Append(" Cause=");
            builder.Append("'" + model.Cause + "',");
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
            builder.Append(model.I_xh + " ");
            builder.Append(" where ");
            builder.Append("UId=");
            builder.Append("'" + model.UId + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateFires(string UID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_Business_Data SET");
            builder.Append(" Cause=");
            builder.Append("''");
            builder.Append(" where ");
            builder.Append("UId=");
            builder.Append("'" + UID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

