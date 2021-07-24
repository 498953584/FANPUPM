namespace cn.justwin.opm.action
{
    using cn.justwin.opm.model;
    using com.jwsoft.pm.data;
    using System;
    using System.Data;
    using System.Text;

    public class ExceptionInfoAction
    {
        public int Add(ExceptionInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into ExceptionInfo(");
            builder.Append("UserIP,UserCode,ExceptionTime,ExceptionAddr,ExceptionMessage,ExceptionSource,SubmitDatas,UrlParams,ExceptionTrace");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.UserIP + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.ExceptionTime + "',");
            builder.Append("'" + model.ExceptionAddr + "',");
            builder.Append("'" + model.ExceptionMessage + "',");
            builder.Append("'" + model.ExceptionSource + "',");
            builder.Append("'" + model.SubmitDatas + "',");
            builder.Append("'" + model.UrlParams + "',");
            builder.Append("'" + model.ExceptionTrace + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(int ExceptionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete ExceptionInfo ");
            builder.Append(" where ExceptionID=" + ExceptionID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select ExceptionID,UserIP,UserCode,ExceptionTime,ExceptionAddr,ExceptionMessage,ExceptionSource,SubmitDatas,UrlParams,ExceptionTrace ");
            builder.Append(" FROM ExceptionInfo ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ExceptionInfoModel GetModel(int ExceptionID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" ExceptionID,UserIP,UserCode,ExceptionTime,ExceptionAddr,ExceptionMessage,ExceptionSource,SubmitDatas,UrlParams,ExceptionTrace ");
            builder.Append(" from ExceptionInfo ");
            builder.Append(" where ExceptionID=" + ExceptionID + " ");
            ExceptionInfoModel model = new ExceptionInfoModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ExceptionID"].ToString() != "")
            {
                model.ExceptionID = int.Parse(set.Tables[0].Rows[0]["ExceptionID"].ToString());
            }
            model.UserIP = set.Tables[0].Rows[0]["UserIP"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["ExceptionTime"].ToString() != "")
            {
                model.ExceptionTime = DateTime.Parse(set.Tables[0].Rows[0]["ExceptionTime"].ToString());
            }
            model.ExceptionAddr = set.Tables[0].Rows[0]["ExceptionAddr"].ToString();
            model.ExceptionMessage = set.Tables[0].Rows[0]["ExceptionMessage"].ToString();
            model.ExceptionSource = set.Tables[0].Rows[0]["ExceptionSource"].ToString();
            model.SubmitDatas = set.Tables[0].Rows[0]["SubmitDatas"].ToString();
            model.UrlParams = set.Tables[0].Rows[0]["UrlParams"].ToString();
            model.ExceptionTrace = set.Tables[0].Rows[0]["ExceptionTrace"].ToString();
            return model;
        }

        public int Update(ExceptionInfoModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update ExceptionInfo set ");
            builder.Append("UserIP='" + model.UserIP + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("ExceptionTime='" + model.ExceptionTime + "',");
            builder.Append("ExceptionAddr='" + model.ExceptionAddr + "',");
            builder.Append("ExceptionMessage='" + model.ExceptionMessage + "',");
            builder.Append("ExceptionSource='" + model.ExceptionSource + "',");
            builder.Append("SubmitDatas='" + model.SubmitDatas + "',");
            builder.Append("UrlParams='" + model.UrlParams + "',");
            builder.Append("ExceptionTrace='" + model.ExceptionTrace + "'");
            builder.Append(" where ExceptionID=" + model.ExceptionID + " ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

