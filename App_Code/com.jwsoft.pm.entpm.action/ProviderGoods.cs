namespace com.jwsoft.pm.entpm.action
{
    using com.jwsoft.pm.data;
    using com.jwsoft.pm.entpm.model;
    using Microsoft.Web.UI.WebControls;
    using System;
    using System.Data;
    using System.Text;

    public class ProviderGoods
    {
        public int Add(ProviderGoodsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("insert into pm_provider_Goods(");
            builder.Append("MaterialId,ProviderId,UserCode,RecordDate,Comment");
            builder.Append(")");
            builder.Append(" values (");
            builder.Append(model.MaterialId + ",");
            builder.Append(model.ProviderId + ",");
            builder.Append("'" + model.UserCode + "',");
            builder.Append("'" + model.RecordDate + "',");
            builder.Append("'" + model.Comment + "'");
            builder.Append(")");
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public void CreateResTypeTree(DataTable dtResType, TreeNodeCollection Nds, string TypeCode, string strTarget, string strNavigationURL)
        {
            TreeNode node;
            if (TypeCode == "")
            {
                DataRow[] rowArray = dtResType.Select("len(TypeCode) = 3");
                for (int i = 0; i < rowArray.Length; i++)
                {
                    node = new TreeNode {
                        Text = rowArray[i]["TypeName"].ToString(),
                        ID = rowArray[i]["TypeCode"].ToString(),
                        NodeData = rowArray[i]["TypeCode"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = strTarget;
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?TypeCode=" + rowArray[i]["TypeCode"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = true;
                    this.CreateResTypeTree(dtResType, node.Nodes, rowArray[i]["TypeCode"].ToString(), strTarget, strNavigationURL);
                }
            }
            else
            {
                DataRow[] rowArray2 = dtResType.Select("TypeCode like '" + TypeCode + "%' and (len(TypeCode) - 3) = " + TypeCode.Length.ToString());
                for (int j = 0; j < rowArray2.Length; j++)
                {
                    node = new TreeNode {
                        Text = rowArray2[j]["TypeName"].ToString(),
                        ID = rowArray2[j]["TypeCode"].ToString(),
                        NodeData = rowArray2[j]["TypeCode"].ToString()
                    };
                    if (strTarget.Trim() != "")
                    {
                        node.Target = strTarget;
                    }
                    if (strNavigationURL.Trim() != "")
                    {
                        node.NavigateUrl = strNavigationURL + "?TypeCode=" + rowArray2[j]["TypeCode"].ToString();
                    }
                    Nds.Add(node);
                    node.Expanded = false;
                    this.CreateResTypeTree(dtResType, node.Nodes, rowArray2[j]["TypeCode"].ToString(), strTarget, strNavigationURL);
                }
            }
        }

        public int Delete(int MaterialId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("delete pm_provider_Goods ");
            builder.Append(" where MaterialId=" + MaterialId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool Exists(int MaterialId)
        {
            int num;
            StringBuilder builder = new StringBuilder();
            builder.Append("select count(1) from pm_provider_Goods where MaterialId=" + MaterialId);
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

        public DataTable GetGoodsInfo(string ProviderId)
        {
            return publicDbOpClass.DataTableQuary("select a.ResPID,a.MaterialId,a.ProviderId,  b.ResCode,b.ResName,b.Specification,b.ResModel,c.UnitName from PM_Prj_Res_Provider a  left join PM_Prj_Res_Item b on a.MaterialId = b.MaterialId left join PM_Prj_Res_Unit c on c.UnitID = b.UnitID where a.ProviderId = " + ProviderId);
        }

        public string GetMaxId()
        {
            return publicDbOpClass.QuaryMaxid("pm_provider_Goods", "MaterialId");
        }

        public ProviderGoodsModel GetModel(int MaterialId)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("select * from pm_provider_Goods ");
            builder.Append(" where MaterialId=" + MaterialId);
            ProviderGoodsModel model = new ProviderGoodsModel();
            DataSet set = publicDbOpClass.DataSetQuary(builder.ToString());
            model.MaterialId = MaterialId;
            if (set.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            if (set.Tables[0].Rows[0]["ProviderId"].ToString() != "")
            {
                model.ProviderId = int.Parse(set.Tables[0].Rows[0]["ProviderId"].ToString());
            }
            model.UserCode = set.Tables[0].Rows[0]["UserCode"].ToString();
            if (set.Tables[0].Rows[0]["RecordDate"].ToString() != "")
            {
                model.RecordDate = DateTime.Parse(set.Tables[0].Rows[0]["RecordDate"].ToString());
            }
            model.Comment = set.Tables[0].Rows[0]["Comment"].ToString();
            return model;
        }

        public DataTable GetProGoodsInfo(string strWhere)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(" select * from pm_Prj_Res_Item ");
            if (strWhere.Trim() != "")
            {
                builder.Append(" where " + strWhere);
            }
            return publicDbOpClass.DataTableQuary(builder.ToString());
        }

        public DataTable GetProID(string strWhere)
        {
            return publicDbOpClass.DataTableQuary("select * from pm_Provider_info where ProviderId='" + strWhere + "'");
        }

        public DataTable GetProResList(string MaterialIds)
        {
            return publicDbOpClass.DataTableQuary("select a.MaterialId,a.ResCode,a.ResName,a.Specification,a.ResModel,b.UnitName from PM_Prj_Res_Item a left join PM_Prj_Res_Unit b on a.UnitID = b.UnitID where a.MaterialId in (" + MaterialIds + ")");
        }

        public string GetProviderResIDs(string ProviderId)
        {
            string str = "";
            DataTable table = publicDbOpClass.DataTableQuary("select MaterialId from PM_Prj_Res_Provider where ProviderId=" + ProviderId);
            if (table.Rows.Count > 0)
            {
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    if (str == "")
                    {
                        str = table.Rows[i]["MaterialId"].ToString().Trim();
                    }
                    else
                    {
                        str = str + "," + table.Rows[i]["MaterialId"].ToString().Trim();
                    }
                }
            }
            return str;
        }

        public DataTable GetResList()
        {
            string sqlString = "select * from PM_Prj_ResType order by TypeCode";
            return publicDbOpClass.DataTableQuary(sqlString);
        }

        public int Update(ProviderGoodsModel model)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("update pm_provider_Goods set ");
            builder.Append("ProviderId=" + model.ProviderId + ",");
            builder.Append("UserCode='" + model.UserCode + "',");
            builder.Append("RecordDate='" + model.RecordDate + "',");
            builder.Append("Comment='" + model.Comment + "'");
            builder.Append(" where MaterialId=" + model.MaterialId);
            return publicDbOpClass.ExecSqlString(builder.ToString());
        }

        public bool updateProvMater(string ProviderId, string MaterialIds)
        {
            string sqlString = "";
            if (MaterialIds == "")
            {
                sqlString = "delete PM_Prj_Res_Provider where ProviderId = " + ProviderId;
            }
            else
            {
                string str2 = "begin delete PM_Prj_Res_Provider where ProviderId = " + ProviderId + " and MaterialId not in (" + MaterialIds + ") ";
                sqlString = str2 + " insert into PM_Prj_Res_Provider (IsValid,ProviderId,MaterialId) select '1'," + ProviderId + ",a.MaterialId from  PM_Prj_Res_Item a where a.MaterialId in (" + MaterialIds + ") and (not EXISTS(select b.MaterialId from PM_Prj_Res_Provider b where b.ProviderId =" + ProviderId + " and  b.MaterialId = a.MaterialId)) end";
            }
            return publicDbOpClass.NonQuerySqlString(sqlString);
        }
    }
}

