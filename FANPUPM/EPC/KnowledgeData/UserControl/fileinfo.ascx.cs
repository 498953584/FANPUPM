using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class FileInfo : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			this.btnAdd.Attributes["onclick"] = "javascript:OpType()";
			this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
			if (!this.Page.IsPostBack)
			{
				Guid guid = new Guid(base.Session["DATUMCODE"].ToString());
				this.ViewState["OPTYPE"] = base.Request["optype"].ToString();
				if (base.Request.QueryString["optype"] != "ADD")
				{
					guid = new Guid(base.Request.QueryString["DatumCode"]);
				}
				if (base.Request.QueryString["optype"] == "SEE")
				{
					this.btnAdd.Attributes["disabled"] = "true;";
					this.btnAdd.CssClass = "button_addu";
				}
				this.ViewState["FILECODES"] = guid;
				this.GridBind(this.ViewState["FILECODES"].ToString());
			}
		}
		private void GridBind(string FileCode)
		{
			DataTable fileList = KnowledgeFileAction.GetFileList(new Guid(FileCode));
			this.lbFileCount.Text = "共" + fileList.Rows.Count.ToString() + "个文件";
			this.DataGrid1.DataSource = fileList;
			this.DataGrid1.DataBind();
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
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.GridBind(this.ViewState["FILECODES"].ToString());
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				string text = this.DataGrid1.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"doClick(this,'",
					this.DataGrid1.ClientID.ToString(),
					"');clickRow('",
					text,
					"','",
					e.Item.Cells[5].Text,
					"','",
					this.ViewState["OPTYPE"],
					"');"
				});
				e.Item.Attributes["ondblclick"] = "OpfinFile('" + e.Item.Cells[5].Text.Trim() + "')";
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["style"] = "cursor:hand";
			}
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			string text = KnowledgeFileUp.DelFile(this.hdnFilePath.Value.Trim());
			string a;
			if ((a = text) != null)
			{
				if (!(a == "1"))
				{
					if (!(a == "2"))
					{
						if (!(a == "3"))
						{
							return;
						}
						this.js.Text = "alert('文件删除失败')";
					}
					else
					{
						int num = KnowledgeFileAction.DleFile(this.hdnFileCode.Value.Trim());
						if (num != 1)
						{
							this.js.Text = "alert('文件删除成功，数据删除失败')";
							return;
						}
						this.GridBind(this.ViewState["FILECODES"].ToString());
						return;
					}
				}
				else
				{
					int num = KnowledgeFileAction.DleFile(this.hdnFileCode.Value.Trim());
					if (num != 1)
					{
						this.js.Text = "alert('文件不存在，数据删除失败')";
						return;
					}
					this.GridBind(this.ViewState["FILECODES"].ToString());
					return;
				}
			}
		}
	}


