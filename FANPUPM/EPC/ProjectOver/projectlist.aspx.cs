using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProjectList : NBasePage, IRequiresSessionState
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.ViewState["SQLWHERE"] = base.Request.QueryString["SqlWhere"];
                if (base.Request.QueryString["SqlWhere"].Trim() == "")
                {
                    PrjInfoModel prjInfoModel = new PrjInfoModel();
                    prjInfoModel.Remark = "2";
                    prjInfoModel.PrjState = "2";
                    this.ViewState["SQLWHERE"] = "1=1";
                }
                this.GridBind(this.ViewState["SQLWHERE"].ToString());
            }
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
        }
        private void GridBind(string SqlWhere)
        {
            this.DataGrid1.DataSource = publicDbOpClass.GetPageData(SqlWhere, "V_Pt_PrjInfo", "StartDate desc");
            this.DataGrid1.DataBind();
            if (this.DataGrid1.Items.Count <= 0)
            {
                this.BtnOver.Enabled = false;
                this.BtnStop.Enabled = false;
            }
        }
        protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
            this.GridBind(this.ViewState["SQLWHERE"].ToString());
        }
        private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex > -1)
            {
                e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
                string text = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
                DataRowView dataRowView = (DataRowView)e.Item.DataItem;
                e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.DataGrid1.ClientID.ToString(),
					"');clickRow('",
					text,
					"','",
					dataRowView["prjCode"].ToString(),
					"',",
					dataRowView["PrjState"].ToString(),
					");"
				});
                e.Item.ToolTip = dataRowView["PrjStateRemark"].ToString();
                e.Item.Cells[10].Text = e.Item.Cells[10].Text.Replace(":设置为竣工;", "").Replace(":设置为停工;", "");
                string text2 = dataRowView["prjName"].ToString();
                if (text2.Length > 15)
                {
                    e.Item.Cells[3].Text = text2.Substring(0, 14) + "......";
                    e.Item.Cells[3].ToolTip = text2;
                }
                string text3 = dataRowView["prjCode"].ToString();
                int arg_1DB_0 = text3.Length;
            }
        }
        protected void BtnOver_Click(object sender, EventArgs e)
        {
            if (this.txtDate.Text.Trim() == "" && this.HidFlag.Value == "0")
            {
                this.JS.Text = "alert('请选择日期！');";
            }
            else
            {
                int state = Convert.ToInt32(this.HdnState.Value);
                Guid code = new Guid(this.hdnID.Value);
                if (this.HidFlag.Value == "0")
                {
                    state = 7;
                }
                else
                {
                    state = 4;
                }
                PMAction.SwitchState(code, state);
                if (this.HidFlag.Value == "0")
                {
                    PMAction.SwitchStateRemark(code, this.txtDate.Text.ToString() + ":设置为竣工; ");
                }
                else
                {
                    PMAction.SwitchStateRemark(code, "");
                }
            }
            this.GridBind(this.ViewState["SQLWHERE"].ToString());
        }
        protected void BtnStop_Click(object sender, EventArgs e)
        {
            if (this.txtDate.Text.Trim() == "")
            {
                this.JS.Text = "alert('请选择日期！');";
            }
            else
            {
                int state = Convert.ToInt32(this.HdnState.Value);
                Guid code = new Guid(this.hdnID.Value);
                state = 6;
                PMAction.SwitchState(code, state);
                PMAction.SwitchStateRemark(code, this.txtDate.Text.ToString() + ":设置为停工; ");
            }
            this.GridBind(this.ViewState["SQLWHERE"].ToString());
        }
    }
