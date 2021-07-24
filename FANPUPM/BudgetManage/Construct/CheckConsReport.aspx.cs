using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ConstructMain : NBasePage, IRequiresSessionState
{
    private PTPrjInfoBll ptPrjInfoBll = new PTPrjInfoBll();
    private string consRepId = string.Empty;

    protected override void OnInit(System.EventArgs e)
    {
        if (base.Request.QueryString["consRepId"] != null)
        {
            this.consRepId = base.Request.QueryString["consRepId"];
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.InitPage();
            PrjInfoModel prjInfoModel = new PrjInfoModel();
            ConstructReport byId = ConstructReport.GetById(this.consRepId);
            prjInfoModel = this.ptPrjInfoBll.GetModelByPrjGuid(byId.ProjectId);
            System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
            stringBuilder.AppendFormat(prjInfoModel.PrjName + "下", new object[0]);
            stringBuilder.Append(byId.InputDate.ToString("yyyy年MM月dd日"));
            stringBuilder.Append("的施工报量");
            this.lblPrjName.Text = stringBuilder.ToString();
            this.BindGv();
            this.hfldConsRepId.Value = this.consRepId;
        }
    }
    public void InitPage()
    {
        if (!string.IsNullOrEmpty(this.consRepId))
        {
            try
            {
                string strWhere = string.Concat(new string[]
				{
					" where I_XGID='",
					this.consRepId,
					"' and V_YHDM='",
					base.UserCode,
					"'"
				});
                PTDbsjBll pTDbsjBll = new PTDbsjBll();
                System.Collections.Generic.List<PTDbsjModel> listArray = pTDbsjBll.GetListArray(strWhere);
                if (listArray.Count == 1)
                {
                    pTDbsjBll.Delete(listArray[0].I_DBSJ_ID);
                }
            }
            catch
            {
            }
        }
    }
    protected void BindGv()
    {
        System.Collections.Generic.List<string> taskIdsByReportId = ConstructTask.GetTaskIdsByReportId(this.consRepId);
        string text = "";
        foreach (string current in taskIdsByReportId)
        {
            if (current != "")
            {
                text += "'";
                text += current;
                text += "',";
            }
        }
        if (text != "")
        {
            text = text.Substring(0, text.Length - 1);
        }
        else
        {
            text = "''";
        }
        DataTable allByTaskIds = ConstructTask.GetAllByTaskIds(text, this.consRepId, true, false);
        this.gvConsTask.DataSource = allByTaskIds;
        this.gvConsTask.DataBind();
    }
    protected void gvConsTask_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string text = this.gvConsTask.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes["id"] = text;
            e.Row.Attributes["onclick"] = "showInfo('" + text + "')";
            e.Row.Style.Add("cursor", "pointer");
        }
    }
}
