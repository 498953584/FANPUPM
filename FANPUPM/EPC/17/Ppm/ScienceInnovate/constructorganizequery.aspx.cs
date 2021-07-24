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
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConstructOrganizeQuery : NBasePage, IRequiresSessionState
	{
		private ConstructOrganizeBBl ConstructBll = new ConstructOrganizeBBl();
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "EPM_Datum_Affair";

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["Type"] != null)
				{
					this.HdnType.Value = base.Request["Type"].ToString();
				}
				if (base.Request["Levels"] != null)
				{
					this.HdnLevels.Value = base.Request["Levels"].ToString();
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
					this.ContructBind();
				}
				else
				{
					this.BtnAdd.Visible = false;
					this.BtnDel.Visible = false;
					this.BtnUpd.Visible = false;
					this.BtnView.Visible = false;
				}
				this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
				this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
				this.BtnView.Attributes["onclick"] = "openview('',1)";
				this.BtnDel.Attributes["onclick"] = "deleteConstruct()";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[ConstructOrganizeQuery.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdConstruct.ItemDataBound += new DataGridItemEventHandler(this.DGrdConstruct_ItemDataBound);
		}
		private void ContructBind()
		{
			string str = this.ViewState["PRJCODE"].ToString();
			string text = " 1=1 ";
			text = text + " and prjcode='" + str + "' ";
			if (!string.IsNullOrEmpty(base.Request["Levels"]))
			{
				text += " and AuditState=1 ";
			}
			if (this.txtUnit.Text != "")
			{
				text = text + " and FillUnit like '%" + this.txtUnit.Text + "%' ";
			}
			if (this.txtName.Text != "")
			{
				text = text + " and TCO_Name like '%" + this.txtName.Text + "%' ";
			}
			if (this.txtStartTime.Text != "" && this.txtEndTime.Text == "")
			{
				text = text + " and WeaveTime >= '" + this.txtStartTime.Text + "' ";
			}
			if (this.txtStartTime.Text == "" && this.txtEndTime.Text != "")
			{
				text = text + " and WeaveTime <= '" + this.txtEndTime.Text + "' ";
			}
			if (this.txtStartTime.Text != "" && this.txtEndTime.Text != "")
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" and (WeaveTime between '",
					this.txtStartTime.Text,
					"' and '",
					this.txtEndTime.Text,
					"') "
				});
			}
			DataTable dataTable = this.ConstructBll.getDataTable("Prj_V_ScienceInnovate", text, " WeaveTime desc");
			this.ViewState[ConstructOrganizeQuery.resourceTable] = dataTable;
			this.DGrdConstruct.DataSource = dataTable;
			this.DGrdConstruct.DataBind();
		}
		private void EntContructBind()
		{
			string strPrjCode = this.ViewState["PRJCODE"].ToString();
			this.DGrdConstruct.DataSource = ConstructOrganizeAction.GetEntContructList(strPrjCode);
			this.DGrdConstruct.DataBind();
		}
		private void DGrdConstruct_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				this.DGrdConstruct.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["id"] = this.DGrdConstruct.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["guid"] = e.Item.Cells[e.Item.Cells.Count - 1].Text.ToString();
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
				if (e.Item.Cells[7].Text.Length > 15)
				{
					e.Item.Cells[7].ToolTip = e.Item.Cells[7].Text;
					e.Item.Cells[7].Text = e.Item.Cells[7].Text.Substring(0, 14) + "...";
				}
			}
		}
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			int num = 0;
			if (this.hfldConstruct.Value.Contains("["))
			{
				List<string> list = new List<string>();
				list = JsonHelper.GetListFromJson(this.hfldConstruct.Value);
				for (int i = 0; i < list.Count; i++)
				{
					num = ConstructOrganizeAction.Del(list[i].ToString());
				}
			}
			else
			{
				num = ConstructOrganizeAction.Del(this.hfldConstruct.Value);
			}
			if (num == 1)
			{
				base.RegisterScript("top.ui.show('删除成功')");
				this.ContructBind();
				return;
			}
			if (num == 2)
			{
				base.RegisterScript("top.ui.alert('施工组织已经审核过，不能删除！');");
				return;
			}
			if (num == 0)
			{
				base.RegisterScript("top.ui.show('删除失败')");
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			if (this.HdnType.Value == "Edit" || this.HdnType.Value == "Auditing")
			{
				this.ContructBind();
				return;
			}
			this.EntContructBind();
		}
		protected void BtnUpd_Click(object sender, EventArgs e)
		{
			if (this.HdnType.Value == "Edit" || this.HdnType.Value == "Auditing")
			{
				this.ContructBind();
				return;
			}
			this.EntContructBind();
		}
		protected void DGrdConstruct_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdConstruct.CurrentPageIndex = e.NewPageIndex;
			if (this.HdnType.Value == "Edit" || this.HdnType.Value == "Auditing")
			{
				this.ContructBind();
				return;
			}
			this.EntContructBind();
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.ContructBind();
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
						if (datumLogic.UpdateByID("Prj_TechnologyConstructOrganize", "id", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, ConstructOrganizeQuery.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[ConstructOrganizeQuery.temTable] != null)
											{
												DataTable dataTable = this.ViewState[ConstructOrganizeQuery.temTable] as DataTable;
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
							this.ContructBind();
						}
					}
					catch (Exception)
					{
						sqlTransaction.Rollback();
						base.RegisterScript("top.ui.alert('操作失败！');");
					}
				}
			}
		}
		private void GET_SelectRow()
		{
			string text = "";
			DataTable dataTable = this.ViewState[ConstructOrganizeQuery.temTable] as DataTable;
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.DGrdConstruct.Items)
			{
				CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					Label label = (Label)dataGridItem.Cells[0].FindControl("lblid");
					string text2 = label.Text;
					if (this.ViewState[ConstructOrganizeQuery.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[ConstructOrganizeQuery.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("id='" + text2 + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["id"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["id"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["TCO_Name"].ToString();
							dataRow["file_info"] = ((array[0]["Maindescripe"].ToString() == "") ? "" : array[0]["Maindescripe"].ToString());
							dataRow["Original_table"] = "Prj_TechnologyConstructOrganize";
							dataRow["sid_ColumnName"] = "id";
							dataRow["sid_ColumnType"] = 1;
							dataRow["prjcode"] = ((array[0]["PrjCode"].ToString() == "") ? "" : array[0]["PrjCode"].ToString());
							dataTable.Rows.Add(dataRow);
							text = text + array[0]["id"].ToString() + ",";
						}
					}
					this.ViewState["i_id"] = text.ToString();
				}
			}
			this.ViewState[ConstructOrganizeQuery.temTable] = dataTable;
		}
	}


