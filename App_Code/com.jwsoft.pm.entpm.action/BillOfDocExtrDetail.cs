namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class BillOfDocExtrDetail
    {
        public bool Add(com.jwsoft.pm.entpm.model.BillOfDocExtrDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_BillOfDocExtrDetail(");
            builder.Append("BillOfDocExtrID,MaterialId,Scalar,UnitPrice,BillOfDocExtrType,ProjectId,TaskId,IsInfluence,MdResourceId");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.BillOfDocExtrID + ",");
            builder.Append(model.MaterialId + ",");
            builder.Append(model.Scalar + ",");
            builder.Append(model.UnitPrice + ",");
            builder.Append("'" + model.BillOfDocExtrType + "',");
            builder.Append(model.ProjectId + ",");
            builder.Append(model.TaskId + ",");
            builder.Append("'" + model.IsInfluence + "',");
            builder.Append(model.MdResourceId);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Delete(int BillOfDocExtrDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_BillOfDocExtrDetail ");
            builder.Append(" where BillOfDocExtrDetailID=" + BillOfDocExtrDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public bool Exists(int BillOfDocExtrDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_BillOfDocExtrDetail");
            builder.Append(" where BillOfDocExtrDetailID=" + BillOfDocExtrDetailID + " ");
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
            builder.Append("select BillOfDocExtrDetailID,BillOfDocExtrID,MaterialId,Scalar,UnitPrice,BillOfDocExtrType,ProjectId,TaskId,IsInfluence,MdResourceId ");
            builder.Append(" FROM pm_Repe_BillOfDocExtrDetail ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_BillOfDocExtrDetail", "BillOfDocExtrDetailID"));
        }

        public com.jwsoft.pm.entpm.model.BillOfDocExtrDetail GetModel(int BillOfDocExtrDetailID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" BillOfDocExtrDetailID,BillOfDocExtrID,MaterialId,Scalar,UnitPrice,BillOfDocExtrType,ProjectId,TaskId,IsInfluence,MdResourceId ");
            builder.Append(" from pm_Repe_BillOfDocExtrDetail ");
            builder.Append(" where BillOfDocExtrDetailID=" + BillOfDocExtrDetailID + " ");
            com.jwsoft.pm.entpm.model.BillOfDocExtrDetail detail = new com.jwsoft.pm.entpm.model.BillOfDocExtrDetail();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["BillOfDocExtrDetailID"].ToString() != "")
            {
                detail.BillOfDocExtrDetailID = int.Parse(set.Tables[0].Rows[0]["BillOfDocExtrDetailID"].ToString());
            }
            if (set.Tables[0].Rows[0]["BillOfDocExtrID"].ToString() != "")
            {
                detail.BillOfDocExtrID = int.Parse(set.Tables[0].Rows[0]["BillOfDocExtrID"].ToString());
            }
            if (set.Tables[0].Rows[0]["MaterialId"].ToString() != "")
            {
                detail.MaterialId = int.Parse(set.Tables[0].Rows[0]["MaterialId"].ToString());
            }
            if (set.Tables[0].Rows[0]["Scalar"].ToString() != "")
            {
                detail.Scalar = decimal.Parse(set.Tables[0].Rows[0]["Scalar"].ToString());
            }
            if (set.Tables[0].Rows[0]["UnitPrice"].ToString() != "")
            {
                detail.UnitPrice = decimal.Parse(set.Tables[0].Rows[0]["UnitPrice"].ToString());
            }
            detail.BillOfDocExtrType = set.Tables[0].Rows[0]["BillOfDocExtrType"].ToString();
            if (set.Tables[0].Rows[0]["ProjectId"].ToString() != "")
            {
                detail.ProjectId = int.Parse(set.Tables[0].Rows[0]["ProjectId"].ToString());
            }
            if (set.Tables[0].Rows[0]["TaskId"].ToString() != "")
            {
                detail.TaskId = int.Parse(set.Tables[0].Rows[0]["TaskId"].ToString());
            }
            detail.IsInfluence = set.Tables[0].Rows[0]["IsInfluence"].ToString();
            if (set.Tables[0].Rows[0]["MdResourceId"].ToString() != "")
            {
                detail.MdResourceId = int.Parse(set.Tables[0].Rows[0]["MdResourceId"].ToString());
            }
            return detail;
        }

        public DataTable GetWBSResource(string TaskId)
        {
            return publicDbOpClass.DataTableQuary("select * ,b.TaskName,c.ResName,c.Specification,d.Grade,e.UnitName from pm_resources a left join pm_wbs b on a.TaskId = b.TaskId left join PM_Prj_Res_Item c on a.ResourcesCode = c.MaterialId left join PM_Prj_Res_Grade d on a.ResGrade = d.GradeID left join PM_Prj_Res_Unit e on c.UnitID = e.UnitID where a.HeadwayState = '3'and b.isvalid=1 and a.PhaseType ='3' and a.ResourcesCode > 0  and  a.TaskId = " + TaskId);
        }

        public bool Update(com.jwsoft.pm.entpm.model.BillOfDocExtrDetail model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_BillOfDocExtrDetail set ");
            builder.Append("BillOfDocExtrID=" + model.BillOfDocExtrID + ",");
            builder.Append("MaterialId=" + model.MaterialId + ",");
            builder.Append("Scalar=" + model.Scalar + ",");
            builder.Append("UnitPrice=" + model.UnitPrice + ",");
            if (model.ProjectId != 0)
            {
                builder.Append("ProjectId=" + model.ProjectId + ",");
            }
            if (model.ProjectId != 0)
            {
                builder.Append("TaskId=" + model.TaskId + ",");
            }
            builder.Append("IsInfluence='" + model.IsInfluence + "',");
            builder.Append("MdResourceId=" + model.MdResourceId + ",");
            builder.Append("BillOfDocExtrType='" + model.BillOfDocExtrType + "'");
            builder.Append(" where BillOfDocExtrDetailID=" + model.BillOfDocExtrDetailID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

