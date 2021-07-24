using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumList : PageBase, IRequiresSessionState
	{
		protected string PrjCode = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.PrjCode = base.Request.QueryString["PrjCode"].ToString();
			this.BtnAdd.Attributes["onclick"] = "return OpType('ADD','" + this.PrjCode + "');";
			this.BtnEdit.Attributes["onclick"] = "return OpType('EDIT','" + this.PrjCode + "');";
			this.BtnSee.Attributes["onclick"] = "return OpType('SEE','" + this.PrjCode + "');";
			this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中记录吗？')){return false;}";
			if (!this.Page.IsPostBack)
			{
				this.ViewState["TYOEID"] = base.Request.QueryString["TypeId"];
				this.ViewState["IsValid"] = "";
				this.hdnTypeId.Value = this.ViewState["TYOEID"].ToString();
				this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString());
			}
		}
		private void GridBind(string TypeId, string IsValid)
		{
			string text = "(1=1) ";
			if (TypeId.Trim() == "")
			{
				text = (text ?? "");
			}
			else
			{
				text = text + " and TypeId = '" + TypeId + "'";
			}
			if (IsValid.Trim() == "")
			{
				text = (text ?? "");
			}
			else
			{
				text = text + " and IsValid = '" + IsValid + "'";
			}
			text = text + " and ProjectCode = '" + this.PrjCode + "'";
			this.GrdDatum.DataSource = publicDbOpClass.GetPageData(text, "EPM_Datum_Data", "AddDate desc");
			this.GrdDatum.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.GrdDatum.ItemDataBound += new DataGridItemEventHandler(this.GrdDatum_ItemDataBound);
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString());
		}
		protected void BtnEdit_Click(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString());
		}
		private void GrdDatum_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				int num = e.Item.ItemIndex + 1;
				string text = this.GrdDatum.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Cells[0].Text = num.ToString();
				e.Item.Cells[2].Text = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, e.Item.Cells[5].Text.Trim());
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.GrdDatum.ClientID.ToString(),
					"');clickRow('",
					text,
					"','",
					((DataRowView)e.Item.DataItem)["AffirmState"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				HyperLink hyperLink = (HyperLink)e.Item.Cells[1].Controls[0];
				hyperLink.NavigateUrl = "#";
				hyperLink.NavigateUrl = string.Concat(new string[]
				{
					"javascript:void(window.open('DatumAdd.aspx?DatumCode=",
					text,
					"&optype=SEE&PrjCode= ",
					this.PrjCode,
					"','','left=150,top=150,width=650,height=460,toolbar=no,status=yes,scrollbars=yes,resiz able=no'));"
				});
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			KnowledgeDataAction knowledgeDataAction = new KnowledgeDataAction();
			knowledgeDataAction.DelFiles(new Guid(this.hdnCode.Value));
			if (KnowledgeDataAction.DelData(new Guid(this.hdnCode.Value)) != 1)
			{
				this.js.Text = "删除失败！";
				return;
			}
			this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString());
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.GrdDatum.CurrentPageIndex = e.NewPageIndex;
			this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString());
		}
	}


