namespace cn.justwin.stockBLL
{
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class MaterialWantPlan
    {
        private cn.justwin.stockDAL.MaterialWantPlan wantPlan = new cn.justwin.stockDAL.MaterialWantPlan();

        public int Add(MaterialWantPlanModel model)
        {
            return this.wantPlan.Add(model);
        }

        public int Add(SqlTransaction trans, MaterialWantPlanModel model)
        {
            return this.wantPlan.Add(trans, model);
        }

        public int AddModel(MaterialWantPlanModel model)
        {
            return this.wantPlan.AddModel(model);
        }

        public int Delete(string swcode)
        {
            return this.wantPlan.Delete(swcode);
        }

        public int ExcuteSql(string sql)
        {
            return this.wantPlan.ExcuteSql(sql);
        }

        public DataTable getAnnexList(string code)
        {
            return this.wantPlan.getAnnxList(code);
        }

        public string GetCodeByGuid(string guid)
        {
            return this.wantPlan.GetCodeByGuid(guid);
        }

        public DataTable GetDiff(MaterialWantPlanModel model, string isWBSRelevance)
        {
            string procode = model.procode;
            return this.wantPlan.GetDiff(model.swcode, procode, isWBSRelevance);
        }

        public string GetGuidByCode(string code)
        {
            return this.wantPlan.GetGuidByCode(code);
        }

        public List<MaterialWantPlanModel> GetListArray(string strWhere)
        {
            return this.wantPlan.GetListArray(strWhere);
        }

        public DataTable GetMaterialPlanInfo(string wantplanCode, string prjId, string isWBSRelevance)
        {
            return this.wantPlan.GetMaterialPlanInfo(wantplanCode, prjId, isWBSRelevance);
        }

        public MaterialWantPlanModel GetModel(string swcode)
        {
            return this.wantPlan.GetModel(swcode);
        }

        public DataTable getTableWantPlan(string procode)
        {
            return this.wantPlan.getTableWantPlan(procode);
        }

        public MaterialPlanStockModel getWantPlanModel(string wpsid)
        {
            return this.wantPlan.getWantPlanStockRow(wpsid);
        }

        public DataTable Search(string sqlWhere)
        {
            return this.wantPlan.Search(sqlWhere);
        }

        public DataTable Select(DateTime? startDate, DateTime? endDate, string swcode, string person, string procode, string flowState)
        {
            StringBuilder builder = new StringBuilder();
            if (startDate.HasValue)
            {
                builder.AppendFormat(" AND intime >= '{0}' ", startDate.Value.ToShortDateString());
            }
            if (endDate.HasValue)
            {
                builder.AppendFormat(" AND intime < '{0}' ", endDate.Value.ToShortDateString());
            }
            if (!string.IsNullOrEmpty(swcode))
            {
                builder.AppendFormat(" and swcode like '%{0}%' ", swcode);
            }
            if (!string.IsNullOrEmpty(person))
            {
                builder.AppendFormat(" and person like '%{0}%' ", person);
            }
            if (!string.IsNullOrEmpty(procode))
            {
                builder.Append(" and procode='" + procode + "' ");
            }
            if (!string.IsNullOrEmpty(flowState))
            {
                builder.Append(" and flowState=" + flowState);
            }
            builder.Append(" order by intime desc");
            return this.wantPlan.Search(builder.ToString());
        }

        public int Update(MaterialWantPlanModel model)
        {
            return this.wantPlan.Update(model);
        }

        public int Update(SqlTransaction trans, MaterialWantPlanModel model)
        {
            return this.wantPlan.Update(trans, model);
        }

        public int UpdateAcceptstate(SqlTransaction trans, string swcode)
        {
            return this.wantPlan.UpdateAcceptstate(trans, swcode);
        }

        public DataTable WantPlanDataBind(string code)
        {
            return this.wantPlan.getTableWantPlan(code);
        }

        public DataTable WantPlanStockDataBind(string code)
        {
            return this.wantPlan.getTableWantPlanStock(code);
        }
    }
}

