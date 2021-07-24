using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WebManagerListAbout : BasePage, IRequiresSessionState
	{
		private string where = "";

		protected string NewsName
		{
			get
			{
				return this.ViewState["NEWSNAME"].ToString();
			}
			set
			{
				this.ViewState["NEWSNAME"] = value;
			}
		}
		protected string NewsCode
		{
			get
			{
				return this.ViewState["NEWSCODE"].ToString();
			}
			set
			{
				this.ViewState["NEWSCODE"] = value;
			}
		}
		protected NewsAction na
		{
			get
			{
				return new NewsAction();
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["na"] == null || base.Request["cd"] == null)
				{
					this.JS.Text = "alert('参数错误!')";
					return;
				}
				this.NewsName = base.Request["na"].ToString();
				this.NewsCode = base.Request["cd"].ToString();
				this.LbNewsName.Text = this.NewsName;
				this.LbNewsName2.Text = this.NewsName;
				this.Btn_add.Attributes["onclick"] = string.Concat(new string[]
				{
					"return NewsInfo('add','",
					this.NewsName,
					"','",
					this.NewsCode,
					"');"
				});
				this.Btn_update.Attributes["onclick"] = string.Concat(new string[]
				{
					"return NewsInfo('update','",
					this.NewsName,
					"','",
					this.NewsCode,
					"');"
				});
				this.BtnSel.Attributes["onclick"] = string.Concat(new string[]
				{
					"return NewsInfo('sel','",
					this.NewsName,
					"','",
					this.NewsCode,
					"');"
				});
				this.Btn_del.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定新闻信息吗？')){return false;}";
				this.NewsDataBind("");
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.BtnSearch.Click += new EventHandler(this.BtnSearch_Click);
			this.Btn_add.Click += new EventHandler(this.Btn_add_Click);
			this.Btn_update.Click += new EventHandler(this.Btn_update_Click);
			this.Btn_del.Click += new EventHandler(this.Btn_del_Click);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void NewsDataBind(string where)
		{
			string text = "where c_xwlxdm = '" + this.NewsCode + "' and c_sfyx = 'y'";
			text += where;
			this.Dbg_item.DataSource = this.na.GetPageData(text);
			this.Dbg_item.DataBind();
		}
		private void Btn_del_Click(object sender, EventArgs e)
		{
			if (this.na.NewsDel(Convert.ToInt32(this.HdnNewsId.Value)) == 1)
			{
				this.JS.Text = "alert('删除成功!');";
				this.NewsDataBind("");
				return;
			}
			this.JS.Text = "alert('删除失败!');";
		}
		private void PageCtrl_PageIndexChange(object sender, EventArgs e)
		{
			this.NewsDataBind("");
		}
		private void BtnSearch_Click(object sender, EventArgs e)
		{
			if (this.TxtSelBt.Text.ToString() == "" && this.DbSelDate.Text.ToString() == "")
			{
				this.where = "";
			}
			else
			{
				if (this.TxtSelBt.Text.ToString() != "")
				{
					this.where = " and v_xwbt like '%" + this.TxtSelBt.Text.ToString() + "%'";
					if (this.DbSelDate.Text != "")
					{
						this.where = this.where + " and datediff(dd,dtm_fbsj,'" + this.DbSelDate.Text.ToString() + "')=0";
					}
				}
				else
				{
					if (this.DbSelDate.Text.ToString() != "")
					{
						this.where = " and datediff(dd,dtm_fbsj,'" + this.DbSelDate.Text.ToString() + "')=0";
					}
				}
			}
			this.NewsDataBind(this.where);
		}
		private void Btn_add_Click(object sender, EventArgs e)
		{
			this.NewsDataBind("");
		}
		private void Btn_update_Click(object sender, EventArgs e)
		{
			this.NewsDataBind("");
		}
		protected void Dbg_item_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowIndex != -1)
			{
				e.Row.Cells[0].Text = Convert.ToString(Convert.ToInt32(e.Row.RowIndex) + 1);
				System.Data.DataRowView dataRowView = (System.Data.DataRowView)e.Row.DataItem;
				e.Row.Attributes["onclick"] = string.Concat(new object[]
				{
					"OnRecord(this);NewsId('",
					e.Row.Cells[1].Text,
					"','",
					dataRowView["i_xw_id"],
					"')"
				});
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
			}
		}
		protected void Dbg_item_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.Dbg_item.PageIndex = e.NewPageIndex;
			this.NewsDataBind(this.where);
		}
	}


