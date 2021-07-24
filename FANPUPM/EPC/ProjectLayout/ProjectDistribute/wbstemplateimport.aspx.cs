using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class WbsTemplateImport : PageBase, IRequiresSessionState
	{
		private string PrjCodeValue;
		private ConstructWBSTaskAction cw = new ConstructWBSTaskAction();
		private TempWbsAction tw = new TempWbsAction();

		public DataTable dt
		{
			get
			{
				return (DataTable)this.ViewState["table"];
			}
			set
			{
				this.ViewState["table"] = value;
			}
		}
		public DataTable dt2
		{
			get
			{
				return (DataTable)this.ViewState["table2"];
			}
			set
			{
				this.ViewState["table2"] = value;
			}
		}
		protected int ImportType
		{
			get
			{
				return Convert.ToInt32(this.ViewState["IMPORTTYPE"]);
			}
			set
			{
				this.ViewState["IMPORTTYPE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"] != null || base.Request["t"] != null)
			{
				this.PrjCodeValue = base.Request["PrjCode"].ToString();
				this.ImportType = Convert.ToInt32(base.Request["t"]);
			}
			bool arg_60_0 = base.IsPostBack;
		}
		protected void Btn_excel_Click(object sender, EventArgs e)
		{
			if (this.File1.PostedFile.ContentLength == 0)
			{
				this.Page.RegisterStartupScript("", "<script>alert('请选择上传文件或上传文件内容为空!');</script>");
				return;
			}
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string[] array = fileUpload.Upload(this.File1.PostedFile, 14);
			if (array[1].ToString().IndexOf(".xls") == -1)
			{
				this.Page.RegisterStartupScript("", "<script>alert('请选择上传excel文件!');</script>");
				return;
			}
			if (array[1].Length == 0 || array[2].Length > 0)
			{
				this.js.Text = "alert('连接数据失败！')";
				return;
			}
			string text = array[1].ToString();
			string myFileName = base.Server.MapPath(text.ToString());
			if (this.ImportType == 1)
			{
				this.data_bind1(myFileName);
			}
			else
			{
				if (this.ImportType == 2)
				{
					this.Rationdata_bind(myFileName);
				}
				else
				{
					if (this.ImportType == 3)
					{
						this.data_bind3(myFileName);
					}
				}
			}
			this.Btn_imp.Enabled = true;
		}
		private void data_bind(string myFileName)
		{
			string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + myFileName + " ;Extended Properties=Excel 8.0;";
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			oleDbConnection.Open();
			DataTable oleDbSchemaTable = oleDbConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			oleDbSchemaTable.Rows[1][2].ToString();
			oleDbSchemaTable.Rows[0][2].ToString();
			OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
			oleDbDataAdapter.SelectCommand = new OleDbCommand("SELECT * FROM   [Sheet1$]", oleDbConnection);
			DataSet dataSet = new DataSet();
			oleDbDataAdapter.Fill(dataSet);
			this.dt = dataSet.Tables[0];
			this.dt.Rows.RemoveAt(0);
			this.dt.Rows.RemoveAt(0);
			this.dt.Columns.Add(new DataColumn("F9", typeof(string)));
			this.dgd_TaskList.DataSource = this.dt;
			this.dgd_TaskList.DataBind();
			OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter();
			oleDbDataAdapter2.SelectCommand = new OleDbCommand("SELECT * FROM   [Sheet2$]", oleDbConnection);
			DataSet dataSet2 = new DataSet();
			oleDbDataAdapter2.Fill(dataSet2);
			this.dt2 = this.FormatDtResource(dataSet2.Tables[0]);
			this.dgd_List.DataSource = this.dt2;
			this.dgd_List.DataBind();
			this.TempDataInsert(this.dt, this.dt2);
			oleDbConnection.Close();
		}
		private void data_bind3(string myFileName)
		{
			string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + myFileName + " ;Extended Properties=Excel 8.0;";
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			oleDbConnection.Open();
			try
			{
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
				oleDbDataAdapter.SelectCommand = new OleDbCommand("SELECT * FROM   [Sheet1$]", oleDbConnection);
				DataSet dataSet = new DataSet();
				oleDbDataAdapter.Fill(dataSet);
				this.dt = dataSet.Tables[0];
				this.dt.Rows.RemoveAt(0);
				this.dt.Rows.RemoveAt(0);
			}
			catch
			{
				this.Page.RegisterStartupScript("", "<script>alert('你要导入的Excel文件里没有附和要求的工作表单Sheet1');</script>");
				return;
			}
			try
			{
				this.dt.Columns.Add(new DataColumn("F9", typeof(string)));
				this.dgd_TaskList.DataSource = this.dt;
				this.dgd_TaskList.DataBind();
				OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter();
				oleDbDataAdapter2.SelectCommand = new OleDbCommand("SELECT * FROM   [Sheet2$]", oleDbConnection);
				DataSet dataSet2 = new DataSet();
				oleDbDataAdapter2.Fill(dataSet2);
				if (this.ImportType == 3)
				{
					this.dt2 = this.FormatResourceList3(dataSet2.Tables[0]);
				}
				else
				{
					if (this.ImportType == 1)
					{
						this.dt2 = this.FormatResourceList1(dataSet2.Tables[0]);
					}
				}
				this.dt2.Rows.RemoveAt(0);
				this.dgd_List.DataSource = this.dt2;
				this.dgd_List.DataBind();
				this.TempDataInsert(this.dt, this.dt2);
			}
			catch
			{
				this.Page.RegisterStartupScript("", "<script>alert('你要导入的Excel文件里没有附和要求的工作表单Sheet2');</script>");
				return;
			}
			oleDbConnection.Close();
		}
		private void data_bind1(string myFileName)
		{
			string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + myFileName + " ;Extended Properties=Excel 8.0;";
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			oleDbConnection.Open();
			try
			{
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
				oleDbDataAdapter.SelectCommand = new OleDbCommand("SELECT * FROM   [Sheet1$]", oleDbConnection);
				DataSet dataSet = new DataSet();
				oleDbDataAdapter.Fill(dataSet);
				this.dt = dataSet.Tables[0];
				this.dt.Rows.RemoveAt(0);
				this.dt.Rows.RemoveAt(0);
			}
			catch
			{
				this.Page.RegisterStartupScript("", "<script>alert('你要导入的Excel文件里没有附和要求的工作表单Sheet1');</script>");
				return;
			}
			try
			{
				this.dgd_TaskList.DataSource = this.dt;
				this.dgd_TaskList.DataBind();
				OleDbDataAdapter oleDbDataAdapter2 = new OleDbDataAdapter();
				oleDbDataAdapter2.SelectCommand = new OleDbCommand("SELECT * FROM   [Sheet2$]", oleDbConnection);
				DataSet dataSet2 = new DataSet();
				oleDbDataAdapter2.Fill(dataSet2);
				this.dt2 = this.FormatResourceList1(dataSet2.Tables[0]);
				this.dt2.Rows.RemoveAt(0);
				this.dgd_List.DataSource = this.dt2;
				this.dgd_List.DataBind();
				this.TempDataInsert(this.dt, this.dt2);
			}
			catch
			{
				this.Page.RegisterStartupScript("", "<script>alert('你要导入的Excel文件里没有附和要求的工作表单Sheet2');</script>");
				return;
			}
			oleDbConnection.Close();
		}
		private DataTable FormatResourceList1(DataTable Rldt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("清单编号", typeof(string)));
			dataTable.Columns.Add(new DataColumn("清单名称", typeof(string)));
			dataTable.Columns.Add(new DataColumn("费用类别", typeof(string)));
			dataTable.Columns.Add(new DataColumn("资源名称", typeof(string)));
			dataTable.Columns.Add(new DataColumn("单位", typeof(string)));
			dataTable.Columns.Add(new DataColumn("单价", typeof(string)));
			dataTable.Columns.Add(new DataColumn("数量", typeof(string)));
			dataTable.Columns.Add(new DataColumn("金额", typeof(string)));
			dataTable.Columns.Add(new DataColumn("综合费率", typeof(string)));
			dataTable.Columns.Add(new DataColumn("含量", typeof(string)));
			for (int i = 0; i < Rldt.Rows.Count; i++)
			{
				if (!(Rldt.Rows[i][0].ToString() == "") || !(Rldt.Rows[i][1].ToString() == "") || !(Rldt.Rows[i][2].ToString() == "") || !(Rldt.Rows[i][3].ToString() == "") || !(Rldt.Rows[i][4].ToString() == ""))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["清单编号"] = Rldt.Rows[i][7].ToString();
					dataRow["清单名称"] = "";
					dataRow["费用类别"] = "";
					dataRow["资源名称"] = Rldt.Rows[i][0].ToString() + Rldt.Rows[i][2].ToString();
					dataRow["单位"] = Rldt.Rows[i][3].ToString();
					dataRow["单价"] = Rldt.Rows[i][5].ToString();
					dataRow["数量"] = Rldt.Rows[i][4].ToString();
					dataRow["金额"] = Rldt.Rows[i][6].ToString();
					dataRow["综合费率"] = "0";
					dataRow["含量"] = "0";
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}
		private DataTable FormatResourceList3(DataTable Rldt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("清单编号", typeof(string)));
			dataTable.Columns.Add(new DataColumn("清单名称", typeof(string)));
			dataTable.Columns.Add(new DataColumn("费用类别", typeof(string)));
			dataTable.Columns.Add(new DataColumn("资源名称", typeof(string)));
			dataTable.Columns.Add(new DataColumn("单位", typeof(string)));
			dataTable.Columns.Add(new DataColumn("单价", typeof(string)));
			dataTable.Columns.Add(new DataColumn("数量", typeof(string)));
			dataTable.Columns.Add(new DataColumn("金额", typeof(string)));
			dataTable.Columns.Add(new DataColumn("综合费率", typeof(string)));
			dataTable.Columns.Add(new DataColumn("含量", typeof(string)));
			for (int i = 0; i < Rldt.Rows.Count; i++)
			{
				if (!(Rldt.Rows[i][0].ToString() == "") || !(Rldt.Rows[i][1].ToString() == "") || !(Rldt.Rows[i][2].ToString() == "") || !(Rldt.Rows[i][3].ToString() == "") || !(Rldt.Rows[i][4].ToString() == ""))
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["清单编号"] = Rldt.Rows[i][5].ToString();
					dataRow["清单名称"] = "";
					dataRow["费用类别"] = "";
					dataRow["资源名称"] = Rldt.Rows[i][0].ToString();
					dataRow["单位"] = Rldt.Rows[i][2].ToString();
					dataRow["单价"] = Rldt.Rows[i][3].ToString();
					dataRow["数量"] = Rldt.Rows[i][1].ToString();
					dataRow["金额"] = Rldt.Rows[i][4].ToString();
					dataRow["综合费率"] = "0";
					dataRow["含量"] = "0";
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}
		public void TempDataInsert1(DataTable dtTask)
		{
			TempWbsInfoCollection tempWbsInfoCollection = new TempWbsInfoCollection();
			for (int i = 0; i < dtTask.Rows.Count; i++)
			{
				tempWbsInfoCollection.Add(new TempWbsInfo
				{
					ProjectCode = this.PrjCodeValue,
					TaskCode = dtTask.Rows[i]["F2"].ToString().Trim(),
					TaskName = dtTask.Rows[i]["F3"].ToString().Trim(),
					Unit = dtTask.Rows[i]["F4"].ToString().Trim(),
					Quantity = (dtTask.Rows[i]["F5"].ToString().Trim() == "") ? "0" : dtTask.Rows[i]["F5"].ToString().Trim(),
					UnitPrice = (dtTask.Rows[i]["F6"].ToString().Trim() == "") ? "0" : dtTask.Rows[i]["F6"].ToString().Trim(),
					SumPrice = (dtTask.Rows[i]["F7"].ToString().Trim() == "") ? "0" : dtTask.Rows[i]["F7"].ToString().Trim()
				});
			}
			TempResourceInfoCollection resourceList = new TempResourceInfoCollection();
			this.tw.TempTaskInsert(tempWbsInfoCollection, resourceList, this.PrjCodeValue);
		}
		public void TempDataInsert(DataTable dtTask, DataTable dtResource)
		{
			TempWbsInfoCollection tempWbsInfoCollection = new TempWbsInfoCollection();
			for (int i = 0; i < dtTask.Rows.Count; i++)
			{
				tempWbsInfoCollection.Add(new TempWbsInfo
				{
					ProjectCode = this.PrjCodeValue,
					TaskCode = dtTask.Rows[i]["F2"].ToString().Trim(),
					TaskName = dtTask.Rows[i]["F3"].ToString().Trim(),
					Unit = dtTask.Rows[i]["F4"].ToString().Trim(),
					Quantity = (dtTask.Rows[i]["F5"].ToString().Trim() == "") ? "0" : dtTask.Rows[i]["F5"].ToString().Trim(),
					UnitPrice = (dtTask.Rows[i]["F6"].ToString().Trim() == "") ? "0" : dtTask.Rows[i]["F6"].ToString().Trim(),
					SumPrice = (dtTask.Rows[i]["F7"].ToString().Trim() == "") ? "0" : dtTask.Rows[i]["F7"].ToString().Trim(),
					Remark = dtTask.Rows[i]["F8"].ToString().Trim(),
					Content = 0m
				});
			}
			TempResourceInfoCollection tempResourceInfoCollection = new TempResourceInfoCollection();
			for (int j = 0; j < dtResource.Rows.Count; j++)
			{
				TempResourceInfo tempResourceInfo = new TempResourceInfo();
				tempResourceInfo.ListCode = dtResource.Rows[j][0].ToString().Trim();
				tempResourceInfo.ListName = dtResource.Rows[j][1].ToString().Trim();
				tempResourceInfo.FeeStyle = ((dtResource.Rows[j][2].ToString().Trim() == "") ? "0" : dtResource.Rows[j][2].ToString().Trim());
				tempResourceInfo.SourceName = dtResource.Rows[j][3].ToString().Trim();
				tempResourceInfo.SourceUnit = ((dtResource.Rows[j][4].ToString().Trim() == "") ? "0" : dtResource.Rows[j][4].ToString().Trim());
				tempResourceInfo.SourcePrice = ((dtResource.Rows[j][5].ToString().Trim() == "") ? "0" : dtResource.Rows[j][5].ToString().Trim());
				tempResourceInfo.Quantity = ((dtResource.Rows[j][6].ToString().Trim() == "") ? "0" : dtResource.Rows[j][6].ToString().Trim());
				tempResourceInfo.Fee = ((dtResource.Rows[j][7].ToString().Trim() == "") ? "0" : dtResource.Rows[j][7].ToString().Trim());
				tempResourceInfo.Fee1 = ((dtResource.Rows[j][8].ToString().Trim() == "") ? "0" : dtResource.Rows[j][8].ToString().Trim());
				if (this.ImportType != 1 && this.ImportType != 3)
				{
					tempResourceInfo.Content = Convert.ToDecimal((dtResource.Rows[j][9].ToString().Trim() == "") ? "0" : dtResource.Rows[j][9].ToString().Trim());
					tempResourceInfo.Fee = Convert.ToString(Convert.ToDecimal(tempResourceInfo.SourcePrice) * Convert.ToDecimal(tempResourceInfo.Quantity));
				}
				else
				{
					tempResourceInfo.Content = 0m;
				}
				tempResourceInfoCollection.Add(tempResourceInfo);
			}
			this.tw.TempTaskInsert(tempWbsInfoCollection, tempResourceInfoCollection, this.PrjCodeValue, this.ImportType.ToString());
		}
		private string FactaryAl(string al)
		{
			if (al.Trim().Length == 1)
			{
				return "00" + al.Trim().ToString();
			}
			if (al.Trim().Length == 2)
			{
				return "0" + al.Trim().ToString();
			}
			if (al.Trim().Length == 3)
			{
				return al.Trim().ToString();
			}
			return "000";
		}
		private string GetTaskCode(string[] alstr)
		{
			string text = "";
			for (int i = 0; i < alstr.Length; i++)
			{
				text += this.FactaryAl(alstr[i].ToString());
			}
			return text;
		}
		protected void Btn_imp_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.tw.ImportTask(this.PrjCodeValue, this.ImportType))
				{
					this.js.Text = "alert('导入成功！')";
				}
				else
				{
					this.js.Text = "alert('导入失败！')";
				}
			}
			catch
			{
				this.js.Text = "alert('Execl表格有误！')";
			}
			this.Btn_imp.Enabled = false;
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
        public DataTable FormatDtResource(DataTable dtResource)
        {
            base.Response.Write(dtResource.Rows[0]["F2"]);
            DataTable table = new DataTable();
            table.Columns.Add(new DataColumn("清单编号", typeof(string)));
            table.Columns.Add(new DataColumn("清单名称", typeof(string)));
            table.Columns.Add(new DataColumn("费用类别", typeof(string)));
            table.Columns.Add(new DataColumn("资源名称", typeof(string)));
            table.Columns.Add(new DataColumn("单位", typeof(string)));
            table.Columns.Add(new DataColumn("单价", typeof(string)));
            table.Columns.Add(new DataColumn("数量", typeof(string)));
            table.Columns.Add(new DataColumn("金额", typeof(string)));
            table.Columns.Add(new DataColumn("综合费率", typeof(string)));
            string str = "";
            string str2 = "";
            string str3 = "";
            string str4 = "";
            for (int i = 0; i < dtResource.Rows.Count; i++)
            {
                switch (dtResource.Rows[i]["F2"].ToString())
                {
                    case "清单编号":
                        str = dtResource.Rows[i]["F3"].ToString();
                        str2 = dtResource.Rows[i]["F5"].ToString();
                        str4 = this.SearchToll(dtResource, i);
                        break;

                    case "<人工费>":
                        str3 = "人工费";
                        break;

                    case "<材料费>":
                        str3 = "材料费";
                        break;

                    case "<机械费>":
                        str3 = "机械费";
                        break;

                    case "综合费率%":
                    case "项目单价":
                    case "":
                    case "工料机名称":
                    case "工料机合计":
                        break;

                    default:
                        {
                            DataRow row = table.NewRow();
                            row["清单编号"] = str;
                            row["清单名称"] = str2;
                            row["费用类别"] = str3;
                            row["资源名称"] = dtResource.Rows[i]["F2"].ToString();
                            row["单位"] = dtResource.Rows[i]["F3"].ToString();
                            row["单价"] = (dtResource.Rows[i]["F4"].ToString() == "") ? "0" : dtResource.Rows[i]["F4"].ToString();
                            row["数量"] = (dtResource.Rows[i]["F5"].ToString() == "") ? "0" : dtResource.Rows[i]["F5"].ToString();
                            row["金额"] = (dtResource.Rows[i]["F6"].ToString() == "") ? "0" : dtResource.Rows[i]["F6"].ToString();
                            if (str4 == "0")
                            {
                                row["综合费率"] = (dtResource.Rows[i]["F6"].ToString() == "") ? "0" : dtResource.Rows[i]["F6"].ToString();
                            }
                            else
                            {
                                row["综合费率"] = ((Convert.ToDecimal((dtResource.Rows[i]["F6"] == DBNull.Value) ? "0" : dtResource.Rows[i]["F6"]) * Convert.ToDecimal(str4)) / 100M).ToString("#.##");
                            }
                            table.Rows.Add(row);
                            break;
                        }
                }
            }
            return table;
        }
		private string SearchToll(DataTable dtResource, int p)
		{
			string text = "";
			int num = 0;
			int num2 = p;
			while (num2 < dtResource.Rows.Count && num == 0)
			{
				if (dtResource.Rows[num2]["F2"].ToString() == "综合费率%")
				{
					text = dtResource.Rows[num2]["F3"].ToString();
					num = 1;
				}
				num2++;
			}
			if (text.Trim() == "")
			{
				text = "0";
			}
			else
			{
				text = text.Replace("(人工费 ＋ 材料费 ＋ 机械费)×", "");
				text = text.Replace("%", "");
			}
			return text;
		}
		private void Rationdata_bind(string myFileName)
		{
			string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + myFileName + " ;Extended Properties=Excel 8.0;";
			OleDbConnection oleDbConnection = new OleDbConnection(connectionString);
			oleDbConnection.Open();
			try
			{
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter();
				oleDbDataAdapter.SelectCommand = new OleDbCommand("SELECT * FROM   [报表$]", oleDbConnection);
				DataSet dataSet = new DataSet();
				oleDbDataAdapter.Fill(dataSet);
				this.dt = this.FormatTaskList(dataSet.Tables[0]);
				this.dgd_TaskList.DataSource = this.dt;
				this.dgd_TaskList.DataBind();
				this.dt2 = this.FormatResourceList(dataSet.Tables[0]);
				this.dgd_List.DataSource = this.dt2;
				this.dgd_List.DataBind();
				this.TempDataInsert(this.dt, this.dt2);
			}
			catch
			{
				this.Page.RegisterStartupScript("", "<script>alert('请将工作表单命名为报表');</script>");
				return;
			}
			oleDbConnection.Close();
		}
		private DataTable FormatTaskList(DataTable Tdt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("F2", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F3", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F4", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F5", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F6", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F7", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F9", typeof(string)));
			dataTable.Columns.Add(new DataColumn("F8", typeof(string)));
			for (int i = 0; i < Tdt.Rows.Count; i++)
			{
				if (!(Tdt.Rows[i][0].ToString() == "") || !(Tdt.Rows[i][1].ToString() == "") || !(Tdt.Rows[i][2].ToString() == "") || !(Tdt.Rows[i][3].ToString() == "") || !(Tdt.Rows[i][4].ToString() == "") || !(Tdt.Rows[i][5].ToString() == "") || !(Tdt.Rows[i][6].ToString() == "") || !(Tdt.Rows[i][7].ToString() == ""))
				{
					if (Tdt.Rows[i]["序号"].ToString() == "" && Tdt.Rows[i][3].ToString() == "")
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["F2"] = Tdt.Rows[i][1].ToString() + Tdt.Rows[i][0].ToString();
						dataRow["F3"] = Tdt.Rows[i][2].ToString();
						dataRow["F4"] = Tdt.Rows[i][3].ToString();
						dataRow["F5"] = ((Tdt.Rows[i][4].ToString().Trim() == "") ? "1" : (dataRow["F5"] = Tdt.Rows[i][4].ToString().Trim()));
						dataRow["F6"] = Tdt.Rows[i][5].ToString();
						dataRow["F7"] = Tdt.Rows[i][6].ToString();
						dataRow["F8"] = Tdt.Rows[i][7].ToString();
						dataTable.Rows.Add(dataRow);
					}
					if (Tdt.Rows[i]["序号"].ToString() != "")
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["F2"] = Tdt.Rows[i][1].ToString() + Tdt.Rows[i][0].ToString();
						dataRow2["F3"] = Tdt.Rows[i][2].ToString();
						dataRow2["F4"] = Tdt.Rows[i][3].ToString();
						dataRow2["F5"] = Tdt.Rows[i][4].ToString();
						dataRow2["F6"] = Tdt.Rows[i][5].ToString();
						dataRow2["F7"] = Tdt.Rows[i][6].ToString();
						dataRow2["F8"] = Tdt.Rows[i][7].ToString();
						dataTable.Rows.Add(dataRow2);
					}
				}
			}
			return dataTable;
		}
		private DataTable FormatResourceList(DataTable Rldt)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add(new DataColumn("清单编号", typeof(string)));
			dataTable.Columns.Add(new DataColumn("清单名称", typeof(string)));
			dataTable.Columns.Add(new DataColumn("费用类别", typeof(string)));
			dataTable.Columns.Add(new DataColumn("资源名称", typeof(string)));
			dataTable.Columns.Add(new DataColumn("单位", typeof(string)));
			dataTable.Columns.Add(new DataColumn("单价", typeof(string)));
			dataTable.Columns.Add(new DataColumn("数量", typeof(string)));
			dataTable.Columns.Add(new DataColumn("金额", typeof(string)));
			dataTable.Columns.Add(new DataColumn("综合费率", typeof(string)));
			dataTable.Columns.Add(new DataColumn("含量", typeof(string)));
			string value = "";
			string value2 = "";
			for (int i = 0; i < Rldt.Rows.Count; i++)
			{
				if (Rldt.Rows[i][0].ToString() != "")
				{
					value = Rldt.Rows[i][1].ToString() + Rldt.Rows[i][0].ToString();
					value2 = Rldt.Rows[i][2].ToString();
				}
				if ((!(Rldt.Rows[i][0].ToString() == "") || !(Rldt.Rows[i][1].ToString() == "") || !(Rldt.Rows[i][2].ToString() == "") || !(Rldt.Rows[i][3].ToString() == "") || !(Rldt.Rows[i][4].ToString() == "") || !(Rldt.Rows[i][5].ToString() == "") || !(Rldt.Rows[i][6].ToString() == "") || !(Rldt.Rows[i][7].ToString() == "")) && Rldt.Rows[i]["序号"].ToString() == "" && Rldt.Rows[i][3].ToString() != "" && Rldt.Rows[i][4].ToString() != "" && Rldt.Rows[i][5].ToString() != "" && Rldt.Rows[i][6].ToString() != "" && Rldt.Rows[i][7].ToString() != "")
				{
					DataRow dataRow = dataTable.NewRow();
					dataRow["清单编号"] = value;
					dataRow["清单名称"] = value2;
					dataRow["费用类别"] = "";
					dataRow["资源名称"] = Rldt.Rows[i][2].ToString();
					dataRow["单位"] = Rldt.Rows[i][3].ToString();
					dataRow["单价"] = Rldt.Rows[i][6].ToString();
					dataRow["数量"] = Rldt.Rows[i][4].ToString();
					dataRow["金额"] = "";
					dataRow["综合费率"] = Rldt.Rows[i][7].ToString();
					dataRow["含量"] = Rldt.Rows[i][5].ToString();
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}
	}


