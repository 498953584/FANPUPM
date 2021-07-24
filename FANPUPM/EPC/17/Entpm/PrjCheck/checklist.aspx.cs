using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm;
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
	public partial class CheckList : NBasePage, IRequiresSessionState
	{
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "Prj_ItemInspect";
		public static string showtitle = string.Empty;
		public static string _page_N = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.gvItemInpect.PageSize = NBasePage.pagesize;
			if (!base.IsPostBack)
			{
				this.type_TF_show.Visible = true;
				if (base.Request["Type"] == "List")
				{
					this.Button_add.Visible = false;
					this.Button_edit.Visible = false;
					this.Button_del.Visible = false;
					this.Button_sp.Visible = false;
					this.btn_Search.Visible = true;
					this.TextBox_jcdw.Visible = true;
					this.TextBox_sjdw.Visible = true;
					this.Literal2.Visible = true;
					this.Literal3.Visible = true;
					this.Literal1.Text = "项目检查查询";
				}
				else
				{
					if (base.Request["Type"] == "Edit")
					{
						this.Button_add.Visible = true;
						this.Button_edit.Visible = true;
						this.Button_del.Visible = true;
						this.Button_sp.Visible = false;
						this.btn_Search.Visible = false;
						this.TextBox_jcdw.Visible = false;
						this.TextBox_sjdw.Visible = false;
						this.Literal2.Visible = false;
						this.Literal3.Visible = false;
						this.Literal1.Text = "检查项目检查过程";
						this.type_TF_show.Visible = false;
						CheckList._page_N = "1";
					}
					else
					{
						if (base.Request["Type"] == "ShenHe")
						{
							this.Button_add.Visible = false;
							this.Button_edit.Visible = false;
							this.Button_del.Visible = false;
							this.Button_sp.Visible = false;
							this.btn_Search.Visible = true;
							this.TextBox_jcdw.Visible = true;
							this.TextBox_sjdw.Visible = true;
							this.Literal2.Visible = true;
							this.Literal3.Visible = true;
							this.Literal1.Text = "项目检查结果审核";
						}
						else
						{
							if (base.Request["Type"] == "Rectify")
							{
								this.Button_add.Visible = false;
								this.Button_del.Visible = false;
								this.Button_edit.Text = "整改";
								this.Button_sp.Visible = false;
								this.Literal1.Text = "整改项目检查过程";
								CheckList._page_N = "2";
							}
							else
							{
								if (base.Request["Type"] == "Certify")
								{
									this.Button_add.Visible = false;
									this.Button_del.Visible = false;
									this.Button_edit.Text = "验证";
									this.Button_sp.Visible = false;
									this.Literal1.Text = "验证项目检查过程";
									CheckList._page_N = "3";
								}
							}
						}
					}
				}
				string s = base.Request.QueryString["PrjName"];
				this.Button_add.Attributes.Add("onclick", "ShowInsertWindow('" + base.Server.UrlEncode(s) + "'); return false;");
				this.Button_edit.Attributes.Add("onclick", "ShowEditWindow(); return false;");
				this.Button_sp.Attributes.Add("onclick", "ShowSpWindow(); return false;");
				this.Button_del.Attributes.Add("onclick", "if( !confirm('确实删除该项吗?')){ return false;}");
				this.bind();
			}
		}
		private void bind()
		{
			if (base.Request["PrjCode"] == null || base.Request["Levels"] == null)
			{
				this.gvItemInpect.DataSource = "";
				this.gvItemInpect.DataBind();
				this.Button_add.Visible = false;
				return;
			}
			string strwhere = "prjcode in (" + this.getprjCodeList() + ") and Flags=" + base.Request["Levels"].ToString();
			DataTable checkCollections = CheckAction.GetCheckCollections(strwhere);
			int arg_89_0 = checkCollections.Rows.Count;
			this.ViewState[CheckList.resourceTable] = checkCollections;
			this.gvItemInpect.DataSource = checkCollections;
			this.gvItemInpect.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[CheckList.temTable] = list.Clone();
			if (base.Request.QueryString["n"] != null)
			{
				CheckList._page_N = base.Request.QueryString["n"].ToString();
			}
			base.OnInit(e);
		}
		public string getprjCodeList()
		{
			string text = string.Empty;
			if (base.Request["PrjCode"] != null)
			{
				string sqlString = "  SELECT * FROM PT_PrjInfo ppi WHERE ppi.PrjGuid='" + base.Request.QueryString["PrjCode"].ToString() + "'";
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable.Rows.Count > 0)
				{
					foreach (DataRow dataRow in dataTable.Rows)
					{
						if (dataRow["PrjGuid"] != null)
						{
							sqlString = " SELECT * FROM PT_PrjInfo ppi WHERE ppi.TypeCode LIKE '" + dataRow["TypeCode"].ToString() + "%'";
							DataTable dataTable2 = publicDbOpClass.DataTableQuary(sqlString);
							if (dataTable2.Rows.Count > 0)
							{
								foreach (DataRow dataRow2 in dataTable2.Rows)
								{
									text = text + "'" + dataRow2["PrjGuid"].ToString() + "',";
								}
							}
						}
					}
				}
			}
			text = text.Substring(0, text.Length - 1);
			return text;
		}
		protected void Button_del_Click(object sender, EventArgs e)
		{
			if (this.HiddenField_ID.Value.Trim() == "")
			{
				this.JavaScriptControl1.Text = "alert('请选择删除的记录！')";
				return;
			}
			List<string> list = new List<string>();
			if (this.HiddenField_ID.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.HiddenField_ID.Value);
			}
			else
			{
				list.Add(this.HiddenField_ID.Value);
			}
			checkAction checkAction = new checkAction();
			using (SqlConnection sqlConnection = new SqlConnection(SqlHelper.ConnectionString))
			{
				sqlConnection.Open();
				SqlTransaction sqlTransaction = sqlConnection.BeginTransaction();
				new StringBuilder();
				try
				{
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i].ToString() != "")
						{
							checkAction.del(list[i].ToString(), sqlTransaction);
						}
					}
					sqlTransaction.Commit();
					this.JavaScriptControl1.Text = "alert('删除成功！')";
					this.bind();
				}
				catch (Exception)
				{
					sqlTransaction.Rollback();
					sqlConnection.Close();
					base.RegisterScript("alert('系统提示：\\n\\n操作失败！');");
				}
			}
		}
		protected void Button_query_Click(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"] == null || base.Request["Levels"] == null)
			{
				this.gvItemInpect.DataSource = "";
				this.gvItemInpect.DataBind();
				return;
			}
			string text = "prjcode='" + base.Request["PrjCode"].ToString() + "' and Flags=" + base.Request["Levels"].ToString();
			if (this.TextBox_sjdw.Text.Trim() != "")
			{
				text = text + " and AcceptCheckUnit like '%" + this.TextBox_sjdw.Text.Trim() + "%'";
			}
			if (this.TextBox_jcdw.Text.Trim() != "")
			{
				text = text + " and ExamineUnit like '%" + this.TextBox_jcdw.Text.Trim() + "%'";
			}
			this.gvItemInpect.DataSource = CheckAction.GetCheckCollections(text);
			this.gvItemInpect.DataBind();
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
						if (datumLogic.UpdateByID(CheckList.table_name, 1, "ID", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, CheckList.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[CheckList.temTable] != null)
											{
												DataTable dataTable = this.ViewState[CheckList.temTable] as DataTable;
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
							this.JavaScriptControl1.Text = "alert('操作成功！')";
							this.bind();
						}
					}
					catch (Exception ex)
					{
						ex.ToString();
						sqlTransaction.Rollback();
						sqlConnection.Close();
						base.RegisterScript("alert('系统提示：\\n\\n操作失败！');");
					}
				}
			}
		}
		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.gvItemInpect.PageIndex = e.NewPageIndex;
			this.bind();
		}
		private void GET_SelectRow()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = this.ViewState[CheckList.temTable] as DataTable;
			dataTable.Clear();
			foreach (GridViewRow gridViewRow in this.gvItemInpect.Rows)
			{
				CheckBox checkBox = gridViewRow.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					HiddenField hiddenField = (HiddenField)gridViewRow.Cells[0].FindControl("HiddenField1_ID");
					string value = hiddenField.Value;
					if (this.ViewState[CheckList.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[CheckList.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("ID='" + value + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["ID"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["ID"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["ExamineUnit"].ToString();
							dataRow["file_info"] = ((array[0]["AcceptCheckContent"].ToString() == "") ? "" : array[0]["AcceptCheckContent"].ToString());
							dataRow["Original_table"] = CheckList.table_name;
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
			this.ViewState[CheckList.temTable] = dataTable;
		}
		private DataTable getTda(string wherestr)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select mark from Prj_ItemInspect ");
			stringBuilder.Append(" WHERE ").Append(wherestr);
			return publicDbOpClass.DataTableQuary(stringBuilder.ToString());
		}
		private DataTable UniteDataTable(DataTable dt1, DataTable dt2, string DTName)
		{
			DataTable dataTable = dt1.Clone();
			for (int i = 0; i < dt2.Columns.Count; i++)
			{
				dataTable.Columns.Add(dt2.Columns[i].ColumnName);
			}
			object[] array = new object[dataTable.Columns.Count];
			for (int j = 0; j < dt1.Rows.Count; j++)
			{
				dt1.Rows[j].ItemArray.CopyTo(array, 0);
				dataTable.Rows.Add(array);
			}
			if (dt1.Rows.Count >= dt2.Rows.Count)
			{
				for (int k = 0; k < dt2.Rows.Count; k++)
				{
					for (int l = 0; l < dt2.Columns.Count; l++)
					{
						dataTable.Rows[k][l + dt1.Columns.Count] = dt2.Rows[k][l].ToString();
					}
				}
			}
			else
			{
				for (int m = 0; m < dt2.Rows.Count - dt1.Rows.Count; m++)
				{
					DataRow row = dataTable.NewRow();
					dataTable.Rows.Add(row);
				}
				for (int n = 0; n < dt2.Rows.Count; n++)
				{
					for (int num = 0; num < dt2.Columns.Count; num++)
					{
						dataTable.Rows[n][num + dt1.Columns.Count] = dt2.Rows[n][num].ToString();
					}
				}
			}
			dataTable.TableName = DTName;
			return dataTable;
		}
		public string GetAnnx(string id, string CheckResult, string IsRectified)
		{
			if (!(base.Request["Type"] == "Rectify"))
			{
				return base.GetAnnx(id);
			}
			if (!(CheckResult == "False") || !(IsRectified == "False"))
			{
				return "";
			}
			string sqlString = "select rectifyMarkID from Prj_ItemInspect where ID=" + id;
			DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
			if (dataTable.Rows.Count <= 0)
			{
				return "";
			}
			if (dataTable.Rows[0]["rectifyMarkID"] != null)
			{
				return base.GetAnnx(dataTable.Rows[0]["rectifyMarkID"].ToString());
			}
			return "";
		}
		protected void gvItemInpect_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.Header && base.Request["Type"] != "Edit")
			{
				e.Row.Cells[0].Visible = false;
				this.btnFiles.Visible = false;
			}
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Attributes["id"] = this.gvItemInpect.DataKeys[e.Row.RowIndex]["ID"].ToString();
				e.Row.Attributes["guid"] = this.gvItemInpect.DataKeys[e.Row.RowIndex]["UID"].ToString();
				e.Row.Attributes["flowState"] = this.gvItemInpect.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
				e.Row.Attributes["ExamineApproveResult"] = dataRowView["ExamineApproveResult"].ToString();
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Row.Attributes["mark"] = dataRowView["mark"].ToString();
				e.Row.Attributes["prjGuid"] = dataRowView["prjCode"].ToString();
				e.Row.Attributes.Add("ondblclick", "ShowInfo(); return false;");
				if (base.Request["Type"] != "Edit")
				{
					e.Row.Cells[0].Visible = false;
					this.btnFiles.Visible = false;
				}
			}
		}
		protected string GetPrjName()
		{
			string result = string.Empty;
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(base.Request["PrjCode"]);
			if (byId != null)
			{
				result = byId.PrjName;
			}
			return result;
		}
	}


