using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Files;
using cn.justwin.stockBLL.Files;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProjectElectronFiles : NBasePage, IRequiresSessionState
{
	private static string prjcode = string.Empty;
	private static string resTableName = "restab";
	private static string temTableName = "temtab";
	private FilesLogic FL = new FilesLogic();

	public static string Prjcode
	{
		get
		{
			return ProjectElectronFiles.prjcode;
		}
		set
		{
			ProjectElectronFiles.prjcode = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.initDataBind();
		}
	}
	protected override void OnInit(EventArgs e)
	{
		if (base.Request.QueryString["Prjcode"] != null && base.Request.QueryString["Prjcode"].ToString() != "")
		{
			ProjectElectronFiles.Prjcode = base.Request.QueryString["Prjcode"].ToString();
		}
		this.btnQuery.Enabled = false;
		this.btnRepeal.Enabled = false;
		base.OnInit(e);
	}
	private void initDataBind()
	{
		string queryWhere = this.getQueryWhere();
		DataTable dataTable = new DataTable();
		if (queryWhere != "")
		{
			dataTable = this.FL.GetList(queryWhere);
		}
		else
		{
			dataTable = this.FL.GetAllList();
		}
		DataTable value = new DataTable();
		value = dataTable.Clone();
		this.ViewState[ProjectElectronFiles.temTableName] = value;
		this.ViewState[ProjectElectronFiles.resTableName] = dataTable;
		this.GridView1.DataSource = dataTable;
		this.GridView1.DataBind();
	}
	private string getQueryWhere()
	{
		StringBuilder stringBuilder = new StringBuilder();
		if (base.Request.QueryString["PrjCode"] != null && base.Request.QueryString["PrjCode"].ToString() != "")
		{
			stringBuilder.Append("prjcode='").Append(base.Request.QueryString["PrjCode"].ToString()).Append("'");
			if (this.txtName.Text.Trim() != "")
			{
				stringBuilder.Append(" ").Append("AND").Append(" ");
				stringBuilder.Append("file_name  LIKE  '%").Append(this.txtName.Text.Trim()).Append("%'");
			}
			if (this.txtInfo.Text.Trim() != "")
			{
				stringBuilder.Append(" ").Append("AND").Append(" ");
				stringBuilder.Append("file_info  LIKE  '%").Append(this.txtInfo.Text.Trim()).Append("%'");
			}
			if (this.txtContext.Text.Trim() != "")
			{
				stringBuilder.Append(" ").Append("AND").Append(" ");
				stringBuilder.Append("Content  LIKE  '%").Append(this.txtContext.Text.Trim()).Append("%'");
			}
		}
		return stringBuilder.ToString();
	}
	protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
	{
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType != DataControlRowType.DataRow)
		{
			DataControlRowType arg_10C_0 = e.Row.RowType;
			return;
		}
		DataRowView dataRowView = (DataRowView)e.Row.DataItem;
		e.Row.Attributes["id"] = dataRowView["ID"].ToString();
		e.Row.Attributes["mark"] = dataRowView["file_mark"].ToString();
		if (e.Row.Cells[4].Text.ToString().Length > 50)
		{
			e.Row.Cells[4].Text = e.Row.Cells[4].Text.ToString().Substring(0, 49) + "....";
			return;
		}
		e.Row.Cells[4].Text = e.Row.Cells[4].Text;
	}
	protected void btnRepeal_Click(object sender, EventArgs e)
	{
		FilesLogic filesLogic = new FilesLogic();
		new List<string>();
		using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
		{
			sqlConnection.Open();
			SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
			try
			{
				this.getViewSet();
				if (this.ViewState[ProjectElectronFiles.temTableName] != null)
				{
					DataTable dataTable = new DataTable();
					dataTable = (this.ViewState[ProjectElectronFiles.temTableName] as DataTable);
					for (int i = 0; i < dataTable.Rows.Count; i++)
					{
						if (filesLogic.Delete(new Guid(dataTable.Rows[i]["ID"].ToString()), sqlTransaction))
						{
							string idvals = "'" + dataTable.Rows[i]["file_sid"].ToString() + "'";
							filesLogic.Update(idvals, dataTable.Rows[i]["Original_table"].ToString(), dataTable.Rows[i]["sid_ColumnName"].ToString(), 1, sqlTransaction);
						}
					}
				}
				sqlTransaction.Commit();
				base.RegisterScript("alert('系统提示：\\n\\n操作成功！');");
			}
			catch (Exception ex)
			{
				sqlTransaction.Rollback();
				sqlConnection.Close();
				base.RegisterScript("alert('系统提示：\\n\\n操作失败！');");
				throw ex;
			}
			this.initDataBind();
		}
	}
	private void getViewSet()
	{
		if (this.ViewState[ProjectElectronFiles.resTableName] != null)
		{
			DataTable dataTable = this.ViewState[ProjectElectronFiles.temTableName] as DataTable;
			object arg_3B_0 = this.ViewState[ProjectElectronFiles.resTableName];
			foreach (GridViewRow gridViewRow in this.GridView1.Rows)
			{
				CheckBox checkBox = gridViewRow.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					HiddenField hiddenField = (HiddenField)gridViewRow.Cells[0].FindControl("HiddenField1");
					string value = hiddenField.Value;
					if (this.ViewState[ProjectElectronFiles.resTableName] != null)
					{
						DataTable dataTable2 = this.ViewState[ProjectElectronFiles.resTableName] as DataTable;
						DataRow[] array = dataTable2.Select("ID='" + value + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["ID"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = array[0]["ID"].ToString();
							dataRow["file_sid"] = array[0]["file_sid"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["file_name"].ToString();
							dataRow["file_info"] = ((array[0]["file_info"].ToString() == "") ? "" : array[0]["file_info"].ToString());
							dataRow["Original_table"] = ((array[0]["Original_table"].ToString() == "") ? "" : array[0]["Original_table"].ToString());
							dataRow["sid_ColumnName"] = array[0]["sid_ColumnName"].ToString();
							dataRow["sid_ColumnType"] = int.Parse(array[0]["sid_ColumnType"].ToString());
							dataTable.Rows.Add(dataRow);
						}
					}
				}
			}
			this.ViewState[ProjectElectronFiles.temTableName] = dataTable;
		}
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GridView1.PageIndex = e.NewPageIndex;
		this.initDataBind();
	}
	protected void btnQueryList_Click(object sender, EventArgs e)
	{
		this.initDataBind();
	}
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		filesModel filesModel = new filesModel();
		string value = this.hidenID.Value;
		if (value != "")
		{
			try
			{
				filesModel = this.FL.GetModel(new Guid(value));
				if (filesModel != null)
				{
					string table = string.Empty;
					table = filesModel.Original_table.ToString();
					string id = filesModel.file_sid.ToString();
					string rowName = filesModel.Sid_ColumnName.ToString();
					this.CreateUrl(table, id, rowName);
				}
			}
			catch (Exception)
			{
				base.RegisterScript("alert('系统提示：\\n\\n查看地址转换失败！');");
			}
		}
	}
	private void CreateUrl(string table, string id, string rowName)
	{
		string text = string.Empty;
		string text2 = string.Empty;
		string value = "Q";
		if (!string.IsNullOrEmpty(table) && !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(rowName))
		{
			Random random = new Random();
			if (table == "EPM_Datum_Affair")
			{
				DataTable list = this.FL.GetList(table, "'" + id + "'", rowName);
				if (list.Rows.Count > 0)
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (list.Rows[0]["DatumClass"] != null)
					{
						if (list.Rows[0]["DatumClass"].ToString() == "6")
						{
							stringBuilder.Append("/EPC/QuaitySafety/ProjectSuperviseView.aspx?");
						}
						else
						{
							stringBuilder.Append("/EPC/QuaitySafety/affairview.aspx?");
						}
					}
					else
					{
						stringBuilder.Append("/EPC/QuaitySafety/affairview.aspx?");
					}
					stringBuilder.Append("r=").Append(random.Next(100, 999));
					stringBuilder.Append("&Type=SEE");
					if (list.Rows[0]["i_id"] != null)
					{
						stringBuilder.Append("&").Append("i_id=").Append(list.Rows[0]["i_id"].ToString());
					}
					if (list.Rows[0]["CA"] != null)
					{
						stringBuilder.Append("&").Append("CA=").Append(list.Rows[0]["CA"].ToString());
						if (list.Rows[0]["CA"].ToString() == "6")
						{
							text2 = "安全目标";
							value = "S";
						}
						else
						{
							if (list.Rows[0]["CA"].ToString() == "5")
							{
								text2 = "安全事故";
								value = "S";
							}
							else
							{
								if (list.Rows[0]["CA"].ToString() == "4")
								{
									text2 = "安全检查";
									value = "S";
								}
								else
								{
									if (list.Rows[0]["CA"].ToString() == "3")
									{
										text2 = "竣工验收";
									}
									else
									{
										if (list.Rows[0]["CA"].ToString() == "2")
										{
											text2 = "事故记录";
										}
										else
										{
											if (list.Rows[0]["CA"].ToString() == "1")
											{
												text2 = "工程质量监督";
											}
										}
									}
								}
							}
						}
					}
					if (list.Rows[0]["DatumClass"] != null)
					{
						stringBuilder.Append("&").Append("DatumClass=").Append(list.Rows[0]["DatumClass"].ToString());
						if (list.Rows[0]["DatumClass"].ToString() == "5")
						{
							text2 = "安全目标";
							value = "S";
						}
						else
						{
							if (list.Rows[0]["DatumClass"].ToString() == "4")
							{
								text2 = "质量事务";
							}
						}
					}
					if (list.Rows[0]["PrjCode"] != null)
					{
						stringBuilder.Append("&").Append("PrjCode=").Append(list.Rows[0]["PrjCode"].ToString());
					}
					stringBuilder.Append("&Flag=").Append(value);
					text = stringBuilder.ToString();
				}
			}
			else
			{
				if (table == "Ent_Quality_Goal")
				{
					DataTable list2 = this.FL.GetList(table, id, rowName);
					if (list2.Rows.Count > 0)
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.Append("/EPC/QuaitySafety/QualityGoal/goalview.aspx?");
						stringBuilder2.Append("r=").Append(random.Next(1000, 9999));
						stringBuilder2.Append("&").Append("Type=Query");
						if (list2.Rows[0]["i_id"] != null)
						{
							stringBuilder2.Append("&").Append("Code=").Append(list2.Rows[0]["i_id"].ToString());
						}
						if (list2.Rows[0]["PrjCode"] != null)
						{
							stringBuilder2.Append("&").Append("PrjCode=").Append(list2.Rows[0]["PrjCode"].ToString());
						}
						text2 = "工程质量目标 ";
						text = stringBuilder2.ToString();
					}
				}
				else
				{
					if (table == "Ent_Safty_Measure")
					{
						DataTable list3 = this.FL.GetList(table, id, rowName);
						if (list3.Rows.Count > 0)
						{
							StringBuilder stringBuilder3 = new StringBuilder();
							stringBuilder3.Append("/EPC/QuaitySafety/SafetyMeasure/MeasureView.aspx?");
							stringBuilder3.Append("r=").Append(random.Next(1000, 9999));
							stringBuilder3.Append("&Flage=1&").Append("Type=Query");
							if (list3.Rows[0]["i_id"] != null)
							{
								stringBuilder3.Append("&").Append("Code=").Append(list3.Rows[0]["i_id"].ToString());
							}
							if (list3.Rows[0]["PrjCode"] != null)
							{
								stringBuilder3.Append("&").Append("PrjCode=").Append(list3.Rows[0]["PrjCode"].ToString());
							}
							text2 = "安全措施维护 ";
							text = stringBuilder3.ToString();
						}
					}
					else
					{
						if (table == "Prj_TechnologyConstructOrganize")
						{
							DataTable list4 = this.FL.GetList(table, id, rowName);
							if (list4.Rows.Count > 0)
							{
								StringBuilder stringBuilder4 = new StringBuilder();
								stringBuilder4.Append("/Epc/17/ppm/ScienceInnovate/ConstructOrganizeView.aspx?");
								stringBuilder4.Append("r=").Append(random.Next(1000, 9999));
								if (list4.Rows[0]["MainID"] != null)
								{
									stringBuilder4.Append("&").Append("id=").Append(list4.Rows[0]["MainID"].ToString());
								}
								text2 = "施工组织 ";
								text = stringBuilder4.ToString();
							}
						}
						else
						{
							if (table == "Prj_ExpertSchemeManage")
							{
								DataTable list5 = this.FL.GetList(table, id, rowName);
								if (list5.Rows.Count > 0)
								{
									StringBuilder stringBuilder5 = new StringBuilder();
									stringBuilder5.Append("/Epc/17/ppm/ScienceInnovate/expertprojectview.aspx?");
									stringBuilder5.Append("r=").Append(random.Next(1000, 9999));
									if (list5.Rows[0]["MainID"] != null)
									{
										stringBuilder5.Append("&").Append("id=").Append(list5.Rows[0]["MainID"].ToString());
									}
									text2 = "专项方案 ";
									text = stringBuilder5.ToString();
								}
							}
							else
							{
								if (table == "Prj_TechnologyCriterion")
								{
									DataTable list6 = this.FL.GetList(table, id, rowName);
									if (list6.Rows.Count > 0)
									{
										StringBuilder stringBuilder6 = new StringBuilder();
										stringBuilder6.Append("/EPC/17/Ppm/ScienceInnovate/TechnologyStandardEdit.aspx?");
										stringBuilder6.Append("r=").Append(random.Next(1000, 9999));
										stringBuilder6.Append("&Type=View");
										if (list6.Rows[0]["MainID"] != null)
										{
											stringBuilder6.Append("&").Append("Id=").Append(list6.Rows[0]["MainID"].ToString());
										}
										if (list6.Rows[0]["PrjCode"] != null)
										{
											stringBuilder6.Append("&").Append("pc=").Append(list6.Rows[0]["PrjCode"].ToString());
										}
										text2 = "技术标准台账 ";
										text = stringBuilder6.ToString();
									}
								}
								else
								{
									if (table == "Prj_TechnologyManage")
									{
										DataTable list7 = this.FL.GetList(table, id, rowName);
										if (list7.Rows.Count > 0)
										{
											text2 = "查看设计变更资料 ";
											StringBuilder stringBuilder7 = new StringBuilder();
											stringBuilder7.Append("/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?");
											stringBuilder7.Append("r=").Append(random.Next(1000, 9999));
											stringBuilder7.Append("&Type=View");
											if (list7.Rows[0]["ID"] != null)
											{
												stringBuilder7.Append("&").Append("Id=").Append(list7.Rows[0]["ID"].ToString());
											}
											if (list7.Rows[0]["PrjCode"] != null)
											{
												stringBuilder7.Append("&").Append("pc=").Append(list7.Rows[0]["PrjCode"].ToString());
											}
											if (list7.Rows[0]["BigSort"] != null)
											{
												stringBuilder7.Append("&").Append("bs=").Append(list7.Rows[0]["BigSort"].ToString());
												if (list7.Rows[0]["BigSort"].ToString() == "1")
												{
													text2 = "查看测量资料";
												}
												if (list7.Rows[0]["BigSort"].ToString() == "2")
												{
													if (list7.Rows[0]["SmallSort"].ToString() == "1")
													{
														text2 = "查看图纸自审";
													}
													else
													{
														text2 = "查看图纸会审";
													}
												}
												if (list7.Rows[0]["BigSort"].ToString() == "3")
												{
													text2 = "查看工程联系单";
												}
												if (list7.Rows[0]["BigSort"].ToString() == "4")
												{
													text2 = "查看设计变更";
												}
												if (list7.Rows[0]["BigSort"].ToString() == "5")
												{
													text2 = "查看中间交接资料";
												}
												if (list7.Rows[0]["BigSort"].ToString() == "8")
												{
													text2 = "查看工程洽商单";
												}
												if (list7.Rows[0]["BigSort"].ToString() == "7")
												{
													text2 = "查看工程确认单";
												}
												if (list7.Rows[0]["BigSort"].ToString() == "6")
												{
													if (list7.Rows[0]["SmallSort"].ToString() == "1")
													{
														text2 = "查看技术竣工计划";
													}
													else
													{
														text2 = "查看试车主要计划";
													}
												}
											}
											if (list7.Rows[0]["SmallSort"] != null)
											{
												stringBuilder7.Append("&").Append("sm=").Append(list7.Rows[0]["SmallSort"].ToString());
											}
											text = stringBuilder7.ToString();
										}
									}
									else
									{
										if (table == "Prj_Summary")
										{
											DataTable list8 = this.FL.GetList(table, id, rowName);
											if (list8.Rows.Count > 0)
											{
												StringBuilder stringBuilder8 = new StringBuilder();
												stringBuilder8.Append("/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?");
												stringBuilder8.Append("r=").Append(random.Next(1000, 9999));
												stringBuilder8.Append("&Type=view");
												if (list8.Rows[0]["ID"] != null)
												{
													stringBuilder8.Append("&").Append("id=").Append(list8.Rows[0]["ID"].ToString());
												}
												if (list8.Rows[0]["Prjid"] != null)
												{
													stringBuilder8.Append("&").Append("PrjCode=").Append(list8.Rows[0]["Prjid"].ToString());
												}
												text2 = "查看工程总结资料 ";
												text = stringBuilder8.ToString();
											}
										}
										else
										{
											if (table == "Prj_ItemProg")
											{
												DataTable list9 = this.FL.GetList(table, id, rowName);
												if (list9.Rows.Count > 0)
												{
													StringBuilder stringBuilder9 = new StringBuilder();
													stringBuilder9.Append("/EPC/17/Ppm/Prog/ItemProgManage.aspx?");
													stringBuilder9.Append("r=").Append(random.Next(1000, 9999));
													stringBuilder9.Append("&Type=view");
													if (list9.Rows[0]["ID"] != null)
													{
														stringBuilder9.Append("&").Append("pk=").Append(list9.Rows[0]["ID"].ToString());
													}
													text2 = "项目奖罚 ";
													text = stringBuilder9.ToString();
												}
											}
											else
											{
												if (table == "Prj_TechnologyTell")
												{
													DataTable list10 = this.FL.GetList(table, id, rowName);
													if (list10.Rows.Count > 0)
													{
														StringBuilder stringBuilder10 = new StringBuilder();
														stringBuilder10.Append("/EPC/17/Ppm/ScienceInnovate/TechnologyJDEdit.aspx?");
														stringBuilder10.Append("r=").Append(random.Next(1000, 9999));
														stringBuilder10.Append("&Type=View");
														if (list10.Rows[0]["MainID"] != null)
														{
															stringBuilder10.Append("&").Append("Id=").Append(list10.Rows[0]["MainID"].ToString());
														}
														text2 = "技术交底 ";
														text = stringBuilder10.ToString();
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
		}
		base.ClientScript.RegisterStartupScript(base.GetType(), "skip", string.Concat(new string[]
		{
			"<script   language=javascript>viewopen_n('",
			text,
			"','",
			text2,
			"');</script>"
		}));
	}
	public string GetAnnxA(string guid, string table)
	{
		int moduleID = 0;
		if (table == "Prj_TechnologyCriterion")
		{
			moduleID = 1725;
		}
		else
		{
			if (table == "EPM_Datum_Affair")
			{
				moduleID = 1755;
			}
			else
			{
				if (table == "Prj_TechnologyConstructOrganize")
				{
					moduleID = 1720;
				}
				else
				{
					if (table == "Prj_ExpertSchemeManage")
					{
						moduleID = 1722;
					}
					else
					{
						if (table == "Prj_TechnologyManage")
						{
							moduleID = 1726;
						}
						else
						{
							if (table == "Prj_Summary")
							{
								moduleID = 1754;
							}
							else
							{
								if (table == "Prj_TechnologyTell")
								{
									moduleID = 1728;
								}
								else
								{
									if (table == "Prj_ItemInspect")
									{
										moduleID = 1755;
									}
								}
							}
						}
					}
				}
			}
		}
		string annx = base.GetAnnx(moduleID, guid);
		return annx.ToString();
	}
}


