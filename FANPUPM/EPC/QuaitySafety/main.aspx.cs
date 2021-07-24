using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
public partial class Main : PageBase, IRequiresSessionState
{
    protected string strTree = "";
    protected string strRur = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            string text = base.Request.QueryString["type"];
            string a;
            if ((a = text) != null)
            {
                if (a == "1")
                {
                    this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=Q&Type=Edit&TypeId=2";
                    this.strTree = "DatumClassTree.aspx?type=1&Parent=2";
                    return;
                }
                if (a == "2")
                {
                    this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=Q&Type=List&TypeId=2";
                    this.strTree = "DatumClassTree.aspx?type=2&Parent=2";
                    return;
                }
                if (a == "3")
                {
                    this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=S&Type=Edit&TypeId=3";
                    this.strTree = "DatumClassTree.aspx?type=3&Parent=3";
                    return;
                }
                if (a == "4")
                {
                    this.strRur = "/EPC/QuaitySafety/CriterionList.aspx?Flage=S&Type=List&TypeId=3";
                    this.strTree = "DatumClassTree.aspx?type=4&Parent=3";
                    return;
                }
                if (a == "11")
                {
                    this.strRur = "DatumClassList.aspx?TypeId=3&Parent=3";
                    this.strTree = "DatumClassTree.aspx?type=11&Parent=3";
                    return;
                }
                if (!(a == "12"))
                {
                    return;
                }
                this.strRur = "DatumClassList.aspx?TypeId=2&Parent=2";
                this.strTree = "DatumClassTree.aspx?type=12&Parent=2";
            }
        }
    }
    protected override void OnInit(EventArgs e)
    {
        this.InitializeComponent();
        base.OnInit(e);
    }
    private void InitializeComponent()
    {
    }
}


