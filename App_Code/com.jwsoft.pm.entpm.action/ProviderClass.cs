namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using Microsoft.Web.UI.WebControls;
    using System;
    using System.Data;
    using System.Text;

    public class ProviderClass
    {
        public int Add(ProviderClassModel model)
        {
            int num = int.Parse(publicDbOpClass.QuaryMaxid("pm_provider_class", "ClassId")) + 1;
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_provider_class(");
            builder.Append("ClassId,ClassName,ParentId,ClassCode,State,WDUUser,UserCode,RecordDate,Comment");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(num.ToString() + ",");
            builder.Append(model.ClassName + ",");
            builder.Append(model.ParentId + ",");
            builder.Append("'" + model.ClassCode + "',");
            builder.Append(model.State + ",");
            builder.Append("'" + model.WDUUser + "',");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append(model.Comment ?? "");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public void CreateProvClassTree(DataTable dtResType, TreeNodeCollection Nds, string TypeCode, string strTarget, string strNavigationURL)
        {
            TreeNode node;
            if (TypeCode == "")
            {
                DataRow[] rowArray = dtResType.Select("len(ClassCode) = 5");
                for (int i = 0; i < rowArray.Length; i++)
                {
                    node = new TreeNode {
                        Text = rowArray[i]["ClassName"].ToString(),
                        ID = rowArray[i]["ClassId"].ToString(),
                        NodeData = rowArray[i]["ClassId"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = strTarget;
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?ClassId=" + rowArray[i]["ClassId"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = true;
                    this.CreateProvClassTree(dtResType, node.Nodes, rowArray[i]["ClassCode"].ToString(), strTarget, strNavigationURL);
                }
            }
            else
            {
                DataRow[] rowArray2 = dtResType.Select("ClassCode like '" + TypeCode + "%' and (len(ClassCode) - 5) = " + TypeCode.Length.ToString());
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    node = new TreeNode {
                        Text = rowArray2[j]["ClassName"].ToString(),
                        ID = rowArray2[j]["ClassId"].ToString(),
                        NodeData = rowArray2[j]["ClassId"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = strTarget;
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?ClassId=" + rowArray2[j]["ClassId"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = false;
                    this.CreateProvClassTree(dtResType, node.Nodes, rowArray2[j]["ClassCode"].ToString(), strTarget, strNavigationURL);
                }
            }
        }

        public int Delete(int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_provider_class ");
            builder.Append(" where ClassId=" + ClassId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public DataTable dtDuty(string DeptID)
        {
            return publicDbOpClass.DataTableQuary("SELECT i_bmdm,DutyCode,I_DUTYID,DutyName RoleTypeName FROM  PT_DUTY WHERE c_sfyx = '1' AND i_bmdm =" + DeptID);
        }

        public DataTable dtDuty2(string I_DUTYID)
        {
            return publicDbOpClass.DataTableQuary("SELECT i_bmdm,DutyCode,I_DUTYID,DutyName RoleTypeName FROM PT_DUTY WHERE PT_DUTY.I_DUTYID in (" + I_DUTYID + ")");
        }

        public bool Exists(int ClassId)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_provider_class where ClassId=" + ClassId);
            object objA = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (object.Equals(objA, null) || object.Equals(objA, DBNull.Value))
            {
                num = 0;
            }
            else
            {
                num = int.Parse(objA.ToString());
            }
            if (num == 0)
            {
                return false;
            }
            return true;
        }

        public string getDutyIDs(string ClassId)
        {
            return publicDbOpClass.ExecuteScalar("select isnull(WDUUser,'')  from pm_provider_class where ClassId=" + ClassId).ToString();
        }

        public string GetDutyName(string I_DUTYID)
        {
            DataTable table = publicDbOpClass.DataTableQuary("SELECT dbo.PT_DUTY.i_bmdm,dbo.PT_DUTY.DutyCode, dbo.PT_D_Role.RoleTypeCode, dbo.PT_D_Role.RoleTypeName, dbo.PT_DUTY.I_DUTYID FROM dbo.PT_D_Role INNER JOIN dbo.PT_DUTY ON dbo.PT_D_Role.RoleTypeCode=dbo.PT_DUTY.DutyCode WHERE    dbo.PT_DUTY.I_DUTYID in (" + I_DUTYID + ")");
            string str2 = "";
            foreach (DataRow row in table.Rows)
            {
                if (str2 == "")
                {
                    str2 = str2 + row["RoleTypeName"].ToString();
                }
                else
                {
                    str2 = str2 + "，" + row["RoleTypeName"].ToString();
                }
            }
            return str2;
        }

        public DataTable GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_provider_class ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            builder.Append(" order by ClassId ");
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public ProviderClassModel GetModel(int ClassId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_provider_class ");
            builder.Append(" where ClassId=" + ClassId);
            ProviderClassModel model = new ProviderClassModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            model.ClassId = ClassId;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            model.ClassName = set.Tables[0].Rows[0]["ClassName"].ToString();
            if (set.Tables[0].Rows[0]["ParentId"].ToString() != "")
            {
                model.ParentId = int.Parse(set.Tables[0].Rows[0]["ParentId"].ToString());
            }
            model.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            if (set.Tables[0].Rows[0]["State"].ToString() != "")
            {
                model.State = int.Parse(set.Tables[0].Rows[0]["State"].ToString());
            }
            model.WDUUser = set.Tables[0].Rows[0]["WDUUser"].ToString();
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            model.Comment = set.Tables[0].Rows[0]["Comment"].ToString();
            return model;
        }

        public DataTable GetProvider(string strWhere)
        {
            return publicDbOpClass.DataTableQuary(("select * from pm_provider_class where ClassID = " + strWhere).ToString());
        }

        public DataTable GetProviderType()
        {
            string str = "select * from pm_provider_class  order by ClassCode";
            return publicDbOpClass.DataTableQuary(str.ToString());
        }

        public DataTable GetUserProClass(string YHDM)
        {
            string sqlString = "";
            if (YHDM == "")
            {
                sqlString = "select * from pm_provider_class ";
                return publicDbOpClass.DataTableQuary(sqlString);
            }
            DataTable table2 = publicDbOpClass.DataTableQuary(((" select a.* from pm_provider_class a" + " where  Patindex(") + " '%,'+(select cast(i_dutyid as varchar) from pt_yhmc where V_YHDM = '" + YHDM + "')  +',%'") + " ,','+isnull(a.WDUUser,'0')+',') > 0  order by ClassCode");
            string str2 = "";
            if (table2.Rows.Count > 0)
            {
                foreach (DataRow row in table2.Rows)
                {
                    if (str2 == "")
                    {
                        str2 = "select * from pm_provider_class where ClassCode like '" + row["ClassCode"].ToString().Trim() + "%'";
                    }
                    else
                    {
                        str2 = str2 + " or ClassCode like '" + row["ClassCode"].ToString().Trim() + "%'";
                    }
                }
            }
            else
            {
                str2 = "select * from pm_provider_class where ClassCode = '-1'";
            }
            return publicDbOpClass.DataTableQuary(str2);
        }

        public int IsHaveProvider(string ClassId)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_provider_info where ClassId=" + ClassId);
        }

        public int LowerLevelsCount(string ClassId)
        {
            return (int) publicDbOpClass.ExecuteScalar("select count(1) from pm_provider_class where ParentId =" + ClassId);
        }

        public string MakeClassCode(string Code)
        {
            string str = "";
            int num = Code.Length + 5;
            StringBuilder builder = new StringBuilder();
            if (Code == "")
            {
                builder.Append(" select max(ClassCode) from pm_provider_class where len(ClassCode)=5");
            }
            else
            {
                builder.Append(" select max(ClassCode) from pm_provider_class where ClassCode like '" + Code + "%' and len(ClassCode)=" + num.ToString());
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = (string) obj2;
                str = Convert.ToString((int) (int.Parse(str.Substring(str.Length - 5, 5)) + 1)).PadLeft(5, '0');
                return (Code.Trim() + str);
            }
            return (Code.Trim() + "001");
        }

        public int Update(ProviderClassModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_provider_class set ");
            builder.Append("ClassName='" + model.ClassName + "',");
            builder.Append("ParentId=" + model.ParentId + ",");
            builder.Append("ClassCode='" + model.ClassCode + "',");
            builder.Append("State=" + model.State + ",");
            builder.Append("WDUUser='" + model.WDUUser + "',");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("Comment='" + model.Comment + "'");
            builder.Append(" where ClassId=" + model.ClassId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpdateClass(ProviderClassModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_provider_class set ");
            builder.Append("ClassName=" + model.ClassName + ",");
            builder.Append("ClassCode='" + model.ClassCode + "',");
            builder.Append("Comment=" + model.Comment);
            builder.Append(" where ClassId=" + model.ClassId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public int UpDateDuty(ProviderClassModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_provider_class set ");
            builder.Append("WDUUser='" + model.WDUUser + "'");
            builder.Append(" where ClassId=" + model.ClassId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool updateWDUUser(string ClassId, string DutyIDs)
        {
            return publicDbOpClass.NonQuerySqlString("update pm_provider_class set WDUUser = '" + DutyIDs + "' where ClassId=" + ClassId);
        }
    }
}

