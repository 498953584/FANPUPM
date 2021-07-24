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
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ScienceInnovateSum : NBasePage, IRequiresSessionState
	{
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "EPM_Datum_Affair";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["PrjCode"] != null)
				{
					if (base.Request.Params["Type"].ToString() == "List")
					{
						this.HdnType.Value = base.Request.QueryString["Type"].ToString();
						this.BindData();
						return;
					}
					this.BindData();
					this.hidPrjCode.Value = base.Request.Params["PrjCode"].ToString();
					this.btnAdd.Attributes["onclick"] = "return doEdit('new');";
					this.btnEdit.Attributes["onclick"] = "return doEdit(\"edit\")";
					this.btnDel.Attributes["onclick"] = "return CheckData(\"edit\");";
					this.button.Attributes["onclick"] = "return doEdit('view');";
					return;
				}
				else
				{
					base.Response.Write("请选择项目");
					base.Response.End();
				}
			}
		}
		private void BindData()
		{
			string text = " PrjId='" + base.Request.Params["PrjCode"].ToString() + "'";
			if (base.Request.Params["Type"].ToString() == "List")
			{
				text += " and CheckState=1";
			}
			if (this.txtUnit.Text != "")
			{
				text = text + " and bm like '%" + this.txtUnit.Text + "%' ";
			}
			if (this.txtName.Text != "")
			{
				text = text + " and summaryName like '%" + this.txtName.Text + "%' ";
			}
			if (this.txtStartTime.Text != "" && this.txtEndTime.Text == "")
			{
				text = text + " and ReporterDate >= '" + this.txtStartTime.Text + "' ";
			}
			if (this.txtStartTime.Text == "" && this.txtEndTime.Text != "")
			{
				text = text + " and ReporterDate <= '" + this.txtEndTime.Text + "' ";
			}
			if (this.txtStartTime.Text != "" && this.txtEndTime.Text != "")
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" and (ReporterDate between '",
					this.txtStartTime.Text,
					"' and '",
					this.txtEndTime.Text,
					"') "
				});
			}
			DataTable pageData = publicDbOpClass.GetPageData(text, "Prj_Summary", "id desc");
			this.ViewState[ScienceInnovateSum.resourceTable] = pageData;
			this.dgList.DataSource = pageData;
			this.dgList.DataBind();
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[ScienceInnovateSum.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgList.ItemDataBound += new DataGridItemEventHandler(this.dgList_ItemDataBound);
		}
		protected void dgList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				e.Item.Attributes["id"] = this.dgList.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["guid"] = e.Item.Cells[e.Item.Cells.Count - 1].Text.ToString();
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				base.Request.Params["PrjCode"].ToString();
				string sqlString = "select PrjName    from pt_PrjInfo where Prjguid='" + dataRowView["prjid"] + "'";
				DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
				if (dataTable.Rows.Count > 0)
				{
					e.Item.Cells[4].Text = dataTable.Rows[0]["PrjName"].ToString();
				}
				else
				{
					e.Item.Cells[4].Text = "";
				}
				userManageDb userManageDb = new userManageDb();
				e.Item.Cells[6].Text = userManageDb.GetUserName(e.Item.Cells[6].Text);
				string a = dataRowView["mark"].ToString();
				e.Item.Attributes["mark"] = dataRowView["mark"].ToString();
				HtmlImage htmlImage = (HtmlImage)e.Item.Cells[1].FindControl("imgPNG");
				if (a == "1")
				{
					htmlImage.Src = "~/images/over.gif";
					return;
				}
				if (a == "2" || a == "0")
				{
					htmlImage.Src = "~/images/Edit.gif";
					return;
				}
				if (a == "3")
				{
					htmlImage.Src = "~/images/Process.gif";
				}
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void dgList_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.dgList.CurrentPageIndex = e.NewPageIndex;
			this.BindData();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldConstruct.Value);
			string text = "";
			for (int i = 0; i < listFromJson.Count; i++)
			{
				if (i < listFromJson.Count - 1)
				{
					text = text + listFromJson[i].ToString() + ",";
				}
				else
				{
					text += listFromJson[i].ToString();
				}
			}
			string sqlString = "delete Prj_Summary where id in (" + text + ")";
			if (publicDbOpClass.NonQuerySqlString(sqlString))
			{
				base.RegisterScript("top.ui.show('删除成功！');");
				this.BindData();
				return;
			}
			base.RegisterScript("top.ui.show('删除失败！');");
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
		protected void button_Click(object sender, EventArgs e)
		{
			this.BindData();
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
						if (datumLogic.UpdateByID("Prj_Summary", "id", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, ScienceInnovateSum.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[ScienceInnovateSum.temTable] != null)
											{
												DataTable dataTable = this.ViewState[ScienceInnovateSum.temTable] as DataTable;
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
							base.RegisterScript("top.ui.show('操作成功!');");
							this.BindData();
						}
					}
					catch (Exception)
					{
						sqlTransaction.Rollback();
						base.RegisterScript("top.ui.show('操作失败！');");
					}
				}
			}
		}
		private void GET_SelectRow()
		{
			string text = "";
			DataTable dataTable = this.ViewState[ScienceInnovateSum.temTable] as DataTable;
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.dgList.Items)
			{
				CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					Label label = (Label)dataGridItem.Cells[0].FindControl("lblid");
					string text2 = label.Text;
					if (this.ViewState[ScienceInnovateSum.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[ScienceInnovateSum.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("id='" + text2 + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["id"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["id"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["summaryName"].ToString();
							dataRow["file_info"] = ((array[0]["Comment"].ToString() == "") ? "" : array[0]["Comment"].ToString());
							dataRow["Original_table"] = "Prj_Summary";
							dataRow["sid_ColumnName"] = "id";
							dataRow["sid_ColumnType"] = 1;
							dataRow["prjcode"] = ((array[0]["prjid"].ToString() == "") ? "" : array[0]["prjid"].ToString());
							dataTable.Rows.Add(dataRow);
							text = text + array[0]["id"].ToString() + ",";
						}
					}
					this.ViewState["i_id"] = text.ToString();
				}
			}
			this.ViewState[ScienceInnovateSum.temTable] = dataTable;
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.BindData();
		}
	}


