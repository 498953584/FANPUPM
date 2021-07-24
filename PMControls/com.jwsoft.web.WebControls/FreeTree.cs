namespace com.jwsoft.web.WebControls
{
    using Microsoft.Web.UI.WebControls;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing.Design;
    using System.Web.UI;
    using System.Web.UI.Design;

    [ToolboxData("<{0}:fillTrees runat=server></{0}:fillTrees>"), DefaultProperty("Text")]
    public class FreeTree : TreeView
    {
        private string _locations = "";
        private string _nodeId;
        private string _nodeText;
        private string _parentNodeId;
        private string _rootNodeCloseImageUrl;
        private string _rootNodeExpandedImageUrl;
        private DataTable _treeDataTable;
        private string _url = "";

        private void AddNode(TreeNode RootNode, DataTable dt)
        {
            if ((dt != null) && (dt.Rows.Count != 0))
            {
                if ((this.ParentId == null) || (this.ParentId == ""))
                {
                    throw new Exception("节点的父ID不能为空");
                }
                if ((this.NodeID == null) || (this.NodeID == ""))
                {
                    throw new Exception("节点的ID不能为空");
                }
                if ((this.NodeText == null) || (this.NodeText == ""))
                {
                    throw new Exception("节点的名称不能为空");
                }
                foreach (DataRow row in dt.Rows)
                {
                    TreeNode tnNode = new TreeNode {
                        Text = row[this.NodeText].ToString(),
                        ID = row[this.NodeID].ToString()
                    };
                    this.SetUrlAndLocation(tnNode, row);
                    try
                    {
                        if ((RootNode == null) && (((row[this.ParentId] == DBNull.Value) || (row[this.ParentId].ToString().Trim() == "")) || (int.Parse(row[this.ParentId].ToString().Trim()) <= 0)))
                        {
                            base.Nodes.Add(tnNode);
                            this.AddNode(tnNode, dt);
                        }
                        else if ((RootNode != null) && (row[this.ParentId].ToString() == RootNode.ID))
                        {
                            RootNode.Nodes.Add(tnNode);
                            this.AddNode(tnNode, dt);
                        }
                    }
                    catch
                    {
                        throw new Exception("转换错误 " + row[this.ParentId].ToString() + "*");
                    }
                }
            }
        }

        public void FillTree()
        {
            try
            {
                if (base.Nodes.Count != 0)
                {
                    if (this.RootNodeExpandedImageUrl != "")
                    {
                        base.Nodes[0].ExpandedImageUrl = this.RootNodeExpandedImageUrl;
                    }
                    if (this.RootNodeCloseImageUrl != "")
                    {
                        base.Nodes[0].ImageUrl = this.RootNodeCloseImageUrl;
                    }
                    this.AddNode(base.Nodes[0], this.TreeDataSource);
                }
                else
                {
                    this.AddNode(null, this.TreeDataSource);
                }
            }
            catch
            {
                throw new Exception("添加根结点出错，请检查数据库字段名以及其中的字段值！");
            }
        }

        protected override void Render(HtmlTextWriter output)
        {
            base.Render(output);
        }

        private void SetUrlAndLocation(TreeNode tnNode, DataRow dr)
        {
            if (this.Url.Trim() != "")
            {
                tnNode.NavigateUrl = this.Url;
            }
            if ((this.Url.Trim() != "") && (this.Locations.Trim() != ""))
            {
                try
                {
                    string[] strArray = this.Locations.Split(new char[] { ',' });
                    if (tnNode.NavigateUrl.IndexOf("?") > 0)
                    {
                        tnNode.NavigateUrl = tnNode.NavigateUrl + "&";
                    }
                    else
                    {
                        tnNode.NavigateUrl = tnNode.NavigateUrl + "?";
                    }
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if ((string.Compare(strArray[i], ",") != 0) && (strArray[i] != ""))
                        {
                            if (i == 0)
                            {
                                tnNode.NavigateUrl = tnNode.NavigateUrl + strArray[i] + "=" + dr[strArray[i]].ToString();
                            }
                            else
                            {
                                string navigateUrl = tnNode.NavigateUrl;
                                tnNode.NavigateUrl = navigateUrl + "&" + strArray[i] + "=" + dr[strArray[i]].ToString();
                            }
                        }
                    }
                }
                catch
                {
                    throw new Exception("指定的链接参数不是数据库表正确的字段名");
                }
            }
        }

        [Category("My Properties"), DefaultValue(""), Description("节点的链接地址所带的参数表（name,id）")]
        public string Locations
        {
            get
            {
                return this._locations;
            }
            set
            {
                this._locations = value.Trim();
            }
        }

        [Description("节点的ID-数据库里的节点名"), DefaultValue(""), Category("My Properties")]
        public string NodeID
        {
            get
            {
                return this._nodeId;
            }
            set
            {
                this._nodeId = value.Trim();
            }
        }

        [Description("节点名称"), Category("My Properties"), DefaultValue("")]
        public string NodeText
        {
            get
            {
                return this._nodeText;
            }
            set
            {
                this._nodeText = value.Trim();
            }
        }

        [Category("My Properties"), DefaultValue(""), Description("节点的父ID-数据库里的节点名")]
        public string ParentId
        {
            get
            {
                return this._parentNodeId;
            }
            set
            {
                this._parentNodeId = value.Trim();
            }
        }

        [Category("My Properties"), Description("根节点闭合时的图片路径"), Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string RootNodeCloseImageUrl
        {
            get
            {
                return this._rootNodeCloseImageUrl;
            }
            set
            {
                this._rootNodeCloseImageUrl = value.Trim();
            }
        }

        [Category("My Properties"), Description("根节点展开时的图片路径"), Editor(typeof(UrlEditor), typeof(UITypeEditor))]
        public string RootNodeExpandedImageUrl
        {
            get
            {
                return this._rootNodeExpandedImageUrl;
            }
            set
            {
                this._rootNodeExpandedImageUrl = value.Trim();
            }
        }

        [Category("My Properties"), Description("数据源")]
        public DataTable TreeDataSource
        {
            get
            {
                return this._treeDataTable;
            }
            set
            {
                this._treeDataTable = value;
            }
        }

        [Description("节点的链接地址"), DefaultValue(""), Category("My Properties")]
        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value.Trim();
            }
        }
    }
}

