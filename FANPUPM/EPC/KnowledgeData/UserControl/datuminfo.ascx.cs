using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumInfo : System.Web.UI.UserControl
	{

		public string txtTitle
		{
			get
			{
				return this.TxtTitle.Text;
			}
			set
			{
				this.TxtTitle.Text = value;
			}
		}
		public string ddlClass
		{
			get
			{
				return this.DDL_Class.SelectedValue;
			}
			set
			{
				this.DDL_Class.SelectedValue = value;
			}
		}
		public string Viscera
		{
			get
			{
				return this.TxtText.Text;
			}
			set
			{
				this.TxtText.Text = value;
			}
		}
		public bool boolVisable
		{
			get
			{
				return this.rb_y.Checked;
			}
			set
			{
				this.rb_y.Checked = value;
			}
		}
		public bool boolVisable1
		{
			get
			{
				return this.rb_n.Checked;
			}
			set
			{
				this.rb_n.Checked = value;
			}
		}
		public string V_xm
		{
			get
			{
				return this.LabAuthor.Text;
			}
			set
			{
				this.LabAuthor.Text = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ViewState["PRJCODE"] = base.Request.QueryString["PrjCode"];
				this.ViewState["DAYUMCODE"] = base.Request.QueryString["DatumCode"];
				this.ViewState["TYPEID"] = base.Request.QueryString["TypeId"];
				if (base.Request.QueryString["optype"] == "ADD")
				{
					this.DDLBind(this.ViewState["TYPEID"].ToString());
					return;
				}
				this.DDLBind(this.ddlClass);
				this.TxtTitle.Text = this.txtTitle;
				this.LabAuthor.Text = this.V_xm;
				this.TxtText.Text = this.Viscera;
				this.rb_y.Checked = this.boolVisable;
				this.rb_n.Checked = this.boolVisable1;
			}
		}
		private void DDLBind(string TypeId)
		{
			DataTable type = KnowledgeTypeAction.GetType("");
			this.DDL_Class.DataSource = type;
			this.DDL_Class.DataTextField = "TypeName";
			this.DDL_Class.DataValueField = "TypeId";
			this.DDL_Class.DataBind();
			if (TypeId.Trim() != "")
			{
				this.DDL_Class.SelectedValue = TypeId;
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


