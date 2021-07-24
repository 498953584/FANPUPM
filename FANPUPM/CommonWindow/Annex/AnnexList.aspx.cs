using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
    public partial class AnnexList : NBasePage, IRequiresSessionState
    {
        private AnnexAction _AnnexAction = new AnnexAction();
        private string _RecordCode = "";
        private int _AnnexType;
        private int _ModuleID;

        private void Page_Load(object sender, EventArgs e)
        {
            if (base.Request["rc"] == null || base.Request["at"] == null || base.Request["mid"] == null)
            {
                this.js.Text = "alert('参数错误！');window.returnValue = false; window.close();";
            }
            else
            {
                this._RecordCode = base.Request["rc"];
                this._AnnexType = int.Parse(base.Request["at"]);
                this._ModuleID = int.Parse(base.Request["mid"]);
            }
            if (!this.Page.IsPostBack)
            {
                if (!(base.Request.QueryString["type"] == "1") && !(base.Request.QueryString["type"] == "2") && base.Request.QueryString["type"] == "0")
                {
                    this.btnAdd.Visible = false;
                    this.btnDel.Visible = false;
                }
                this.btnAdd.Attributes["onclick"] = string.Concat(new object[]
				{
					"if (!upLoadFile('",
					this._RecordCode,
					"',",
					this._ModuleID,
					")){return false;}"
				});
                this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
                this.dgdAnnex_Bind(this._RecordCode, this._AnnexType, this._ModuleID);
            }
        }
        private void dgdAnnex_Bind(string recordCode, int annexType, int moduleID)
        {
            ArrayList annexList = this._AnnexAction.GetAnnexList(recordCode, annexType, moduleID);
            this.dgdAnnex.DataSource = annexList;
            this.dgdAnnex.DataBind();
            this.lblAnnexState.Text = "共" + annexList.Count.ToString() + "个文件！";
        }
        private void dgdAnnex_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            switch (e.Item.ItemType)
            {
                case ListItemType.Item:
                case ListItemType.AlternatingItem:
                    {
                        AnnexInfo annexInfo = (AnnexInfo)e.Item.DataItem;
                        e.Item.Attributes["id"] = annexInfo.AnnexCode.ToString();
                        e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgdAnnex.ClientID,
					"');clickAnnexRow('",
					this.btnDel.ClientID,
					"','",
					this.hdnAnnexCode.ClientID,
					"','",
					annexInfo.AnnexCode.ToString(),
					"');"
				});
                        e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
                        e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
                        ((HyperLink)e.Item.Cells[4].Controls[1]).NavigateUrl = "/Common/DownLoad2.aspx?path=" + HttpUtility.UrlEncode(annexInfo.FilePath + annexInfo.AnnexName, Encoding.UTF8) + "&Name=" + HttpUtility.UrlEncode(annexInfo.OriginalName, Encoding.UTF8);
                        ((HyperLink)e.Item.Cells[4].Controls[1]).Target = "_blank";
                        return;
                    }
                default:
                    return;
            }
        }
        private void FileDownload(string filePath, string fileName)
        {
            filePath = base.Server.MapPath(filePath);
            FileStream fileStream = new FileStream(filePath, FileMode.Open);
            byte[] array = new byte[(int)fileStream.Length];
            fileStream.Read(array, 0, array.Length);
            fileStream.Close();
            base.Response.ContentType = "application/octet-stream";
            base.Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName, Encoding.UTF8));
            base.Response.BinaryWrite(array);
            base.Response.Flush();
            base.Response.End();
        }
        protected override void OnInit(EventArgs e)
        {
            this.InitializeComponent();
            base.OnInit(e);
        }
        private void InitializeComponent()
        {
            this.dgdAnnex.ItemDataBound += new DataGridItemEventHandler(this.dgdAnnex_ItemDataBound);
            this.btnDel.Click += new EventHandler(this.btnDel_Click);
            this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
            base.Load += new EventHandler(this.Page_Load);
        }
        private void btnDel_Click(object sender, EventArgs e)
        {
            Guid annexCode = new Guid(this.hdnAnnexCode.Value);
            if (this._AnnexAction.DelAnnexTemp(annexCode) != 1)
            {
                this.Page.RegisterStartupScript("warn", "<script language=\"javascript\">alert('删除失败！');</script>");
            }
            this._AnnexAction.SynchronizeAnnexList(this._RecordCode, this._AnnexType, this._ModuleID, this.Session["yhdm"].ToString());
            this._AnnexAction.ClearAnnexList(this._RecordCode, this._AnnexType, this._ModuleID, this.Session["yhdm"].ToString());
            this.dgdAnnex_Bind(this._RecordCode, this._AnnexType, this._ModuleID);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.dgdAnnex_Bind(this._RecordCode, this._AnnexType, this._ModuleID);
        }
    }
