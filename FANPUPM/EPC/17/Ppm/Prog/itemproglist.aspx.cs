using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ItemProgList : NBasePage, IRequiresSessionState
	{
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "Prj_ItemProg";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["Type"] == "List")
			{
				this.Button_add.Visible = false;
				this.btnAction.Visible = false;
				this.btnCanCel.Visible = false;
				this.Button_edit.Visible = false;
				this.Button_del.Visible = false;
				this.Button_sp.Visible = false;
				this.btn_Search.Visible = true;
				this.TextBox_cfdw.Visible = true;
				this.TextBox_bcfdx.Visible = true;
				this.DropDownList_lb.Visible = true;
				this.Literal1.Visible = true;
				this.Literal2.Visible = true;
				this.Literal3.Visible = true;
				this.Literal4.Text = "项目奖罚查询";
			}
			else
			{
				if (base.Request["Type"] == "Edit")
				{
					this.Button_add.Visible = true;
					this.btnAction.Visible = false;
					this.btnCanCel.Visible = false;
					this.Button_edit.Visible = true;
					this.Button_del.Visible = true;
					this.Button_sp.Visible = false;
					this.btn_Search.Visible = false;
					this.TextBox_cfdw.Visible = false;
					this.TextBox_bcfdx.Visible = false;
					this.DropDownList_lb.Visible = false;
					this.Literal1.Visible = false;
					this.Literal2.Visible = false;
					this.Literal3.Visible = false;
					this.TRS.Visible = false;
					this.Literal4.Text = "项目奖罚";
				}
				else
				{
					if (base.Request["Type"] == "ShenHe")
					{
						this.Button_add.Visible = false;
						this.Button_edit.Visible = false;
						this.btnCanCel.Visible = false;
						this.btnAction.Visible = false;
						this.Button_del.Visible = false;
						this.Button_sp.Visible = true;
						this.btn_Search.Visible = true;
						this.TextBox_cfdw.Visible = true;
						this.TextBox_bcfdx.Visible = true;
						this.DropDownList_lb.Visible = true;
						this.Literal1.Visible = true;
						this.Literal2.Visible = true;
						this.Literal4.Text = "项目奖罚审核";
						this.Literal3.Visible = true;
					}
					else
					{
						if (base.Request["Type"] == "View")
						{
							this.Button_add.Visible = false;
							this.Button_edit.Visible = false;
							this.btnAction.Visible = true;
							this.btnCanCel.Visible = true;
							this.Button_del.Visible = false;
							this.Button_sp.Visible = false;
							this.btn_Search.Visible = true;
							this.TextBox_cfdw.Visible = true;
							this.TextBox_bcfdx.Visible = true;
							this.DropDownList_lb.Visible = true;
							this.Literal1.Visible = true;
							this.Literal2.Visible = true;
							this.Literal3.Visible = true;
							this.Literal4.Text = "项目奖罚执行";
						}
					}
				}
			}
			this.Button_add.Attributes.Add("onclick", "ShowInsertWindow()");
			this.Button_edit.Attributes.Add("onclick", "ShowEditWindow()");
			this.Button_sp.Attributes.Add("onclick", "ShowSpWindow()");
			this.Button_del.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
			this.btnAction.Attributes["onclick"] = "return ActionRecord();";
			this.btnCanCel.Attributes["onclick"] = "return ActionRecord();";
			if (!base.IsPostBack)
			{
				this.bind();
				DataTable allProgSortCollections = ProgSortAction.GetAllProgSortCollections();
				this.DropDownList_lb.DataSource = allProgSortCollections;
				this.DropDownList_lb.DataTextField = "ProgSortName";
				this.DropDownList_lb.DataValueField = "ProgSortCode";
				this.DropDownList_lb.DataBind();
			}
		}
		private void bind()
		{
			DataTable dataTable = new DataTable();
			if (base.Request["PrjCode"] == null || base.Request["Levels"] == null)
			{
				this.DataGrid1.DataSource = "";
				this.DataGrid1.DataBind();
			}
			else
			{
				string text = "prjcode='" + base.Request["PrjCode"].ToString() + "' and ProgSign=" + base.Request["Levels"].ToString();
				if (base.Request["Type"] == "View" || base.Request["Type"] == "List")
				{
					text += " and FlowState=1";
				}
				dataTable = ItemProgAction.GetItemProgCollections(text);
				this.DataGrid1.DataSource = dataTable;
				this.DataGrid1.DataBind();
			}
			this.ViewState[ItemProgList.resourceTable] = dataTable;
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[ItemProgList.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DataGrid1.ItemDataBound += new DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
		}
		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["id"] = e.Item.ItemIndex.ToString();
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this)";
				e.Item.Attributes["style"] = "cursor:hand";
				e.Item.Attributes["mark"] = dataRowView["mark"].ToString();
				e.Item.Attributes["FlowState"] = dataRowView["FlowState"].ToString();
				string text;
				if ((text = e.Item.Cells[12].Text) != null)
				{
					if (text == "False")
					{
						e.Item.Cells[12].Text = "未执行";
						goto IL_157;
					}
					if (text == "True")
					{
						e.Item.Cells[12].Text = "已执行";
						goto IL_157;
					}
				}
				e.Item.Cells[12].Text = "未审核";
				IL_157:
				e.Item.Attributes.Add("ondblclick", "ShowInfo(); return false;");
				e.Item.ToolTip = "双击查看详细信息";
				int num = 0;
				if (e.Item.Cells[12].Text.ToString() == "未执行")
				{
					num = 1;
				}
				e.Item.Attributes["onclick"] = string.Concat(new object[]
				{
					"OnRecord(this);setvalue('",
					e.Item.Cells[2].Text,
					"',",
					num,
					")"
				});
				e.Item.Attributes["guid"] = dataRowView["UID"].ToString();
				e.Item.Attributes["prjGuid"] = dataRowView["PrjCode"].ToString();
			}
		}
		protected void DataGrid1_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DataGrid1.CurrentPageIndex = e.NewPageIndex;
			this.bind();
		}
		protected void Button_del_Click(object sender, EventArgs e)
		{
			string text = string.Empty;
			foreach (DataGridItem dataGridItem in this.DataGrid1.Items)
			{
				CheckBox checkBox = (CheckBox)dataGridItem.FindControl("chk");
				if (checkBox.Checked)
				{
					text = text + dataGridItem.Cells[2].Text + ",";
				}
			}
			if (string.IsNullOrEmpty(text))
			{
				this.JavaScriptControl1.Text = "top.ui.alert('你没有选中数据！')";
				return;
			}
			text = text.Substring(0, text.Length - 1);
			bool flag = ItemProgAction.Delete(text);
			if (flag)
			{
				this.JavaScriptControl1.Text = "alert('删除成功！');window.location.href=window.location.href";
				return;
			}
			this.JavaScriptControl1.Text = "alert('删除失败！')";
		}
		protected void Button_query_Click(object sender, EventArgs e)
		{
			DataTable value = new DataTable();
			if (base.Request["PrjCode"] == null || base.Request["Levels"] == null)
			{
				this.DataGrid1.DataSource = "";
				this.DataGrid1.DataBind();
			}
			else
			{
				string text = string.Concat(new string[]
				{
					"prjcode='",
					base.Request["PrjCode"].ToString(),
					"' and ProgSign=",
					base.Request["Levels"].ToString(),
					" and ProgSortCode=",
					this.DropDownList_lb.SelectedValue
				});
				if (base.Request["Type"] == "View" || base.Request["Type"] == "List")
				{
					text += " and FlowState=1";
				}
				if (!string.IsNullOrEmpty(this.TextBox_bcfdx.Text.Trim()))
				{
					text = text + " and ByProgObject like '%" + this.TextBox_bcfdx.Text.Trim() + "%'";
				}
				if (!string.IsNullOrEmpty(this.TextBox_cfdw.Text.Trim()))
				{
					text = text + " and ProgUnit like '%" + this.TextBox_cfdw.Text.Trim() + "%'";
				}
				value = ItemProgAction.GetItemProgCollections(text);
				this.DataGrid1.DataSource = ItemProgAction.GetItemProgCollections(text);
				this.DataGrid1.DataBind();
			}
			this.ViewState[ItemProgList.resourceTable] = value;
		}
		protected void btnAction_Click(object sender, EventArgs e)
		{
			if (ItemProgAction.ActionItemProg(int.Parse(this.hidItemId.Value)))
			{
				this.JavaScriptControl1.Text = "alert('操作成功！');window.location.href=window.location.href;";
				return;
			}
			this.JavaScriptControl1.Text = "alert('操作失败！')";
		}
		protected void btnCanCel_Click(object sender, EventArgs e)
		{
			if (ItemProgAction.CancelActionItemProg(int.Parse(this.hidItemId.Value)))
			{
				this.JavaScriptControl1.Text = "alert('操作成功！');window.location.href=window.location.href;";
				return;
			}
			this.JavaScriptControl1.Text = "alert('操作失败！')";
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
						if (datumLogic.UpdateByID(ItemProgList.table_name, 1, "ID", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, ItemProgList.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[ItemProgList.temTable] != null)
											{
												DataTable dataTable = this.ViewState[ItemProgList.temTable] as DataTable;
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
							this.JavaScriptControl1.Text = "alert('操作成功！');";
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
		private void GET_SelectRow()
		{
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = this.ViewState[ItemProgList.temTable] as DataTable;
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.DataGrid1.Items)
			{
				CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					HiddenField hiddenField = (HiddenField)dataGridItem.Cells[0].FindControl("HiddenField1");
					string value = hiddenField.Value;
					if (this.ViewState[ItemProgList.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[ItemProgList.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("ID='" + value + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["ID"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["ID"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["ProgUnit"].ToString();
							dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
							dataRow["Original_table"] = ItemProgList.table_name;
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
			this.ViewState[ItemProgList.temTable] = dataTable;
		}
	}


