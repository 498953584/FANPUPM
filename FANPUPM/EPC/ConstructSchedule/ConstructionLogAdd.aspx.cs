using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.Model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProjectManage_Construction_ConstructionLogAdd : NBasePage, IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        base.Response.Cache.SetNoStore();
        if (!this.Page.IsPostBack)
        {
            if (base.Request.QueryString["t"] == "2")
            {
                ConstructionLogModel model = ConstructionLog.GetModel(base.Request.QueryString["LogID"].ToString());
                this.hdnRecordId.Value = base.Request.QueryString["LogID"].ToString();
                this.txtcode.Text = model.code.ToString();
                this.txtamweather.Text = model.amweather;
                this.txtoperations.Text = model.operations;
                this.txtthisDate.Text = model.thisDate.ToShortDateString();
                DateTime thisDate = model.thisDate;
                int month = thisDate.Month;
                int year = thisDate.Year;
                int day = thisDate.Day;
                switch (ProjectManage_Construction_ConstructionLogAdd.getWeekDay(year, month, day))
                {
                    case 1:
                        this.lbWeek.Text = "星期一";
                        break;
                    case 2:
                        this.lbWeek.Text = "星期二";
                        break;
                    case 3:
                        this.lbWeek.Text = "星期三";
                        break;
                    case 4:
                        this.lbWeek.Text = "星期四";
                        break;
                    case 5:
                        this.lbWeek.Text = "星期五";
                        break;
                    case 6:
                        this.lbWeek.Text = "星期六";
                        break;
                    case 7:
                        this.lbWeek.Text = "星期日";
                        break;
                }
                this.txtdaycontent.Text = model.daycontent;
                this.txtdesign.Text = model.design;
                this.txtacceptance.Text = model.acceptance;
                this.txtbeton.Text = model.beton;
                this.txtdatum.Text = model.datum;
                this.txtproduct.Text = model.product;
                this.txtremark.Text = model.remark;
                this.txtPart.Text = model.part;
                this.Literal1.Visible = false;
                this.FileLink1.MID = 1913;
                this.FileLink1.FID = this.hdnRecordId.Value;
                this.FileLink1.Type = 1;
                return;
            }
            if (base.Request.QueryString["t"] == "3")
            {
                ConstructionLogModel model2 = ConstructionLog.GetModel(base.Request.QueryString["LogID"].ToString());
                this.txtcode.Text = model2.code.ToString();
                this.txtamweather.Text = model2.amweather;
                this.txtoperations.Text = model2.operations;
                this.txtthisDate.Text = model2.thisDate.ToShortDateString();
                DateTime thisDate2 = model2.thisDate;
                int month2 = thisDate2.Month;
                int year2 = thisDate2.Year;
                int day2 = thisDate2.Day;
                switch (ProjectManage_Construction_ConstructionLogAdd.getWeekDay(year2, month2, day2))
                {
                    case 1:
                        this.lbWeek.Text = "星期一";
                        break;
                    case 2:
                        this.lbWeek.Text = "星期二";
                        break;
                    case 3:
                        this.lbWeek.Text = "星期三";
                        break;
                    case 4:
                        this.lbWeek.Text = "星期四";
                        break;
                    case 5:
                        this.lbWeek.Text = "星期五";
                        break;
                    case 6:
                        this.lbWeek.Text = "星期六";
                        break;
                    case 7:
                        this.lbWeek.Text = "星期日";
                        break;
                }
                this.txtdaycontent.Text = model2.daycontent;
                this.txtdesign.Text = model2.design;
                this.txtacceptance.Text = model2.acceptance;
                this.txtbeton.Text = model2.beton;
                this.txtdatum.Text = model2.datum;
                this.txtproduct.Text = model2.product;
                this.txtremark.Text = model2.remark;
                this.txtPart.Text = model2.part;
                this.txtcode.ReadOnly = true;
                this.txtamweather.ReadOnly = true;
                this.txtoperations.ReadOnly = true;
                this.txtthisDate.ReadOnly = true;
                this.txtdaycontent.ReadOnly = true;
                this.txtdesign.ReadOnly = true;
                this.txtacceptance.ReadOnly = true;
                this.txtbeton.ReadOnly = true;
                this.txtdatum.ReadOnly = true;
                this.txtproduct.ReadOnly = true;
                this.txtremark.ReadOnly = true;
                this.txtPart.ReadOnly = true;
                this.BtnAdd.Visible = false;
                this.FileLink1.MID = 1913;
                this.FileLink1.FID = model2.logID.ToString();
                this.FileLink1.Type = 1;
                this.FileLink1.Visible = false;
                this.Literal1.Text = FileView.FilesBind(1913, model2.logID.ToString());
                return;
            }
            this.hdnRecordId.Value = Convert.ToString(Guid.NewGuid());
            this.txtthisDate.Text = DateTime.Today.ToShortDateString();
            this.Literal1.Visible = false;
            this.FileLink1.MID = 1913;
            this.FileLink1.FID = this.hdnRecordId.Value;
            this.FileLink1.Type = 1;
            DateTime today = DateTime.Today;
            int month3 = today.Month;
            int year3 = today.Year;
            int day3 = today.Day;
            switch (ProjectManage_Construction_ConstructionLogAdd.getWeekDay(year3, month3, day3))
            {
                case 1:
                    this.lbWeek.Text = "星期一";
                    return;
                case 2:
                    this.lbWeek.Text = "星期二";
                    return;
                case 3:
                    this.lbWeek.Text = "星期三";
                    return;
                case 4:
                    this.lbWeek.Text = "星期四";
                    return;
                case 5:
                    this.lbWeek.Text = "星期五";
                    return;
                case 6:
                    this.lbWeek.Text = "星期六";
                    return;
                case 7:
                    this.lbWeek.Text = "星期日";
                    break;
                default:
                    return;
            }
        }
    }
    public static int getWeekDay(int y, int m, int d)
    {
        if (m == 1)
        {
            m = 13;
        }
        if (m == 2)
        {
            m = 14;
        }
        return (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7 + 1;
    }
    private ConstructionLogModel GetCLM()
    {
        string text = "";
        if (this.txtcode.Text.ToString() == "")
        {
            text += "编码不能为空！\\n";
        }
        if (text != "")
        {
            this.JS.Text = "alert('" + text + "')";
        }
        string value = this.hdnRecordId.Value;
        string text2 = this.txtcode.Text;
        string text3 = this.txtamweather.Text;
        string text4 = this.txtoperations.Text;
        DateTime thisDate = DateTime.Parse(this.txtthisDate.Text);
        string text5 = this.txtdaycontent.Text;
        string text6 = this.txtdesign.Text;
        string text7 = this.txtacceptance.Text;
        string text8 = this.txtbeton.Text;
        string text9 = this.txtdatum.Text;
        string text10 = this.txtproduct.Text;
        string text11 = this.txtremark.Text;
        string text12 = this.txtPart.Text;
        return new ConstructionLogModel
        {
            logID = value,
            code = text2,
            amweather = text3,
            operations = text4,
            thisDate = thisDate,
            daycontent = text5,
            design = text6,
            acceptance = text7,
            beton = text8,
            datum = text9,
            product = text10,
            remark = text11,
            part = text12
        };
    }
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        ConstructionLogModel cLM = this.GetCLM();
        if (base.Request.QueryString["t"] == "2")
        {
            cLM.logID = base.Request.QueryString["LogID"].ToString();
            if (publicDbOpClass.Update(cLM) == 1)
            {
                this.Page.RegisterClientScriptBlock("", "<script languange='javascript'>parent.desktop.flowclass.location=parent.desktop.flowclass.location;alert('修改成功！');window.returnValue=true;top.frameWorkArea.window.desktop.getActive().close();</script>");
                return;
            }
        }
        else
        {
            if (publicDbOpClass.Exist(this.txtcode.Text, base.Request["pmId"].ToString()) == 1)
            {
                this.Page.RegisterClientScriptBlock("", "<script>alert('编码已存在，重新填写！');</script>");
                return;
            }
            if (publicDbOpClass.Add(cLM, base.Request["pmId"].ToString()) == 1)
            {
                this.Page.RegisterClientScriptBlock("", "<script>parent.desktop.flowclass.location=parent.desktop.flowclass.location;alert('添加成功！');top.frameWorkArea.window.desktop.getActive().close();</script>");
            }
        }
    }
}
