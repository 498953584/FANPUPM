namespace com.jwsoft.pm.datas
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.Model;
    using System;
    using System.Data;

    public class WokLog
    {
        public static int Add(ConstructionLogModel mdl)
        {
            object obj2 = (((((((((("" + "insert into pm_Work_Log(" + "logID,ProjectId,code,part,attendance,temperature,amweather,pmweather,operations,thisDate,daycontent,design,acceptance,beton,datum,product,situation,remark") + ")" + " values (") + "'" + mdl.logID + "',") + mdl.ProjectId + ",") + "'" + mdl.code + "',") + "'" + mdl.part + "',") + mdl.attendance + ",") + "'" + mdl.temperature + "',") + "'" + mdl.amweather + "',") + "'" + mdl.pmweather + "',") + "'" + mdl.operations + "',";
            return publicDbOpClass.ExecSqlString((((((((((string.Concat(new object[] { obj2, "'", mdl.thisDate, "'," }) + "'" + mdl.daycontent + "',") + "'" + mdl.design + "',") + "'" + mdl.acceptance + "',") + "'" + mdl.beton + "',") + "'" + mdl.datum + "',") + "'" + mdl.product + "',") + "'" + mdl.situation + "',") + "'" + mdl.remark + "'") + ")").ToString());
        }

        public static int Add(ConstructionLogModel mdl, string projectId)
        {
            object obj2 = (((((((((("" + "insert into pm_Work_Log(" + "logID,ProjectId,code,part,attendance,temperature,amweather,pmweather,operations,thisDate,daycontent,design,acceptance,beton,datum,product,situation,remark") + ")" + " values (") + "'" + mdl.logID + "',") + "'" + projectId.Trim() + "',") + "'" + mdl.code + "',") + "'" + mdl.part + "',") + mdl.attendance + ",") + "'" + mdl.temperature + "',") + "'" + mdl.amweather + "',") + "'" + mdl.pmweather + "',") + "'" + mdl.operations + "',";
            return publicDbOpClass.ExecSqlString((((((((((string.Concat(new object[] { obj2, "'", mdl.thisDate, "'," }) + "'" + mdl.daycontent + "',") + "'" + mdl.design + "',") + "'" + mdl.acceptance + "',") + "'" + mdl.beton + "',") + "'" + mdl.datum + "',") + "'" + mdl.product + "',") + "'" + mdl.situation + "',") + "'" + mdl.remark + "'") + ")").ToString());
        }

        public static int Delete(string logID)
        {
            string str = "";
            return publicDbOpClass.ExecSqlString(((str + "delete pm_Work_Log ") + " where logID='" + logID + "'").ToString());
        }

        public static int Exist(string code)
        {
            string str = "";
            return int.Parse(publicDbOpClass.ExecuteScalar(((str + "select count(*) from pm_Work_Log where code='") + code + "'").ToString()).ToString());
        }

        public static DataTable GetList(string pmId)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary((((str + "select * from pm_Work_Log ") + "where" + pmId) + "order by thisDate desc").ToString());
        }

        public static ConstructionLogModel GetModel(string logID)
        {
            string str = "";
            str = (str + "select * from pm_Work_Log ") + " where logID='" + logID + "'";
            ConstructionLogModel model = new ConstructionLogModel();
            DataSet set = publicDbOpClass.DataSetQuary(str.ToString());
            model.logID = logID;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ProjectId"].ToString() != "")
            {
                model.ProjectId = int.Parse(set.Tables[0].Rows[0]["ProjectId"].ToString());
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

        public static DataTable GetQuery(string code, string part, string operations, string pmid, string workdate)
        {
            string str = "";
            str = str + "select * from pm_Work_Log where 1=1 ";
            string str2 = "";
            if (code.Trim() != "")
            {
                str2 = str2 + " and code like '" + code + "'";
            }
            if ((part.Trim() != "") && (str2 == ""))
            {
                str2 = str2 + "and part like '%" + part + "%'";
            }
            else if ((part.Trim() != "") && (str2 != ""))
            {
                str2 = str2 + " and  part like '%" + part + "%'";
            }
            if ((operations.Trim() != "") && (str2 == ""))
            {
                str2 = str2 + " and operations like '" + operations + "'";
            }
            else if ((operations.Trim() != "") && (str2 != ""))
            {
                str2 = str2 + " and operations like '%" + operations + "%'";
            }
            if (str2 == "")
            {
                str2 = str2 + " and projectid ='" + pmid + "'";
            }
            else
            {
                str2 = str2 + " and projectid ='" + pmid + "'";
            }
            if (!string.IsNullOrEmpty(workdate))
            {
                object obj2 = str2;
                str2 = string.Concat(new object[] { obj2, " and thisDate='", Convert.ToDateTime(workdate), "'" });
            }
            return publicDbOpClass.DataTableQuary((str + str2 + " order by thisDate desc").ToString());
        }

        public static int Update(ConstructionLogModel mdl)
        {
            string str = "";
            object obj2 = str + "update pm_Work_Log set ";
            object obj3 = (string.Concat(new object[] { obj2, "ProjectId=", mdl.ProjectId, "," }) + "code='" + mdl.code + "',") + "part='" + mdl.part + "',";
            object obj4 = (((string.Concat(new object[] { obj3, "attendance=", mdl.attendance, "," }) + "temperature='" + mdl.temperature + "',") + "amweather='" + mdl.amweather + "',") + "pmweather='" + mdl.pmweather + "',") + "operations='" + mdl.operations + "',";
            return publicDbOpClass.ExecSqlString((((((((((string.Concat(new object[] { obj4, "thisDate='", mdl.thisDate, "'," }) + "daycontent='" + mdl.daycontent + "',") + "design='" + mdl.design + "',") + "acceptance='" + mdl.acceptance + "',") + "beton='" + mdl.beton + "',") + "datum='" + mdl.datum + "',") + "product='" + mdl.product + "',") + "situation='" + mdl.situation + "',") + "remark='" + mdl.remark + "'") + " where logID='" + mdl.logID + "'").ToString());
        }
    }
}

