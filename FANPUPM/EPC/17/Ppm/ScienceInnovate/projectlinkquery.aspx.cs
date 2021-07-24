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
	public partial class ProjectLinkQuery : NBasePage, System.Web.SessionState.IRequiresSessionState
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
				if (this._BigSort == "3")
				{
					this.queryShowTitle = "查看工程联系单";
					this.HiddenField1.Value = "工程联系单";
					if (this._Type == "View")
					{
						this.LblTitle.Text = "工程联系单查询";
					}
					else
					{
						this.LblTitle.Text = "工程联系单";
					}
					this.hfldIsAllowAudit.Value = "true";
					this.hfldBigSort.Value = this._BigSort;
					this.DGrdStandard.Columns[8].Visible = false;
					this.DGrdStandard.Columns[9].Visible = false;
					this.DGrdStandard.Columns[7].Visible = true;
				}
				else
				{
					if (this._BigSort == "4")
					{
						this.queryShowTitle = "查看设计变更资料";
						this.HiddenField1.Value = "设计变更资料";
						if (this._Type == "View")
						{
							this.LblTitle.Text = "工程变更查询";
						}
						else
						{
							this.LblTitle.Text = "工程变更资料管理";
						}
						this.hfldIsAllowAudit.Value = "true";
						this.hfldBigSort.Value = this._BigSort;
						this.DGrdStandard.Columns[8].HeaderText = "";
						this.DGrdStandard.Columns[9].HeaderText = "";
						this.DGrdStandard.Columns[8].Visible = false;
						this.DGrdStandard.Columns[9].Visible = false;
						this.DGrdStandard.Columns[7].Visible = true;
					}
					else
					{
						if (this._BigSort == "5")
						{
							this.queryShowTitle = "查看中间交接资料";
							this.HiddenField1.Value = "中间交接资料";
							if (this._Type == "View")
							{
								this.LblTitle.Text = "中间交接资料查询";
							}
							else
							{
								this.LblTitle.Text = "中间交接资料";
							}
							this.hfldIsAllowAudit.Value = "false";
							this.hfldBigSort.Value = "";
							this.tdP.Visible = true;
							this.tdJ.Visible = true;
							this.tdN.Visible = true;
							this.tdR.Visible = true;
						}
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
			this.ViewState[ProjectLinkQuery.temTable] = list.Clone();
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
			this.ViewState[ProjectLinkQuery.resourceTable] = measureList;
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
				base.RegisterScript("top.ui.show('删除成功');");
				this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
				return;
			}
			if (num == 0)
			{
				base.RegisterScript("top.ui.alert('删除失败！'); ");
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
				System.Web.UI.WebControls.Label label2 = e.Item.Cells[4].FindControl("Label1") as System.Web.UI.WebControls.Label;
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
				e.Item.Attributes["prjGuid"] = ((HiddenField)e.Item.FindControl("hfldPrjCode")).Value;
				e.Item.Attributes["guid"] = ((HiddenField)e.Item.FindControl("hfldGuid")).Value;
				if (e.Item.Cells[5].Text.ToString().Length > 20)
				{
					e.Item.Cells[5].Text = e.Item.Cells[5].Text.ToString().Substring(0, 20) + "…";
					e.Item.Cells[5].ToolTip = e.Item.Cells[5].Text.ToString();
				}
				else
				{
					e.Item.Cells[5].Text = e.Item.Cells[5].Text;
					e.Item.Cells[5].ToolTip = e.Item.Cells[5].Text.ToString();
				}
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
						if (datumLogic.UpdateByID(ProjectLinkQuery.table_name, 1, "ID", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, ProjectLinkQuery.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[ProjectLinkQuery.temTable] != null)
											{
												DataTable dataTable = this.ViewState[ProjectLinkQuery.temTable] as DataTable;
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
							base.RegisterScript("top.ui.show('操作成功');");
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
			DataTable dataTable = this.ViewState[ProjectLinkQuery.temTable] as DataTable;
			dataTable.Clear();
			foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.DGrdStandard.Items)
			{
				System.Web.UI.WebControls.CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as System.Web.UI.WebControls.CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					System.Web.UI.WebControls.Label label = (System.Web.UI.WebControls.Label)dataGridItem.Cells[0].FindControl("Label3");
					string text = label.Text;
					if (this.ViewState[ProjectLinkQuery.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[ProjectLinkQuery.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("ID='" + text + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["ID"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["ID"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["ItemName"].ToString();
							dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
							dataRow["Original_table"] = ProjectLinkQuery.table_name;
							dataRow["sid_ColumnName"] = "ID";
							dataRow["sid_ColumnType"] = 1;
							dataRow["prjCode"] = this._PrjCode;
							dataTable.Rows.Add(dataRow);
							stringBuilder.Append(array[0]["ID"].ToString() + ",");
						}
					}
					this.ViewState["i_id"] = stringBuilder.ToString();
				}
			}
			this.ViewState[ProjectLinkQuery.temTable] = dataTable;
		}
		protected void btnQuery_Click(object sender, EventArgs e)
		{
			DataTable dataTable = this.ViewState[ProjectLinkQuery.resourceTable] as DataTable;
			for (int i = 0; i < dataTable.Rows.Count; i++)
			{
				if (!string.IsNullOrEmpty(dataTable.Rows[i]["JoinPerson"].ToString()) && !string.IsNullOrEmpty(WebUtil.GetUserNames(dataTable.Rows[i]["JoinPerson"].ToString())))
				{
					dataTable.Rows[i]["JoinPerson"] = WebUtil.GetUserNames(dataTable.Rows[i]["JoinPerson"].ToString());
				}
				if (!string.IsNullOrEmpty(dataTable.Rows[i]["ReceivePerson"].ToString()) && !string.IsNullOrEmpty(WebUtil.GetUserNames(dataTable.Rows[i]["ReceivePerson"].ToString())))
				{
					dataTable.Rows[i]["ReceivePerson"] = WebUtil.GetUserNames(dataTable.Rows[i]["ReceivePerson"].ToString());
				}
			}
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
			if (!string.IsNullOrEmpty(this.txtJoin.Text))
			{
				stringBuilder.Append(" JoinPerson like '%").Append(this.txtJoin.Text).Append("%'");
			}
			if (!string.IsNullOrEmpty(this.txtReceive.Text))
			{
				stringBuilder.Append(" ReceivePerson like'%").Append(this.txtReceive.Text).Append("%'");
			}
			if (dataTable.Rows.Count > 0)
			{
				DataRow[] array = dataTable.Select(stringBuilder.ToString(), "ID DESC");
				DataRow[] array2 = array;
				for (int j = 0; j < array2.Length; j++)
				{
					DataRow row = array2[j];
					dataTable2.ImportRow(row);
				}
			}
			this.DGrdStandard.DataSource = dataTable2;
			this.DGrdStandard.DataBind();
		}
	}


