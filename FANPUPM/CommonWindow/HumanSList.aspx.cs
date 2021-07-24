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
	public partial class HumanSList : BasePage, IRequiresSessionState
	{
		private userManageDb Umdb = new userManageDb();
		protected static userManageDb UserAct
		{
			get
			{
				return new userManageDb();
			}
		}
		public int DeptID
		{
			get
			{
				object obj = this.ViewState["DEPTID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["DEPTID"] = value;
			}
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["pid"] == null)
				{
					this.DeptID = 0;
				}
				else
				{
					this.DeptID = System.Convert.ToInt32(base.Request["pid"]);
					this.bmdm.Value = base.Request["pid"].ToString();
					this.bmmc.Value = this.Umdb.depName(this.bmdm.Value);
				}
				this.LBoxHuman_Bind(this.DeptID);
				this.LBoxHuman.Attributes["onclick"] = "selOnPerson();";
				this.LBoxHuman.Attributes["ondblClick"] = "pickOnPerson(this);";
				this.JS.Text = "parent.document.getElementById('BtnOK').disabled = true;";
			}
		}
		protected override void OnInit(System.EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			base.Load += new System.EventHandler(this.Page_Load);
		}
		private void LBoxHuman_Bind(int deptiD)
		{
			DataSet dataSet = HumanSList.UserAct.DepUserQuaryForMail(deptiD.ToString());
			DataTable dataTable = dataSet.Tables[0];
			this.LBoxHuman.Items.Clear();
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				ListItem item = new ListItem(dataTable.Rows[i]["v_xm"].ToString(), dataTable.Rows[i]["v_yhdm"].ToString());
				this.LBoxHuman.Items.Add(item);
			}
		}
	}


