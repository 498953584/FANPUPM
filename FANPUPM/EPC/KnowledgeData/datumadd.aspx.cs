using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class DatumAdd : PageBase, IRequiresSessionState
	{
		private DatumInfo DI;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.DI = (DatumInfo)this.MPTabDatum.FindControl("DatumInfo");
				if (base.Request.QueryString["optype"] == "ADD")
				{
					this.Session["DATUMCODE"] = Guid.NewGuid();
					this.DI.V_xm = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, this.Session["yhdm"].ToString().Trim());
				}
				else
				{
					if (base.Request.QueryString["optype"] == "SEE")
					{
						this.Btn_Save.Visible = false;
					}
					this.Session["DATUMCODE"] = base.Request.QueryString["DatumCode"];
					this.GetValueFromTable();
				}
				this.hdnCode.Value = this.Session["DATUMCODE"].ToString();
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
		private void GetValueFromTable()
		{
			KnowledgeDataModel knowledgeDataModel = new KnowledgeDataModel();
			knowledgeDataModel = KnowledgeDataAction.GetSingleValue(new Guid(this.Session["DATUMCODE"].ToString()));
			this.DI = (DatumInfo)this.MPTabDatum.FindControl("DatumInfo");
			this.DI.Viscera = knowledgeDataModel.Viscera;
			this.DI.boolVisable = Convert.ToBoolean(knowledgeDataModel.IsValid);
			this.DI.boolVisable1 = !Convert.ToBoolean(knowledgeDataModel.IsValid);
			this.DI.txtTitle = knowledgeDataModel.DatumName;
			this.DI.V_xm = com.jwsoft.pm.entpm.PageHelper.QueryUser(this, knowledgeDataModel.Author.Trim());
			this.DI.ddlClass = knowledgeDataModel.TypeId.ToString();
		}
		private KnowledgeDataModel GetValueFromText()
		{
			KnowledgeDataModel knowledgeDataModel = new KnowledgeDataModel();
			this.DI = (DatumInfo)this.MPTabDatum.FindControl("DatumInfo");
			knowledgeDataModel.KnowledgeCode = new Guid(this.Session["DATUMCODE"].ToString());
			knowledgeDataModel.ProjectCode = new Guid(base.Request.QueryString["PrjCode"].ToString());
			knowledgeDataModel.AuditingDate = DateTime.Now;
			knowledgeDataModel.AddDate = DateTime.Now;
			knowledgeDataModel.UpdateDate = DateTime.Now;
			knowledgeDataModel.IsAuditing = "1";
			knowledgeDataModel.Viscera = this.DI.Viscera;
			knowledgeDataModel.TypeId = Convert.ToInt32(this.DI.ddlClass);
			if (this.DI.boolVisable)
			{
				knowledgeDataModel.IsValid = "1";
			}
			else
			{
				knowledgeDataModel.IsValid = "0";
			}
			knowledgeDataModel.DatumName = this.DI.txtTitle;
			knowledgeDataModel.Author = this.Session["yhdm"].ToString();
			return knowledgeDataModel;
		}
		protected void Btn_Save_Click(object sender, EventArgs e)
		{
			KnowledgeDataModel kdm = new KnowledgeDataModel();
			kdm = this.GetValueFromText();
			if (base.Request.QueryString["optype"] == "ADD")
			{
				if (KnowledgeDataAction.AddNewData(kdm) != 1)
				{
					this.js.Text = "alert('增加失败！');";
					return;
				}
				this.js.Text = "alert('增加成功！');window.returnValue=true;window.close();";
				return;
			}
			else
			{
				if (KnowledgeDataAction.UpdateData(kdm) != 1)
				{
					this.js.Text = "alert('修改失败！');";
					return;
				}
				this.js.Text = "alert('修改成功！');window.returnValue=true;window.close();";
				return;
			}
		}
	}


