namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class DepositoryInfo
    {
        public bool Add(com.jwsoft.pm.entpm.model.DepositoryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_DepositoryInfo(");
            builder.Append("DepoClassID,DepositoryName,DepositoryCode,Purview,Principal,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.DepoClassID + ",");
            builder.Append("'" + model.DepositoryName + "',");
            builder.Append("'" + model.DepositoryCode + "',");
            builder.Append("'" + model.Purview + "',");
            builder.Append("'" + model.Principal + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int DepositoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_DepositoryInfo ");
            builder.Append(" where DepositoryID=" + DepositoryID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public DataTable dtDuty(string DeptID)
        {
            return publicDbOpClass.DataTableQuary("SELECT dbo.PT_DUTY.i_bmdm,dbo.PT_DUTY.DutyCode, dbo.PT_D_Role.RoleTypeCode, dbo.PT_D_Role.RoleTypeName, dbo.PT_DUTY.I_DUTYID FROM dbo.PT_D_Role INNER JOIN dbo.PT_DUTY ON dbo.PT_D_Role.RoleTypeCode=dbo.PT_DUTY.DutyCode WHERE  (dbo.PT_DUTY.c_sfyx = '1') and i_bmdm = " + DeptID);
        }

        public DataTable dtDuty2(string I_DUTYID)
        {
            return publicDbOpClass.DataTableQuary("SELECT dbo.PT_DUTY.i_bmdm,dbo.PT_DUTY.DutyCode, dbo.PT_D_Role.RoleTypeCode, dbo.PT_D_Role.RoleTypeName, dbo.PT_DUTY.I_DUTYID FROM dbo.PT_D_Role INNER JOIN dbo.PT_DUTY ON dbo.PT_D_Role.RoleTypeCode=dbo.PT_DUTY.DutyCode WHERE    dbo.PT_DUTY.I_DUTYID in (" + I_DUTYID + ")");
        }

        public bool Exists(int DepositoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_DepositoryInfo");
            builder.Append(" where DepositoryID=" + DepositoryID + " ");
            if (publicDbOpClass.ExecuteSQL(builder.ToString()) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public string getDutyIDs(string DepositoryID)
        {
            return publicDbOpClass.ExecuteScalar("select isnull(Purview,'')  from pm_Repe_DepositoryInfo where DepositoryID=" + DepositoryID).ToString();
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DepositoryID,DepoClassID,DepositoryName,DepositoryCode,Purview,Principal,Remark ");
            builder.Append(" FROM pm_Repe_DepositoryInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_DepositoryInfo", "DepositoryID"));
        }

        public com.jwsoft.pm.entpm.model.DepositoryInfo GetModel(int DepositoryID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" DepositoryID,DepoClassID,DepositoryName,DepositoryCode,Purview,Principal,Remark ");
            builder.Append(" from pm_Repe_DepositoryInfo ");
            builder.Append(" where DepositoryID=" + DepositoryID + " ");
            com.jwsoft.pm.entpm.model.DepositoryInfo info = new com.jwsoft.pm.entpm.model.DepositoryInfo();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DepositoryID"].ToString() != "")
            {
                info.DepositoryID = int.Parse(set.Tables[0].Rows[0]["DepositoryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["DepoClassID"].ToString() != "")
            {
                info.DepoClassID = int.Parse(set.Tables[0].Rows[0]["DepoClassID"].ToString());
            }
            info.DepositoryName = set.Tables[0].Rows[0]["DepositoryName"].ToString();
            info.DepositoryCode = set.Tables[0].Rows[0]["DepositoryCode"].ToString();
            info.Purview = set.Tables[0].Rows[0]["Purview"].ToString();
            info.Principal = set.Tables[0].Rows[0]["Principal"].ToString();
            info.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return info;
        }

        public string GetUserName(string UserID)
        {
            string str = "";
            if (UserID.Length > 0)
            {
                foreach (string str2 in UserID.Split(new char[] { ',' }))
                {
                    if (str == "")
                    {
                        str = str + new userManageDb().GetUserName(str2);
                    }
                    else
                    {
                        str = str + "," + new userManageDb().GetUserName(str2);
                    }
                }
            }
            return str;
        }

        public DataTable GetUserProClass(string YHDM)
        {
            string sqlString = "";
            if (YHDM == "")
            {
                sqlString = "select * from pm_Repe_DepositoryInfo ";
            }
            else
            {
                sqlString = "select a.* from pm_Repe_DepositoryInfo a";
                sqlString = ((sqlString + " where  Patindex(") + " '%,'+(select top 1 cast(b.I_DUTYID as varchar) from pt_duty b where b.i_bmdm = (select top 1 c.i_bmdm  from pt_yhmc c where c.V_YHDM = '" + YHDM + "'))  +',%'") + " ,','+isnull(a.Purview,'0')+',') > 0  order by ClassCode";
            }
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int LowerLevelsCount(string DepositoryID)
        {
            string classCode = publicDbOpClass.ExecuteScalar("select b.ClassCode from pm_Repe_DepositoryInfo a,pm_Repe_DepositoryClass b where  a.DepoClassID=b.DepoClassID and a.DepositoryID =" + DepositoryID).ToString();
            com.jwsoft.pm.entpm.action.DepositoryClass class2 = new com.jwsoft.pm.entpm.action.DepositoryClass();
            return class2.LowerLevelsCount(classCode);
        }

        public bool Update(com.jwsoft.pm.entpm.model.DepositoryInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_DepositoryInfo set ");
            builder.Append("DepoClassID=" + model.DepoClassID + ",");
            builder.Append("DepositoryName='" + model.DepositoryName + "',");
            builder.Append("DepositoryCode='" + model.DepositoryCode + "',");
            builder.Append("Purview='" + model.Purview + "',");
            builder.Append("Principal='" + model.Principal + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where DepositoryID=" + model.DepositoryID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool updateWDUUser(string DepositoryID, string DutyIDs)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_Repe_DepositoryInfo set Purview = '" + DutyIDs + "' where DepositoryID=" + DepositoryID);
        }
    }
}

