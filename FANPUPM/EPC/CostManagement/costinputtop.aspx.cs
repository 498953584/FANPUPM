using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CostInputTop : PageBase, IRequiresSessionState
	{

		public string PrjCodeValue
		{
			get
			{
				object obj = this.ViewState["PrjCodeValue"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["PrjCodeValue"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.WF1.ISdel = 0;
				if (base.Request["PrjCode"] != null && base.Request.QueryString["PrjCode"].ToString() != "")
				{
					this.PrjCode.Value = base.Request["PrjCode"].ToString();
					this.PrjCodeValue = base.Request["PrjCode"].ToString();
					this.btn_Add.Enabled = true;
				}
				else
				{
					if (base.Request["ic"] != null && base.Request["ic"].ToString() != "")
					{
						this.dgCostInputPri.DataSource = publicDbOpClass.GetPageData(" recordID='" + base.Request["ic"].ToString().Trim() + "'", "EPM_V_CostImport", "   happendate desc");
						this.dgCostInputPri.DataBind();
						this.FraFlow.Attributes["height"] = "150";
						return;
					}
					this.CostInputDatabind("");
				}
				this.CostInputDatabind(this.PrjCodeValue);
			}
			this.ViewState["sTypeValue"] = base.Request["Type"].ToString();
			this.btn_Add.Attributes["onclick"] = "if( !OpType('add')){ return false;}";
			this.btn_Fix.Attributes["onclick"] = "if( !OpType('edit')){ return false;}";
			this.btn_Del.Attributes["onclick"] = "javascript:if(!confirm('确定删除该记录吗?')){return false;}";
			this.btn_Judge.Attributes["onclick"] = "OpType('Judge');";
			this.butOK.Attributes["onclick"] = "OpenAudit()";
		}
		private void CostInputDatabind(string PrjCode)
		{
			string sqlWhere = "";
			if (this.PrjCode.Equals(""))
			{
				this.dgCostInputPri.DataSource = publicDbOpClass.GetPageData(sqlWhere, "EPM_V_CostImport", "   happendate desc");
				this.dgCostInputPri.DataBind();
				return;
			}
			if (base.Request["Type"] != null && base.Request["Type"].ToString() == "ShenHe")
			{
				this.butOK.Enabled = false;
				if (base.Request["PrjCode"] != null && base.Request["PrjCode"].ToString() != "")
				{
					sqlWhere = string.Format("PrjCode = '{0}'  and (AuditResult!=1)  ", PrjCode);
				}
				else
				{
					sqlWhere = string.Format(" (AuditResult!=1)", new object[0]);
				}
				this.dgCostInputPri.DataSource = publicDbOpClass.GetPageData(sqlWhere, "EPM_V_CostImport", "   happendate desc");
				this.dgCostInputPri.DataBind();
				return;
			}
			if (base.Request["Type"] == null || !(base.Request["Type"].ToString() == "List"))
			{
				this.butOK.Visible = false;
				if (base.Request["PrjCode"] != null && base.Request["PrjCode"].ToString() != "")
				{
					sqlWhere = string.Format("PrjCode = '{0}' ", PrjCode);
				}
				else
				{
					sqlWhere = string.Format(" (AuditResult=1)", new object[0]);
				}
				this.dgCostInputPri.DataSource = publicDbOpClass.GetPageData(sqlWhere, "EPM_V_CostImport", "   happendate desc");
				this.dgCostInputPri.DataBind();
				return;
			}
			if (base.Request["PrjCode"] != null)
			{
				sqlWhere = string.Format("PrjCode = '{0}' ", PrjCode);
				this.dgCostInputPri.DataSource = publicDbOpClass.GetPageData(sqlWhere, "EPM_V_CostImport", "   happendate desc");
				this.dgCostInputPri.DataBind();
				return;
			}
			this.dgCostInputPri.DataSource = "";
			this.dgCostInputPri.DataBind();
		}
		private void dgCostInputPri_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				string value = ((HtmlInputHidden)e.Item.FindControl("hidState")).Value;
				if (!value.Equals("1"))
				{
					e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgCostInputPri.ClientID + "');" + string.Format("OnClickGridItem('{0}','{1}','{2}','{3}')", new object[]
					{
						e.Item.Cells[1].Text,
						e.Item.Cells[11].Text,
						e.Item.Cells[9].Text,
						e.Item.Cells[8].Text
					});
				}
				else
				{
					e.Item.Attributes["onclick"] = "doClick(this,'" + this.dgCostInputPri.ClientID + "');" + string.Format("OnClickGridItem('{0}','{1}','{2}')", e.Item.Cells[1].Text, e.Item.Cells[11].Text, e.Item.Cells[9].Text);
				}
				e.Item.Cells[0].Text = Convert.ToString(e.Item.ItemIndex + 1);
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				e.Item.Cells[8].Text = AduitAction.SetOkState(e.Item.Cells[9].Text.ToString()).Substring(0, 3);
				string value2 = AduitAction.SetOkState(e.Item.Cells[9].Text.ToString()).Substring(3);
				e.Item.Cells[8].Style.Add("color", value2);
			}
		}
		protected void btn_Add_Click(object sender, EventArgs e)
		{
			this.CostInputDatabind(this.PrjCodeValue);
		}
		protected void btn_Fix_Click(object sender, EventArgs e)
		{
			this.CostInputDatabind(this.PrjCodeValue);
		}
		protected void btn_Del_Click(object sender, EventArgs e)
		{
			int num = CostInputPriAction.deleteCostInputPri(this.RecordID.Value);
			this.CostInputDatabind(this.PrjCodeValue);
			if (num == 1)
			{
				this.Js.Text = "alert('删除成功!');";
				return;
			}
			this.Js.Text = "alert('删除失败!');";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgCostInputPri.ItemDataBound += new DataGridItemEventHandler(this.dgCostInputPri_ItemDataBound);
		}
		protected void btn_Search_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void Data_Bind()
		{
			string text = "(1=1)";
			string text2 = this.txt_HappenUnit.Text.ToString();
			string text3 = this.txt_FillPeople.Text.ToString();
			if (text2 != "")
			{
				text = text + " and HappenUnit like '%" + text2.Trim() + "%'";
			}
			if (text3 != "")
			{
				text = text + " and FillPeople like '%" + text3 + "%'";
			}
			text = text + " and PrjCode = '" + this.PrjCodeValue + "'";
			this.dgCostInputPri.DataSource = publicDbOpClass.GetPageData(text, "EPM_V_CostImport");
			this.dgCostInputPri.DataBind();
		}
		protected void btn_Judge_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void BtnAudit_Click(object sender, EventArgs e)
		{
			Guid instanceCode = new Guid(this.RecordID.Value);
			bool flag = FlowAuditAction.BeginFlow("112", instanceCode, 17, this.PrjCode.Value.ToString(), base.UserCode);
			if (flag)
			{
				this.Js.Text = "alert('启动申请审核成功！')";
			}
			else
			{
				this.Js.Text = "alert('启动申请审核失败！')";
			}
			this.Data_Bind();
		}
		protected void butOK_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgCostInputPri.CurrentPageIndex = e.NewPageIndex;
			this.CostInputDatabind(this.PrjCodeValue);
		}
	}


