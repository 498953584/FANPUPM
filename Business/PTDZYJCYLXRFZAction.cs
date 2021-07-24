using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Text;

public class PTDZYJCYLXRFZAction
{
    public int Add(PTDZYJCYLXRFZModel model)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("insert into PT_DZYJ_CYLXRFZ(");
        builder.Append("UserCode,GroupName,IsValid");
        builder.Append(")");
        builder.Append(" values (");
        builder.Append("'" + model.UserCode + "',");
        builder.Append("'" + model.GroupName + "',");
        builder.Append("'" + model.IsValid + "'");
        builder.Append(")");
        builder.Append(";select @@IDENTITY");
        return publicDbOpClass.ExecSqlString(builder.ToString());
    }

    public int Delete(int RecordID)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("delete PT_DZYJ_CYLXRFZ ");
        builder.Append(" where RecordID=" + RecordID + " ");
        builder.Append(" delete PT_DZYJ_CYLXR where GroupID = " + RecordID + " ");
        return publicDbOpClass.ExecSqlString(builder.ToString());
    }

    public DataTable GetList(string strWhere)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("select RecordID,UserCode,GroupName,IsValid ");
        builder.Append(" FROM PT_DZYJ_CYLXRFZ ");
        if (strWhere.Trim() != "")
        {
            builder.Append(" where " + strWhere);
        }
        return publicDbOpClass.DataTableQuary(builder.ToString());
    }

    public PTDZYJCYLXRFZModel GetModel(int RecordID)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("select   ");
        builder.Append(" RecordID,UserCode,GroupName,IsValid ");
        builder.Append(" from PT_DZYJ_CYLXRFZ ");
        builder.Append(" where RecordID=" + RecordID + " ");
        PTDZYJCYLXRFZModel model = new PTDZYJCYLXRFZModel();
        DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
        if (set.Tables[0].Rows.Count <= 0)
        {
            return null;
        }
        if (set.Tables[0].Rows[0]["RecordID"].ToString() != "")
        {
            model.RecordID = int.Parse(set.Tables[0].Rows[0]["RecordID"].ToString());
        }
        model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
        model.GroupName = set.Tables[0].Rows[0]["GroupName"].ToString();
        model.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
        return model;
    }

    public int Update(PTDZYJCYLXRFZModel model)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("update PT_DZYJ_CYLXRFZ set ");
        builder.Append("UserCode='" + model.UserCode + "',");
        builder.Append("GroupName='" + model.GroupName + "',");
        builder.Append("IsValid='" + model.IsValid + "'");
        builder.Append(" where RecordID=" + model.RecordID + " ");
        return publicDbOpClass.ExecSqlString(builder.ToString());
    }
}

