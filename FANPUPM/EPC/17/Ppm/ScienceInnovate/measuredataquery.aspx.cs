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
	public partial class MeasureDataQuery : NBasePage, IRequiresSessionState
	{
		private string _SmallSort = "";
		private string _PrjCode = "";
		private string _BigSort = "";
		private string _Levels = "";
		private string queryShowTitle = string.Empty;
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "Prj_TechnologyManage";
		private string _Type = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.Session["PRJCODE"].ToString().Trim() != "")
				{
					this.ViewState["PRJCODE"] = this.Session["PRJCODE"].ToString();
					this.ViewState["SMALLSORT"] = base.Request.QueryString["SmallSort"].ToString();
					this.ViewState["BIGSORT"] = base.Request.QueryString["BigSort"].ToString();
					this.HdnPrjCode.Value = this.ViewState["PRJCODE"].ToString();
					this.HdnSmallSort.Value = this.ViewState["SMALLSORT"].ToString();
					this.HdnBigSort.Value = this.ViewState["BIGSORT"].ToString();
					if (this.HdnPrjCode.Value.Trim() != "")
					{
						this._PrjCode = this.ViewState["PRJCODE"].ToString();
						this._SmallSort = this.ViewState["SMALLSORT"].ToString();
						this._BigSort = this.ViewState["BIGSORT"].ToString();
						if (this._BigSort == "1")
						{
							this.queryShowTitle = "查看测量资料";
							if (this._SmallSort.Equals("1"))
							{
								this.LblTitle.Text = "业主控制网资料";
							}
							else
							{
								if (this._SmallSort.Equals("2"))
								{
									this.LblTitle.Text = "测量控制网资料";
								}
								else
								{
									this.LblTitle.Text = "测量放线资料";
								}
							}
							this.hfldIsAllowAudit.Value = "false";
						}
						else
						{
							if (this._BigSort == "2")
							{
								if (this._SmallSort == "1")
								{
									this.queryShowTitle = "查看图纸自审";
									this.LblTitle.Text = "图纸自审";
								}
								else
								{
									this.queryShowTitle = "查看图纸会审";
									this.LblTitle.Text = "图纸会审";
								}
								this.hfldIsAllowAudit.Value = "true";
							}
							else
							{
								if (this._BigSort == "3")
								{
									this.LblTitle.Text = "工程联系单编辑";
									this.hfldIsAllowAudit.Value = "true";
								}
								else
								{
									if (this._BigSort == "4")
									{
										this.LblTitle.Text = "设计变更列表";
										this.hfldIsAllowAudit.Value = "true";
									}
									else
									{
										if (this._BigSort == "5")
										{
											this.LblTitle.Text = "中间交接资料";
											this.hfldIsAllowAudit.Value = "false";
										}
										else
										{
											if (this._BigSort == "6")
											{
												if (this._SmallSort == "1")
												{
													this.queryShowTitle = "查看技术竣工计划";
													this.LblTitle.Text = "技术竣工计划";
												}
												else
												{
													this.queryShowTitle = "查看试车主要计划";
													this.LblTitle.Text = "试车主要计划";
												}
												this.hfldIsAllowAudit.Value = "false";
											}
										}
									}
								}
							}
						}
						this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
					}
					if (this.Session["MEASURELEVELS"] != null)
					{
						this.ViewState["MEASURELEVELS"] = this.Session["MEASURELEVELS"].ToString();
						this._Levels = this.ViewState["MEASURELEVELS"].ToString();
					}
					if (this._Levels.Trim() == "1")
					{
						this.BtnAdd.Visible = false;
						this.BtnUpd.Visible = false;
						this.BtnDel.Visible = false;
						this.hfldIsAllowAudit.Value = "false";
					}
				}
				else
				{
					this.Table1.Visible = false;
					this.BtnAdd.Visible = false;
					this.BtnUpd.Visible = false;
					this.BtnDel.Visible = false;
					this.BtnView.Visible = false;
				}
				this.BtnView.Attributes["onclick"] = "openEdit('View')";
				this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
				this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				return;
			}
			this._PrjCode = this.ViewState["PRJCODE"].ToString();
			this._SmallSort = this.ViewState["SMALLSORT"].ToString();
			this._BigSort = this.ViewState["BIGSORT"].ToString();
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["Levels"] != null)
			{
				this._Levels = base.Request.QueryString["Levels"].ToString();
			}
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[MeasureDataQuery.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdStandard.ItemDataBound += new DataGridItemEventHandler(this.DGrdStandard_ItemDataBound);
		}
		private void MeasureBind(string PrjCode, string bigsort, string smallsort)
		{
			DataTable measureList = MeasureDataAction.GetMeasureList(PrjCode, bigsort, smallsort);
			this.ViewState[MeasureDataQuery.resourceTable] = measureList;
			this.DGrdStandard.DataSource = measureList;
			this.DGrdStandard.DataBind();
		}
		public DataTable GetAnnex(string Id)
		{
			string sqlString = "select OriginalName,FilePath,AnnexName from XPM_Basic_AnnexList where ModuleID=1726 and RecordCode='26" + Id + "' and (State=1 or State=0)";
			return publicDbOpClass.DataTableQuary(sqlString);
		}
		private void DGrdStandard_ItemDataBound(object sender, DataGridItemEventArgs e)
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
				e.Item.Attributes["prjGuid"] = ((HiddenField)e.Item.FindControl("hfldPrjCode")).Value;
				e.Item.Attributes["guid"] = ((HiddenField)e.Item.FindControl("hfldGuid")).Value;
				Label label = e.Item.Cells[4].FindControl("Label1") as Label;
				label.Attributes["onclick"] = string.Concat(new object[]
				{
					"rowQuery('/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&Id=",
					this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString(),
					"&pc=",
					this.ViewState["PRJCODE"],
					"&bs=",
					this.HdnBigSort.Value,
					"&sm=",
					this.HdnSmallSort.Value.Trim(),
					"','",
					this.queryShowTitle,
					"')"
				});
				label.ToolTip = dataRowView["ItemName"].ToString();
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
				if (this._Levels == "1")
				{
					e.Item.Cells[0].Visible = false;
					return;
				}
			}
			else
			{
				if (this._Levels == "1")
				{
					e.Item.Cells[0].Visible = false;
				}
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
		}
		protected void DGrdStandard_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdStandard.CurrentPageIndex = e.NewPageIndex;
			this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
		}
		protected void BtnUpd_Click(object sender, EventArgs e)
		{
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
				base.RegisterScript("top.ui.show('删除成功')");
				this.MeasureBind(this._PrjCode, this._BigSort, this._SmallSort);
				return;
			}
			if (num == 0)
			{
				base.RegisterScript("top.ui.show('删除失败')");
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
						if (datumLogic.UpdateByID(MeasureDataQuery.table_name, 1, "ID", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, MeasureDataQuery.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[MeasureDataQuery.temTable] != null)
											{
												DataTable dataTable = this.ViewState[MeasureDataQuery.temTable] as DataTable;
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
							base.RegisterScript("top.ui.show('操作成功')");
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
			DataTable dataTable = this.ViewState[MeasureDataQuery.temTable] as DataTable;
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.DGrdStandard.Items)
			{
				CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					HiddenField hiddenField = (HiddenField)dataGridItem.Cells[0].FindControl("HiddenField1");
					string value = hiddenField.Value;
					if (this.ViewState[MeasureDataQuery.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[MeasureDataQuery.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("ID='" + value + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["ID"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["ID"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["ItemName"].ToString();
							dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
							dataRow["Original_table"] = MeasureDataQuery.table_name;
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
			this.ViewState[MeasureDataQuery.temTable] = dataTable;
		}
	}


