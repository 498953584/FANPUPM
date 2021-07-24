using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ResourceBindList : PageBase, IRequiresSessionState
	{

		protected int noteid
		{
			get
			{
				return Convert.ToInt32(this.ViewState["NOTEID"]);
			}
			set
			{
				this.ViewState["NOTEID"] = value;
			}
		}
		protected string resourcename
		{
			get
			{
				return this.ViewState["RESOURCENAME"].ToString();
			}
			set
			{
				this.ViewState["RESOURCENAME"] = value;
			}
		}
		protected int resourcestyle
		{
			get
			{
				return Convert.ToInt32(this.ViewState["resourcestyle"]);
			}
			set
			{
				this.ViewState["resourcestyle"] = value;
			}
		}
		protected ResourceBindAction rba
		{
			get
			{
				return new ResourceBindAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["ni"] == null || base.Request["rn"] == null || base.Request["rs"] == null)
				{
					this.JS.Text = "alert('参数错误!');";
					return;
				}
				this.noteid = Convert.ToInt32(base.Request["ni"]);
				this.resourcename = base.Request["rn"].ToString();
				this.resourcestyle = Convert.ToInt32(base.Request["rs"]);
				this.ResourceListBind();
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgd_List.ItemDataBound += new DataGridItemEventHandler(this.dgd_List_ItemDataBound);
		}
		private void ResourceListBind()
		{
			this.dgd_List.DataSource = this.rba.ResourceBindList(this.resourcename, this.resourcestyle);
			this.dgd_List.DataBind();
		}
		private void dgd_List_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgd_List.ClientID.ToString(),
					"');dorows('",
					e.Item.Cells[1].Text,
					"','",
					e.Item.Cells[6].Text,
					"');"
				});
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				string text;
				if ((text = e.Item.Cells[5].Text) != null)
				{
					if (text == "1")
					{
						e.Item.Cells[5].Text = "人工费";
						return;
					}
					if (text == "2")
					{
						e.Item.Cells[5].Text = "材料费";
						return;
					}
					if (!(text == "3"))
					{
						return;
					}
					e.Item.Cells[5].Text = "机械费";
				}
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (this.rba.RsourceCodeUp(this.resourcename, this.HdnResourceCode.Value.ToString(), this.HdnResourceStyle.Value.ToString()) == 1)
			{
				this.JS.Text = "alert('绑定成功!');window.returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('绑定失败!');";
		}
		protected void BtnSearch_Click(object sender, EventArgs e)
		{
			this.dgd_List.DataSource = this.rba.ResourceBindList(this.TxtResourceName.Text, this.TxtResourceCode.Text);
			this.dgd_List.DataBind();
		}
	}


