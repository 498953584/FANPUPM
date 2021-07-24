namespace cn.justwin.opm.BuildDiary
{
    using cn.justwin.DAL;
    using System;
    using System.Data;
    using System.Text;

    public class BuildDiary_mxAction
    {
        public int Add(BuildDiary_mxModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into opm_Epcm_BuildDiary_mx(UID,BDID,TaskCode,TaskName,WorkGroup,WorkersCount,BuildPosition,Jdqk,Remark) ");
            builder.Append("values(");
            builder.Append("'" + model.UID + "',");
            builder.Append("'" + model.BDID + "',");
            builder.Append("'" + model.TaskCode + "',");
            builder.Append("'" + model.TaskName + "',");
            builder.Append("'" + model.WorkGroup + "',");
            builder.Append("'" + model.WorkersCount + "',");
            builder.Append("'" + model.BuildPosition + "',");
            builder.Append("'" + model.Jdqk + "',");
            builder.Append("'" + model.Remark + "')");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Delete(string uids)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete from opm_Epcm_BuildDiary_mx ");
            builder.Append("where uid in(" + uids + ")");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable Exists(string bdid, string taskCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_Epcm_BuildDiary_mx  ");
            builder.Append(" where bdid='" + bdid + "'  ");
            builder.Append(" and taskCode='" + taskCode + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetList(string bdId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_Epcm_BuildDiary_mx ");
            builder.Append("where BDID='" + bdId + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public DataTable GetModel(string uid)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from opm_Epcm_BuildDiary_mx ");
            builder.Append("where UID='" + uid + "'");
            return SqlHelper.ExecuteQuery(CommandType.Text, builder.ToString(), null);
        }

        public int Update(BuildDiary_mxModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update opm_Epcm_BuildDiary_mx set ");
            builder.Append("TaskCode='" + model.TaskCode + "',");
            builder.Append("TaskName='" + model.TaskName + "',");
            builder.Append("WorkGroup='" + model.WorkGroup + "',");
            builder.Append("WorkersCount='" + model.WorkersCount + "',");
            builder.Append("BuildPosition='" + model.BuildPosition + "',");
            builder.Append("jdqk='" + model.Jdqk + "',");
            builder.Append("Remark='" + model.Remark + "'");
            builder.Append(" where uid='" + model.UID + "'");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, builder.ToString(), null);
        }
    }
}

