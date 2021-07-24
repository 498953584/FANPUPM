using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.Model;
using System;
using System.Data;

public class ConstructionLog
{
    public static ConstructionLogModel GetModel(string logID)
    {
        string str = "";
        str = str + "select * from pm_Construction_Log  where logID='" + logID + "'";
        ConstructionLogModel model = new ConstructionLogModel();
        DataSet set = publicDbOpClass.DataSetQuary(str.ToString());
        model.logID = logID;
        if (set.Tables[0].Rows.Count <= 0)
        {
            return null;
        }
        model.code = set.Tables[0].Rows[0]["code"].ToString();
        model.part = set.Tables[0].Rows[0]["part"].ToString();
        if (set.Tables[0].Rows[0]["attendance"].ToString() != "")
        {
            model.attendance = int.Parse(set.Tables[0].Rows[0]["attendance"].ToString());
        }
        model.temperature = set.Tables[0].Rows[0]["temperature"].ToString();
        model.amweather = set.Tables[0].Rows[0]["amweather"].ToString();
        model.pmweather = set.Tables[0].Rows[0]["pmweather"].ToString();
        model.operations = set.Tables[0].Rows[0]["operations"].ToString();
        if (set.Tables[0].Rows[0]["thisDate"].ToString() != "")
        {
            model.thisDate = DateTime.Parse(set.Tables[0].Rows[0]["thisDate"].ToString());
        }
        model.daycontent = set.Tables[0].Rows[0]["daycontent"].ToString();
        model.design = set.Tables[0].Rows[0]["design"].ToString();
        model.acceptance = set.Tables[0].Rows[0]["acceptance"].ToString();
        model.beton = set.Tables[0].Rows[0]["beton"].ToString();
        model.datum = set.Tables[0].Rows[0]["datum"].ToString();
        model.product = set.Tables[0].Rows[0]["product"].ToString();
        model.situation = set.Tables[0].Rows[0]["situation"].ToString();
        model.remark = set.Tables[0].Rows[0]["remark"].ToString();
        return model;
    }
}

