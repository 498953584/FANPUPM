namespace cn.justwin.stockBLL
{
    using cn.justwin.DAL;
    using cn.justwin.stockDAL;
    using cn.justwin.stockModel;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;
    using System.Web.UI.WebControls;

    public class Treasury
    {
        private cn.justwin.stockDAL.Treasury dal = new cn.justwin.stockDAL.Treasury();

        public int Add(TreasuryModel model)
        {
            return this.dal.Add(model);
        }

        public void AddChildNodes(TreeNodeCollection treeNodes, string tcode, DataTable dt)
        {
            foreach (DataRow row in dt.Rows)
            {
                row["tcode"].ToString();
            }
        }

        public int AddRoot()
        {
            return this.dal.AddRoot();
        }

        public void CancelTotal()
        {
            this.dal.CancelTotal();
        }

        public string CreateNewCode(string tcode)
        {
            int chidldNodesLength = this.GetChidldNodesLength(tcode);
            string str2 = string.Empty;
            if (!tcode.Equals("0"))
            {
                str2 = tcode;
            }
            if (chidldNodesLength == 0)
            {
                return (str2 + "001");
            }
            return (str2 + this.newLastThreeCode(tcode));
        }

        public void DelContrast()
        {
            this.dal.DelContrast();
        }

        public int Delete(string tcode)
        {
            return this.dal.Delete(tcode);
        }

        public int Delete(string[] arrTcode)
        {
            return this.dal.Delete(arrTcode);
        }

        public int GetChidldNodesLength(string tcode)
        {
            return this.dal.GetChildNodesLength(tcode);
        }

        public DataTable GetChildData(string tcode)
        {
            return this.dal.GetChildData(tcode);
        }

        public string GetContrastTreasuryCode()
        {
            return this.dal.GetContrastTreasuryCode();
        }

        public int GetCount()
        {
            return this.dal.GetCount();
        }

        public List<TreasuryModel> GetList()
        {
            return this.dal.GetList();
        }

        public List<TreasuryModel> GetList(string condition)
        {
            return this.dal.GetList(condition);
        }

        public SmEnum.SmSetValue GetManageMode()
        {
            string str = basicSetBLL.getValue(SmEnum.SmSetName.DepotType.ToString());
            if (!str.Equals(SmEnum.SmSetValue.ParallelMode.ToString()) && str.Equals(SmEnum.SmSetValue.TotalMode.ToString()))
            {
                return SmEnum.SmSetValue.TotalMode;
            }
            return SmEnum.SmSetValue.ParallelMode;
        }

        public TreasuryModel GetModel(string tcode)
        {
            return this.dal.GetModel(tcode);
        }

        public string GetParallel()
        {
            return this.dal.GetParallel();
        }

        public string GetPrjName(string prjCode)
        {
            string str = "";
            string[] strArray = prjCode.Split(new char[] { ',' });
            string str2 = "";
            foreach (string str3 in strArray)
            {
                if (str3 != "")
                {
                    str = str + ",'" + str3 + "'";
                }
            }
            if (str.Length > 0)
            {
                DataTable table = DBHelper.GetTable("select prjGuid,prjName from pt_prjinfo where prjGuid in (" + str.Substring(1, str.Length - 1) + ")");
                if (table.Rows.Count <= 0)
                {
                    return str2;
                }
                foreach (DataRow row in table.Rows)
                {
                    str2 = str2 + row[1].ToString() + ",";
                }
            }
            return str2;
        }

        public DataTable GetSameModel(string strName)
        {
            return this.dal.GetSameModel(strName);
        }

        public string GetTcode(string prjCode)
        {
            return this.dal.GetTcodeByPrjcode(prjCode);
        }

        public string GetTotalCode()
        {
            return this.dal.GetTotalCode();
        }

        public TreasuryModel GetTotalTreasury()
        {
            return this.dal.GetTotalTreasury();
        }

        private static bool IsParent(TreasuryModel model)
        {
            return model.tcode.Equals("0");
        }

        public bool IsRelationTreasury(string prjCode)
        {
            string cmdText = string.Format("SELECT COUNT(*) FROM SM_Treasury WHERE prjCode LIKE '%{0}%'", prjCode);
            return (DBHelper.GetInt(SqlHelper.ExecuteScalar(CommandType.Text, cmdText)) > 0);
        }

        private string newLastThreeCode(string tcode)
        {
            int num = Convert.ToInt32(this.dal.GetMaxChildLastThreeCode(tcode)) + 1;
            return num.ToString().PadLeft(3, '0');
        }

        public void ParseTreasury(TreeNodeCollection treeNodes, string ptcode)
        {
            DataView view = new DataView(this.dal.GetTreeViewDataSource());
            if (string.IsNullOrEmpty(ptcode))
            {
                view.RowFilter = "tcode = '0'";
            }
            else
            {
                view.RowFilter = string.Format("ptcode = '{0}'", ptcode);
            }
            foreach (DataRow row in view.ToTable().Rows)
            {
                TreeNode child = new TreeNode(row["tname"].ToString(), row["tcode"].ToString());
                treeNodes.Add(child);
                this.ParseTreasury(child.ChildNodes, row["tcode"].ToString());
            }
        }

        public void ParseTreasuryList(TreeNodeCollection treeNodes, string ptcode, string selectCode)
        {
            List<TreasuryModel> list = this.dal.GetList();
            List<TreasuryModel> list2 = new List<TreasuryModel>();
            if (string.IsNullOrEmpty(ptcode))
            {
                list2 = list.FindAll(new Predicate<TreasuryModel>(cn.justwin.stockBLL.Treasury.IsParent));
            }
            else
            {
                foreach (TreasuryModel model in list)
                {
                    if (model.ptcode.Equals(ptcode))
                    {
                        list2.Add(model);
                    }
                }
            }
            foreach (TreasuryModel model2 in list2)
            {
                TreeNode child = new TreeNode(model2.tname, model2.tcode);
                if ((selectCode != null) && (selectCode == model2.tcode))
                {
                    child.Select();
                }
                treeNodes.Add(child);
                this.ParseTreasuryList(child.ChildNodes, model2.tcode, selectCode);
            }
        }

        public void ParseTreasuryList(TreeView tree, string userCode, bool isLimitPermit, bool disabledRootNode)
        {
            foreach (DataRow row in this.dal.GetTreasury(userCode, isLimitPermit).Rows)
            {
                string str = row["tcode"].ToString();
                string text = row["tname"].ToString();
                string str3 = (row["permit"] == null) ? string.Empty : row["permit"].ToString();
                string str4 = (row["ptcode"] == null) ? string.Empty : row["ptcode"].ToString();
                TreeNode child = new TreeNode(text, str);
                if (isLimitPermit && str3.Equals("0"))
                {
                    child.SelectAction = TreeNodeSelectAction.None;
                    if (!string.IsNullOrEmpty(str4))
                    {
                        child.Text = "<font color='#808080'>" + text + "</font>";
                        child.ToolTip = "无权限";
                    }
                }
                if (string.IsNullOrEmpty(str4))
                {
                    tree.Nodes.Add(child);
                }
                else
                {
                    StringBuilder builder = new StringBuilder("0");
                    for (int i = 0; i < ((str.Length / 3) - 1); i++)
                    {
                        builder.Append("/" + str.Substring(0, 3 * (i + 1)));
                    }
                    tree.FindNode(builder.ToString()).ChildNodes.Add(child);
                }
            }
            if (!disabledRootNode && (tree.Nodes.Count > 0))
            {
                tree.Nodes[0].SelectAction = TreeNodeSelectAction.Select;
            }
            else
            {
                tree.Nodes[0].SelectAction = TreeNodeSelectAction.None;
            }
        }

        public TreasuryModel SetTotalTreasuryByMinTcode()
        {
            return this.dal.SetTotalTreasuryByMinTcode();
        }

        public int Update(TreasuryModel model)
        {
            return this.dal.Update(model);
        }
    }
}

