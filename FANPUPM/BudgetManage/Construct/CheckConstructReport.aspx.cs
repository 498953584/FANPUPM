using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_ConstructReport : NBasePage, IRequiresSessionState
{
    private PT_PrjInfo prjInfo = new PT_PrjInfo();
    private string prjId = string.Empty;
    private string year = string.Empty;
    protected override void OnInit(System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(base.Request["prjId"]))
        {
            this.prjId = base.Request["prjId"];
        }
        if (!string.IsNullOrEmpty(base.Request["year"]))
        {
            this.year = base.Request["year"];
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!base.IsPostBack)
        {
            this.hfldYear.Value = this.year;
            this.BindGv();
            this.hfldPrjId.Value = this.prjId;
        }
    }
    protected void BindGv()
    {
        System.Collections.Generic.IList<ConstructReport> auditedByPrj = ConstructReport.GetAuditedByPrj(this.prjId);
        this.gvConstruct.DataSource = auditedByPrj;
        this.gvConstruct.DataBind();
    }
    protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            string text = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
            e.Row.Attributes["id"] = text;
            string value = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[1].ToString();
            e.Row.Attributes["state"] = value;
            e.Row.Style.Add("cursor", "default");
            Image image = new Image();
            image.ImageUrl = "../../images/tree/more.gif";
            image.ToolTip = "详细信息";
            image.ID = "imgMore";
            image.Attributes.Add("rowId", text);
            image.Attributes["onclick"] = "showInfo('" + text + "')";
            image.Style.Add("vertical-align", "middle");
            image.Style.Add("float", "right");
            image.Style.Add("cursor", "hand");
            e.Row.Cells[1].Controls.Add(image);
        }
    }
    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
        if (this.hfldPurchaseChecked.Value.Contains('['))
        {
            list = JsonHelper.GetListFromJson(this.hfldPurchaseChecked.Value);
        }
        else
        {
            list.Add(this.hfldPurchaseChecked.Value);
        }
        try
        {
            ConstructReport.Delete(list);
            base.RegisterScript("$('#btnBindResTask').hide(); alert('系统提示：\\n\\删除成功！')");
            this.BindGv();
        }
        catch
        {
            base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
        }
    }
}
