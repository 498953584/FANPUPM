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
	public partial class ClassAdd : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack && base.Request.QueryString["optype"] != "ADD")
			{
				KnowledgeTypeModel singleType = KnowledgeTypeAction.GetSingleType(base.Request.QueryString["ID"]);
				this.GetValueFromTable(singleType);
			}
		}
		private void GetValueFromTable(KnowledgeTypeModel ktm)
		{
			this.txtTypeName.Text = ktm.TypeName;
			this.TxtRemark.Text = ktm.Remark;
			this.rbdIsFixup_y.Checked = Convert.ToBoolean(ktm.IsFixup);
			this.rdbIsDelete_y.Checked = Convert.ToBoolean(ktm.isDelete);
			this.rdbIsValid_y.Checked = Convert.ToBoolean(ktm.IsValid);
			this.rdbIsVisible_y.Checked = Convert.ToBoolean(ktm.IsVisible);
			this.rdbIsVisible_n.Checked = !Convert.ToBoolean(ktm.IsVisible);
			this.rbdIsFixup_n.Checked = !Convert.ToBoolean(ktm.IsFixup);
			this.rdbIsDelete_n.Checked = !Convert.ToBoolean(ktm.isDelete);
			this.rdbIsValid_n.Checked = !Convert.ToBoolean(ktm.IsValid);
		}
		private KnowledgeTypeModel GetValueFromText()
		{
			KnowledgeTypeModel knowledgeTypeModel = new KnowledgeTypeModel();
			knowledgeTypeModel.TypeName = this.txtTypeName.Text.Trim();
			knowledgeTypeModel.Remark = this.TxtRemark.Text.Trim();
			knowledgeTypeModel.ParentId = Convert.ToInt32(base.Request.QueryString["ParId"]);
			if (this.rbdIsFixup_y.Checked)
			{
				knowledgeTypeModel.IsFixup = "1";
			}
			else
			{
				knowledgeTypeModel.IsFixup = "0";
			}
			if (this.rdbIsDelete_y.Checked)
			{
				knowledgeTypeModel.isDelete = "1";
			}
			else
			{
				knowledgeTypeModel.isDelete = "0";
			}
			if (this.rdbIsValid_y.Checked)
			{
				knowledgeTypeModel.IsValid = "1";
			}
			else
			{
				knowledgeTypeModel.IsValid = "0";
			}
			if (this.rdbIsVisible_y.Checked)
			{
				knowledgeTypeModel.IsVisible = "1";
			}
			else
			{
				knowledgeTypeModel.IsVisible = "0";
			}
			if (base.Request.QueryString["optype"] != "ADD")
			{
				knowledgeTypeModel.TypeId = Convert.ToInt32(base.Request.QueryString["ID"]);
			}
			return knowledgeTypeModel;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			KnowledgeTypeModel knowledgeTypeModel = new KnowledgeTypeModel();
			knowledgeTypeModel = this.GetValueFromText();
			if (!ClassAdd.Exists(knowledgeTypeModel.TypeName, knowledgeTypeModel.ParentId))
			{
				base.RegisterScript("top.ui.alert('类型名称已存在');");
				return;
			}
			if (base.Request.QueryString["optype"] == "ADD")
			{
				if (KnowledgeTypeAction.AddNewType(knowledgeTypeModel) != 1)
				{
					base.RegisterScript("top.ui.alert('保存失败');");
					return;
				}
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_knowlegedatatype' });");
				return;
			}
			else
			{
				if (KnowledgeTypeAction.UpdateType(knowledgeTypeModel) != 1)
				{
					base.RegisterScript("top.ui.alert('修改失败');");
					return;
				}
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_knowlegedatatype' });");
				return;
			}
		}
		public static bool Exists(string val, int pid)
		{
			string sqlString = string.Concat(new object[]
			{
				"SELECT * FROM EPM_Datum_Class edc WHERE edc.TypeName='",
				val,
				"' AND Parentid=",
				pid
			});
			bool result;
			if (val != "")
			{
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				result = (dataTable.Rows.Count <= 0);
			}
			else
			{
				result = false;
			}
			return result;
		}
	}


