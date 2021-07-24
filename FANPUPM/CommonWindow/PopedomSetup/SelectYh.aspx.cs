using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_PopedomSetup_SelectYh : BasePage, IRequiresSessionState
{

	protected PrivProjectListAction ppl
	{
		get
		{
			return new PrivProjectListAction();
		}
	}
	public StatAction sa
	{
		get
		{
			return new StatAction();
		}
	}
	protected string BussinessCode
	{
		get
		{
			return this.ViewState["BUSSINESSCODE"].ToString();
		}
		set
		{
			this.ViewState["BUSSINESSCODE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["bc"] != null)
			{
				this.BussinessCode = base.Request["bc"].ToString();
			}
			this.Tree_Bind(this.TVDept.Nodes, "1");
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		DataTable userDT = this.GetUserDT();
		if (userDT.Rows.Count <= 0)
		{
			this.JS.Text = "alert('请选择用户!');";
			return;
		}
		if (this.ppl.Add(userDT) == 1)
		{
			this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
	protected DataTable GetUserDT()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add(new DataColumn("BussinessCode", typeof(string)));
		dataTable.Columns.Add(new DataColumn("UserCode", typeof(string)));
		ArrayList arrayList = (ArrayList)this.Session["YHList"];
		for (int i = 0; i < this.GVUserList.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			CheckBox checkBox = (CheckBox)this.GVUserList.Rows[i].FindControl("CBSelectYh");
			int num = 0;
			if (checkBox.Checked)
			{
				for (int j = 0; j < arrayList.Count; j++)
				{
					if (this.GVUserList.Rows[i].Cells[1].Text == arrayList[j].ToString())
					{
						num = 1;
						break;
					}
				}
				if (num == 0)
				{
					dataRow["BussinessCode"] = this.BussinessCode;
					dataRow["UserCode"] = this.GVUserList.Rows[i].Cells[1].Text;
					dataTable.Rows.Add(dataRow);
				}
			}
		}
		return dataTable;
	}
	private void Tree_Bind(TreeNodeCollection nodes, string sj)
	{
		DataTable bMList = this.sa.GetBMList(this.Session["CorpCode"].ToString(), sj);
		if (bMList.Rows.Count > 0)
		{
			foreach (DataRow dataRow in bMList.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				nodes.Add(treeNode);
				this.Tree_Bind(treeNode.ChildNodes, dataRow["i_bmdm"].ToString());
			}
		}
	}
	protected void TVDept_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.SqlUserList.SelectCommand = "SELECT [V_DLID], [V_DLMM], [V_BGFW], [v_yhdm], [v_xm], [i_xh], [c_sfyx], [i_bmdm], [V_BMMC] FROM [v_UserInfoList] WHERE ([c_sfyx] = 'y') and i_bmdm = '" + this.TVDept.SelectedNode.Value + "'";
		this.GVUserList.DataBind();
	}
	protected void GVUserList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			ArrayList arrayList = (ArrayList)this.Session["YHList"];
			for (int i = 0; i < arrayList.Count; i++)
			{
				if (e.Row.Cells[1].Text == arrayList[i].ToString())
				{
					((CheckBox)e.Row.Cells[0].FindControl("CBSelectYh")).Checked = true;
					return;
				}
			}
		}
	}
}


