namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Collections;
    using System.Data;

    public class TemplatesAction
    {
        public static bool Add(Templates model)
        {
            string str = "";
            str = "insert into Templates(";
            str = str + "TemplatesCode,TemplatesName,ParentCode)" + " values (";
            return (publicDbOpClass.ExecSqlString(str + "'" + model.TemplatesCode + "','" + model.TemplatesName + "','" + model.ParentCode + "')") > 0);
        }

        public static bool Delete(string TemplatesCode)
        {
            string str = "";
            str = "delete from Templates ";
            return (publicDbOpClass.ExecSqlString(str + " where TemplatesCode='" + TemplatesCode + "'") > 0);
        }

        public static bool Exists(string TemplatesCode)
        {
            string str = "";
            str = "select count(1) from Templates";
            return (publicDbOpClass.ExecSqlString(str + " where TemplatesCode='" + TemplatesCode + "'") > 0);
        }

        public static ArrayList getAllTemp()
        {
            string sqlString = "select * from Templates";
            DataSet set = publicDbOpClass.DataSetQuary(sqlString);
            ArrayList list = new ArrayList();
            for (int i = 0; i < set.Tables[0].Rows.Count; i++)
            {
                Templates templates = new Templates {
                    TemplatesCode = set.Tables[0].Rows[i]["TemplatesCode"].ToString(),
                    TemplatesName = set.Tables[0].Rows[i]["TemplatesName"].ToString(),
                    ParentCode = set.Tables[0].Rows[i]["ParentCode"].ToString()
                };
                list.Add(templates);
            }
            return list;
        }

        public static DataSet GetList(string strWhere)
        {
            string sqlString = "";
            sqlString = "select TemplatesCode,TemplatesName,ParentCode ";
            sqlString = sqlString + " FROM Templates ";
            if (strWhere.Trim() != "")
            {
                sqlString = sqlString + " where " + strWhere;
            }
            return publicDbOpClass.DataSetQuary(sqlString);
        }

        public static Templates GetModel(string TemplatesCode)
        {
            string sqlString = "";
            sqlString = "select  top 1 TemplatesCode,TemplatesName,ParentCode from Templates ";
            sqlString = sqlString + " where TemplatesCode='" + TemplatesCode + "' ";
            Templates templates = new Templates();
            DataSet set = publicDbOpClass.DataSetQuary(sqlString);
            if (set.Tables[0].Rows.Count > 0)
            {
                templates.TemplatesCode = set.Tables[0].Rows[0]["TemplatesCode"].ToString();
                templates.TemplatesName = set.Tables[0].Rows[0]["TemplatesName"].ToString();
                templates.ParentCode = set.Tables[0].Rows[0]["ParentCode"].ToString();
                return templates;
            }
            return null;
        }

        public static bool getPare(string pareCode)
        {
            return (publicDbOpClass.DataSetQuary("select * from Templates where ParentCode='" + pareCode + "'").Tables[0].Rows.Count > 0);
        }

        public static string initCode(string pareCode)
        {
            int num = pareCode.Length + 3;
            DataTable table = publicDbOpClass.DataTableQuary(string.Concat(new object[] { "select max(TemplatesCode) from Templates where (TemplatesCode like '", pareCode, "%') and len(TemplatesCode) = ", num }));
            string str3 = table.Rows[0][0].ToString().Trim();
            if (pareCode.Trim().Length == 0)
            {
                if (str3.Length == 0)
                {
                    return "001";
                }
                return Convert.ToString((int) (Convert.ToInt32(str3) + 1)).PadLeft(3, '0');
            }
            if (str3.Length == 0)
            {
                return (pareCode + "001");
            }
            string str4 = Convert.ToString((int) (Convert.ToInt32(str3.Substring(str3.Length - 3, 3)) + 1));
            return (pareCode + str4.PadLeft(3, '0'));
        }

        public static bool Update(Templates model)
        {
            string str = "";
            str = "update Templates set ";
            return (publicDbOpClass.ExecSqlString(((str + "TemplatesName='" + model.TemplatesName + "',") + "ParentCode='" + model.ParentCode + "'") + " where TemplatesCode='" + model.TemplatesCode + "'") > 0);
        }
    }
}

