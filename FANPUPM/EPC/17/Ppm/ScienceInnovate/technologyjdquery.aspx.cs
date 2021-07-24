using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using cn.justwin.stockBLL.prj;
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
	public partial class TechnologyJDQuery : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		private string _PrjCode = "";
		private string _Levels = "";
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "Prj_TechnologyTell";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.ViewState[TechnologyJDQuery.resourceTable] != null)
				{
					this.ViewState[TechnologyJDQuery.resourceTable] = null;
				}
				if (base.Request["prjId"] != null)
				{
					this.HdnPrjCode.Value = base.Request["prjId"].ToString();
					PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
					PTPrjInfo byId = pTPrjInfoService.GetById(this.HdnPrjCode.Value);
					if (byId != null)
					{
						this.HdnPrjName.Value = byId.PrjName;
					}
					this.ViewState["PRJCODE"] = this.HdnPrjCode.Value;
					this.ViewState["PRJNAME"] = this.HdnPrjName.Value;
					this.TechnologyBind(this.HdnPrjCode.Value);
					if (this._Levels.Trim() == "1")
					{
						this.BtnAdd.Visible = false;
						this.BtnUpd.Visible = false;
						this.BtnDel.Visible = false;
					}
				}
				else
				{
					this.BtnAdd.Visible = false;
					this.BtnUpd.Visible = false;
					this.BtnDel.Visible = false;
					this.BtnView.Visible = false;
				}
				this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
				this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
				this.BtnView.Attributes["onclick"] = "openEdit('View')";
				this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				return;
			}
			this._PrjCode = this.ViewState["PRJCODE"].ToString();
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[TechnologyJDQuery.temTable] = list.Clone();
			if (base.Request.QueryString["Levels"] != null)
			{
				this.ViewState["LEVELS"] = base.Request.QueryString["Levels"].ToString().Trim();
				this._Levels = this.ViewState["LEVELS"].ToString();
			}
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdTechnology.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DGrdTechnology_ItemDataBound);
		}
		private void TechnologyBind(string PrjCode)
		{
			DataTable technologyList = TechnologyJDAction.GetTechnologyList(PrjCode);
			this.ViewState[TechnologyJDQuery.resourceTable] = technologyList;
			this.DGrdTechnology.DataSource = technologyList;
			this.DGrdTechnology.DataBind();
		}
		private void DGrdTechnology_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["id"] = dataRowView["MainID"].ToString();
				e.Item.Attributes["Levels"] = this._Levels;
				System.Web.UI.WebControls.Label label = e.Item.Cells[5].FindControl("Label1") as System.Web.UI.WebControls.Label;
				label.Attributes["onclick"] = string.Concat(new string[]
				{
					"rowQuery('/EPC/17/Ppm/ScienceInnovate/TechnologyJDEdit.aspx?Type=View&Id=",
					this.DGrdTechnology.DataKeys[e.Item.ItemIndex].ToString(),
					"&pc=",
					this.HdnPrjCode.Value,
					"&pn=",
					this.HdnPrjName.Value,
					"','查看技术交底')"
				});
				label.Text = dataRowView["PrejectName"].ToString();
				label.ToolTip = dataRowView["PrejectName"].ToString();
				e.Item.Attributes["style"] = "cursor:hand";
				e.Item.Attributes["mark"] = dataRowView["mark"].ToString();
				e.Item.Attributes["prjGuid"] = ((HiddenField)e.Item.FindControl("hfldPrjCode")).Value;
				e.Item.Attributes["guid"] = ((HiddenField)e.Item.FindControl("hfldGuid")).Value;
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
			this.TechnologyBind(this._PrjCode);
		}
		protected void DGrdTechnology_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.DGrdTechnology.CurrentPageIndex = e.NewPageIndex;
			this.TechnologyBind(this._PrjCode);
		}
		protected void BtnUpd_Click(object sender, EventArgs e)
		{
			this.TechnologyBind(this._PrjCode);
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			if (this.HdnId.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.HdnId.Value);
			}
			else
			{
				list.Add(this.HdnId.Value);
			}
			int num = 0;
			try
			{
				for (int i = 0; i < list.Count; i++)
				{
					num = TechnologyJDAction.Del(list[i].ToString());
				}
				if (num == 1)
				{
					this.Js.Text = "alert('删除成功！');";
					this.TechnologyBind(this._PrjCode);
				}
			}
			catch (Exception)
			{
				this.Js.Text = "alert('删除失败！');";
			}
		}
		protected void btnFiles_Click(object sender, EventArgs e)
		{
			this.GET_SelectRow();
			string text = string.Empty;
			if (this.ViewState["MainID"] != null)
			{
				text = this.ViewState["MainID"].ToString();
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
						if (datumLogic.UpdateByID(TechnologyJDQuery.table_name, "MainID", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, TechnologyJDQuery.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[TechnologyJDQuery.temTable] != null)
											{
												DataTable dataTable = this.ViewState[TechnologyJDQuery.temTable] as DataTable;
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
							base.RegisterScript("top.ui.show('操作成功！');");
							this.TechnologyBind(this._PrjCode);
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
			this.ViewState["MainID"] = "";
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = this.ViewState[TechnologyJDQuery.temTable] as DataTable;
			dataTable.Clear();
			foreach (System.Web.UI.WebControls.DataGridItem dataGridItem in this.DGrdTechnology.Items)
			{
				System.Web.UI.WebControls.CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as System.Web.UI.WebControls.CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					HiddenField hiddenField = (HiddenField)dataGridItem.Cells[0].FindControl("HiddenField1");
					string value = hiddenField.Value;
					if (this.ViewState[TechnologyJDQuery.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[TechnologyJDQuery.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("MainID='" + value + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["MainID"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["MainID"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["ConstructionUnit"].ToString();
							dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
							dataRow["Original_table"] = TechnologyJDQuery.table_name;
							dataRow["sid_ColumnName"] = "MainID";
							dataRow["sid_ColumnType"] = 2;
							dataRow["prjcode"] = ((array[0]["PrjCode"].ToString() == "") ? "" : array[0]["PrjCode"].ToString());
							dataTable.Rows.Add(dataRow);
							stringBuilder.Append(array[0]["MainID"].ToString() + ",");
						}
					}
					this.ViewState["MainID"] = stringBuilder.ToString();
				}
			}
			this.ViewState[TechnologyJDQuery.temTable] = dataTable;
		}
		protected void btnQuery_Click(object sender, EventArgs e)
		{
			DataTable dataSource = new DataTable();
			TechnologyTellLogic technologyTellLogic = new TechnologyTellLogic();
			dataSource = technologyTellLogic.GetDataTable(this.getQueryWhere());
			this.DGrdTechnology.DataSource = dataSource;
			this.DGrdTechnology.DataBind();
		}
		private string getQueryWhere()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.TellDateBegin.Text.Trim().ToString() != "")
			{
				stringBuilder.Append("  TellDate BETWEEN '").Append(this.TellDateBegin.Text.Trim().ToString()).Append("'");
				if (this.TellDateEnd.Text.Trim().ToString() != "")
				{
					stringBuilder.Append(" AND ");
					stringBuilder.Append("  '").Append(this.TellDateBegin.Text.Trim().ToString()).Append("'");
				}
				else
				{
					stringBuilder.Append(" AND ");
					stringBuilder.Append("  '").Append(new DateTime(9999, 12, 31)).Append("'");
				}
			}
			else
			{
				if (this.TellDateEnd.Text.Trim().ToString() != "")
				{
					stringBuilder.Append(" AND ");
					stringBuilder.Append("TellDate BETWEEN  '").Append(new DateTime(1753, 1, 1)).Append("'");
					stringBuilder.Append(" AND ");
					stringBuilder.Append("  '").Append(this.TellDateEnd.Text.Trim().ToString()).Append("'");
				}
			}
			if (this.TellDateEnd.Text.Trim().ToString() == "" && this.TellDateBegin.Text.Trim().ToString() == "")
			{
				stringBuilder.Append("TellDate BETWEEN  '").Append(new DateTime(1753, 1, 1)).Append("'");
				stringBuilder.Append(" AND ");
				stringBuilder.Append("  '").Append(new DateTime(9999, 12, 31)).Append("'");
			}
			if (this._PrjCode != "")
			{
				stringBuilder.Append(" AND ");
				stringBuilder.Append("PrjCode ='").Append(this._PrjCode).Append("'");
			}
			if (this.txtByTellUnit.Text.Trim().ToString() != "")
			{
				stringBuilder.Append("ByTellUnit like'%").Append(this.txtByTellUnit.Text.Trim().ToString()).Append("%'");
			}
			if (this.txtConstructionUnit.Text.Trim().ToString() != "")
			{
				stringBuilder.Append(" AND ");
				stringBuilder.Append("ConstructionUnit like'%").Append(this.txtConstructionUnit.Text.Trim().ToString()).Append("%'");
			}
			return stringBuilder.ToString();
		}
	}


