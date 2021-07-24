namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.Script.Serialization;

    public class EPM_IntendanceInfoAction
    {
        public int Add(EPM_IntendanceInfo model, string userCode)
        {
            StringBuilder builder = new StringBuilder();
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            Guid noteId = model.NoteId;
            builder2.Append("NoteId,");
            builder3.Append("'" + model.NoteId + "',");
            Guid intendanceGuid = model.IntendanceGuid;
            builder2.Append("IntendanceGuid,");
            builder3.Append("'" + model.IntendanceGuid + "',");
            if (model.AskQuestionsYhdm != null)
            {
                builder2.Append("AskQuestionsYhdm,");
                builder3.Append("'" + model.AskQuestionsYhdm + "',");
            }
            if (model.AskQuestionsDate.HasValue)
            {
                builder2.Append("AskQuestionsDate,");
                builder3.Append("'" + model.AskQuestionsDate + "',");
            }
            if (model.QuestionExplain != null)
            {
                builder2.Append("QuestionExplain,");
                builder3.Append("'" + model.QuestionExplain + "',");
            }
            if (model.SettleYhdm != null)
            {
                builder2.Append("SettleYhdm,");
                builder3.Append("'" + model.SettleYhdm + "',");
            }
            if (model.SettleToPerson != null)
            {
                builder2.Append("SettleToPerson,");
                builder3.Append("'" + model.SettleToPerson + "',");
            }
            if (model.SettleDate.HasValue)
            {
                builder2.Append("SettleDate,");
                builder3.Append("'" + model.SettleDate + "',");
            }
            if (model.ToCause != null)
            {
                builder2.Append("ToCause,");
                builder3.Append("'" + model.ToCause + "',");
            }
            if (model.SettleExplain != null)
            {
                builder2.Append("SettleExplain,");
                builder3.Append("'" + model.SettleExplain + "',");
            }
            if (model.QuestionTag.HasValue)
            {
                builder2.Append("QuestionTag,");
                builder3.Append(model.QuestionTag + ",");
            }
            builder.Append("insert into OPM_EPCM_IntendanceInfo (");
            builder.Append(builder2.ToString().Remove(builder2.Length - 1));
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(builder3.ToString().Remove(builder3.Length - 1));
            builder.Append(")");
            builder.Append(string.Concat(new object[] { " update OPM_EPCM_IntendancePhotoList set InfoGuid='", model.NoteId, "' where InfoGuid ='00000000-0000-0000-0000-000000000000' and OPyhdm='", userCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static string buildJson(List<EPM_IntendanceInfo> tlInfos)
        {
            string str2;
            try
            {
                str2 = new JavaScriptSerializer().Serialize(tlInfos);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        public int Delete(Guid NoteId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OPM_EPCM_IntendanceInfo  ");
            builder.Append(" where NoteId='" + NoteId + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public static string getJson(string IntendanceGuid, int flag)
        {
            StringBuilder builder = new StringBuilder();
            builder.Remove(0, builder.Length);
            if (flag == 0)
            {
                builder.Append("SELECT a.*, b.v_xm as xm FROM dbo.OPM_EPCM_IntendanceInfo as a INNER JOIN dbo.PT_yhmc as b ON a.SettleYhdm = b.v_yhdm WHERE a.IntendanceGuid='" + IntendanceGuid + "' ORDER BY a.AskQuestionsDate DESC");
            }
            else
            {
                builder.Append("SELECT a.*, b.v_xm as xm FROM dbo.OPM_EPCM_IntendanceInfo as a INNER JOIN dbo.PT_yhmc as b ON a.SettleYhdm = b.v_yhdm WHERE a.IntendanceGuid='" + IntendanceGuid + "'");
                builder.Append(" AND a.NoteId != (SELECT TOP 1 NoteId FROM  dbo.OPM_EPCM_IntendanceInfo WHERE IntendanceGuid='" + IntendanceGuid + "' ORDER BY AskQuestionsDate DESC )");
                builder.Append(" ORDER BY a.AskQuestionsDate DESC");
            }
            List<EPM_IntendanceInfo> tlInfos = new List<EPM_IntendanceInfo>();
            DataTable table = publicDbOpClass.DataTableQuary(builder.ToString());
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    EPM_IntendanceInfo item = new EPM_IntendanceInfo();
                    Guid noteId = item.NoteId;
                    item.NoteId = new Guid(row["NoteId"].ToString());
                    Guid intendanceGuid = item.IntendanceGuid;
                    item.IntendanceGuid = new Guid(row["IntendanceGuid"].ToString());
                    item.QuestionExplain = (row["QuestionExplain"] as string) ?? string.Empty;
                    item.ToCause = (row["ToCause"] as string) ?? string.Empty;
                    item.SettleExplain = (row["SettleExplain"] as string) ?? string.Empty;
                    item.SettleYhdm = (row["xm"] as string) ?? string.Empty;
                    item.Sdate = Convert.ToDateTime(row["SettleDate"]).ToString() ?? string.Empty;
                    tlInfos.Add(item);
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return buildJson(tlInfos);
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select NoteId,IntendanceGuid,AskQuestionsYhdm,AskQuestionsDate,QuestionExplain,SettleYhdm,SettleToPerson,SettleDate,ToCause,SettleExplain,QuestionTag ");
            builder.Append(" FROM OPM_EPCM_IntendanceInfo  ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ");
            if (Top > 0)
            {
                builder.Append(" top " + Top.ToString());
            }
            builder.Append(" NoteId,IntendanceGuid,AskQuestionsYhdm,AskQuestionsDate,QuestionExplain,SettleYhdm,SettleToPerson,SettleDate,ToCause,SettleExplain,QuestionTag ");
            builder.Append(" FROM OPM_EPCM_IntendanceInfo  ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by " + filedOrder);
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public EPM_IntendanceInfo GetModel(Guid IntendanceGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select  top 1  ");
            builder.Append(" NoteId,IntendanceGuid,AskQuestionsYhdm,AskQuestionsDate,QuestionExplain,SettleYhdm,SettleToPerson,SettleDate,ToCause,SettleExplain,QuestionTag ");
            builder.Append(" from OPM_EPCM_IntendanceInfo  ");
            builder.Append(" where IntendanceGuid='" + IntendanceGuid + "' order by AskQuestionsDate desc ");
            EPM_IntendanceInfo info = new EPM_IntendanceInfo();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["NoteId"].ToString() != "")
            {
                info.NoteId = new Guid(set.Tables[0].Rows[0]["NoteId"].ToString());
            }
            if (set.Tables[0].Rows[0]["IntendanceGuid"].ToString() != "")
            {
                info.IntendanceGuid = new Guid(set.Tables[0].Rows[0]["IntendanceGuid"].ToString());
            }
            info.AskQuestionsYhdm = set.Tables[0].Rows[0]["AskQuestionsYhdm"].ToString();
            if (set.Tables[0].Rows[0]["AskQuestionsDate"].ToString() != "")
            {
                info.AskQuestionsDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["AskQuestionsDate"].ToString()));
            }
            info.QuestionExplain = set.Tables[0].Rows[0]["QuestionExplain"].ToString();
            info.SettleYhdm = set.Tables[0].Rows[0]["SettleYhdm"].ToString();
            info.SettleToPerson = set.Tables[0].Rows[0]["SettleToPerson"].ToString();
            if (set.Tables[0].Rows[0]["SettleDate"].ToString() != "")
            {
                info.SettleDate = new DateTime?(DateTime.Parse(set.Tables[0].Rows[0]["SettleDate"].ToString()));
            }
            info.ToCause = set.Tables[0].Rows[0]["ToCause"].ToString();
            info.SettleExplain = set.Tables[0].Rows[0]["SettleExplain"].ToString();
            if (set.Tables[0].Rows[0]["QuestionTag"].ToString() != "")
            {
                info.QuestionTag = new int?(int.Parse(set.Tables[0].Rows[0]["QuestionTag"].ToString()));
            }
            return info;
        }

        public DataTable GetParams(Guid IntendanceGuid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select a.*,b.prjname,prjcode from opm_epcm_intendancemaster a inner join ");
            builder.Append("pt_prjinfo b on a.prjguid=b.prjguid where intendanceguid='" + IntendanceGuid + "'");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public int Update(EPM_IntendanceInfo model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OPM_EPCM_IntendanceInfo  set ");
            Guid intendanceGuid = model.IntendanceGuid;
            builder.Append("IntendanceGuid='" + model.IntendanceGuid + "',");
            if (model.AskQuestionsYhdm != null)
            {
                builder.Append("AskQuestionsYhdm='" + model.AskQuestionsYhdm + "',");
            }
            if (model.AskQuestionsDate.HasValue)
            {
                builder.Append("AskQuestionsDate='" + model.AskQuestionsDate + "',");
            }
            if (model.QuestionExplain != null)
            {
                builder.Append("QuestionExplain='" + model.QuestionExplain + "',");
            }
            if (model.SettleYhdm != null)
            {
                builder.Append("SettleYhdm='" + model.SettleYhdm + "',");
            }
            if (model.SettleToPerson != null)
            {
                builder.Append("SettleToPerson='" + model.SettleToPerson + "',");
            }
            if (model.SettleDate.HasValue)
            {
                builder.Append("SettleDate='" + model.SettleDate + "',");
            }
            if (model.ToCause != null)
            {
                builder.Append("ToCause='" + model.ToCause + "',");
            }
            if (model.SettleExplain != null)
            {
                builder.Append("SettleExplain='" + model.SettleExplain + "',");
            }
            if (model.QuestionTag.HasValue)
            {
                builder.Append("QuestionTag=" + model.QuestionTag + ",");
            }
            int startIndex = builder.ToString().LastIndexOf(",");
            builder.Remove(startIndex, 1);
            builder.Append(" where NoteId='" + model.NoteId + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

