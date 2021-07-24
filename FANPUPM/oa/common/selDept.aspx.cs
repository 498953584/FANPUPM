using ASP;
using cn.justwin.BLL;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class selDept : BasePage, IRequiresSessionState
	{
		private string cDept = string.Empty;
		private IList<string> bmdmList = new List<string>();
		protected static DeptManageDb DeptAct
		{
			get
			{
				return new DeptManageDb();
			}
		}

		protected override void OnInit(EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["dept"]))
			{
				this.cDept = "," + base.Request["dept"] + ",";
			}
			if (!string.IsNullOrEmpty(base.Request["bmdmJson"]))
			{
				this.bmdmList = JsonHelper.JsonDeserialize<string[]>(base.Request["bmdmJson"]);
			}
			base.OnInit(e);
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				new userManageDb();
				this.tvDepartment_AppendNode(this.tvDepartment.Nodes, 0, true);
			}
		}
		private void tvDepartment_AppendNode(TreeNodeCollection nodes, int parentID, bool Enabled)
		{
			DataTable subDepartment = selDept.DeptAct.GetSubDepartment(parentID);
			int count = subDepartment.Rows.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					DataRow dataRow = subDepartment.Rows[i];
					TreeNode treeNode = new TreeNode();
					treeNode.Value = dataRow["v_bmbm"].ToString();
					treeNode.Text = dataRow["v_bmmc"].ToString();
					treeNode.ToolTip = dataRow["i_bmdm"].ToString();
					treeNode.NavigateUrl = "#";
					string value = "," + dataRow["v_bmbm"].ToString() + ",";
					if (this.cDept.IndexOf(value) >= 0 || this.bmdmList.Contains(dataRow["i_bmdm"].ToString()))
					{
						treeNode.Checked = true;
						this.tvDepartment_AppendNode(treeNode.ChildNodes, Convert.ToInt32(dataRow["i_bmdm"].ToString()), false);
					}
					else
					{
						this.tvDepartment_AppendNode(treeNode.ChildNodes, Convert.ToInt32(dataRow["i_bmdm"].ToString()), true);
					}
					nodes.Add(treeNode);
				}
			}
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				string text = string.Empty;
				string text2 = string.Empty;
				string arg_11_0 = string.Empty;
				List<string> list = new List<string>();
				List<string> list2 = new List<string>();
				foreach (TreeNode treeNode in this.tvDepartment.CheckedNodes)
				{
					text = text + treeNode.Value + ",";
					text2 = text2 + treeNode.Text + ",";
					list.Add(treeNode.Value);
					list2.Add(treeNode.ToolTip);
				}
				text = text.Substring(0, text.Length - 1);
				text2 = text2.Substring(0, text2.Length - 1);
				string text3 = JsonHelper.JsonSerializer<string[]>(list.ToArray());
				string text4 = JsonHelper.JsonSerializer<string[]>(list2.ToArray());
				base.RegisterScript(string.Concat(new string[]
				{
					"closeWin('",
					text,
					"', '",
					text2,
					"', '",
					text3,
					"', '",
					text4,
					"');"
				}));
			}
			catch
			{
				base.RegisterScript("cancel();");
			}
		}
	}


