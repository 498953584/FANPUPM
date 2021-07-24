using ASP;
using cn.justwin.BLL;
using cn.justwin.BLL.Privilege;
using cn.justwin.BLL.ProgressManagement;
using cn.justwin.contractDAL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectAllUser : NBasePage, IRequiresSessionState
{
	private string method = string.Empty;
	private string ids = string.Empty;
	private static System.Collections.Generic.List<string> userCodes;
	private PtYhmcBll ptYhmcBll = new PtYhmcBll();
	private ContractType contractType = new ContractType();
	private PTbdmBll ptdbmBll = new PTbdmBll();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Method"]))
		{
			this.method = base.Request["Method"];
		}
		if (!string.IsNullOrEmpty(base.Request["ids"]))
		{
			this.ids = base.Request["ids"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
			if (base.Request["showSel"] == "true")
			{
				this.bindSelUser();
			}
			if (!string.IsNullOrEmpty(this.ids))
			{
				System.Collections.Generic.List<string> list = (
					from s in this.ids.Split(new char[]
					{
						','
					})
					where s.Length == 8
					select s).ToList<string>();
				this.hdId.Value = string.Join(",", list.ToArray()) + ",";
				System.Collections.Generic.List<string> names = this.ptYhmcBll.GetNames(list);
				this.hdName.Value = string.Join(",", names.ToArray()) + ",";
				this.AddUsers(list);
			}
		}
	}
	protected void bindSelUser()
	{
		string a = base.Request["showType"];
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (a == "1")
		{
			string id = base.Request["showId"];
			FileInfoBll fileInfoBll = new FileInfoBll();
			FileInfoModel model = fileInfoBll.GetModel(id);
			if (model != null)
			{
				list = this.getUserCodes(model.UserCodes);
			}
		}
		else
		{
			if (a == "2")
			{
				string id2 = base.Request["showId"];
				PersonalFileBll personalFileBll = new PersonalFileBll();
				PersonalFileModel model2 = personalFileBll.GetModel(id2);
				if (model2 != null)
				{
					list = this.getUserCodes(model2.ShareUsers);
					this.hfldNoSelCodes.Value = "00000000," + model2.FileOwner;
				}
			}
			else
			{
				if (a == "3")
				{
					string progressId = base.Request["progressId"];
					list = cn.justwin.BLL.ProgressManagement.Privilege.GetUserCodes(progressId);
				}
				else
				{
					list = cn.justwin.BLL.Privilege.Privilege.GetUserCodes(base.Request["RelationsTable"], base.Request["RelationsKey"]);
				}
			}
		}
		this.hdId.Value = string.Join(",", list.ToArray()) + ",";
		this.AddUsers(list);
	}
	protected System.Collections.Generic.List<string> getUserCodes(string userCodes)
	{
		System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
		if (!string.IsNullOrEmpty(userCodes) && userCodes.Length > 1)
		{
			userCodes = userCodes.Replace("\"", "");
			userCodes = userCodes.Substring(1, userCodes.Length - 2);
			result = new System.Collections.Generic.List<string>(userCodes.Split(new char[]
			{
				','
			}));
		}
		return result;
	}
	protected void AddUsers(System.Collections.Generic.List<string> userCodes)
	{
		if (userCodes != null)
		{
			PtYhmcBll ptYhmcBll = new PtYhmcBll();
			System.Collections.Generic.List<string> names = ptYhmcBll.GetNames(userCodes);
			for (int i = 0; i < names.Count; i++)
			{
				if (!this.hdName.Value.Contains(names[i]))
				{
					this.lbUser.Items.Add(new ListItem(names[i], userCodes[i]));
				}
			}
		}
	}
	protected void BindTree()
	{
		this.TvBranch.Nodes.Clear();
		System.Collections.Generic.IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere(" where i_sjdm=0 ");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			treeNode.Selected = true;
			this.AddChildNodes(treeNode);
			this.TvBranch.Nodes.Add(treeNode);
		}
	}
	protected void AddChildNodes(TreeNode node)
	{
		System.Collections.Generic.IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere("where i_sjdm='" + node.Value + "'");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			node.ChildNodes.Add(treeNode);
			this.AddChildNodes(treeNode);
		}
	}
	protected void trvwDepartment_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		string selectedValue = this.TvBranch.SelectedValue;
		if (base.Request.QueryString["Type"] == "Sms")
		{
			this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} and state=1 and MobilePhoneCode !=''", selectedValue));
			this.lbSelect.DataValueField = "MobilePhoneCode";
			this.lbSelect.DataBind();
			return;
		}
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} and state=1", selectedValue));
		this.lbSelect.DataValueField = "v_yhdm";
		this.lbSelect.DataBind();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.method))
		{
			base.RegisterScript("setVal('" + this.method + "')");
		}
		string text = base.Request["RelationsTable"];
		string text2 = base.Request["RelationsKey"];
		if (!string.IsNullOrEmpty(text2) && !string.IsNullOrEmpty(text))
		{
			string value = this.hdId.Value;
			if (value.Length > 1)
			{
				Common_DivSelectAllUser.userCodes = new System.Collections.Generic.List<string>(value.Split(new char[]
				{
					','
				}));
				Common_DivSelectAllUser.userCodes.RemoveAll((string a) => a.Length != 8);
			}
			else
			{
				Common_DivSelectAllUser.userCodes = null;
			}
			cn.justwin.BLL.Privilege.Privilege.Add(Common_DivSelectAllUser.userCodes, text, text2);
		}
		base.RegisterScript("divClose(parent);");
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		if (base.Request.QueryString["Type"] == "Sms")
		{
			this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where v_xm like '%{0}%' and state=1 and MobilePhoneCode !=''", this.txtQuery.Text.Trim()));
			this.lbSelect.DataValueField = "MobilePhoneCode";
			this.lbUser.DataValueField = "MobilePhoneCode";
			this.lbUser.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where MobilePhoneCode in({0})", StringUtility.GetArrayToInStr(this.hdId.Value.Split(new char[]
			{
				','
			}))));
			this.lbSelect.DataBind();
			return;
		}
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where v_xm like '%{0}%' and state=1 ", this.txtQuery.Text.Trim()));
		this.lbSelect.DataValueField = "v_yhdm";
		this.lbUser.DataValueField = "v_yhdm";
		this.lbUser.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where v_yhdm in({0})", StringUtility.GetArrayToInStr(this.hdId.Value.Split(new char[]
		{
			','
		}))));
		this.lbSelect.DataBind();
	}
}


