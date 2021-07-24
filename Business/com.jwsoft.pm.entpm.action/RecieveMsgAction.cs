namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using System;
    using System.Data;
    using System.Text;

    public class RecieveMsgAction
    {
        public int Add(RecieveMsgModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" begin");
            builder.Append(" insert into WF_RecieveMsg(");
            builder.Append("v_yhdm,InstanceCode,TheOrder,LookUrl,RecieveUserCode,RecieveDate");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.v_yhdm + "',");
            builder.Append("'" + model.InstanceCode + "',");
            builder.Append(model.TheOrder + ",");
            builder.Append("'" + model.LookUrl + "',");
            builder.Append("'" + model.RecieveUserCode + "',");
            builder.Append("'" + model.RecieveDate + "'");
            builder.Append(")");
            builder.Append(";select @@IDENTITY");
            builder.Append(" end");
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != null)
            {
                this.getPTDBSJ(obj2.ToString(), model.LookUrl, model.v_yhdm);
                return 1;
            }
            return 0;
        }

        public int Delete(Guid InstanceCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete WF_RecieveMsg ");
            builder.Append(" where InstanceCode='" + InstanceCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int Delete(Guid InstanceCode, string RecieveUserCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete WF_RecieveMsg ");
            builder.Append(string.Concat(new object[] { " where InstanceCode='", InstanceCode, "' and RecieveUserCode = '", RecieveUserCode, "' " }));
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select v_yhdm,InstanceCode,TheOrder,LookUrl ,RecieveUserCode,RecieveDate");
            builder.Append(" FROM WF_RecieveMsg ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetList(int RecordID, bool bl)
        {
            string str = "";
            return publicDbOpClass.DataTableQuary(str + "SELECT top 1 v_yhdm, InstanceCode, TheOrder, LookUrl, (SELECT TOP 1 n.BusinessClassName FROM WF_Business_Class AS n INNER JOIN WF_Instance_Main AS m ON n.BusinessCode = m.BusinessCode and n.BusinessClass = m.BusinessClass WHERE (m.InstanceCode = a.InstanceCode)) AS BusinessClassName,RecieveUserCode FROM WF_RecieveMsg AS a WHERE a.RecordID =" + RecordID);
        }

        public RecieveMsgModel GetModel(Guid InstanceCode)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" v_yhdm,InstanceCode,TheOrder,LookUrl,RecieveUserCode,RecieveDate ");
            builder.Append(" from WF_RecieveMsg ");
            builder.Append(" where InstanceCode='" + InstanceCode + "' ");
            RecieveMsgModel model = new RecieveMsgModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            model.v_yhdm = set.Tables[0].Rows[0]["v_yhdm"].ToString();
            if (set.Tables[0].Rows[0]["InstanceCode"].ToString() != "")
            {
                model.InstanceCode = new Guid(set.Tables[0].Rows[0]["InstanceCode"].ToString());
            }
            if (set.Tables[0].Rows[0]["TheOrder"].ToString() != "")
            {
                model.TheOrder = new decimal?(decimal.Parse(set.Tables[0].Rows[0]["TheOrder"].ToString()));
            }
            model.LookUrl = set.Tables[0].Rows[0]["LookUrl"].ToString();
            model.RecieveUserCode = set.Tables[0].Rows[0]["RecieveUserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecieveDate"].ToString() != "")
            {
                model.RecieveDate = DateTime.Parse(set.Tables[0].Rows[0]["RecieveDate"].ToString());
            }
            return model;
        }

        private void getPTDBSJ(string xgid, string Mes, string jsyhdm)
        {
            DataTable table = publicDbOpClass.DataTableQuary(string.Format("\r\n\t\t\t\tSELECT DISTINCT MSG.*, IM.BusinessCode, BC.DoWithUrl\r\n\t\t\t\tFROM WF_RecieveMsg AS MSG\r\n\t\t\t\tJOIN WF_Instance_Main AS IM ON IM.InstanceCode = MSG.InstanceCode\r\n\t\t\t\tJOIN WF_BusinessCode AS BC ON BC.BusinessCode = IM.BusinessCode\r\n\t\t\t\tWHERE MSG.RecordID='{0}'\r\n\t\t\t", xgid));
            if (table.Rows.Count > 0)
            {
                PTDBSJ model = new PTDBSJ {
                    V_LXBM = "014",
                    I_XGID = xgid,
                    DTM_DBSJ = DateTime.Now,
                    V_Content = Mes
                };
                string str2 = table.Rows[0]["DoWithUrl"].ToString().TrimStart(new char[] { '/' });
                if (!str2.Contains("?"))
                {
                    model.V_DBLJ = str2 + "?ic=" + table.Rows[0]["InstanceCode"].ToString();
                }
                else
                {
                    model.V_DBLJ = str2 + "&ic=" + table.Rows[0]["InstanceCode"].ToString();
                }
                model.V_YHDM = jsyhdm;
                PublicInterface.SendSysMsg(model);
            }
        }

        public int Update(RecieveMsgModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update WF_RecieveMsg set ");
            builder.Append("v_yhdm='" + model.v_yhdm + "',");
            builder.Append("TheOrder=" + model.TheOrder + ",");
            builder.Append("LookUrl='" + model.LookUrl + "'");
            builder.Append("RecieveUserCode='" + model.RecieveUserCode + "',");
            builder.Append("RecieveDate='" + model.RecieveDate + "'");
            builder.Append(" where InstanceCode='" + model.InstanceCode + "' ");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }
    }
}

