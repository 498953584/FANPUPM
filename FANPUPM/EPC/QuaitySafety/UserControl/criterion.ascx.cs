using ASP;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class Criterion : System.Web.UI.UserControl
	{

		public string Head
		{
			get
			{
				return this.LblHead.Text;
			}
			set
			{
				this.LblHead.Text = value;
			}
		}
		public string CriterionName
		{
			get
			{
				return this.TxtCriterionName.Text;
			}
			set
			{
				this.TxtCriterionName.Text = value;
			}
		}
		public string Publisher
		{
			get
			{
				return this.TxtPublisher.Text;
			}
			set
			{
				this.TxtPublisher.Text = value;
			}
		}
		public string Yhmc
		{
			get
			{
				return this.PublisherYhmc.Text;
			}
			set
			{
				this.PublisherYhmc.Text = value;
			}
		}
		public string Remark
		{
			get
			{
				return this.TxtRemark.Text;
			}
			set
			{
				this.TxtRemark.Text = value;
			}
		}
		public string ClassName
		{
			get
			{
				return this.txtClass.Text;
			}
			set
			{
				this.txtClass.Text = value;
			}
		}
		public bool Va
		{
			get
			{
				return this.IMG1.Visible;
			}
			set
			{
				this.IMG1.Visible = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.LblHead.Text = this.Head;
				this.TxtCriterionName.Text = this.CriterionName;
				this.TxtPublisher.Text = this.Publisher;
				this.TxtRemark.Text = this.Remark;
				this.PublisherYhmc.Text = this.Yhmc;
				this.txtClass.Text = this.ClassName;
				this.IMG1.Visible = this.Va;
				this.IMG2.Visible = this.Va;
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


