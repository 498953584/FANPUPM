using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MeasureList : NBasePage, IRequiresSessionState
	{
		private string page_type = string.Empty;
		private string page_PrjCode = string.Empty;
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "Ent_Safty_Measure";
		private QualityGoalAction ActObj = new QualityGoalAction();

		public string Page_type
		{
			get
			{
				return this.page_type;
			}
			set
			{
				this.page_type = value;
			}
		}
		public string Page_PrjCode
		{
			get
			{
				return this.page_PrjCode;
			}
			set
			{
				this.page_PrjCode = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (this.page_type == "Edit")
				{
					this.lbltitle.Text = "安全目标维护";
				}
				else
				{
					this.lbltitle.Text = "目标查看";
				}
				this.Data_Bind();
				this.ClientEvent();
			}
		}
		private void ClientEvent()
		{
			this.BtnSee.Attributes["onclick"] = "OpType('Query','');";
			this.BtnModify.Attributes["onclick"] = "OpType('Update','');";
		}
		protected void Data_Bind()
		{
			string strWhere;
			if (this.page_PrjCode != "")
			{
				strWhere = "PrjCode= '" + this.page_PrjCode + "'";
			}
			else
			{
				strWhere = "PrjCode= '" + Guid.NewGuid() + "'";
			}
			DataTable pageData = SafetyMeasureAction.GetPageData(strWhere);
			this.GridView_goall.DataSource = pageData;
			this.ViewState[MeasureList.resourceTable] = pageData;
			this.GridView_goall.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"].ToString() != "")
			{
				this.page_type = base.Request.QueryString["Type"].ToString();
				this.hiden_page_Type.Value = this.Page_type;
			}
			if (base.Request.QueryString["prjId"] != null && base.Request.QueryString["prjId"].ToString() != "")
			{
				this.page_PrjCode = base.Request.QueryString["prjId"].ToString();
				this.hiden_PrjCode.Value = this.Page_PrjCode;
			}
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[MeasureList.temTable] = list.Clone();
			this.btnFiles.Enabled = false;
			base.OnInit(e);
		}
		protected void BtnDelete_Click(object sender, EventArgs e)
		{
			new SafetyMeasureAction();
			if (SafetyMeasureAction.DEL(new SafetyMeasureInfo
			{
				i_id = Convert.ToInt32(this.hiden_i_id.Value)
			}.i_id.ToString()) == 1)
			{
				this.JS.Text = "alert('操作成功!');";
				this.Data_Bind();
				return;
			}
			this.JS.Text = "alert('操作失败,网络连接故障，请稍候再试')";
		}
		protected void GridView_goall_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.GridView_goall.PageIndex = e.NewPageIndex;
			this.Data_Bind();
		}
		protected void GridView_goall_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["style"] = "Cursor:hand";
				e.Row.Attributes["id"] = dataRowView["i_id"].ToString();
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"clickRow('",
					this.GridView_goall.DataKeys[e.Row.RowIndex].Value.ToString(),
					"','",
					this.page_type,
					"')"
				});
				Label label = (Label)e.Row.Cells[3].FindControl("lblTaskName");
				e.Row.Attributes["mark"] = dataRowView["mark"].ToString();
				label.Attributes["onclick"] = "OpType('Query','" + this.GridView_goall.DataKeys[e.Row.RowIndex].Value.ToString() + "')";
				CheckBox checkBox = (CheckBox)e.Row.Cells[0].FindControl("chk");
				checkBox.Attributes["class"] = "chk";
				checkBox.Attributes["id"] = dataRowView["i_id"].ToString();
				Label label2 = (Label)e.Row.Cells[0].FindControl("Label3");
				label2.Attributes["style"] = "display:none";
				if (this.page_type == "List")
				{
					e.Row.Cells[0].Text = "";
					e.Row.Cells[0].Visible = false;
					return;
				}
			}
			else
			{
				if (e.Row.RowType == DataControlRowType.Header && this.page_type == "List")
				{
					e.Row.Cells[0].Text = "";
					e.Row.Cells[0].Visible = false;
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
						if (datumLogic.UpdateByID(MeasureList.table_name, 1, "i_id", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, MeasureList.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[MeasureList.temTable] != null)
											{
												DataTable dataTable = this.ViewState[MeasureList.temTable] as DataTable;
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
							this.JS.Text = "alert('操作成功!');";
							this.Data_Bind();
						}
					}
					catch (Exception)
					{
						sqlTransaction.Rollback();
						base.RegisterScript("alert('系统提示：\\n\\n操作失败！');");
					}
				}
			}
		}
		private void GET_SelectRow()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = this.ViewState[MeasureList.temTable] as DataTable;
			dataTable.Clear();
			foreach (GridViewRow gridViewRow in this.GridView_goall.Rows)
			{
				CheckBox checkBox = gridViewRow.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					Label label = (Label)gridViewRow.Cells[0].FindControl("Label3");
					string text = label.Text;
					if (this.ViewState[MeasureList.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[MeasureList.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("i_id='" + text + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["i_id"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["i_id"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["SaftyMeasure"].ToString();
							dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
							dataRow["Original_table"] = MeasureList.table_name;
							dataRow["sid_ColumnName"] = "i_id";
							dataRow["sid_ColumnType"] = 1;
							dataRow["prjcode"] = ((array[0]["PrjCode"].ToString() == "") ? "" : array[0]["PrjCode"].ToString());
							dataTable.Rows.Add(dataRow);
							stringBuilder.Append(array[0]["i_id"].ToString() + ",");
						}
					}
					this.ViewState["i_id"] = stringBuilder.ToString();
				}
			}
			this.ViewState[MeasureList.temTable] = dataTable;
		}
		protected void btnQuery_Click(object sender, EventArgs e)
		{
			string arg_05_0 = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.TextBox1.Text.Trim().ToString() != "")
			{
				stringBuilder.Append("TaskName like '%" + this.TextBox1.Text.Trim().ToString() + "%'");
			}
			if (this.TextBox2.Text.Trim().ToString() != "")
			{
				if (this.TextBox1.Text.Trim().ToString() != "")
				{
					stringBuilder.Append(" and ");
				}
				stringBuilder.Append("SaftyMeasure like '%" + this.TextBox2.Text.Trim().ToString() + "%'");
			}
			DataTable dataTable = this.ViewState[MeasureList.resourceTable] as DataTable;
			DataTable dataTable2 = new DataTable();
			dataTable2 = dataTable.Clone();
			if (dataTable.Rows.Count > 0)
			{
				DataRow[] array = dataTable.Select(stringBuilder.ToString());
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow row = array2[i];
					dataTable2.ImportRow(row);
				}
				this.GridView_goall.DataSource = dataTable2;
				this.GridView_goall.DataBind();
			}
		}
	}


