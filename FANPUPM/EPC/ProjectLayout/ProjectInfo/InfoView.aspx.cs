using ASP;
using cn.justwin.stockBLL.PTPrjInfo;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class InfoView : BasePage, IRequiresSessionState
	{
		public string tpcode;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.tpcode = base.Request.QueryString["code"].ToString();
				PublicInterface.PTDBSJDelete(this.tpcode, "024");
				this.labTypeCode.Text = this.tpcode;
				this.bindPagedata(this.tpcode);
				this.lbllink.Text = this.FilesBind(18, this.tpcode);
			}
		}
		private void TextBind(PrjInfoModel PIM)
		{
			PTPrjInfoZTBBll pTPrjInfoZTBBll = new PTPrjInfoZTBBll();
			this.Literal1.Text = "------";
			DataTable dataTable = pTPrjInfoZTBBll.getDataTable(PIM.PrjGuid.ToString());
			if (dataTable != null && dataTable.Rows.Count > 0)
			{
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					this.Label1.Text = dataTable.Rows[i]["telephone"].ToString();
					this.Literal1.Text = ((dataTable.Rows[i]["grade"].ToString() == "") ? "------" : dataTable.Rows[i]["grade"].ToString());
					this.Label3.Text = dataTable.Rows[i]["businessman"].ToString();
					string text = dataTable.Rows[i]["businessman"].ToString();
					this.Label3.Text = text.Substring(text.LastIndexOf("-") + 1);
				}
			}
			this.labPrjcode.Text = PIM.PrjCode;
			this.labPrjname.Text = PIM.PrjName;
			this.labstarttime.Text = PIM.StartDate.ToString("yyyy-MM-dd");
			this.labendtime.Text = PIM.EndDate.ToString("yyyy-MM-dd");
			this.labPrjCost.Text = PIM.PrjCost;
			this.labgq.Text = PIM.Duration.ToString();
			this.labquclass.Text = PMAction.gettypeName(PIM.QualityClass, 13);
			this.labPrjkind.Text = PMAction.gettypeName(PIM.PrjKindClass, 3);
			this.labstata.Text = PMAction.gettypeName(PIM.PrjState, 7);
			this.labarea.Text = PMAction.gettypeName(PIM.Area, 19);
			this.LabRemark.Text = PIM.Remark;
			this.labOwner.Text = PIM.Owner;
			this.LabCounsellor.Text = PIM.Counsellor;
			this.LabDesigner.Text = PIM.Designer;
			this.LabInspector.Text = PIM.Inspector;
			this.Labprjinfo.Text = PIM.PrjInfo;
			this.LabPrjPlace.Text = PIM.PrjPlace;
			this.Labrank.Text = PMAction.gettypeName(PIM.Rank, 22);
			this.LabBudgetWay.Text = PMAction.gettypeName(PIM.BudgetWay, 23);
			this.LabContractWay.Text = PMAction.gettypeName(PIM.ContractWay, 24);
			this.LabPayCondition.Text = PMAction.gettypeName(PIM.PayCondition, 25);
			this.LabTenderWay.Text = PMAction.gettypeName(PIM.TenderWay, 26);
			this.LabPayWay.Text = PMAction.gettypeName(PIM.PayWay, 27);
			this.LabKeyPart.Text = PMAction.gettypeName(PIM.KeyPart, 28);
			this.LabWorkUnit.Text = PIM.WorkUnit;
			this.Lablinkman.Text = PIM.LinkMan;
			if (PIM.PrjManager.ToString().Length > 8)
			{
				this.Labmanager.Text = PIM.PrjManager.Substring(9);
			}
			this.LabBuildingType.Text = PMAction.gettypeName(PIM.BuildingType, 29);
			this.LabTotalHouseNum.Text = PIM.TotalHouseNum;
			this.LabBuildingArea.Text = PIM.BuildingArea;
			this.LabUsegrounArea.Text = PIM.UsegrounArea;
			this.LabUndergroundArea.Text = PIM.UndergroundArea;
			this.LabPrjFundInfo.Text = PIM.PrjFundInfo;
			this.LabOtherStatement.Text = PIM.OtherStatement;
			for (int j = 0; j < this.Controls.Count; j++)
			{
				foreach (Control control in this.Controls[j].Controls)
				{
					if (control is Label && (control as Label).Text == "")
					{
						(control as Label).Text = "------";
					}
				}
			}
		}
		public void bindPagedata(string typecode)
		{
			PrjInfoModel pIM = new PrjInfoModel();
			pIM = PrjInfoAction.GetSingleInfo(typecode.ToString());
			this.TextBind(pIM);
		}
		public string FilesBind(int moduleID, string recordCode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string sqlString = string.Concat(new string[]
			{
				"select * from XPM_Basic_AnnexList where (RecordCode = '",
				recordCode,
				"' and ModuleID = ",
				moduleID.ToString(),
				"  and state<>-1)"
			});
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			int arg_53_0 = dataTable.Rows.Count;
			foreach (DataRow dataRow in dataTable.Rows)
			{
				string text = string.Empty;
				text = dataRow["OriginalName"].ToString();
				text = string.Concat(new object[]
				{
					"<span class=\"link\" onclick=\"javascript:return openannexpage('",
					dataRow["RecordCode"],
					"',",
					dataRow["ModuleID"],
					")\" title=\"",
					text,
					"\">",
					text,
					"</span>"
				});
				stringBuilder.Append(text);
				stringBuilder.Append(", ");
			}
			string result = string.Empty;
			if (stringBuilder.Length > 2)
			{
				result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
			}
			return result;
		}
	}


