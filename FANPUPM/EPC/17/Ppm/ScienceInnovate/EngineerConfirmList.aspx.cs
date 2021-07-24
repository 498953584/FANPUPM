using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_17_Ppm_ScienceInnovate_EngineerConfirmList : NBasePage, System.Web.SessionState.IRequiresSessionState
{
	private string _BigSort = "";
	private string _PrjCode = "";
	private string _SmallSort = "0";
	private string _Type = "";
	private string queryShowTitle = string.Empty;
	private FilesLogic flc = new FilesLogic();
	private static string temTable = "temtable";
	private static string resourceTable = "resourceTable";
	private static string table_name = "Prj_TechnologyManage";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.IsPostBack)
		{
			this._PrjCode = this.ViewState["PRJCODE"].ToString();
			this._BigSort = this.ViewState["LEVELS"].ToString();
			this._SmallSort = "0";
			this._Type = this.ViewState["TYPE"].ToString();
			return;
		}
		if (base.Request["PrjCode"] != null)
		{
			this.ViewState["PRJCODE"] = base.Request.QueryString["PrjCode"].ToString();
			this.ViewState["LEVELS"] = base.Request.QueryString["Levels"].ToString();
			this._PrjCode = this.ViewState["PRJCODE"].ToString();
			this._BigSort = this.ViewState["LEVELS"].ToString();
			this.ViewState["TYPE"] = base.Request.QueryString["Type"].ToString().Trim();
			this._Type = this.ViewState["TYPE"].ToString();
			this.HdnBigSort.Value = this._BigSort;
			this.HdnPrjCode.Value = this.ViewState["PRJCODE"].ToString();
			this._SmallSort = "0";
			if (this._BigSort == "7")
			{
				this.queryShowTitle = "查看工程确认单";
				this.LblTitle.Text = "工程确认单";
				this.HiddenField1.Value = "工程确认单";
				this.hfldIsAllowAudit.Value = "false";
				this.DGrdStandard.Columns[7].Visible = false;
			}
			else
			{
				if (this._BigSort == "8")
				{
					this.queryShowTitle = "查看工程洽商单";
					if (this._Type == "View")
					{
						this.LblTitle.Text = "工程洽商单查询";
					}
					else
					{
						this.LblTitle.Text = "工程洽商单";
					}
					this.HiddenField1.Value = "工程洽商单";
					this.hfldIsAllowAudit.Value = "true";
				}
			}
			if (this._Type == "View")
			{
				this.BtnAdd.Visible = false;
				this.BtnUpd.Visible = false;
				this.BtnDel.Visible = false;
			}
			this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
			this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
			this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
			this.BtnView.Attributes["onclick"] = "openEdit('View')";
			this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
			return;
		}
		this.BtnAdd.Enabled = false;
		this.Table1.Visible = false;
	}
	protected override void OnInit(EventArgs e)
	{
		DataTable list = this.flc.GetList("1<0");
		this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.temTable] = list.Clone();
		this.InitializeComponent();
		base.OnInit(e);
	}
	private void InitializeComponent()
	{
		this.DGrdStandard.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGrdStandard_ItemDataBound);
	}
	private void MeasureBind(string PrjCode, string bigsort, string smallsort)
	{
		DataTable measureList = MeasureDataAction.GetMeasureList(PrjCode, bigsort, smallsort);
		this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.resourceTable] = measureList;
		this.DGrdStandard.DataSource = measureList;
		this.DGrdStandard.DataBind();
	}
	protected void BtnAdd_Click(object sender, EventArgs e)
	{
		this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
	}
	protected void BtnUpd_Click(object sender, EventArgs e)
	{
		this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
	}
	protected void DGrdStandard_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
	{
		this.DGrdStandard.CurrentPageIndex = e.NewPageIndex;
		this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
	}
	protected void BtnDel_Click(object sender, EventArgs e)
	{
		string text = string.Empty;
		if (this.HdnId.Value.Contains("["))
		{
			List<string> list = new List<string>();
			list = JsonHelper.GetListFromJson(this.HdnId.Value);
			foreach (string current in list)
			{
				text = text + current + ',';
			}
			text = text.Substring(0, text.Length - 1);
		}
		else
		{
			text = this.HdnId.Value;
		}
		int num = MeasureDataAction.MeasureDel(text);
		if (num == 1)
		{
			base.RegisterScript("top.ui.show('删除成功！')");
			this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
			return;
		}
		if (num == 0)
		{
			base.RegisterScript("top.ui.show('删除失败！')");
		}
	}
	public DataTable GetAnnex(string Id)
	{
		string sqlString = "select OriginalName,FilePath,AnnexName from XPM_Basic_AnnexList where ModuleID=1726 and RecordCode='26" + Id + "'";
		return publicDbOpClass.DataTableQuary(sqlString);
	}
	private void DGrdStandard_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
	{
		if (e.Item.ItemIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Item.DataItem;
			e.Item.Attributes["id"] = dataRowView["ID"].ToString();
			e.Item.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);clickRow('",
				this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString(),
				"','",
				this._Type,
				"');"
			});
			e.Item.Attributes["style"] = "cursor:hand";
			e.Item.Attributes["mark"] = dataRowView["mark"].ToString();
			System.Web.UI.WebControls.Label label = e.Item.Cells[0].FindControl("Label3") as System.Web.UI.WebControls.Label;
			label.Attributes["style"] = "display:none";
			e.Item.Attributes["prjGuid"] = ((HiddenField)e.Item.FindControl("hfldPrjCode")).Value;
			e.Item.Attributes["guid"] = ((HiddenField)e.Item.FindControl("hfldGuid")).Value;
			System.Web.UI.WebControls.Label label2 = e.Item.Cells[4].FindControl("Label1") as System.Web.UI.WebControls.Label;
			this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString();
			label2.Attributes["onclick"] = string.Concat(new object[]
			{
				"rowQuery('/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&Id=",
				this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString(),
				"&pc=",
				this.ViewState["PRJCODE"],
				"&bs=",
				this.HdnBigSort.Value,
				"&sm=0','",
				this.queryShowTitle,
				"')"
			});
			label2.ToolTip = dataRowView["ItemName"].ToString();
			if (this._Type == "View")
			{
				e.Item.Cells[0].Visible = false;
				return;
			}
		}
		else
		{
			if (this._Type == "View")
			{
				e.Item.Cells[0].Visible = false;
			}
		}
	}
	public string FilesBind(string recordCode)
	{
		int num = 1726;
		StringBuilder stringBuilder = new StringBuilder();
		string sqlString = string.Concat(new string[]
		{
			"select * from XPM_Basic_AnnexList where (RecordCode = '",
			recordCode,
			"' and ModuleID = ",
			num.ToString(),
			"  and state<>-1)"
		});
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		int arg_59_0 = dataTable.Rows.Count;
		foreach (DataRow dataRow in dataTable.Rows)
		{
			stringBuilder.Append(dataRow["OriginalName"].ToString());
			stringBuilder.Append(", ");
		}
		string result = string.Empty;
		if (stringBuilder.Length > 2)
		{
			result = stringBuilder.ToString().Substring(0, stringBuilder.Length - 2);
		}
		return result;
	}
	public string GetConModifyName(int TechnologyId)
	{
		string result = string.Empty;
		if (TechnologyId > 0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT cim.ChangeCode,cim.ID,cim.ContractID FROM Prj_TechnologyManage cimt ").Append(" ");
			stringBuilder.Append("LEFT JOIN Con_IncomeModify_Technology cimt2 ON cimt2.TechnologyId=cimt.ID").Append(" ");
			stringBuilder.Append("LEFT JOIN Con_Incomet_Modify cim ON cim.ID=cimt2.ConModifyId").Append(" ");
			stringBuilder.Append("WHERE cimt.ID=" + TechnologyId + ";");
			DataTable dataTable = publicDbOpClass.DataTableQuary(stringBuilder.ToString());
			int count = dataTable.Rows.Count;
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < count; i++)
			{
				stringBuilder2.Append("<a href=\"#\" class=\"al\"");
				stringBuilder2.Append(string.Concat(new string[]
				{
					"onclick=\"rowQueryA('/ContractManage/IncometModify/AddIncometModify.aspx?backurl=ECL&t=1&id=",
					dataTable.Rows[i]["ID"].ToString(),
					"&contractId=",
					dataTable.Rows[i]["ContractID"].ToString(),
					"')\">"
				}));
				stringBuilder2.Append(dataTable.Rows[i]["ChangeCode"].ToString());
				stringBuilder2.Append("</a>,");
			}
			if (!string.IsNullOrEmpty(stringBuilder2.ToString()))
			{
				stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
			}
			result = stringBuilder2.ToString();
		}
		return result;
	}
	protected void btnFiles_Click(object sender, EventArgs e)
	{
		this.GET_SelectRow();
		string text = string.Empty;
		if (this.ViewState["i_id"] != null)
		{
			text = this.ViewState["i_id"].ToString();
		}
		if (text != "")
		{
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				DatumLogic datumLogic = new DatumLogic();
				try
				{
					if (datumLogic.UpdateByID(EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.table_name, 1, "ID", text, sqlTransaction))
					{
						string[] array = text.Split(new char[]
						{
							','
						});
						if (array.Length > 0)
						{
							for (int i = 0; i < array.Length; i++)
							{
								if (!string.IsNullOrEmpty(array[i].ToString()))
								{
									if (this.flc.Exists(array[i].ToString(), sqlTransaction))
									{
										this.flc.Update(array[i].ToString(), 1, EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.table_name, sqlTransaction);
									}
									else
									{
										if (this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.temTable] != null)
										{
											DataTable dataTable = this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.temTable] as DataTable;
											foreach (DataRow dr in dataTable.Rows)
											{
												filesModel filesModel = new filesModel();
												filesModel = this.flc.getModelByRow(dr);
												if (filesModel.file_sid == array[i].ToString())
												{
													this.flc.Add(filesModel, sqlTransaction);
												}
											}
										}
									}
								}
							}
						}
						sqlTransaction.Commit();
						base.RegisterScript("top.ui.show('操作成功！')");
						this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
					}
				}
				catch (Exception ex)
				{
					ex.ToString();
					sqlTransaction.Rollback();
					sqlConnection.Close();
					base.RegisterScript("top.ui.show('操作失败！');");
				}
			}
		}
	}
	private void GET_SelectRow()
	{
		StringBuilder stringBuilder = new StringBuilder();
		DataTable dataTable = this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.temTable] as DataTable;
		dataTable.Clear();
		foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.DGrdStandard.Items)
		{
			System.Web.UI.WebControls.CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as System.Web.UI.WebControls.CheckBox;
			if (checkBox != null && checkBox.Checked)
			{
				DataRow dataRow = dataTable.NewRow();
				System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)dataGridItem.Cells[0].FindControl("Label3");
				string text = label.Text;
				if (this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.resourceTable] != null)
				{
					DataTable dataTable2 = this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.resourceTable] as DataTable;
					DataRow[] array = dataTable2.Select("ID='" + text + "'");
					DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["ID"].ToString() + "'");
					if (array2.Length == 0)
					{
						dataRow["ID"] = Guid.NewGuid();
						dataRow["file_sid"] = array[0]["ID"].ToString();
						dataRow["file_mark"] = 1;
						dataRow["file_name"] = array[0]["ItemName"].ToString();
						dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
						dataRow["Original_table"] = EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.table_name;
						dataRow["sid_ColumnName"] = "ID";
						dataRow["sid_ColumnType"] = 1;
						dataRow["prjcode"] = ((array[0]["PrjCode"].ToString() == "") ? "" : array[0]["PrjCode"].ToString());
						dataTable.Rows.Add(dataRow);
						stringBuilder.Append(array[0]["ID"].ToString() + ",");
					}
				}
				this.ViewState["i_id"] = stringBuilder.ToString();
			}
		}
		this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.temTable] = dataTable;
	}
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		DataTable dataTable = this.ViewState[EPC_17_Ppm_ScienceInnovate_EngineerConfirmList.resourceTable] as DataTable;
		DataTable dataTable2 = new DataTable();
		dataTable2 = dataTable.Clone();
		StringBuilder stringBuilder = new StringBuilder();
		if (this.TextBox1.Text.Trim().ToString() != "")
		{
			stringBuilder.Append("SerialNumber like '%").Append(this.TextBox1.Text.Trim().ToString()).Append("%'");
			if (this.TextBox2.Text.Trim().ToString() != "")
			{
				stringBuilder.Append(" and");
				stringBuilder.Append(" ItemName like '%").Append(this.TextBox2.Text.Trim().ToString()).Append("%'");
			}
		}
		else
		{
			if (this.TextBox2.Text.Trim().ToString() != "")
			{
				stringBuilder.Append(" ItemName like '%").Append(this.TextBox2.Text.Trim().ToString()).Append("%'");
			}
		}
		if (dataTable.Rows.Count > 0)
		{
			DataRow[] array = dataTable.Select(stringBuilder.ToString(), "ID DESC");
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row = array2[i];
				dataTable2.ImportRow(row);
			}
		}
		this.DGrdStandard.DataSource = dataTable2;
		this.DGrdStandard.DataBind();
	}
}


