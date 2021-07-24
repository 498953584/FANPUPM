using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Tender;
using System;
using System.Configuration;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_SetInfoBid : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDrop();
			this.bindBidData();
		}
	}
	protected void bindBidData()
	{
		string text = base.Request["id"];
		DataTable partTender = TenderInfo.GetPartTender(text, "4", base.UserCode);
		if (partTender.Rows.Count > 0)
		{
			DataRow dataRow = partTender.Rows[0];
			this.dropTenderAppraiseMethod.SelectedValue = dataRow["TenderAppraiseMethod"].ToString();
			this.txtTenderAnswerDate.Text = Common2.GetTime(dataRow["ProjTenderAnswerDate"]);
			this.txtTenderBeginDate.Text = Common2.GetTime(dataRow["ProjTenderBeginDate"]);
			this.txtTenderAverage.Text = dataRow["TenderAverage"].ToString();
			this.txtTenderCeilingPrice.Text = dataRow["TenderCeilingPrice"].ToString();
			this.txtTenderContent.Text = dataRow["ProjTenderContent"].ToString();
			this.txtTenderCostContent.Text = dataRow["ProjTenderCostContent"].ToString();
			this.txtTenderEarnestMoney.Text = dataRow["ProjTenderEarnestMoney"].ToString();
			this.txtTenderPrjManager.Text = dataRow["PrjManagerName"].ToString();
			this.hfldPrjManage.Value = dataRow["PrjManager"].ToString();
			this.txtTenderQuote.Text = dataRow["TenderQuote"].ToString();
			this.txtTenderRemark.Text = dataRow["ProjTenderRemark"].ToString();
			this.txtTenderUnit.Text = dataRow["TenderUnit"].ToString();
			this.dropTenderPayWay.SelectedValue = dataRow["ProjTenderPayWay"].ToString();
			if (!string.IsNullOrEmpty(dataRow["TenderProspect"].ToString()))
			{
				this.txtTenderProspect.Text = Common2.GetTime(dataRow["TenderProspect"]);
			}
			if (!string.IsNullOrEmpty(dataRow["TenderReadOne"].ToString()))
			{
				this.hfldTenderReadOne.Value = dataRow["TenderReadOne"].ToString();
				PtYhmc modelById = new PtYhmcBll().GetModelById(this.hfldTenderReadOne.Value);
				if (modelById != null)
				{
					this.txtTenderReadOne.Text = modelById.v_xm;
				}
			}
		}
		this.FileUpload1.Folder = ConfigurationManager.AppSettings["ProjectFile"].ToString();
		this.FileUpload1.RecordCode = text + "_4";
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (this.txtTenderCostContent.Text.Trim().Length > 500)
		{
			base.RegisterShow("系统提示", "现场费内容不能超过500个字！");
			return;
		}
		if (this.txtTenderContent.Text.Trim().Length > 500)
		{
			base.RegisterShow("系统提示", "标书内容不能超过500个字！");
			return;
		}
		string text = base.Request["id"];
		TenderInfo byId = TenderInfo.GetById(text);
		if (byId != null)
		{
			byId.ProjTenderBeginDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderBeginDate.Text));
			decimal? tenderAverage = null;
			if (!string.IsNullOrEmpty(this.txtTenderAverage.Text))
			{
				tenderAverage = new decimal?(System.Convert.ToDecimal(this.txtTenderAverage.Text));
			}
			byId.TenderAverage = tenderAverage;
			decimal? tenderCeilingPrice = null;
			if (!string.IsNullOrEmpty(this.txtTenderCeilingPrice.Text))
			{
				tenderCeilingPrice = new decimal?(System.Convert.ToDecimal(this.txtTenderCeilingPrice.Text));
			}
			byId.TenderCeilingPrice = tenderCeilingPrice;
			decimal? projTenderEarnestMoney = null;
			if (!string.IsNullOrEmpty(this.txtTenderEarnestMoney.Text))
			{
				projTenderEarnestMoney = new decimal?(System.Convert.ToDecimal(this.txtTenderEarnestMoney.Text));
			}
			byId.ProjTenderEarnestMoney = projTenderEarnestMoney;
			decimal? tenderQuote = null;
			if (!string.IsNullOrEmpty(this.txtTenderQuote.Text))
			{
				tenderQuote = new decimal?(System.Convert.ToDecimal(this.txtTenderQuote.Text));
			}
			byId.TenderQuote = tenderQuote;
			byId.TenderUnit = this.txtTenderUnit.Text;
			byId.TenderAppraiseMethod = this.dropTenderAppraiseMethod.SelectedValue;
			byId.ProjTenderCostContent = this.txtTenderCostContent.Text;
			System.DateTime? projTenderAnswerDate = null;
			if (!string.IsNullOrEmpty(this.txtTenderAnswerDate.Text))
			{
				projTenderAnswerDate = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderAnswerDate.Text));
			}
			byId.ProjTenderAnswerDate = projTenderAnswerDate;
			byId.ProjTenderPayWay = this.dropTenderPayWay.SelectedValue;
			byId.ProjTenderRemark = this.txtTenderRemark.Text;
			byId.ProjTenderContent = this.txtTenderContent.Text;
			byId.PrjManager = this.txtTenderPrjManager.Text;
			if (!string.IsNullOrWhiteSpace(this.txtTenderProspect.Text))
			{
				byId.TenderProspect = new System.DateTime?(System.Convert.ToDateTime(this.txtTenderProspect.Text));
			}
			if (!string.IsNullOrEmpty(this.hfldTenderReadOne.Value))
			{
				byId.TenderReadOne = this.hfldTenderReadOne.Value;
				PTDBSJService pTDBSJService = new PTDBSJService();
				pTDBSJService.Add(new PTDBSJ
				{
					I_XGID = byId.PrjGuid.ToString(),
					V_LXBM = "024",
					V_YHDM = this.hfldTenderReadOne.Value,
					DTM_DBSJ = new System.DateTime?(System.DateTime.Now),
					C_OpenFlag = "0",
					V_Content = "项目：" + byId.PrjName + "已经投标",
					V_DBLJ = "TenderManage/InfoView.aspx?ic=" + byId.PrjGuid.ToString(),
					V_TPLJ = "new_Mail.gif"
				});
			}
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(new System.Guid(text));
			byId2.PrjStateChangeTime = new System.DateTime?(System.DateTime.Now);
			pTPrjInfoZTBService.Update(byId2);
			byId.UpdatePart(byId, "4");
			string str = "BidManage.aspx";
			if (!string.IsNullOrEmpty(base.Request["purl"]))
			{
				str = base.Request["purl"];
			}
			string message = "CancelClick('" + str + "',true);";
			base.RegisterScript(message);
		}
	}
	private void BindDrop()
	{
		TypeList.BindDrop(this.dropTenderAppraiseMethod, "TenderAppraiseMethod", "", null, true);
		TypeList.BindDrop(this.dropTenderPayWay, "PaymentMethods", "", null, true);
	}
}


