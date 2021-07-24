using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ExpertProjectQuery : NBasePage, IRequiresSessionState
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
				if (base.Request.QueryString["Type"] != null)
				{
					this.HdnType.Value = base.Request.QueryString["Type"].ToString();
				}
				if (base.Request.QueryString["Levels"] != null)
				{
					this.HdnLevels.Value = base.Request.QueryString["Levels"].ToString();
				}
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
					if (this.HdnType.Value == "Edit" || this.HdnType.Value == "Auditing")
					{
						this.PPMExpertBind();
					}
					else
					{
						this.EntExpertBind();
					}
				}
				else
				{
					this.BtnAdd.Visible = false;
				}
				this.BtnView.Attributes["onclick"] = "openview('',1)";
				this.BtnAdd.Attributes["onclick"] = "openEdit('Add')";
				this.BtnUpd.Attributes["onclick"] = "openEdit('Upd')";
				this.BtnDel.Attributes["onclick"] = "deleteConstruct()";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[ExpertProjectQuery.temTable] = list.Clone();
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.DGrdExpert.ItemDataBound += new DataGridItemEventHandler(this.DGrdExpert_ItemDataBound);
		}
		private void PPMExpertBind()
		{
			string str = this.ViewState["PRJCODE"].ToString();
			string text = " 1=1 ";
			text = text + " and prejectName='" + str + "' ";
			if (!string.IsNullOrEmpty(base.Request.QueryString["Levels"]))
			{
				text += " and AuditState=1 ";
			}
			if (this.txtUnit.Text != "")
			{
				text = text + " and SchemeName like '%" + this.txtUnit.Text + "%' ";
			}
			if (this.txtStartTime.Text != "" && this.txtEndTime.Text == "")
			{
				text = text + " and WeaveDate >= '" + this.txtStartTime.Text + "' ";
			}
			if (this.txtStartTime.Text == "" && this.txtEndTime.Text != "")
			{
				text = text + " and WeaveDate <= '" + this.txtEndTime.Text + "' ";
			}
			if (this.txtStartTime.Text != "" && this.txtEndTime.Text != "")
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" and (WeaveDate between '",
					this.txtStartTime.Text,
					"' and '",
					this.txtEndTime.Text,
					"') "
				});
			}
			DataTable dataTable = this.ConstructBll.getDataTable("Prj_V_ExpertProject", text, "");
			this.ViewState[ExpertProjectQuery.resourceTable] = dataTable;
			this.DGrdExpert.DataSource = dataTable;
			this.DGrdExpert.DataBind();
		}
		private void EntExpertBind()
		{
			string strPrjCode = this.ViewState["PRJCODE"].ToString();
			this.DGrdExpert.DataSource = ExperProjectAction.GetEntContructList(strPrjCode);
			this.DGrdExpert.DataBind();
		}
		private void DGrdExpert_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemIndex != -1)
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["id"] = this.DGrdExpert.DataKeys[e.Item.ItemIndex].ToString();
				e.Item.Attributes["guid"] = e.Item.Cells[e.Item.Cells.Count - 1].Text.ToString();
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
		protected void BtnDel_Click(object sender, EventArgs e)
		{
			int num = ExperProjectAction.ExperDel(this.hfldConstruct.Value);
			if (num == 1)
			{
				base.RegisterScript("top.ui.show('删除成功');");
				this.PPMExpertBind();
				return;
			}
			if (num == 2)
			{
				base.RegisterScript("top.ui.alert('施工组织已经审核过，不能删除！'); ");
				return;
			}
			if (num == 0)
			{
				base.RegisterScript("top.ui.show('删除失败');");
			}
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			if (this.HdnType.Value == "Edit")
			{
				this.PPMExpertBind();
				return;
			}
			this.EntExpertBind();
		}
		protected void BtnUpd_Click(object sender, EventArgs e)
		{
			if (this.HdnType.Value == "Edit")
			{
				this.PPMExpertBind();
				return;
			}
			this.EntExpertBind();
		}
		protected void DGrdExpert_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.DGrdExpert.CurrentPageIndex = e.NewPageIndex;
			if (this.HdnType.Value == "Edit")
			{
				this.PPMExpertBind();
				return;
			}
			this.EntExpertBind();
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.PPMExpertBind();
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
						if (datumLogic.UpdateByID("Prj_ExpertSchemeManage", "MainId", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, ExpertProjectQuery.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[ExpertProjectQuery.temTable] != null)
											{
												DataTable dataTable = this.ViewState[ExpertProjectQuery.temTable] as DataTable;
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
							this.PPMExpertBind();
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
			DataTable dataTable = this.ViewState[ExpertProjectQuery.temTable] as DataTable;
			dataTable.Clear();
			foreach (DataGridItem dataGridItem in this.DGrdExpert.Items)
			{
				CheckBox checkBox = dataGridItem.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					Label label = (Label)dataGridItem.Cells[0].FindControl("lblid");
					string text2 = label.Text;
					if (this.ViewState[ExpertProjectQuery.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[ExpertProjectQuery.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("MainId='" + text2 + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["MainId"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["MainId"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["SchemeName"].ToString();
							dataRow["file_info"] = ((array[0]["schemebewrite"].ToString() == "") ? "" : array[0]["schemebewrite"].ToString());
							dataRow["Original_table"] = "Prj_ExpertSchemeManage";
							dataRow["sid_ColumnName"] = "MainId";
							dataRow["sid_ColumnType"] = 1;
							dataRow["prjcode"] = ((array[0]["prejectName"].ToString() == "") ? "" : array[0]["prejectName"].ToString());
							dataTable.Rows.Add(dataRow);
							text = text + array[0]["MainId"].ToString() + ",";
						}
					}
					this.ViewState["i_id"] = text.ToString();
				}
			}
			this.ViewState[ExpertProjectQuery.temTable] = dataTable;
		}
	}


