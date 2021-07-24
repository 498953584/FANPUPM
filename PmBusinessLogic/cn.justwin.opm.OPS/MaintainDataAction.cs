namespace cn.justwin.opm.OPS
{
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class MaintainDataAction
    {
        public bool AddmaintainDatas(Guid id)
        {
            MaintainDataAction action = new MaintainDataAction();
            StringBuilder builder = new StringBuilder();
            DataTable table = action.getMaintainData_ByID(id);
            string str = table.Rows[0]["MaintainCode"].ToString();
            string str2 = table.Rows[0]["MaintainItem"].ToString();
            string str3 = table.Rows[0]["MaintainId"].ToString();
            string[] strArray = str2.Trim(new char[] { ',' }).Split(new char[] { ',' });
            for (int i = 0; i < strArray.Length; i++)
            {
                builder.Append("insert into OPM_Maintain_Data(UID,MaintainCode,MaintainID,MaintainType,MaintainItem,MaintainEndTime,MaintainSumTime,MaintainMoney,MaintainTeam,MaintainPerson,MaintainStatus,AddUser,AddTime,Remark,IsValid) values(");
                builder.Append("'" + Guid.NewGuid() + "',");
                builder.Append("'" + str + "',");
                builder.Append("'" + str3 + "',");
                builder.Append("'" + strArray[i] + "',");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("0,");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("'',");
                builder.Append("''");
                builder.Append(")");
            }
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public int DeleteMaintains(Guid InUid)
        {
            return publicDbOpClass.ExecSqlString("delete from OPM_maintain where maintainid='" + InUid + "'");
        }

        public string GetCodeID(int typeid, string codeName)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select CodeID from XPM_Basic_CodeList where typeID=", typeid, " and codeName='", codeName, "'" }));
            string str2 = string.Empty;
            if (table.Rows.Count > 0)
            {
                str2 = table.Rows[0][0].ToString();
            }
            return str2;
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

        public DataTable getMaintainData(Guid prjID)
        {
            string str = "select OPM_Maintain_Data.*,OPM_Maintain.ApplyDate,OPM_Maintain.ApplyFee,OPM_Maintain.prjId, prjplace,prjname ";
            str = str + "from OPM_Maintain_Data inner join OPM_Maintain on OPM_Maintain.MaintainID = OPM_Maintain_Data.MaintainID " + "  inner join pt_prjinfo on OPM_Maintain.prjid=pt_prjinfo.PrjGuid";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, " where OPM_Maintain.prjid='", prjID, "'" }));
        }

        public DataTable getMaintainData_ByID(Guid maintainID)
        {
            string str = "select OPM_Maintain.* ,pt_prjinfo.prjname,prjplace from OPM_Maintain join pt_prjinfo ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, "on pt_prjinfo.prjguid=OPM_Maintain.prjid where OPM_Maintain.maintainId='", maintainID, "'" }));
        }

        public DataTable getMaintainData_ByMainID(Guid maintainID)
        {
            string str = "select OPM_Maintain_Data.*,OPM_Maintain.MaintainID,OPM_Maintain.ApplyDate,OPM_Maintain.MaintainItem,OPM_Maintain.prjId ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, "from OPM_Maintain_Data inner join OPM_Maintain on OPM_Maintain.MaintainID = OPM_Maintain_Data.MaintainID where OPM_Maintain_Data.Uid='", maintainID, "'" }));
        }

        public DataTable getMaintainDataExcel(Guid prjID, string maintainID)
        {
            string str = "select prjplace,prjname ,OPM_Maintain_Data.*,OPM_Maintain.ApplyDate,OPM_Maintain.prjId,OPM_Maintain.ApplyFee";
            str = str + " from OPM_Maintain_Data inner join OPM_Maintain on OPM_Maintain.MaintainID = OPM_Maintain_Data.MaintainID" + "  inner join pt_prjinfo on OPM_Maintain.prjid=pt_prjinfo.PrjGuid";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { str, " where OPM_Maintain.prjid='", prjID, "'" }) + " and OPM_Maintain_Data.UID  in (" + maintainID + ")");
        }

        public DataTable GetTableDescription(string tableName)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" SELECT a.MaintainID N'name', isnull(g.[value],'') AS N'value'");
            builder.Append(" FROM OPM_Maintain_Data a inner join OPM_Maintain d ");
            builder.Append(" on a.id=d.id and d.xtype='U' and d.name<>'dtproperties' ");
            builder.Append(" left join sys.extended_properties   g ");
            builder.Append(" on a.id=g.major_id AND a.colid = g.minor_id ");
            builder.Append(" where d.name='" + tableName + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int IsmaintainCode(string maintainCode)
        {
            return publicDbOpClass.DataTableQuary("select * from OPM_maintain where maintainCode='" + maintainCode + "'").Rows.Count;
        }

        public DataTable SearchMaintains(string maintainCode, string prjid, string prjplace, DateTime startDate, DateTime endDate, string IsThisPro)
        {
            string str = "select a.* ,b.ApplyDate,b.ApplyFee,b.prjId, prjplace,prjname from OPM_Maintain_Data a \r\n                                left join OPM_Maintain b on b.MaintainID = a.MaintainID  \r\n                                  left join pt_prjinfo on b.prjid=pt_prjinfo.PrjGuid where 1=1 ";
            if ((prjid != "") && (IsThisPro == "1"))
            {
                str = str + " and b.prjid='" + prjid + "'";
            }
            object obj2 = (str + " and a.maintainCode  like '%" + maintainCode.Trim() + "%'") + " and prjplace like '%" + prjplace + "%' ";
            return publicDbOpClass.DataTableQuary(string.Concat(new object[] { obj2, " and applydate  between '", startDate, "' and '", endDate, "'" }));
        }

        public int UpdateDataMaintains(MaintainDataModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_maintain_Data SET");
            builder.Append(" MaintainItem=");
            builder.Append("'" + model.MaintainItem + "',");
            builder.Append(" ApprovalFee=");
            builder.Append(model.ApprovalFee + ",");
            builder.Append(" MaintainEndTime =");
            builder.Append("'" + model.MaintainEndTime + "',");
            builder.Append(" MaintainSumTime=");
            builder.Append("'" + model.MaintainSumTime + "',");
            builder.Append(" MaintainMoney=");
            builder.Append("'" + model.MaintainMoney + "',");
            builder.Append(" MaintainTeam=");
            builder.Append("'" + model.MaintainTeam + "',");
            builder.Append(" MaintainPerson=");
            builder.Append("'" + model.MaintainPerson + "',");
            builder.Append("MaintainStatus=");
            builder.Append("'" + model.MaintainStatus + "',");
            builder.Append("FlowState=");
            builder.Append("'" + model.FlowState + "',");
            builder.Append("IsValid=");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("AddUser=");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("Remark=");
            builder.Append("'" + model.Remark + "',");
            builder.Append("I_xh=");
            builder.Append(model.I_xh);
            builder.Append(" where ");
            builder.Append("MaintainID=");
            builder.Append("'" + model.MaintainID + "'");
            builder.Append("and");
            builder.Append(" UID=");
            builder.Append("'" + model.UID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateDataMaintains(string MaintainCode, string codeID, MaintainDataModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Update OPM_maintain_Data SET");
            builder.Append(" MaintainItem=");
            builder.Append("'" + model.MaintainItem + "',");
            builder.Append(" MaintainEndTime =");
            builder.Append("'" + model.MaintainEndTime + "',");
            builder.Append(" MaintainSumTime=");
            builder.Append("'" + model.MaintainSumTime + "',");
            builder.Append(" ApprovalFee=");
            builder.Append(model.ApprovalFee + ",");
            builder.Append(" MaintainMoney=");
            builder.Append("'" + model.MaintainMoney + "',");
            builder.Append(" MaintainTeam=");
            builder.Append("'" + model.MaintainTeam + "',");
            builder.Append(" MaintainPerson=");
            builder.Append("'" + model.MaintainPerson + "',");
            builder.Append("MaintainStatus=");
            builder.Append("'" + model.MaintainStatus + "',");
            builder.Append("FlowState=");
            builder.Append("'" + model.FlowState + "',");
            builder.Append("IsValid=");
            builder.Append("'" + model.IsValid + "',");
            builder.Append("AddUser=");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("AddTime=");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("Remark=");
            builder.Append("'" + model.Remark + "',");
            builder.Append("I_xh=");
            builder.Append(model.I_xh);
            builder.Append(" where ");
            builder.Append("MaintainType=");
            builder.Append("'" + codeID + "'");
            builder.Append("and");
            builder.Append(" MaintainCode=");
            builder.Append("'" + MaintainCode + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

