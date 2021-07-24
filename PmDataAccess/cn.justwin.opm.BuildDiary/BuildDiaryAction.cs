namespace cn.justwin.opm.BuildDiary
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text;

    public class BuildDiaryAction
    {
        public int Add(BuildDiaryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into OPM_EPCM_BuildDiary(UID,SN,PrjID,Sfgl,AddUser,Record,AddTime,Remark,Fsrq,Yjqk,Ysqk,Sjbg,");
            builder.Append("Cljc,Jsjd,Zljj,Clsj,Wbhy,Sjjc,Bzrq,Aqcl,Qtqk,Jbr,Cemperature2,Cemperature8,Cemperature14,Cemperature20,");
            builder.Append("AmWeather,PmWeather,FlowState,Shyj,WaterElec,Mason,Painter,Carpentry) ");
            builder.Append("values(");
            builder.Append("'" + model.UID + "',");
            builder.Append("'" + model.SN + "',");
            builder.Append("'" + model.PrjID + "',");
            builder.Append("'" + model.Sfgl + "',");
            builder.Append("'" + model.AddUser + "',");
            builder.Append("'" + model.Record + "',");
            builder.Append("'" + model.AddTime + "',");
            builder.Append("'" + model.Remark + "',");
            builder.Append("'" + model.Fsrq + "',");
            builder.Append("'" + model.Yjqk + "',");
            builder.Append("'" + model.Ysqk + "',");
            builder.Append("'" + model.Sjbg + "',");
            builder.Append("'" + model.Cljc + "',");
            builder.Append("'" + model.Jsjd + "',");
            builder.Append("'" + model.Zljj + "',");
            builder.Append("'" + model.Clsj + "',");
            builder.Append("'" + model.Wbhy + "',");
            builder.Append("'" + model.Sjjc + "',");
            builder.Append("'" + model.Bzrq + "',");
            builder.Append("'" + model.Aqcl + "',");
            builder.Append("'" + model.Qtqk + "',");
            builder.Append("'" + model.Jbr + "',");
            builder.Append("'" + model.Cemperature2 + "',");
            builder.Append("'" + model.Cemperature8 + "',");
            builder.Append("'" + model.Cemperature14 + "',");
            builder.Append("'" + model.Cemperature20 + "',");
            builder.Append("'" + model.AmWeather + "',");
            builder.Append("'" + model.PmWeather + "',");
            builder.Append(model.FlowState + ",");
            builder.Append("'" + model.Shyj + "',");
            builder.Append(model.WaterElec + ",");
            builder.Append(model.Mason + ",");
            builder.Append(model.Painter + ",");
            builder.Append(model.Carpentry + ")");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Delete(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from OPM_EPCM_BuildDiary ");
            builder.Append("where uid in(" + uid + ")");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable Exists(string sn)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from OPM_EPCM_BuildDiary ");
            builder.Append("where sn='" + sn + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable ExistsBuildDiary(string uids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_Epcm_BuildDiary_mx ");
            builder.Append("where bdid in(" + uids + ")");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetBuildList(string userCode, string pc, string flowState)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select b.*,p.prjname from opm_epcm_builddiary as b inner join pt_prjinfo as p on p.prjguid=b.prjid ");
            builder.Append(" where PrjID='" + pc + "' ");
            if (flowState == "")
            {
                builder.Append(" and flowState=1  ");
            }
            builder.Append(" order by addtime desc ");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetBuildListAll(string userCode, string pc, string flowState, string creatorName, string jobName, string recordName, DateTime? startTime, DateTime? endTime, int pagesize, int pageindex)
        {
            StringBuilder builder = new StringBuilder();
            string str = ((pagesize * (pageindex - 1)) + 1).ToString();
            string str2 = (pagesize * pageindex).ToString();
            builder.AppendFormat("\r\n                select * from (select row_number() over (order by addtime desc)as pageindex,b.*,p.prjname\r\n                from opm_epcm_builddiary as b  inner join pt_prjinfo as p  on  p.prjguid=b.prjid ", new object[0]).AppendLine();
            builder.Append("JOIN PT_yhmc ON b.AddUser=PT_yhmc.v_yhdm AND PT_yhmc.v_xm LIKE '%" + creatorName + "%'").AppendLine();
            builder.Append("where PrjID='" + pc + "'");
            if (flowState == "")
            {
                builder.Append(" and flowState=1").AppendLine();
            }
            if (!string.IsNullOrEmpty(jobName))
            {
                builder.Append(" AND Jbr like '%" + jobName + "%'").AppendLine();
            }
            if (!string.IsNullOrEmpty(recordName))
            {
                builder.Append(" AND Record like '%" + recordName + "%'").AppendLine();
            }
            if (!string.IsNullOrEmpty(startTime.ToString()))
            {
                builder.Append(" AND AddTime >='" + startTime + "'");
            }
            if (!string.IsNullOrEmpty(endTime.ToString()))
            {
                builder.Append(" AND AddTime<='" + endTime + "'");
            }
            builder.Append(") as result where pageindex between " + str + " and " + str2);
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), new SqlParameter[0]);
        }

        public int GetCount(string pc, string creatorName, string jobName, string recordName, DateTime? startTime, DateTime? endTime)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(*) from OPM_EPCM_BuildDiary as b inner join pt_prjinfo as p  on  p.prjguid=b.prjid ");
            builder.Append("JOIN PT_yhmc ON b.AddUser=PT_yhmc.v_yhdm AND PT_yhmc.v_xm LIKE '%" + creatorName + "%'");
            builder.Append("where PrjID='" + pc + "'");
            if (!string.IsNullOrEmpty(jobName))
            {
                builder.Append(" AND Jbr like '%" + jobName + "%'").AppendLine();
            }
            if (!string.IsNullOrEmpty(recordName))
            {
                builder.Append(" AND Record like '%" + recordName + "%'").AppendLine();
            }
            if (!string.IsNullOrEmpty(startTime.ToString()))
            {
                builder.Append(" AND AddTime >='" + startTime + "'");
            }
            if (!string.IsNullOrEmpty(endTime.ToString()))
            {
                builder.Append(" AND AddTime<='" + endTime + "'");
            }
            return Convert.ToInt32(SqlHelper.ExecuteScalar(CommandType.Text, builder.ToString(), new SqlParameter[0]));
        }

        public DataTable GetModel(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select b.*,p.prjname,p.PrjGuid from opm_epcm_builddiary as b inner join pt_prjinfo as p on p.prjguid=b.prjid ");
            builder.Append("where uid='" + uid + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Update(BuildDiaryModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update OPM_EPCM_BuildDiary set ");
            builder.Append("SN='" + model.SN + "',");
            builder.Append("PrjID='" + model.PrjID + "',");
            builder.Append("IsValid='" + model.IsValid + "',");
            builder.Append("Record='" + model.Record + "',");
            builder.Append("AddTime='" + model.AddTime + "',");
            builder.Append("Remark='" + model.Remark + "',");
            builder.Append("Fsrq='" + model.Fsrq + "',");
            builder.Append("Yjqk='" + model.Yjqk + "',");
            builder.Append("Ysqk='" + model.Ysqk + "',");
            builder.Append("Sjbg='" + model.Sjbg + "',");
            builder.Append("Cljc='" + model.Cljc + "',");
            builder.Append("Jsjd='" + model.Jsjd + "',");
            builder.Append("Zljj='" + model.Zljj + "',");
            builder.Append("Clsj='" + model.Clsj + "',");
            builder.Append("Wbhy='" + model.Wbhy + "',");
            builder.Append("Sjjc='" + model.Sjjc + "',");
            builder.Append("Bzrq='" + model.Bzrq + "',");
            builder.Append("Aqcl='" + model.Aqcl + "',");
            builder.Append("Qtqk='" + model.Qtqk + "',");
            builder.Append("Jbr='" + model.Jbr + "',");
            builder.Append("Cemperature2='" + model.Cemperature2 + "',");
            builder.Append("Cemperature8='" + model.Cemperature8 + "',");
            builder.Append("Cemperature14='" + model.Cemperature14 + "',");
            builder.Append("Cemperature20='" + model.Cemperature20 + "',");
            builder.Append("AmWeather='" + model.AmWeather + "',");
            builder.Append("PmWeather='" + model.PmWeather + "',");
            builder.Append("Shyj='" + model.Shyj + "', ");
            builder.Append("WaterElec=" + model.WaterElec + ", ");
            builder.Append("Mason=" + model.Mason + ", ");
            builder.Append("Painter=" + model.Painter + ", ");
            builder.Append("Carpentry=" + model.Carpentry + " ");
            builder.Append("where UID='" + model.UID + "'");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
    }
}

