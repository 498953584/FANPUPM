namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class GodoEntryDetail
    {
        public bool Add(com.jwsoft.pm.entpm.model.GodoEntryDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_GodoEntryDetail(");
            builder.Append("GodoEntryID,MaterialId,EntryType,Scalar,UnitPrice,PlanPrice,ProjectId,TaskId");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.GodoEntryID + ",");
            builder.Append(model.MaterialId + ",");
            builder.Append("'" + model.EntryType + "',");
            builder.Append(model.Scalar + ",");
            builder.Append(model.UnitPrice + ",");
            builder.Append(model.PlanPrice + ",");
            builder.Append(model.ProjectId + ",");
            builder.Append(model.TaskId);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int GodoEntryDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_GodoEntryDetail ");
            builder.Append(" where GodoEntryDetailID=" + GodoEntryDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int GodoEntryDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_GodoEntryDetail");
            builder.Append(" where GodoEntryDetailID=" + GodoEntryDetailID + " ");
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

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select GodoEntryDetailID,GodoEntryID,MaterialId,EntryType,Scalar,UnitPrice ");
            builder.Append(" FROM pm_Repe_GodoEntryDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_GodoEntryDetail", "GodoEntryDetailID"));
        }

        public com.jwsoft.pm.entpm.model.GodoEntryDetail GetModel(int GodoEntryDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" GodoEntryDetailID,GodoEntryID,a.MaterialId,EntryType,Scalar,UnitPrice,PlanPrice,b.ProjectName,c.TaskName,a.ProjectId,a.TaskId ");
            builder.Append(" from pm_Repe_GodoEntryDetail as a inner join pm_projects as b on a.ProjectId=b.ProjectId inner join pm_wbs as c on a.TaskId=c.TaskId");
            builder.Append(" where GodoEntryDetailID=" + GodoEntryDetailID + " ");
            com.jwsoft.pm.entpm.model.GodoEntryDetail detail = new com.jwsoft.pm.entpm.model.GodoEntryDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["GodoEntryDetailID"].ToString() != "")
            {
                detail.GodoEntryDetailID = int.Parse(set.Tables[0].Rows[0]["GodoEntryDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["GodoEntryID"].ToString() != "")
            {
                detail.GodoEntryID = int.Parse(set.Tables[0].Rows[0]["GodoEntryID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialId"].ToString() != "")
            {
                detail.MaterialId = int.Parse(set.Tables[0].Rows[0]["MaterialId"].ToString());
            }
            detail.EntryType = set.Tables[0].Rows[0]["EntryType"].ToString();
            if (set.Tables[0].Rows[0]["Scalar"].ToString() != "")
            {
                detail.Scalar = decimal.Parse(set.Tables[0].Rows[0]["Scalar"].ToString());
            }
            if (set.Tables[0].Rows[0]["UnitPrice"].ToString() != "")
            {
                detail.UnitPrice = decimal.Parse(set.Tables[0].Rows[0]["UnitPrice"].ToString());
            }
            if (set.Tables[0].Rows[0]["PlanPrice"].ToString() != "")
            {
                detail.PlanPrice = decimal.Parse(set.Tables[0].Rows[0]["UnitPrice"].ToString());
            }
            if (set.Tables[0].Rows[0]["ProjectName"].ToString() != "")
            {
                detail.ProjectName = set.Tables[0].Rows[0]["ProjectName"].ToString();
            }
            if (set.Tables[0].Rows[0]["TaskName"].ToString() != "")
            {
                detail.TaskName = set.Tables[0].Rows[0]["TaskName"].ToString();
            }
            if (set.Tables[0].Rows[0]["ProjectId"].ToString() != "")
            {
                detail.ProjectId = int.Parse(set.Tables[0].Rows[0]["ProjectId"].ToString());
            }
            if (set.Tables[0].Rows[0]["TaskId"].ToString() != "")
            {
                detail.TaskId = int.Parse(set.Tables[0].Rows[0]["TaskId"].ToString());
            }
            return detail;
        }

        public bool Update(com.jwsoft.pm.entpm.model.GodoEntryDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_GodoEntryDetail set ");
            builder.Append("GodoEntryID=" + model.GodoEntryID + ",");
            builder.Append("MaterialId=" + model.MaterialId + ",");
            builder.Append("EntryType='" + model.EntryType + "',");
            builder.Append("Scalar=" + model.Scalar + ",");
            builder.Append("UnitPrice=" + model.UnitPrice + ",");
            builder.Append("PlanPrice=" + model.PlanPrice + ",");
            builder.Append("ProjectId=" + model.ProjectId + ",");
            builder.Append("TaskId=" + model.TaskId);
            builder.Append(" where GodoEntryDetailID=" + model.GodoEntryDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

