using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumSeach : PageBase, IRequiresSessionState
	{
		protected string PrjCode = "";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.PrjCode = base.Request.QueryString["PrjCode"].ToString();
			if (!this.Page.IsPostBack)
			{
				this.BtnSee.Attributes["onclick"] = "return OpType('SEE');";
				this.ViewState["TYOEID"] = base.Request.QueryString["TypeId"];
				this.ViewState["IsValid"] = "";
				this.hdnTypeId.Value = this.ViewState["TYOEID"].ToString();
				this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString(), " and AffirmState = 1 and ProjectCode = '" + this.PrjCode + "'");
			}
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
		private void GridBind(string TypeId, string IsValid, string strWhere)
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
			text += strWhere;
			this.GrdDatum.DataSource = publicDbOpClass.GetPageData(text, "EPM_Datum_Data", "AddDate desc");
			this.GrdDatum.DataBind();
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
		private string getSqlWhere()
		{
			string text = " and AffirmState = 1 and ProjectCode = '" + this.PrjCode + "'";
			if (this.TxtDatum.Text.ToString() != "")
			{
				text = text + " and DatumName like '%" + this.TxtDatum.Text.ToString() + "%'";
			}
			if (this.DBAdd.Text.ToString() != "")
			{
				text = text + "and datediff(d,AddDate,'" + this.DBAdd.Text.ToString() + "') =0";
			}
			if (this.DBUpdate.Text.ToString() != "")
			{
				text = text + "and datediff(d,UpdateDate,'" + this.DBUpdate.Text.ToString() + "') =0";
			}
			return text;
		}
		protected void BtnSeach_Click(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString(), this.getSqlWhere());
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.GrdDatum.CurrentPageIndex = e.NewPageIndex;
			this.GridBind(this.ViewState["TYOEID"].ToString(), this.ViewState["IsValid"].ToString(), this.getSqlWhere());
		}
	}


