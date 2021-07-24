using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TechnologyStandardQuery : NBasePage, IRequiresSessionState
	{
		private string _Levels = "";
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "EPM_Datum_Affair";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["prjId"] != null)
				{
					this.HdnPrjCode.Value = base.Request.QueryString["prjId"].ToString();
					PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
					PTPrjInfo byId = pTPrjInfoService.GetById(this.HdnPrjCode.Value);
					if (byId != null)
					{
						this.HdnPrjName.Value = byId.PrjName;
					}
					this.ViewState["PRJCODE"] = this.HdnPrjCode.Value;
					this.ViewState["PRJNAME"] = this.HdnPrjName.Value;
					this.ViewState["LEVELS"] = base.Request.QueryString["Levels"].ToString().Trim();
					this._Levels = this.ViewState["LEVELS"].ToString();
					if (this._Levels.Trim() != "")
					{
						this.BtnAdd.Attributes.Add("style", "display:none");
						this.BtnSelect.Attributes.Add("style", "display:none");
						this.BtnUpd.Attributes.Add("style", "display:none");
						this.BtnDel.Attributes.Add("style", "display:none");
						this.btnSave.Attributes.Add("style", "display:none");
						this.btnFiles.Attributes.Add("style", "display:none");
					}
					else
					{
						this.BtnUpd.Enabled = false;
						this.BtnDel.Enabled = false;
						if (base.Request.QueryString["Type"] == "Auditing")
						{
							this.btnFiles.Attributes.Add("style", "display:none");
							this.BtnSelect.Attributes.Add("style", "display:none");
							this.BtnDel.Attributes.Add("style", "display:none");
							this.BtnUpd.Attributes.Add("style", "display:none");
							this.BtnAdd.Attributes.Add("style", "display:none");
							this.DGrdStandard.Columns[this.DGrdStandard.Columns.Count - 1].Visible = true;
						}
						else
						{
							this.btnSave.Attributes.Add("style", "display:none");
						}
					}
					this.StandardBind();
					this.BtnSelect.Attributes["onclick"] = "StandardSelect()";
					this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
					this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
					this.BtnView.Attributes["onclick"] = "openEdit('View')";
					this.BtnDel.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
					return;
				}
				this.BtnAdd.Visible = false;
				this.BtnSelect.Visible = false;
				this.BtnAdd.Visible = false;
				this.BtnUpd.Visible = false;
				this.BtnDel.Visible = false;
				this.BtnView.Visible = false;
				this.btnSave.Visible = false;
				this.btnFiles.Visible = false;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[TechnologyStandardQuery.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdStandard.ItemDataBound += new DataGridItemEventHandler(this.DGrdStandard_ItemDataBound);
		}
		private void StandardBind()
		{
			string strPrjCode = this.ViewState["PRJCODE"].ToString();
			DataTable standardList = TechnologyStandardAction.GetStandardList(strPrjCode);
			this.ViewState[TechnologyStandardQuery.resourceTable] = standardList;
			this.DGrdStandard.DataSource = standardList;
			this.DGrdStandard.DataBind();
		}
		private void DGrdStandard_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex > -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				object obj = DataBinder.Eval(e.Item.DataItem, "MainKey");
				e.Item.Attributes["id"] = this.DGrdStandard.DataKeys[e.Item.ItemIndex].ToString();
				if (obj != DBNull.Value && obj.ToString() != "0")
				{
					AttributeCollection attributes;
					(attributes = e.Item.Attributes)["onclick"] = attributes["onclick"] + "CanEdit(true);";
				}
				else
				{
					AttributeCollection attributes2;
					(attributes2 = e.Item.Attributes)["onclick"] = attributes2["onclick"] + "CanEdit(false);";
				}
				object obj2 = DataBinder.Eval(e.Item.DataItem, "IsAudited");
				if (obj2 != DBNull.Value)
				{
					((CheckBox)e.Item.FindControl("isAudit")).Checked = Convert.ToBoolean(obj2.ToString().ToLower());
				}
				if (e.Item.Cells[7].Text.ToString().Length > 15)
				{
					e.Item.Cells[7].Text = e.Item.Cells[7].Text.ToString().Substring(0, 14) + "…";
				}
				string a = dataRowView["mark"].ToString();
				e.Item.Attributes["mark"] = dataRowView["mark"].ToString();
				HtmlImage htmlImage = (HtmlImage)e.Item.Cells[1].FindControl("imgPNG");
				if (a == "1")
				{
					htmlImage.Src = "~/images/over.gif";
				}
				else
				{
					if (a == "2" || a == "0")
					{
						htmlImage.Src = "~/images/Edit.gif";
					}
					else
					{
						if (a == "3")
						{
							htmlImage.Src = "~/images/Process.gif";
						}
					}
				}
				if (base.Request.QueryString["Type"] == "Auditing")
				{
					e.Item.Cells[0].Text = "";
					e.Item.Cells[0].Visible = false;
					return;
				}
			}
			else
			{
				if (base.Request.QueryString["Type"] == "Auditing")
				{
					e.Item.Cells[0].Text = "";
					e.Item.Cells[0].Visible = false;
				}
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.StandardBind();
		}
		protected void BtnUpd_Click(object sender, EventArgs e)
		{
			this.StandardBind();
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			int num = TechnologyStandardAction.TechnologyDel(this.HdnId.Value);
			if (num == 1)
			{
				base.RegisterScript("top.ui.show('删除成功')");
				this.StandardBind();
				return;
			}
			if (num == 0)
			{
				base.RegisterScript("top.ui.alert('删除失败')");
			}
		}
		protected void BtnSelect_Click(object sender, EventArgs e)
		{
			this.StandardBind();
		}
		protected void DGrdStandard_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdStandard.CurrentPageIndex = e.NewPageIndex;
			this.StandardBind();
		}
		private DataTable getData()
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("Keys"));
			dataTable.Columns.Add(new DataColumn("IsAudit"));
			foreach (DataGridItem dataGridItem in this.DGrdStandard.Items)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["Keys"] = this.DGrdStandard.DataKeys[dataGridItem.ItemIndex].ToString();
				dataRow["IsAudit"] = (((CheckBox)dataGridItem.FindControl("isAudit")).Checked ? "1" : "0");
				dataTable.Rows.Add(dataRow.ItemArray);
			}
			return dataTable;
		}
		protected void btnSave_Click(object sender, EventArgs e)
		{
			if (TechnologyStandardAction.AuditTechnologyCriterion(this.getData()))
			{
				base.RegisterScript("top.ui.show('操作成功');");
				this.StandardBind();
				return;
			}
			base.RegisterScript("top.ui.alert('操作失败！'); ");
		}
		protected void PaginationControl1_PageIndexChange(object sender, EventArgs e)
		{
			this.StandardBind();
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
						if (datumLogic.UpdateByID("Prj_TechnologyCriterion", "MainId", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, TechnologyStandardQuery.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[TechnologyStandardQuery.temTable] != null)
											{
												DataTable dataTable = this.ViewState[TechnologyStandardQuery.temTable] as DataTable;
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
							base.RegisterScript("alert('操作成功!');");
							this.StandardBind();
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
			string text = "";
			DataTable dataTable = this.ViewState[TechnologyStandardQuery.temTable] as DataTable;
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.DGrdStandard.Items)
			{
				CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					Label label = (Label)dataGridItem.Cells[0].FindControl("lblid");
					string text2 = label.Text;
					if (this.ViewState[TechnologyStandardQuery.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[TechnologyStandardQuery.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("MainId='" + text2 + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["MainId"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["MainId"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["TechnologyCriterionID"].ToString();
							dataRow["file_info"] = ((array[0]["TechnologyName"].ToString() == "") ? "" : array[0]["TechnologyName"].ToString());
							dataRow["Original_table"] = "Prj_TechnologyCriterion";
							dataRow["sid_ColumnName"] = "MainId";
							dataRow["sid_ColumnType"] = 1;
							dataRow["prjcode"] = ((array[0]["PrjCode"].ToString() == "") ? "" : array[0]["PrjCode"].ToString());
							dataTable.Rows.Add(dataRow);
							text = text + array[0]["MainId"].ToString() + ",";
						}
					}
					this.ViewState["i_id"] = text.ToString();
				}
			}
			this.ViewState[TechnologyStandardQuery.temTable] = dataTable;
		}
	}


