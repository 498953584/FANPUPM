namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class SystemInfoAction
    {
        public int Add(SystemInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OA_System_Info(");
            builder.Append("RecordID,ClassID,AuditState,SystemType,CorpCode,UserCode,RecordDate,SystemName,SystemCode,SignMan,SignDate,isCurrent,Remark");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.RecordID + "',");
            builder.Append(model.ClassID + ",");
            builder.Append(model.AuditState + ",");
            builder.Append("'" + model.SystemType + "',");
            builder.Append("'" + model.CorpCode + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.SystemName + "',");
            builder.Append("'" + model.SystemCode + "',");
            builder.Append("'" + model.SignMan + "',");
            builder.Append("'" + model.SignDate + "',");
            builder.Append("'" + model.IsCurrent + "',");
            builder.Append("'" + model.Remark + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete OA_System_Info ");
            builder.Append(" where RecordID='" + RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from OA_System_Info where RecordID=" + RecordID);
            return (publicDbOpClass.ExecuteScalar(builder.ToString()) != null);
        }

        public DataTable GetClassID(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.*,b.* from PT_d_CorpCode a ");
            builder.Append("right join PT_MultiClasses b on ");
            builder.Append("a.CorpCode=b.CorpCode ");
            if (strWhere.Trim() != "")
            {
                builder.Append("where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetCorpCode(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from PT_d_bm where i_bmdm in (");
            builder.Append("select i_bmdm from PT_yhmc ");
            if (strWhere.Trim() != "")
            {
                builder.Append("where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetCorpName()
        {
            string sqlString = "select * from PT_d_CorpCode ";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_System_Info ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by RecordID ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("OA_System_Info", "RecordID");
        }

        public SystemInfoModel GetModel(Guid RecordID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OA_System_Info ");
            builder.Append(" where RecordID='" + RecordID + "'");
            SystemInfoModel model = new SystemInfoModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            model.RecordID = RecordID;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ClassID"].ToString() != "")
            {
                model.ClassID = int.Parse(set.Tables[0].Rows[0]["ClassID"].ToString());
            }
            if (set.Tables[0].Rows[0]["AuditState"].ToString() != "")
            {
                model.AuditState = int.Parse(set.Tables[0].Rows[0]["AuditState"].ToString());
            }
            model.SystemType = set.Tables[0].Rows[0]["SystemType"].ToString();
            model.CorpCode = set.Tables[0].Rows[0]["CorpCode"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            model.IsCurrent = set.Tables[0].Rows[0]["IsCurrent"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            model.SystemName = set.Tables[0].Rows[0]["SystemName"].ToString();
            model.SystemCode = set.Tables[0].Rows[0]["SystemCode"].ToString();
            model.SignMan = set.Tables[0].Rows[0]["SignMan"].ToString();
            if (set.Tables[0].Rows[0]["SignDate"].ToString() != "")
            {
                model.SignDate = DateTime.Parse(set.Tables[0].Rows[0]["SignDate"].ToString());
            }
            model.Remark = set.Tables[0].Rows[0]["Remark"].ToString();
            return model;
        }

        public DataTable GetUsreNameList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.*,b.* from OA_System_Info a ");
            builder.Append(" left join PT_yhmc b on ");
            builder.Append(" a.UserCode = b.v_yhdm ");
            if (strWhere.Trim() != "")
            {
                builder.Append("where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(SystemInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OA_System_Info set ");
            builder.Append("AuditState=" + model.AuditState + ",");
            builder.Append("SystemType='" + model.SystemType + "',");
            builder.Append("CorpCode='" + model.CorpCode + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("SystemName='" + model.SystemName + "',");
            builder.Append("SystemCode='" + model.SystemCode + "',");
            builder.Append("SignMan='" + model.SignMan + "',");
            builder.Append("SignDate='" + model.SignDate + "',");
            builder.Append("IsCurrent='" + model.IsCurrent + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where RecordID='" + model.RecordID + "'");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

