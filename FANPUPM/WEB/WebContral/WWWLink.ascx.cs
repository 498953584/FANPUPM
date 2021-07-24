using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
	public partial class WWWLink : System.Web.UI.UserControl
	{

		protected NewsAction na
		{
			get
			{
				return new NewsAction();
			}
		}
		private void Page_Load(object sender, EventArgs e)
		{
			this.DDLBind();
			this.DDLBrer.Attributes["onchange"] = "OpenWeb(this);";
			this.DDLWay.Attributes["onchange"] = "OpenWeb(this)";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new EventHandler(this.Page_Load);
		}
		private void DDLBind()
		{
			DataTable dataTable = this.na.EquipSel("1");
			for (int i = 1; i <= dataTable.Rows.Count; i++)
			{
				this.DDLBrer.Items.Add(new ListItem(dataTable.Rows[i - 1]["LinkName"].ToString(), dataTable.Rows[i - 1]["LinkUrL"].ToString()));
			}
			DataTable dataTable2 = this.na.EquipSel("2");
			for (int j = 1; j <= dataTable2.Rows.Count; j++)
			{
				this.DDLWay.Items.Add(new ListItem(dataTable2.Rows[j - 1]["LinkName"].ToString(), dataTable2.Rows[j - 1]["LinkUrL"].ToString()));
			}
		}
	}


