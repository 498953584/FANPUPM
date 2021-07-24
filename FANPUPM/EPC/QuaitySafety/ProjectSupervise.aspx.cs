using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Files;
using cn.justwin.stockBLL.epm.datum;
using cn.justwin.stockBLL.Files;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProjectSupervise : NBasePage, IRequiresSessionState
	{
		protected string strTitle = "";
		private static string _typeVal = string.Empty;
		public string listTitleStr = string.Empty;
		public static string _QS = string.Empty;
		public static string TYPE = "Edit";
		private int CA_val;
		private FilesLogic flc = new FilesLogic();
		private static string temTable = "temtable";
		private static string resourceTable = "resourceTable";
		private static string table_name = "EPM_Datum_Affair";

		public Guid PrjCode
		{
			get
			{
				object obj = this.ViewState["PrjCode"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PrjCode"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			this.P_Type();
			if (!base.IsPostBack)
			{
				if (base.Request.QueryString["TypeId"] == null)
				{
					base.Response.End();
				}
				if (base.Request.QueryString["prjId"] != null)
				{
					this.PrjCode = new Guid(base.Request.QueryString["prjId"].ToString());
				}
				if (this.ViewState[ProjectSupervise.resourceTable] != null)
				{
					this.ViewState[ProjectSupervise.resourceTable] = null;
				}
				this.Data_Bind();
				this.ClientEvent();
			}
		}
		private void ClientEvent()
		{
			this.BtnAdd.Attributes["onclick"] = "javascript:return OpType('ADD','" + this.PrjCode + "','')";
			this.BtnModify.Attributes["onclick"] = "javascript:return OpType('EDIT','" + this.PrjCode + "','')";
			this.BtnDelete.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
			this.BtnSee.Attributes["onclick"] = "javascript:return OpType('SEE','" + this.PrjCode + "','');";
		}
		private void P_Type()
		{
			this.EDIT.Value = "Edit";
			if (base.Request.QueryString["Type"] != "Edit")
			{
				this.EDIT.Value = "List";
				this.BtnAdd.Visible = false;
				this.BtnModify.Visible = false;
				this.BtnDelete.Visible = false;
			}
			if (base.Request.QueryString["Flag"] == "Q")
			{
				this.Session["AffairFlageCode"] = "11";
				this.hdnClass.Value = "10";
			}
			else
			{
				if (base.Request.QueryString["Flag"] == "S")
				{
					this.Session["AffairFlageCode"] = "10";
					this.hdnClass.Value = "9";
				}
			}
			this.Session["DATUMCLASS"] = base.Request.QueryString["TypeId"].ToString();
		}
		protected void Data_Bind()
		{
			this.Session["DATUMCLASS"].ToString();
			DataTable pageData = AffairAction.GetPageData(this.PrjCode, this.Session["DATUMCLASS"].ToString(), this.DDLLookup.SelectedValue, this.TxtLookup.Text);
			DataRow[] array = pageData.Select("CA=" + this.CA_val);
			DataTable dataTable = new DataTable();
			dataTable = pageData.Clone();
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row = array2[i];
				dataTable.ImportRow(row);
			}
			this.ViewState[ProjectSupervise.resourceTable] = dataTable;
			this.GridView1.DataSource = dataTable;
			this.GridView1.DataBind();
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void btn_Search_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected void BtnDelete_Click(object sender, EventArgs e)
		{
			List<string> list = new List<string>();
			if (this.Hidden1.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.Hidden1.Value);
			}
			else
			{
				list.Add(this.Hidden1.Value);
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].ToString() != "")
				{
					if (list.Count - 1 == i)
					{
						stringBuilder.Append("'").Append(list[i].ToString()).Append("'");
					}
					else
					{
						stringBuilder.Append("'").Append(list[i].ToString()).Append("',");
					}
				}
			}
			DatumLogic datumLogic = new DatumLogic();
			if (datumLogic.DeleteList(stringBuilder.ToString()))
			{
				this.JS.Text = "alert('操作成功!');";
				this.Data_Bind();
				return;
			}
			this.JS.Text = "alert('操作失败,网络连接故障，请稍候再试')";
		}
		protected void BtnModify_Click(object sender, EventArgs e)
		{
			this.Data_Bind();
		}
		protected override void OnInit(EventArgs e)
		{
			if (base.Request.QueryString["CA"] != null && base.Request.QueryString["CA"].ToString() != "")
			{
				this.hiden_CA.Value = base.Request.QueryString["CA"].ToString();
				this.CA_val = int.Parse(base.Request.QueryString["CA"].ToString());
			}
			if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"] == "Q")
			{
				ProjectSupervise._QS = "Q";
				this.QS.Value = "Q";
			}
			else
			{
				if (base.Request.QueryString["Flag"] != null && base.Request.QueryString["Flag"] == "S")
				{
					ProjectSupervise._QS = "S";
					this.QS.Value = "S";
				}
			}
			if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"] == "Edit")
			{
				ProjectSupervise._typeVal = "Edit";
				this.strTitle = this.swTitle(this.CA_val.ToString(), "Edit", ProjectSupervise._QS);
			}
			else
			{
				if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"] == "List")
				{
					this.strTitle = this.swTitle(this.CA_val.ToString(), "List", ProjectSupervise._QS);
					ProjectSupervise._typeVal = "List";
					this.btnFiles.Visible = false;
				}
			}
			DataTable list = this.flc.GetList("1<0");
			this.ViewState[ProjectSupervise.temTable] = list.Clone();
			base.OnInit(e);
		}
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				string text = this.GridView1.DataKeys[e.Row.RowIndex].Values[0].ToString();
				e.Row.Attributes["id"] = text;
				e.Row.Attributes["mark"] = dataRowView["mark"].ToString();
				e.Row.Attributes["onclick"] = "clickRow(this);";
				CheckBox checkBox = (CheckBox)e.Row.Cells[0].FindControl("chk");
				checkBox.Attributes["class"] = "chk";
				checkBox.Attributes["id"] = text;
				Label label = (Label)e.Row.Cells[0].FindControl("Label3");
				label.Attributes["style"] = "display:none";
				string text2 = ProjectSupervise.StripHTML(e.Row.Cells[5].Text);
				if (text2.Length > 22)
				{
					e.Row.Cells[5].Text = text2.Substring(0, 21) + "...";
				}
				else
				{
					e.Row.Cells[5].Text = text2;
				}
				e.Row.Cells[5].ToolTip = text2;
				Label label2 = (Label)e.Row.Cells[2].FindControl("Label1");
				label2.Attributes["onclick"] = string.Concat(new object[]
				{
					"OpType('SEE','",
					this.PrjCode,
					"','",
					text,
					"');"
				});
				label2.Text = dataRowView["AffairTitle"].ToString();
				if (ProjectSupervise._typeVal == "List")
				{
					e.Row.Cells[0].Text = "";
					e.Row.Cells[0].Visible = false;
					return;
				}
			}
			else
			{
				if (e.Row.RowType == DataControlRowType.Header)
				{
					e.Row.Cells[3].Text = this.listTitleStr;
					if (ProjectSupervise._typeVal == "List")
					{
						e.Row.Cells[0].Text = "";
						e.Row.Cells[0].Visible = false;
					}
				}
			}
		}
		protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
		{
			this.GridView1.PageIndex = e.NewPageIndex;
			this.Data_Bind();
		}
		public static string DelHTML(string Htmlstring)
		{
			Htmlstring = Regex.Replace(Htmlstring, "<script[^>]*?>.*?</script>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<(.[^>]*)>", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "([\\r\\n])[\\s]+", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "-->", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<!--.*", "", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "<A>.*</A>", "");
			Htmlstring = Regex.Replace(Htmlstring, "&(quot|#34);", "\"", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(amp|#38);", "&", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(lt|#60);", "<", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(gt|#62);", ">", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(nbsp|#160);", " ", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(iexcl|#161);", "¡", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(cent|#162);", "¢", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(pound|#163);", "£", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&(copy|#169);", "©", RegexOptions.IgnoreCase);
			Htmlstring = Regex.Replace(Htmlstring, "&#(\\d+);", "", RegexOptions.IgnoreCase);
			Htmlstring.Replace("<", "");
			Htmlstring.Replace(">", "");
			Htmlstring.Replace("\r\n", "");
			return Htmlstring;
		}
		public static string StripHTML(string strHtml)
		{
			string[] array = new string[]
			{
				"<script[^>]*?>.*?</script>",
				"<(\\/\\s*)?!?((\\w+:)?\\w+)(\\w+(\\s*=?\\s*(([\"'])(file://[\"'tbnr]|[^/7])*?/7|/w+)|.{0})|/s)*?(///s*)?>",
				"([\\r\\n])[\\s]+",
				"&(quot|#34);",
				"&(amp|#38);",
				"&(lt|#60);",
				"&(gt|#62);",
				"&(nbsp|#160);",
				"&(iexcl|#161);",
				"&(cent|#162);",
				"&(pound|#163);",
				"&(copy|#169);",
				"&#(\\d+);",
				"-->",
				"<!--.*\\n"
			};
			string[] array2 = new string[]
			{
				"",
				"",
				"",
				"\"",
				"&",
				"<",
				">",
				" ",
				"¡",
				"¢",
				"£",
				"©",
				"",
				"\r\n",
				""
			};
			string arg_135_0 = array[0];
			string text = strHtml;
			for (int i = 0; i < array.Length; i++)
			{
				Regex regex = new Regex(array[i], RegexOptions.IgnoreCase);
				text = regex.Replace(text, array2[i]);
			}
			text.Replace("<", "");
			text.Replace(">", "");
			text.Replace("\r\n", "");
			return ProjectSupervise.DelHTML(text);
		}
		public string swTitle(string v, string edit, string QS)
		{
			string result = string.Empty;
			if (v != null)
			{
				if (!(v == "1"))
				{
					if (!(v == "2"))
					{
						if (!(v == "3"))
						{
							if (!(v == "4"))
							{
								if (!(v == "5"))
								{
									if (v == "6")
									{
										if (QS == "S")
										{
											if (edit == "Edit")
											{
												result = "安全措施维护";
											}
											else
											{
												if (edit == "List")
												{
													result = "安全措施查看";
												}
											}
											this.listTitleStr = "安全措施名称";
										}
									}
								}
								else
								{
									if (QS == "S")
									{
										if (edit == "Edit")
										{
											result = "安全事故报告";
										}
										else
										{
											if (edit == "List")
											{
												result = "事故查看";
											}
										}
										this.listTitleStr = "安全事故记录名称";
									}
								}
							}
							else
							{
								if (QS == "S")
								{
									if (edit == "Edit")
									{
										result = "安全监督";
									}
									else
									{
										if (edit == "List")
										{
											result = "安全检查记录查看";
										}
									}
									this.listTitleStr = "安全检查记录名称";
								}
							}
						}
						else
						{
							if (QS == "Q")
							{
								if (edit == "Edit")
								{
									result = "工程质量竣工验收资料管理";
								}
								else
								{
									if (edit == "List")
									{
										result = "工程质量竣工验收记录查看";
									}
								}
								this.listTitleStr = "资料名称";
							}
						}
					}
					else
					{
						if (QS == "Q")
						{
							if (edit == "Edit")
							{
								result = "质量事故记录";
							}
							else
							{
								if (edit == "List")
								{
									result = "事故记录查看";
								}
							}
							this.listTitleStr = "事故记录名称";
						}
					}
				}
				else
				{
					if (QS == "Q")
					{
						if (edit == "Edit")
						{
							result = "质量检查记录";
						}
						else
						{
							if (edit == "List")
							{
								result = "质量检查资料查询";
							}
						}
						this.listTitleStr = "检查记录名称";
					}
				}
			}
			return result;
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
						if (datumLogic.UpdateByID(ProjectSupervise.table_name, "i_id", text, sqlTransaction))
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
											this.flc.Update(array[i].ToString(), 1, ProjectSupervise.table_name, sqlTransaction);
										}
										else
										{
											if (this.ViewState[ProjectSupervise.temTable] != null)
											{
												DataTable dataTable = this.ViewState[ProjectSupervise.temTable] as DataTable;
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
			this.ViewState["i_id"] = "";
			StringBuilder stringBuilder = new StringBuilder();
			DataTable dataTable = this.ViewState[ProjectSupervise.temTable] as DataTable;
			dataTable.Clear();
			foreach (GridViewRow gridViewRow in this.GridView1.Rows)
			{
				CheckBox checkBox = gridViewRow.Cells[0].FindControl("chk") as CheckBox;
				if (checkBox != null && checkBox.Checked)
				{
					DataRow dataRow = dataTable.NewRow();
					Label label = (Label)gridViewRow.Cells[0].FindControl("Label3");
					string text = label.Text;
					if (this.ViewState[ProjectSupervise.resourceTable] != null)
					{
						DataTable dataTable2 = this.ViewState[ProjectSupervise.resourceTable] as DataTable;
						DataRow[] array = dataTable2.Select("i_id='" + text + "'");
						DataRow[] array2 = dataTable.Select("file_sid='" + array[0]["i_id"].ToString() + "'");
						if (array2.Length == 0)
						{
							dataRow["ID"] = Guid.NewGuid();
							dataRow["file_sid"] = array[0]["i_id"].ToString();
							dataRow["file_mark"] = 1;
							dataRow["file_name"] = array[0]["AffairTitle"].ToString();
							dataRow["file_info"] = ((array[0]["Remark"].ToString() == "") ? "" : array[0]["Remark"].ToString());
							dataRow["Original_table"] = ProjectSupervise.table_name;
							dataRow["sid_ColumnName"] = "i_id";
							dataRow["sid_ColumnType"] = 2;
							dataRow["prjcode"] = ((array[0]["PrjCode"].ToString() == "") ? "" : array[0]["PrjCode"].ToString());
							dataRow["Content"] = ((array[0]["Context"].ToString() == "") ? "" : array[0]["Context"].ToString());
							dataTable.Rows.Add(dataRow);
							stringBuilder.Append(array[0]["i_id"].ToString() + ",");
						}
					}
					this.ViewState["i_id"] = stringBuilder.ToString();
				}
			}
			this.ViewState[ProjectSupervise.temTable] = dataTable;
		}
	}


