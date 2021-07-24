using ASP;
using cn.justwin.BLL;
using cn.justwin.XPMBasicContactCorp;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ContactCorpListView : PageBase, IRequiresSessionState
	{
		protected string isAudit = string.Empty;
		private BasicContactCorp Cont = new BasicContactCorp();
		protected int CorpTypeID
		{
			get
			{
				return Convert.ToInt32(this.ViewState["CORPTYPEID"]);
			}
			set
			{
				this.ViewState["CORPTYPEID"] = value;
			}
		}
		public string WindowType
		{
			get
			{
				object obj = this.ViewState["WINDOWTYPE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["WINDOWTYPE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			base.Response.Clear();
			if (!base.IsPostBack)
			{
				this.DDL_CorpKind.DataTextField = "CodeName";
				this.DDL_CorpKind.DataValueField = "CodeID";
				this.DDL_CorpKind.DataSource = CodingAction.getTypebyID("1");
				this.DDL_CorpKind.DataBind();
				if (!string.IsNullOrEmpty(base.Request["cti"]) && !string.IsNullOrEmpty(base.Request["w"]))
				{
					if (!string.IsNullOrEmpty(base.Request["Audit"]))
					{
						this.isAudit = base.Request["Audit"].ToString().Replace("'", "");
					}
					if (!string.IsNullOrEmpty(base.Request["cti"]))
					{
						this.CorpTypeID = Convert.ToInt32(base.Request["cti"]);
						if (this.CorpTypeID != 4 && this.CorpTypeID != 5 && this.CorpTypeID != 10 && this.CorpTypeID != 19 && this.CorpTypeID != 20)
						{
							this.WF1.Type = 0;
						}
					}
					if (!string.IsNullOrEmpty(base.Request["w"]))
					{
						this.WindowType = base.Request["w"];
					}
				}
				if (this.WindowType == "1")
				{
					this.BtnClose.Visible = true;
					this.BtnAdd.Attributes["style"] = "display:none";
					this.BtnClose.Attributes["style"] = "display:none";
					this.BtnDel.Attributes["style"] = "display:none";
					this.BtnMod.Attributes["style"] = "display:none";
				}
				this.HdnTypeID.Value = this.CorpTypeID.ToString();
				this.Page_CustomInit();
				this.DgrdList_Bind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DgrdList.ItemDataBound += new DataGridItemEventHandler(this.DgrdList_ItemDataBound);
		}
		private void DgrdList_Bind()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("1=1");
			if (!string.IsNullOrEmpty(this.txtCorpName.Text.Trim()))
			{
				stringBuilder.Append(" and CorpName like'%" + this.txtCorpName.Text.Trim() + "%' ");
			}
			if (this.DDL_CorpKind.SelectedIndex != 0)
			{
				stringBuilder.Append(" and CorpKind='" + this.DDL_CorpKind.SelectedItem.Value + "' ");
			}
			if (!string.IsNullOrEmpty(this.txtLinkMan.Text.Trim()))
			{
				stringBuilder.Append(" and LinkMan like  '%" + this.txtLinkMan.Text.Trim() + "%' ");
			}
			if (!stringBuilder.ToString().Equals("1=1"))
			{
				stringBuilder.Append(" and CorpTypeID=" + this.CorpTypeID + " and IsValid=1");
				this.DgrdList.DataSource = this.Cont.QueryCorpList1(stringBuilder.ToString());
				this.DgrdList.DataBind();
				this.Txt_CorpSearch.Text = "";
			}
			else
			{
				if (this.isAudit == "Audit")
				{
					this.BtnAudit.Width = 60;
					this.DgrdList.DataSource = this.Cont.QueryCorpList3(this.CorpTypeID);
					this.DgrdList.DataBind();
				}
				else
				{
					if (this.isAudit == "DEL")
					{
						this.btnActiv.Width = 60;
						this.btnpzdel.Width = 60;
						this.DgrdList.DataSource = this.Cont.ShouDelQueryCorpList();
						this.DgrdList.DataBind();
						if (this.CorpTypeID != 4 || this.CorpTypeID != 5 || this.CorpTypeID != 10 || this.CorpTypeID != 19 || this.CorpTypeID != 20)
						{
							this.WF1.Type = 0;
						}
					}
					else
					{
						this.BtnAdd.Width = 60;
						this.BtnMod.Width = 60;
						this.BtnDel.Width = 60;
						this.BtnSearch.Visible = true;
						this.DgrdList.DataSource = this.Cont.QueryCorpList(this.CorpTypeID, base.UserCode);
						this.DgrdList.DataBind();
					}
				}
			}
			if (!string.IsNullOrEmpty(base.Request["ic"]) && !string.IsNullOrEmpty(base.Request["ic"].ToString()))
			{
				this.DgrdList.DataSource = this.Cont.QuerylistForguidandSta(base.Request["ic"].ToString());
				this.DgrdList.DataBind();
			}
		}
		private void Page_CustomInit()
		{
		}
		private void DgrdList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				BasicContactCorpModel basicContactCorpModel = (BasicContactCorpModel)e.Item.DataItem;
				e.Item.Cells[3].Text = this.Cont.GetCodeName(basicContactCorpModel.CorpKind.ToString(), "1");
				e.Item.Cells[4].Text = this.Cont.GetCodeName(basicContactCorpModel.WorkType.ToString(), "2");
				if (this.WindowType == "1")
				{
					e.Item.Attributes["ondblclick"] = string.Concat(new string[]
					{
						"doDblClickRow('",
						basicContactCorpModel.CorpID.ToString(),
						"','",
						basicContactCorpModel.CorpName,
						"');"
					});
				}
				this.Session["id"] = basicContactCorpModel.CorpID.ToString();
				if (basicContactCorpModel.AuditState == -1)
				{
					e.Item.Cells[8].Text = "未审核";
				}
				else
				{
					if (basicContactCorpModel.AuditState == -2)
					{
						e.Item.Cells[8].Text = "驳回";
					}
					else
					{
						if (basicContactCorpModel.AuditState == 1)
						{
							e.Item.Cells[8].Text = "已审核";
						}
						else
						{
							e.Item.Cells[8].Text = "处理中";
						}
					}
				}
				break;
			}
			}
			if (this.CorpTypeID != 7)
			{
				e.Item.Cells[8].Style["display"] = "none";
			}
			for (int i = 0; i < this.DgrdList.Columns.Count; i++)
			{
				string text = e.Item.Cells[i].Text.Trim();
				if (text.Length > 30)
				{
					e.Item.Cells[i].Text = text.Substring(0, 30);
					e.Item.Cells[i].ToolTip = text;
				}
				e.Item.Cells[i].ToolTip = text;
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			if (this.Cont.DelContactCorp(Convert.ToInt32(this.HdnCorpID.Value)) != 1)
			{
				this.JS.Text = "alert('删除数据出错！');";
				return;
			}
			this.DgrdList_Bind();
			com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.DgrdList_Bind();
			com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
		}
		protected void BtnMod_Click(object sender, EventArgs e)
		{
			this.DgrdList_Bind();
			com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
		}
		protected void BtnSearch_ServerClick(object sender, EventArgs e)
		{
			this.DgrdList.CurrentPageIndex = 0;
			this.DgrdList_Bind();
		}
		protected void BtnAudit_Click(object sender, EventArgs e)
		{
			this.DgrdList.DataBind();
		}
		protected void btnActiv_Click(object sender, EventArgs e)
		{
			this.Cont.ActiveDeptList(int.Parse(this.Session["id"].ToString()));
			this.Session.Remove("id");
			this.prebind();
		}
		protected DataTable AllInfo()
		{
			string text = "select identity(int,1,1) as 序号, corpname as 单位名称,brand as 品牌专长,speciality as 产品概况,linkman as 联系人, handphone as 手机号码,telephone as 联系电话,fax as 传真,";
			text += " corporation as 法人代表,aptitude as 公司资质,corpbrief as 单位简介,address as 地址,postcode as 邮编, contry as 国家, email as 邮箱, website as 网址,shopcard  as 营业执照,";
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				"taxcard as 税号,bankaccounts as 银行账号,accountbank as 开户银行,convert(nvarchar(80),corpkind)as 单位性质,Convert(nvarchar(80),worktype) as 经营类别,client  as 委托人,underlayability as 垫资能力,capital as 固定资产 into #NewContactcorp from xpm_basic_contactcorp where corptypeid=",
				Convert.ToInt32(base.Request["cti"]),
				" and isvalid=1"
			});
			text += " select * from #NewContactcorp";
			DataTable dataTable = publicDbOpClass.DataTableQuary(text);
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				string key;
				switch (key = dataTable.Rows[i]["单位性质"].ToString())
				{
				case "1":
					dataTable.Rows[i]["单位性质"] = "有限公司";
					break;
				case "7":
					dataTable.Rows[i]["单位性质"] = "外资企业";
					break;
				case "8":
					dataTable.Rows[i]["单位性质"] = "台资企业";
					break;
				case "12":
					dataTable.Rows[i]["单位性质"] = "国有企业";
					break;
				case "15":
					dataTable.Rows[i]["单位性质"] = "股份公司";
					break;
				case "16":
					dataTable.Rows[i]["单位性质"] = "个人投资";
					break;
				case "17":
					dataTable.Rows[i]["单位性质"] = "公司部门";
					break;
				case "18":
					dataTable.Rows[i]["单位性质"] = "政府";
					break;
				case null:
					dataTable.Rows[i]["单位性质"] = "";
					break;
				}
				string key2;
				switch (key2 = dataTable.Rows[i]["经营类别"].ToString())
				{
				case "1":
					dataTable.Rows[i]["经营类别"] = "其他";
					break;
				case "89":
					dataTable.Rows[i]["经营类别"] = "建筑工程";
					break;
				case "90":
					dataTable.Rows[i]["经营类别"] = "机电安装";
					break;
				case "91":
					dataTable.Rows[i]["经营类别"] = "煤炭生产";
					break;
				case "94":
					dataTable.Rows[i]["经营类别"] = "产品销售";
					break;
				case "98":
					dataTable.Rows[i]["经营类别"] = "房地产开发";
					break;
				case "99":
					dataTable.Rows[i]["经营类别"] = "劳务公司";
					break;
				case null:
					dataTable.Rows[i]["经营类别"] = "";
					break;
				}
			}
			string sqlString = "drop table #NewContactcorp";
			publicDbOpClass.ExecSqlString(sqlString);
			return dataTable;
		}
		protected void btnExp_Click1(object sender, EventArgs e)
		{
			DataTable table = this.AllInfo();
			ExcelExporter excelExporter = new ExcelExporter();
			excelExporter.Export(table, "单位信息.xls", base.Request.Browser.Browser);
		}
		public override void VerifyRenderingInServerForm(Control control)
		{
		}
		protected void btnpzdel_Click(object sender, EventArgs e)
		{
			if (!string.IsNullOrEmpty(this.HdnCorpID.Value.ToString()))
			{
				if (this.Cont.Delete(Convert.ToInt32(this.HdnCorpID.Value)) != 1)
				{
					this.JS.Text = "alert('删除数据出错！');";
					return;
				}
				this.prebind();
				com.jwsoft.pm.entpm.PageHelper.RefreshCorp(this);
			}
		}
		protected void DgrdList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DgrdList.CurrentPageIndex = e.NewPageIndex;
			this.DgrdList_Bind();
		}
		public void prebind()
		{
			this.btnActiv.Width = 60;
			this.btnpzdel.Width = 60;
			this.DgrdList.DataSource = this.Cont.ShouDelQueryCorpList();
			this.DgrdList.DataBind();
			if (this.CorpTypeID != 4 || this.CorpTypeID != 5 || this.CorpTypeID != 10 || this.CorpTypeID != 19 || this.CorpTypeID != 20)
			{
				this.WF1.Type = 0;
			}
		}
	}


