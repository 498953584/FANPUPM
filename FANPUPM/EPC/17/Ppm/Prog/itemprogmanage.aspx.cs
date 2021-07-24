using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.epm.datum;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ItemProgManage : NBasePage, IRequiresSessionState
	{
		private string pk = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Textbox_cfrq.Text = DateTime.Now.ToShortDateString().Replace("/", "-");
				DataTable allProgSortCollections = ProgSortAction.GetAllProgSortCollections();
				this.DropDownList_lb.DataSource = allProgSortCollections;
				this.DropDownList_lb.DataTextField = "ProgSortName";
				this.DropDownList_lb.DataValueField = "ProgSortCode";
				this.DropDownList_lb.DataBind();
				if (base.Request.Params["Type"] != null && base.Request.Params["Type"].ToString() == "View")
				{
					this.Button_save.Visible = false;
					this.ButtonColse.Style.Add("display", "");
					this.ButtonColse.Value = "关 闭";
					this.TextBox_cfdw.ReadOnly = true;
					this.TextBox_bcfdx.ReadOnly = true;
					this.Textbox_cfje.ReadOnly = true;
					this.Textbox_fzr.ReadOnly = true;
					this.TextBox_cfyj.ReadOnly = true;
					this.TextBox_cfyy.ReadOnly = true;
					this.Textbox_bz.ReadOnly = true;
					this.Textbox_cfrq.ReadOnly = true;
					this.Textbox_cfdkcdw.ReadOnly = true;
					this.DropDownList_lb.Enabled = false;
				}
				if (base.Request["pk"] != null)
				{
					this.pk = base.Request["pk"].ToString();
				}
				if (string.IsNullOrWhiteSpace(this.pk) && base.Request["ic"] != null)
				{
					this.pk = ItemProgAction.GetIdByUID(base.Request["ic"]);
				}
				if (!string.IsNullOrWhiteSpace(this.pk))
				{
					this.bind(this.pk);
				}
			}
		}
		private void bind(string pk)
		{
			ItemProgInfo itemProgInfo = ItemProgAction.GetItemProgInfo(pk);
			this.TextBox_bcfdx.Text = itemProgInfo.ByProgObject;
			this.Textbox_bz.Text = itemProgInfo.Remark;
			this.TextBox_cfdw.Text = itemProgInfo.ProgUnit;
			this.Textbox_cfje.Text = itemProgInfo.ProgMoney.ToString();
			this.Textbox_cfrq.Text = itemProgInfo.ProgDate.ToShortDateString().Replace("/", "-");
			this.TextBox_cfyj.Text = itemProgInfo.ProgGist;
			this.TextBox_cfyy.Text = itemProgInfo.ProgCause;
			this.Textbox_fzr.Text = itemProgInfo.Principal;
			this.Textbox_cfdkcdw.Text = itemProgInfo.ProgBurstunit;
			this.DropDownList_lb.SelectedValue = itemProgInfo.ProgSortCode.ToString();
			string sqlString = string.Empty;
			sqlString = "SELECT mark,filesType FROM Prj_ItemProg WHERE ID=" + itemProgInfo.ID;
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable != null && dataTable.Rows.Count > 0 && dataTable.Rows[0]["mark"] != null)
			{
				if (dataTable.Rows[0]["mark"].ToString() != "2")
				{
					this.cbkmark.Checked = true;
					if (dataTable.Rows[0]["filesType"] != null)
					{
						this.hidenClass.Value = dataTable.Rows[0]["filesType"].ToString();
					}
				}
				else
				{
					this.cbkmark.Checked = false;
				}
				this.hdnmark.Value = dataTable.Rows[0]["mark"].ToString();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			if (!string.IsNullOrEmpty(base.Request["pk"]))
			{
				this.pk = base.Request["pk"].ToString();
			}
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
			base.ClientScript.RegisterStartupScript(base.GetType(), "script", "init_Info();", true);
		}
		protected void Button_save_Click(object sender, EventArgs e)
		{
			if (this.TextBox_cfyj.Text.Length > this.TextBox_cfyj.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('奖罚依据输入内容过长！')";
				return;
			}
			if (this.TextBox_cfyy.Text.Length > this.TextBox_cfyy.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('奖罚原因输入内容过长！')";
				return;
			}
			if (this.Textbox_bz.Text.Length > this.Textbox_bz.MaxLength)
			{
				this.JavaScriptControl1.Text = "alert('备注输入内容过长！')";
				return;
			}
			ItemProgInfo itemProgInfo = new ItemProgInfo();
			if (!string.IsNullOrWhiteSpace(this.pk))
			{
				itemProgInfo.ID = int.Parse(this.pk);
			}
			itemProgInfo.ApproveResult = 0;
			itemProgInfo.ByProgObject = this.TextBox_bcfdx.Text.Trim();
			itemProgInfo.Remark = this.Textbox_bz.Text.Trim();
			itemProgInfo.ProgUnit = this.TextBox_cfdw.Text;
			if (this.Textbox_cfje.Text.Trim() != "")
			{
				itemProgInfo.ProgMoney = decimal.Parse(this.Textbox_cfje.Text.Trim());
			}
			if (this.Textbox_cfrq.Text.Trim() != "")
			{
				itemProgInfo.ProgDate = DateTime.Parse(this.Textbox_cfrq.Text.Trim());
			}
			itemProgInfo.ProgGist = this.TextBox_cfyj.Text.Trim();
			itemProgInfo.ProgCause = this.TextBox_cfyy.Text.Trim();
			itemProgInfo.Principal = this.Textbox_fzr.Text.Trim();
			itemProgInfo.ProgBurstunit = this.Textbox_cfdkcdw.Text.Trim();
			if (base.Request["Levels"] != null)
			{
				itemProgInfo.ProgSign = int.Parse(base.Request["Levels"].ToString());
			}
			if (base.Request["Prjcode"] != null)
			{
				itemProgInfo.PrjCode = base.Request["Prjcode"].ToString();
			}
			if (this.DropDownList_lb.SelectedValue != "")
			{
				itemProgInfo.ProgSortCode = int.Parse(this.DropDownList_lb.SelectedValue);
			}
			string sqlString = string.Empty;
			bool flag;
			if (string.IsNullOrWhiteSpace(this.pk))
			{
				flag = ItemProgAction.ItemProgInfoOp(itemProgInfo, "Insert");
				DatumLogic datumLogic = new DatumLogic();
				int maxID = datumLogic.getMaxID("Prj_ItemProg", "ID");
				if (this.cbkmark.Checked)
				{
					sqlString = string.Concat(new object[]
					{
						"UPDATE Prj_ItemProg SET filesType = ",
						this.DDTClass.SelectedValue,
						",[mark] = 3 WHERE ID=",
						maxID
					});
				}
				else
				{
					sqlString = string.Concat(new object[]
					{
						"UPDATE Prj_ItemProg SET filesType = ",
						this.DDTClass.SelectedValue,
						",[mark] = 2 WHERE ID=",
						maxID
					});
				}
			}
			else
			{
				flag = ItemProgAction.ItemProgInfoOp(itemProgInfo, "Update");
				if (this.cbkmark.Checked)
				{
					sqlString = string.Concat(new object[]
					{
						"UPDATE Prj_ItemProg SET filesType = ",
						this.DDTClass.SelectedValue,
						",[mark] = 3 WHERE ID=",
						itemProgInfo.ID
					});
				}
				else
				{
					sqlString = string.Concat(new object[]
					{
						"UPDATE Prj_ItemProg SET filesType = ",
						this.DDTClass.SelectedValue,
						",[mark] = 2 WHERE ID=",
						itemProgInfo.ID
					});
				}
			}
			if (flag)
			{
				publicDbOpClass.ExecSqlString(sqlString);
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_ItemProgManage' })");
				return;
			}
			this.JavaScriptControl1.Text = "alert('保存失败！');";
		}
	}


