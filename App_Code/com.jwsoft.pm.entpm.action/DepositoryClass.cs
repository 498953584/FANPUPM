namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using Microsoft.Web.UI.WebControls;
    using System;
    using System.Data;
    using System.Text;

    public class DepositoryClass
    {
        public bool Add(com.jwsoft.pm.entpm.model.DepositoryClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_Repe_DepositoryClass(");
            builder.Append("ClassCode,ClassName,IsValid,i_ChildNum");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append("'" + model.ClassCode + "',");
            builder.Append("'" + model.ClassName + "',");
            builder.Append("'" + model.IsValid + "',");
            builder.Append(model.i_ChildNum);
            builder.Append(")");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        public void CreateDepoClassTree(DataTable dt, TreeNodeCollection Nds, string ClassCode, string strTarget, string strNavigationURL)
        {
            TreeNode node;
            if (ClassCode == "")
            {
                DataRow[] rowArray = dt.Select("len(ClassCode) = 3");
                for (int i = 0; i < rowArray.Length; i++)
                {
                    node = new TreeNode {
                        Text = rowArray[i]["ClassName"].ToString(),
                        ID = rowArray[i]["DepoClassID"].ToString(),
                        NodeData = rowArray[i]["DepoClassID"].ToString()
                    };
                    Nds.Add(node);
                    this.CreateDepoClassTree(dt, node.Nodes, rowArray[i]["ClassCode"].ToString(), strTarget, strNavigationURL);
                }
            }
            else
            {
                DataRow[] rowArray2 = dt.Select("ClassCode like '" + ClassCode + "%' and (len(ClassCode) - 3) = " + ClassCode.Length.ToString());
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    node = new TreeNode {
                        Text = rowArray2[j]["ClassName"].ToString(),
                        ID = rowArray2[j]["DepoClassID"].ToString(),
                        NodeData = rowArray2[j]["DepoClassID"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = "dFrame";
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?DepoClassID=" + rowArray2[j]["DepoClassID"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = false;
                    this.CreateDepoClassTree(dt, node.Nodes, rowArray2[j]["ClassCode"].ToString(), strTarget, strNavigationURL);
                }
            }
        }

        private int CreateLevel(DataTable dtDepoClassTemp, string ClassCode)
        {
            DataRow[] rowArray = dtDepoClassTemp.Select("ClassCode like '" + ClassCode + "%' and (len(ClassCode) - 3) = " + ClassCode.Length.ToString());
            if ((rowArray.Length != 0) && !(rowArray[0]["ClassCode"].ToString() == ""))
            {
                return (this.CreateLevel(dtDepoClassTemp, rowArray[0]["ClassCode"].ToString()) + 1);
            }
            return 0;
        }

        public bool Delete(int DepoClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_Repe_DepositoryClass ");
            builder.Append(" where DepoClassID=" + DepoClassID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }

        private void DepoClassTree_Generate(DataTable dtDepoClassStru, DataTable dtDepoClassTemp, string ClassCode, bool isEnd, int iLevel, string strHead)
        {
            DataRow[] rowArray = dtDepoClassTemp.Select("ClassCode like '" + ClassCode + "%' and (len(ClassCode) - 3) = " + ClassCode.Length.ToString());
            if (isEnd)
            {
                for (int i = 0; i < rowArray.Length; i++)
                {
                    if (i != (rowArray.Length - 1))
                    {
                        DataRow row = dtDepoClassStru.NewRow();
                        row["DepoClassID"] = Convert.ToInt32(rowArray[i]["DepoClassID"].ToString());
                        row["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                        row["ClassName"] = strHead + this.RepeatString("│", iLevel) + "├" + rowArray[i]["ClassName"].ToString();
                        dtDepoClassStru.Rows.Add(row);
                        if (this.GetSubClassCount(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                        {
                            this.DepoClassTree_Generate(dtDepoClassStru, dtDepoClassTemp, rowArray[i]["ClassCode"].ToString(), false, this.CreateLevel(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                        }
                    }
                    else
                    {
                        DataRow row2 = dtDepoClassStru.NewRow();
                        row2["DepoClassID"] = Convert.ToInt32(rowArray[i]["DepoClassID"].ToString());
                        row2["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                        row2["ClassName"] = strHead + this.RepeatString("│", iLevel) + "└" + rowArray[i]["ClassName"].ToString();
                        dtDepoClassStru.Rows.Add(row2);
                        if (this.GetSubClassCount(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                        {
                            this.DepoClassTree_Generate(dtDepoClassStru, dtDepoClassTemp, rowArray[i]["ClassCode"].ToString(), true, this.CreateLevel(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                        }
                    }
                }
            }
            else
            {
                for (int j = 0; j < rowArray.Length; j++)
                {
                    DataRow row3 = dtDepoClassStru.NewRow();
                    row3["ClassCode"] = rowArray[j]["ClassCode"].ToString();
                    if (j != (rowArray.Length - 1))
                    {
                        row3["ClassName"] = strHead + this.RepeatString("│", iLevel) + "├ " + rowArray[j]["ClassName"].ToString();
                    }
                    else
                    {
                        row3["ClassName"] = strHead + this.RepeatString("│", iLevel) + "└ " + rowArray[j]["ClassName"].ToString();
                    }
                    dtDepoClassStru.Rows.Add(row3);
                    if (this.GetSubClassCount(dtDepoClassTemp, rowArray[j]["ClassCode"].ToString()) > 0)
                    {
                        this.DepoClassTree_Generate(dtDepoClassStru, dtDepoClassTemp, rowArray[j]["ClassCode"].ToString(), true, this.CreateLevel(dtDepoClassTemp, rowArray[j]["ClassCode"].ToString()), strHead);
                    }
                }
            }
        }

        private void DepoClassTree_GenerateFirst(DataTable dtDepoClassStru, DataTable dtDepoClassTemp, string ClassCode)
        {
            DataRow[] rowArray = dtDepoClassTemp.Select("ClassCode like '" + ClassCode + "%' and (len(ClassCode) - 3) = " + ClassCode.Length.ToString());
            for (int i = 0; i < rowArray.Length; i++)
            {
                if (i != (rowArray.Length - 1))
                {
                    DataRow row = dtDepoClassStru.NewRow();
                    row["DepoClassID"] = Convert.ToInt32(rowArray[i]["DepoClassID"].ToString());
                    row["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                    row["ClassName"] = "├" + rowArray[i]["ClassName"].ToString();
                    dtDepoClassStru.Rows.Add(row);
                    if (this.GetSubClassCount(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.DepoClassTree_Generate(dtDepoClassStru, dtDepoClassTemp, rowArray[i]["ClassCode"].ToString(), false, this.CreateLevel(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()), "");
                    }
                }
                else
                {
                    DataRow row2 = dtDepoClassStru.NewRow();
                    row2["DepoClassID"] = Convert.ToInt32(rowArray[i]["DepoClassID"].ToString());
                    row2["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                    row2["ClassName"] = "└" + rowArray[i]["ClassName"].ToString();
                    dtDepoClassStru.Rows.Add(row2);
                    if (this.GetSubClassCount(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.DepoClassTree_Generate(dtDepoClassStru, dtDepoClassTemp, rowArray[i]["ClassCode"].ToString(), true, this.CreateLevel(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()), " ");
                    }
                }
            }
        }

        public bool Exists(int DepoClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_Repe_DepositoryClass");
            builder.Append(" where DepoClassID=" + DepoClassID + " ");
            if (publicDbOpClass.ExecuteSQL(builder.ToString()) <= 0)
            {
                return false;
            }
            return true;
        }

        public DataSet GetAllList()
        {
            return this.GetList("");
        }

        public DataTable GetDepoClassStru()
        {
            DataTable dtDepoClassStru = new DataTable();
            dtDepoClassStru.Columns.Add("DepoClassID", typeof(int));
            dtDepoClassStru.Columns.Add("ClassCode", typeof(string));
            dtDepoClassStru.Columns.Add("ClassName", typeof(string));
            DataTable dtDepoClassTemp = this.GetAllList().Tables[0];
            DataRow[] rowArray = dtDepoClassTemp.Select("len(ClassCode) = 3");
            if (rowArray.Length > 0)
            {
                for (int i = 0; i < rowArray.Length; i++)
                {
                    DataRow row = dtDepoClassStru.NewRow();
                    row["DepoClassID"] = rowArray[i]["DepoClassID"].ToString();
                    row["ClassCode"] = rowArray[i]["ClassCode"].ToString();
                    row["ClassName"] = rowArray[i]["ClassName"].ToString();
                    dtDepoClassStru.Rows.Add(row);
                    if (this.GetSubClassCount(dtDepoClassTemp, rowArray[i]["ClassCode"].ToString()) > 0)
                    {
                        this.DepoClassTree_GenerateFirst(dtDepoClassStru, dtDepoClassTemp, rowArray[i]["ClassCode"].ToString());
                    }
                }
                return dtDepoClassStru;
            }
            DataRow row2 = dtDepoClassStru.NewRow();
            row2["DepoClassID"] = 0;
            row2["ClassCode"] = "";
            row2["ClassName"] = "---没有项目分类---";
            dtDepoClassStru.Rows.Add(row2);
            return dtDepoClassStru;
        }

        public DataSet GetList(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select DepoClassID,ClassCode,ClassName,IsValid,i_ChildNum ");
            builder.Append(" FROM pm_Repe_DepositoryClass ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataSetQuary(builder.ToString());
        }

        public int GetMaxId()
        {
            return int.Parse(publicDbOpClass.QuaryMaxid("pm_Repe_DepositoryClass", "DepoClassID"));
        }

        public com.jwsoft.pm.entpm.model.DepositoryClass GetModel(int DepoClassID)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select   ");
            builder.Append(" DepoClassID,ClassCode,ClassName,IsValid,i_ChildNum ");
            builder.Append(" from pm_Repe_DepositoryClass ");
            builder.Append(" where DepoClassID=" + DepoClassID + " ");
            com.jwsoft.pm.entpm.model.DepositoryClass class2 = new com.jwsoft.pm.entpm.model.DepositoryClass();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["DepoClassID"].ToString() != "")
            {
                class2.DepoClassID = int.Parse(set.Tables[0].Rows[0]["DepoClassID"].ToString());
            }
            class2.ClassCode = set.Tables[0].Rows[0]["ClassCode"].ToString();
            class2.ClassName = set.Tables[0].Rows[0]["ClassName"].ToString();
            class2.IsValid = set.Tables[0].Rows[0]["IsValid"].ToString();
            if (set.Tables[0].Rows[0]["i_ChildNum"].ToString() != "")
            {
                class2.i_ChildNum = int.Parse(set.Tables[0].Rows[0]["i_ChildNum"].ToString());
            }
            return class2;
        }

        private int GetSubClassCount(DataTable dtDepoClassTemp, string ClassCode)
        {
            int length = ClassCode.Length;
            return dtDepoClassTemp.Select("ClassCode like '" + ClassCode + "%' and (len(ClassCode) - 3) = " + length.ToString()).Length;
        }

        public int LowerLevelsCount(string ClassCode)
        {
            string[] strArray = new string[] { "select count(1) from pm_Repe_DepositoryClass  where (ClassCode like '", ClassCode, "%') and (len(ClassCode)= ", (ClassCode.Length + 3).ToString(), ")" };
            return (int) publicDbOpClass.ExecuteScalar(string.Concat(strArray));
        }

        public string MakeClassCode(string Code)
        {
            string str = "";
            int num = Code.Length + 3;
            StringBuilder builder = new StringBuilder();
            if (Code == "")
            {
                builder.Append(" select max(ClassCode) from pm_repe_DepositoryClass where len(ClassCode)=3");
            }
            else
            {
                builder.Append(" select max(ClassCode) from pm_repe_DepositoryClass where ClassCode like '" + Code + "%' and len(ClassCode)=" + num.ToString());
            }
            object obj2 = publicDbOpClass.ExecuteScalar(builder.ToString());
            if (obj2 != DBNull.Value)
            {
                str = (string) obj2;
                str = Convert.ToString((int) (int.Parse(str.Substring(str.Length - 3, 3)) + 1)).PadLeft(3, '0');
                return (Code.Trim() + str);
            }
            return (Code.Trim() + "001");
        }

        private string RepeatString(string strStr, int iNumber)
        {
            string str = "";
            for (int i = 0; i < iNumber; i++)
            {
                str = str + strStr;
            }
            return str;
        }

        public bool Update(com.jwsoft.pm.entpm.model.DepositoryClass model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_Repe_DepositoryClass set ");
            builder.Append("ClassCode='" + model.ClassCode + "',");
            builder.Append("ClassName='" + model.ClassName + "',");
            builder.Append("IsValid='" + model.IsValid + "',");
            builder.Append("i_ChildNum=" + model.i_ChildNum);
            builder.Append(" where DepoClassID=" + model.DepoClassID + " ");
            return publicDbOpClass.NonQuerySqlString(builder.ToString());
        }
    }
}

