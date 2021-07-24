using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CorpTypeTree : PageBase, IRequiresSessionState
	{

		private static CodingAction CodingAct
		{
			get
			{
				return new CodingAction();
			}
		}
		private ContactCorpAction Cont
		{
			get
			{
				return new ContactCorpAction();
			}
		}
		public string TypeList
		{
			get
			{
				object obj = this.ViewState["TYPELIST"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["TYPELIST"] = value;
			}
		}
		public string WindowType
		{
			get
			{
				object obj = this.ViewState["WINDOWTYPE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["WINDOWTYPE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["w"] == null || base.Request["ts"] == null)
				{
					this.JS.Text = "alert('参数错误！');window.close;";
					return;
				}
				this.WindowType = base.Request["w"];
				this.TVType.Nodes.Clear();
				TreeNode treeNode = new TreeNode();
				treeNode.Text = "往来单位";
				treeNode.Value = "";
				treeNode.NavigateUrl = "/EPC/UserControl1/webNullTS.aspx";
				treeNode.Target = "FraCorpList";
				this.TVType.Nodes.Add(treeNode);
				if (base.Request["ts"].Trim() == "")
				{
					this.TypeList = "";
					this.TVType_Create(treeNode, this.GetDataFromCollection(CorpTypeTree.CodingAct.QueryCodeInfoList(Convert.ToInt32(BasicType.CorpType), ValidState.Valid)), 0);
					return;
				}
				this.TypeList = base.Request["ts"];
				this.TVType_Create(treeNode, this.GetDataFromCollection(CorpTypeTree.CodingAct.QueryCorpTypeList(this.TypeList)), 0);
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
		private DataTable GetDataFromCollection(CodingInfoCollection colt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("CodeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("ParentCodeID", typeof(int)));
			dataTable.Columns.Add(new DataColumn("CodeName", typeof(string)));
			dataTable.Columns.Add(new DataColumn("ChildNumber", typeof(int)));
			int count = colt.Count;
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["CodeID"] = colt[i].CodeID.ToString();
				dataRow["ParentCodeID"] = colt[i].ParentCodeID.ToString();
				dataRow["CodeName"] = colt[i].CodeName;
				dataRow["ChildNumber"] = colt[i].ChildNumber;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
		private void TVType_Create(TreeNode tns, DataTable dt, int parentID)
		{
			DataRow[] array = dt.Select("ParentCodeID=" + parentID.ToString(), "CodeID ASC");
			int num = array.Length;
			for (int i = 0; i < num; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Value = array[i]["CodeID"].ToString();
				treeNode.Text = array[i]["CodeName"].ToString();
				treeNode.NavigateUrl = string.Concat(new string[]
				{
					"ContactCorpList.aspx?cti=",
					array[i]["CodeID"].ToString(),
					"&w=",
					this.WindowType,
					"&Audit=",
					base.Request["Audit"].ToString()
				});
				treeNode.Target = "FraCorpList";
				tns.ChildNodes.Add(treeNode);
				if ((int)array[i]["ChildNumber"] > 0)
				{
					this.TVType_Create(treeNode, dt, (int)array[i]["CodeID"]);
				}
			}
		}
	}


