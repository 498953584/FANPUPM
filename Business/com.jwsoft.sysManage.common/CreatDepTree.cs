namespace com.jwsoft.sysManage.common
{
    using com.jwsoft.pm.entpm.action;
    using System;
    using System.Data;
    using System.Web.UI.WebControls;

    public class CreatDepTree
    {
        private bool blnEnabledLink;
        protected DeptManageDb dmd = new DeptManageDb();
        private string strNavigationURL;
        private string strTarget;
        private TreeNodeCollection tncNodes;
        protected userManageDb umd = new userManageDb();

        public CreatDepTree(TreeNodeCollection Nodes)
        {
            this.tncNodes = Nodes;
        }

        public void BuildChildTree(TreeNodeCollection Nodes, DeptManageDb DepManage, int DepCode, string UserCode, string flag)
        {
            DataTable subDepartment = this.dmd.GetSubDepartment(DepCode);
            for (int i = 0; i < subDepartment.Rows.Count; i++)
            {
                TreeNode child = new TreeNode();
                int depCode = int.Parse(subDepartment.Rows[i]["i_bmdm"].ToString());
                this.BuildChildTree(child.ChildNodes, DepManage, depCode, UserCode, "y");
                child.Value = subDepartment.Rows[i]["i_bmdm"].ToString();
                child.Text = subDepartment.Rows[i]["v_bmmc"].ToString();
                if (this.blnEnabledLink)
                {
                    child.Target = this.strTarget;
                    child.NavigateUrl = this.strNavigationURL + "?code=" + depCode.ToString();
                }
                if (flag == "y")
                {
                    Nodes.Add(child);
                }
                else if (flag == "n")
                {
                    string[] strArray = this.umd.manageDept(UserCode).Split(new char[] { ',' });
                    for (int j = 0; j < strArray.Length; j++)
                    {
                        if (depCode == Convert.ToInt32(strArray[j].ToString()))
                        {
                            Nodes.Add(child);
                        }
                    }
                }
            }
        }

        public void BuildTreeView(string UserCode)
        {
            int depCode = this.umd.getUserParentDept(UserCode);
            if (depCode != 0)
            {
                TreeNode child = new TreeNode {
                    Text = this.umd.depName(depCode.ToString()),
                    ImageUrl = "img/root.gif",
                    NavigateUrl = ""
                };
                this.Nodes.Add(child);
                this.BuildChildTree(child.ChildNodes, this.dmd, depCode, UserCode, "n");
            }
            else
            {
                this.BuildChildTree(this.Nodes, this.dmd, depCode, UserCode, "y");
            }
        }

        public void BuildTreeView(string UserCode, int iDeptCode)
        {
            if (iDeptCode == 0)
            {
                this.BuildChildTree(this.Nodes, this.dmd, iDeptCode, UserCode, "y");
            }
            else
            {
                TreeNode child = new TreeNode {
                    Text = this.umd.depName(iDeptCode.ToString()),
                    Target = this.strTarget,
                    NavigateUrl = this.strNavigationURL + "?code=" + iDeptCode.ToString()
                };
                this.Nodes.Add(child);
                this.BuildChildTree(child.ChildNodes, this.dmd, iDeptCode, UserCode, "y");
            }
        }

        public bool EnabledLink
        {
            get
            {
                return this.blnEnabledLink;
            }
            set
            {
                if (this.blnEnabledLink != value)
                {
                    this.blnEnabledLink = value;
                }
            }
        }

        public string NavigationURL
        {
            get
            {
                return this.strNavigationURL;
            }
            set
            {
                if (this.strNavigationURL != value)
                {
                    this.strNavigationURL = value;
                }
            }
        }

        public TreeNodeCollection Nodes
        {
            get
            {
                return this.tncNodes;
            }
            set
            {
                if (this.tncNodes != value)
                {
                    this.tncNodes = value;
                }
            }
        }

        public string Target
        {
            get
            {
                return this.strTarget;
            }
            set
            {
                if (this.strTarget != value)
                {
                    this.strTarget = value;
                }
            }
        }
    }
}

