using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_TreasuryPermit_PermitList : NBasePage, IRequiresSessionState
{
	private PtYhmcBll ptYhmcBll = new PtYhmcBll();
	private TreasuryPermitBll treasuryPermitBll = new TreasuryPermitBll();
	private PtdutyBll ptdutyBll = new PtdutyBll();
	protected string SubPrjGuid = "";
	private PTbdmBll ptdbmBll = new PTbdmBll();
	private PTDRoleBll pTDRoleBll = new PTDRoleBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.BindTree();
			this.ViewState["where"] = " where  i_bmdm=" + this.TvBranch.SelectedValue;
			this.ViewState["type"] = "0";
			this.BindTreasury();
			this.BindMsg();
			this.BindGv();
		}
	}
	public void BindMsg()
	{
		this.lblUserList.Text = "";
		this.lblBranchList.Text = "";
		this.lblPostList.Text = "";
		this.hdUserList.Value = "";
		this.hdBranchList.Value = "";
		this.hdPostList.Value = "";
		this.hdBranchName.Value = "";
		string str = (this.tvTreasury.SelectedValue == string.Empty) ? "0" : this.tvTreasury.SelectedValue;
		IList<TreasuryPermit> allTreasuryPermitByWhere = this.treasuryPermitBll.GetAllTreasuryPermitByWhere(" where tcode = '" + str + "' ");
		foreach (TreasuryPermit current in allTreasuryPermitByWhere)
		{
			if (current.ptype == SmEnum.PermitType.Person.ToString() && current.tcode == this.tvTreasury.SelectedValue)
			{
				PtYhmc modelById = this.ptYhmcBll.GetModelById(current.pcode);
				if (modelById != null && !this.hdUserList.Value.Contains(modelById.v_yhdm))
				{
					HiddenField expr_137 = this.hdUserList;
					expr_137.Value = expr_137.Value + modelById.v_yhdm + ",";
					Label expr_158 = this.lblUserList;
					string text = expr_158.Text;
					expr_158.Text = string.Concat(new string[]
					{
						text,
						modelById.v_xm,
						"<span style=' cursor:pointer;' onclick=\"delUser('",
						modelById.v_yhdm,
						"','hdUserList','lblUserList')\">×</span>,&nbsp;&nbsp;&nbsp;"
					});
				}
			}
			if (current.ptype == SmEnum.PermitType.Department.ToString() && current.tcode == this.tvTreasury.SelectedValue)
			{
				PTbdm pTbdmById = this.ptdbmBll.GetPTbdmById(Convert.ToInt32(current.pcode));
				if (pTbdmById != null && !this.hdBranchList.Value.Contains(Convert.ToString(pTbdmById.i_bmdm)))
				{
					HiddenField expr_21C = this.hdBranchList;
					expr_21C.Value = expr_21C.Value + pTbdmById.i_bmdm + ",";
					Label expr_243 = this.lblBranchList;
					object text2 = expr_243.Text;
					expr_243.Text = string.Concat(new object[]
					{
						text2,
						pTbdmById.V_BMMC,
						"<span style=' cursor:pointer;' onclick=\"delUser('",
						pTbdmById.i_bmdm,
						"','hdBranchList','lblBranchList')\">×</span>,&nbsp;&nbsp;&nbsp;"
					});
					HiddenField expr_298 = this.hdBranchName;
					object value = expr_298.Value;
					expr_298.Value = string.Concat(new object[]
					{
						value,
						pTbdmById.V_BMMC,
						"<span style=' cursor:pointer;' onclick=\"delUser('",
						pTbdmById.i_bmdm,
						"','hdBranchList','lblBranchList')\">×</span>,&nbsp;&nbsp;&nbsp;"
					});
				}
			}
			if (current.ptype == SmEnum.PermitType.Post.ToString() && current.tcode == this.tvTreasury.SelectedValue)
			{
				Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(current.pcode));
				if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
				{
					HiddenField expr_359 = this.hdPostList;
					expr_359.Value = expr_359.Value + ptDutyById.I_DUTYID + ",";
					Label expr_380 = this.lblPostList;
					object text3 = expr_380.Text;
					expr_380.Text = string.Concat(new object[]
					{
						text3,
						ptDutyById.DutyName,
						"<span style=' cursor:pointer;' onclick=\"delUser('",
						ptDutyById.I_DUTYID,
						"','hdPostList','lblPostList')\">×</span>,&nbsp;&nbsp;&nbsp;"
					});
				}
			}
		}
	}
	protected void SelectBranch(TreeNode Node)
	{
		string text = " where ptype='" + this.ddlType.SelectedValue + "'";
		if (this.tvTreasury.SelectedValue != "")
		{
			text = text + " and tcode=" + this.tvTreasury.SelectedValue;
		}
		if (this.ViewState["type"] == "0")
		{
			IList<TreasuryPermit> allTreasuryPermitByWhere = this.treasuryPermitBll.GetAllTreasuryPermitByWhere(text);
			using (IEnumerator<TreasuryPermit> enumerator = allTreasuryPermitByWhere.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TreasuryPermit current = enumerator.Current;
					if (current.pcode == Node.Value)
					{
						Node.Checked = true;
					}
				}
				return;
			}
		}
		if (this.hdBranchList.Value.Contains(Node.Value))
		{
			Node.Checked = true;
		}
	}
	protected void BindGv()
	{
		this.lblBranchList.Text = this.hdBranchName.Value;
		if (this.ddlType.SelectedValue == SmEnum.PermitType.Person.ToString())
		{
			this.tdGv.Visible = true;
			this.gvPost.Visible = false;
			this.gvBranch.Visible = true;
			this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
			this.gvBranch.DataSource = this.ptYhmcBll.GetAllModelByWhere(this.ViewState["where"].ToString() + " and state='1'");
			this.gvBranch.DataBind();
			IEnumerator enumerator = this.gvBranch.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
					CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
					if (this.hdUserList.Value.Contains(checkBox.ToolTip))
					{
						checkBox.Checked = true;
					}
				}
				return;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		if (this.ddlType.SelectedValue == SmEnum.PermitType.Post.ToString())
		{
			this.tdGv.Visible = true;
			this.gvPost.Visible = true;
			this.gvBranch.Visible = false;
			this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
			IList<Ptduty> allPtdutyByWhere = this.ptdutyBll.GetAllPtdutyByWhere(this.ViewState["where"].ToString());
			this.gvPost.DataSource = allPtdutyByWhere;
			this.gvPost.DataBind();
			IEnumerator enumerator2 = this.gvPost.Rows.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					GridViewRow gridViewRow2 = (GridViewRow)enumerator2.Current;
					CheckBox checkBox2 = gridViewRow2.FindControl("cbPost") as CheckBox;
					if (this.hdPostList.Value.Contains(checkBox2.ToolTip))
					{
						checkBox2.Checked = true;
					}
				}
				return;
			}
			finally
			{
				IDisposable disposable2 = enumerator2 as IDisposable;
				if (disposable2 != null)
				{
					disposable2.Dispose();
				}
			}
		}
		if (this.ddlType.SelectedValue == SmEnum.PermitType.Department.ToString())
		{
			this.TvBranch.ShowCheckBoxes = TreeNodeTypes.All;
			this.TvBranch.ExpandDepth = 1;
			this.gvPost.Visible = false;
			this.gvBranch.Visible = false;
			this.tdGv.Visible = false;
			return;
		}
		this.gvPost.Visible = false;
		this.gvBranch.Visible = false;
		this.TvBranch.ShowCheckBoxes = TreeNodeTypes.None;
	}
	protected void BindTree()
	{
		this.TvBranch.Nodes.Clear();
		IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere(" where i_sjdm=0 ");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			this.SelectBranch(treeNode);
			treeNode.Selected = true;
			this.AddChildNodes(treeNode);
			this.TvBranch.Nodes.Add(treeNode);
		}
	}
	protected void AddChildNodes(TreeNode node)
	{
		IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere("where i_sjdm='" + node.Value + "'");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			this.SelectBranch(treeNode);
			node.ChildNodes.Add(treeNode);
			this.AddChildNodes(treeNode);
		}
	}
	protected void cbBox_CheckedChanged(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvBranch.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("cbBox") as CheckBox;
			PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox.ToolTip);
			if (checkBox.Checked)
			{
				if (!this.hdUserList.Value.Contains(modelById.v_yhdm))
				{
					HiddenField expr_71 = this.hdUserList;
					expr_71.Value = expr_71.Value + modelById.v_yhdm + ",";
					Label expr_92 = this.lblUserList;
					string text = expr_92.Text;
					expr_92.Text = string.Concat(new string[]
					{
						text,
						modelById.v_xm,
						"<span style=' cursor:pointer;' onclick=\"delUser('",
						modelById.v_yhdm,
						"','hdUserList','lblUserList')\">×</span>,&nbsp;&nbsp;&nbsp;"
					});
				}
			}
			else
			{
				if (this.hdUserList.Value.Contains(modelById.v_yhdm))
				{
					this.RemoveMsg(this.hdUserList, this.lblUserList, modelById.v_yhdm);
				}
			}
		}
	}
	protected void cbAllBox_CheckedChanged(object sender, EventArgs e)
	{
		CheckBox checkBox = this.gvBranch.HeaderRow.FindControl("cbAllBox") as CheckBox;
		if (checkBox.Checked)
		{
			IEnumerator enumerator = this.gvBranch.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
					CheckBox checkBox2 = gridViewRow.FindControl("cbBox") as CheckBox;
					PtYhmc modelById = this.ptYhmcBll.GetModelById(checkBox2.ToolTip);
					checkBox2.Checked = true;
					if (!this.hdUserList.Value.Contains(modelById.v_yhdm))
					{
						HiddenField expr_92 = this.hdUserList;
						expr_92.Value = expr_92.Value + modelById.v_yhdm + ",";
						Label expr_B3 = this.lblUserList;
						string text = expr_B3.Text;
						expr_B3.Text = string.Concat(new string[]
						{
							text,
							modelById.v_xm,
							"<span style=' cursor:pointer;' onclick=\"delUser('",
							modelById.v_yhdm,
							"','hdUserList','lblUserList')\">×</span>,&nbsp;&nbsp;&nbsp;"
						});
					}
				}
				return;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		foreach (GridViewRow gridViewRow2 in this.gvBranch.Rows)
		{
			CheckBox checkBox3 = gridViewRow2.FindControl("cbBox") as CheckBox;
			PtYhmc modelById2 = this.ptYhmcBll.GetModelById(checkBox3.ToolTip);
			checkBox3.Checked = false;
			if (this.hdUserList.Value.Contains(modelById2.v_yhdm))
			{
				this.RemoveMsg(this.hdUserList, this.lblUserList, modelById2.v_yhdm);
			}
		}
	}
	public void RemoveMsg(HiddenField hdList, Label lbList, string yhdm)
	{
		string[] array = hdList.Value.Split(new char[]
		{
			','
		});
		string[] array2 = lbList.Text.Replace(">,", "|").Split(new char[]
		{
			'|'
		});
		string[] array3 = array;
		for (int i = 0; i < array3.Length; i++)
		{
			string text = array3[i];
			if (text == yhdm)
			{
				hdList.Value = hdList.Value.Replace(yhdm + ",", "");
				string[] array4 = array2;
				for (int j = 0; j < array4.Length; j++)
				{
					string text2 = array4[j];
					if (text2.Contains(text))
					{
						lbList.Text = lbList.Text.Replace(text2 + ">,", "");
					}
				}
			}
		}
		this.BindGv();
	}
	public void RemoveBranchMsg(HiddenField hdList, Label lbList, string yhdm)
	{
		string[] array = hdList.Value.Split(new char[]
		{
			','
		});
		string[] array2 = this.hdBranchName.Value.Replace(">,", "|").Split(new char[]
		{
			'|'
		});
		string[] array3 = array;
		for (int i = 0; i < array3.Length; i++)
		{
			string text = array3[i];
			if (text == yhdm)
			{
				hdList.Value = hdList.Value.Replace(yhdm + ",", "");
				string[] array4 = array2;
				for (int j = 0; j < array4.Length; j++)
				{
					string text2 = array4[j];
					if (text2.Contains(text))
					{
						lbList.Text = this.hdBranchName.Value.Replace(text2 + ">,", "");
						this.hdBranchName.Value = this.hdBranchName.Value.Replace(text2 + ">,", "");
					}
				}
			}
		}
		this.ViewState["type"] = "1";
		this.BindTree();
	}
	protected void cbBox_CheckedChanged2(object sender, EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvPost.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("cbPost") as CheckBox;
			Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox.ToolTip));
			if (checkBox.Checked)
			{
				if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
				{
					HiddenField expr_7B = this.hdPostList;
					expr_7B.Value = expr_7B.Value + ptDutyById.I_DUTYID + ",";
					Label expr_A1 = this.lblPostList;
					object text = expr_A1.Text;
					expr_A1.Text = string.Concat(new object[]
					{
						text,
						ptDutyById.DutyName,
						"<span style=' cursor:pointer;' onclick=\"delUser('",
						ptDutyById.I_DUTYID,
						"','hdPostList','lblPostList')\">×</span>,&nbsp;&nbsp;&nbsp;"
					});
				}
			}
			else
			{
				if (this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
				{
					this.RemoveMsg(this.hdPostList, this.lblPostList, ptDutyById.I_DUTYID.ToString());
				}
			}
		}
	}
	protected void cbAllBoxPost_CheckedChanged(object sender, EventArgs e)
	{
		CheckBox checkBox = this.gvPost.HeaderRow.FindControl("cbAllBoxPost") as CheckBox;
		if (checkBox.Checked)
		{
			IEnumerator enumerator = this.gvPost.Rows.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
					CheckBox checkBox2 = gridViewRow.FindControl("cbPost") as CheckBox;
					Ptduty ptDutyById = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox2.ToolTip));
					checkBox2.Checked = true;
					if (!this.hdPostList.Value.Contains(Convert.ToString(ptDutyById.I_DUTYID)))
					{
						HiddenField expr_9C = this.hdPostList;
						expr_9C.Value = expr_9C.Value + ptDutyById.I_DUTYID + ",";
						Label expr_C2 = this.lblPostList;
						object text = expr_C2.Text;
						expr_C2.Text = string.Concat(new object[]
						{
							text,
							ptDutyById.DutyName,
							"<span style=' cursor:pointer;' onclick=\"delUser('",
							ptDutyById.I_DUTYID,
							"','hdPostList','lblPostList')\">×</span>,&nbsp;&nbsp;&nbsp;"
						});
					}
				}
				return;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}
		foreach (GridViewRow gridViewRow2 in this.gvPost.Rows)
		{
			CheckBox checkBox3 = gridViewRow2.FindControl("cbPost") as CheckBox;
			Ptduty ptDutyById2 = this.ptdutyBll.GetPtDutyById(Convert.ToInt32(checkBox3.ToolTip));
			checkBox3.Checked = false;
			if (this.hdPostList.Value.Contains(Convert.ToString(ptDutyById2.I_DUTYID)))
			{
				this.RemoveMsg(this.hdPostList, this.lblPostList, ptDutyById2.I_DUTYID.ToString());
			}
		}
	}
	protected void TvBranch_SelectedNodeChanged(object sender, EventArgs e)
	{
		if (this.ddlType.SelectedValue != Convert.ToString(SmEnum.PermitType.Department))
		{
			this.ViewState["where"] = " where  i_bmdm=" + this.TvBranch.SelectedValue;
			this.BindGv();
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (this.tvTreasury.SelectedValue == "")
		{
			base.RegisterScript("top.ui.alert('请选择仓库!');");
			return;
		}
		this.treasuryPermitBll.DelTreasuryPermitByTcode(this.tvTreasury.SelectedValue);
		string[] uList = this.hdBranchList.Value.Split(new char[]
		{
			','
		});
		bool flag = this.AddPermit(uList, SmEnum.PermitType.Department.ToString());
		string[] uList2 = this.hdUserList.Value.Split(new char[]
		{
			','
		});
		bool flag2 = this.AddPermit(uList2, SmEnum.PermitType.Person.ToString());
		string[] uList3 = this.hdPostList.Value.Split(new char[]
		{
			','
		});
		bool flag3 = this.AddPermit(uList3, SmEnum.PermitType.Post.ToString());
		if (flag && flag3 && flag2)
		{
			base.RegisterScript("top.ui.alert('设置成功！');location='PermitList.aspx'");
		}
	}
	public bool AddPermit(string[] uList, string type)
	{
		bool result = true;
		for (int i = 0; i < uList.Length; i++)
		{
			string text = uList[i];
			if (!string.IsNullOrEmpty(text))
			{
				TreasuryPermit treasuryPermit = new TreasuryPermit();
				treasuryPermit.tpid = Guid.NewGuid().ToString();
				treasuryPermit.tcode = this.tvTreasury.SelectedValue;
				treasuryPermit.ptype = type;
				treasuryPermit.pcode = text;
				if (this.treasuryPermitBll.AddTreasuryPermit(treasuryPermit) == 0)
				{
					result = false;
				}
			}
		}
		return result;
	}
	public void BindTreasury()
	{
		Treasury treasury = new Treasury();
		treasury.ParseTreasuryList(this.tvTreasury, string.Empty, false, true);
	}
	protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.BindGv();
		this.ViewState["type"] = "1";
		this.BindTree();
	}
	public string GetDutyName(string id)
	{
		PTDRole model = this.pTDRoleBll.GetModel(id);
		return model.RoleTypeName;
	}
	protected void tvTreasury_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.BindTree();
		this.BindMsg();
		this.BindGv();
	}
	protected void btnRe_Click(object sender, EventArgs e)
	{
		if (this.hdType.Value == "hdUserList")
		{
			this.RemoveMsg(this.hdUserList, this.lblUserList, this.hdDelId.Value);
		}
		if (this.hdType.Value == "hdPostList")
		{
			this.RemoveMsg(this.hdPostList, this.lblPostList, this.hdDelId.Value);
		}
		if (this.hdType.Value == "hdBranchList")
		{
			this.RemoveBranchMsg(this.hdBranchList, this.lblBranchList, this.hdDelId.Value);
		}
	}
}


