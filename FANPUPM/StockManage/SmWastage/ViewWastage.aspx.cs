using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_SmWastage_ViewWastage : NBasePage, IRequiresSessionState
{
	private PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
	private TreasuryStockBll treasuryStockBll = new TreasuryStockBll();
	private Treasury treasury = new Treasury();
	private OutReserveBll outReserveBll = new OutReserveBll();
	private OutStockBll outStockBll = new OutStockBll();
	private SmWastageBll smWastageBll = new SmWastageBll();
	private SmWastageStockBll smWastageStockBll = new SmWastageStockBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		SmWastageModel smWastageModel = null;
		if (base.Request.QueryString["id"] != null && base.Request.QueryString["ic"] == null)
		{
			smWastageModel = this.smWastageBll.GetModel(base.Request.QueryString["id"]);
		}
		else
		{
			if (base.Request.QueryString["ic"] != null)
			{
				smWastageModel = this.smWastageBll.GetModelByIc(base.Request.QueryString["ic"]);
			}
		}
		if (smWastageModel != null)
		{
			this.lblExplain.Text = smWastageModel.Explain;
			this.lblInTime.Text = Common2.GetTime(smWastageModel.InputDate.ToString());
			this.lblPeople.Text = smWastageModel.InputPerson;
			this.lblPPCode.Text = smWastageModel.Code;
			this.lblBllProducer.Text = smWastageModel.InputPerson;
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			this.hdGuid.Value = smWastageModel.Id;
			this.hdflowstate.Value = smWastageModel.Flowstate.ToString();
			this.lblTerasuryName.Text = this.treasury.GetModel(smWastageModel.Treasurycode).tname;
			System.Collections.Generic.List<SmWastageStockModel> listArray = this.smWastageStockBll.GetListArray(" where WastageCode='" + smWastageModel.Code + "'");
			string text = "";
			foreach (SmWastageStockModel current in listArray)
			{
				text = text + "'" + current.ResourceCode + "',";
			}
			if (text != "")
			{
				text = text.Substring(0, text.Length - 1);
			}
			this.ViewState["DataTable"] = this.smWastageStockBll.GetTableByWastageCode(smWastageModel.Code, smWastageModel.Treasurycode);
			this.BindGv();
			this.FileUpload1.InnerHtml = this.FilesBind(smWastageModel.Id);
		}
	}
	public void BindGv()
	{
		DataTable dataTable = (DataTable)this.ViewState["DataTable"];
		if (dataTable.Rows.Count == 0)
		{
			dataTable.Rows.Add(dataTable.NewRow());
			this.gvWastage.DataSource = dataTable;
			this.gvWastage.DataBind();
			this.gvWastage.HeaderRow.Cells[0].Visible = false;
			this.gvWastage.Rows[0].Visible = false;
			return;
		}
		this.gvWastage.DataSource = dataTable;
		this.gvWastage.DataBind();
		string total = System.Convert.ToDecimal(dataTable.Compute("SUM(Total)", string.Empty)).ToString("0.000");
		GridViewUtility.AddTotalRow(this.gvWastage, total, 8);
	}
	protected void gvWastage_RowDataBound(object sender, GridViewRowEventArgs e)
	{
	}
	public string FilesBind(string consID)
	{
		string text = ConfigurationManager.AppSettings["Wastage"].ToString();
		string result;
		try
		{
			string[] files = System.IO.Directory.GetFiles(base.Server.MapPath(text) + consID);
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			string[] array = files;
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				string text3 = string.Empty;
				text3 = text2.Substring(text2.LastIndexOf("\\") + 1);
				string str = text + "/" + consID;
				string str2 = str + "/" + text3;
				text3 = string.Concat(new string[]
				{
					"<a  class=\"link\" target=_blank  href=\"../../Common/DownLoad.aspx?path=",
					HttpUtility.UrlEncode(str2),
					"\"  >",
					text3,
					"</a>"
				});
				stringBuilder.Append(text3);
				stringBuilder.Append(", ");
			}
			string text4 = string.Empty;
			if (stringBuilder.Length > 2)
			{
				text4 = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			result = text4;
		}
		catch
		{
			result = "";
		}
		return result;
	}
}


