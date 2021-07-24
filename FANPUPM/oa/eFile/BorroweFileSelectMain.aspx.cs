using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_BorroweFileSelectMain : BasePage, IRequiresSessionState
{
	private DataTable eFileInfoDT = new DataTable();

	public string ClassTypeCode
	{
		get
		{
			return this.ViewState["CLASSTYPECODE"].ToString();
		}
		set
		{
			this.ViewState["CLASSTYPECODE"] = value;
		}
	}
	private PTMultiClassesAction pca
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}
	private eFileInfoAction efia
	{
		get
		{
			return new eFileInfoAction();
		}
	}
	private eFileLendAction efla
	{
		get
		{
			return new eFileLendAction();
		}
	}
	protected Guid ProjectCode
	{
		get
		{
			return (Guid)this.ViewState["PROJECTCODE"];
		}
		set
		{
			this.ViewState["PROJECTCODE"] = value;
		}
	}
	protected int ClassID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["CLASSID"].ToString());
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		this.eFileInfo();
		if (!base.IsPostBack)
		{
			this.Session["eFileInfoDT"] = this.eFileInfoDT;
			if (base.Request["p"] != null || base.Request["cd"] != null)
			{
				this.ClassID = Convert.ToInt32(base.Request["cd"]);
				if (base.Request["pc"] != null)
				{
					this.ProjectCode = new Guid(base.Request["pc"]);
				}
				else
				{
					this.ProjectCode = Guid.Empty;
				}
				if (base.Request["p"].ToString() == "1")
				{
					this.LbTilteName.Text = "项目类档案记录清单";
					this.ClassTypeCode = "005";
					SqlDataSource expr_F5 = this.sqlBulletin;
					object selectCommand = expr_F5.SelectCommand;
					expr_F5.SelectCommand = string.Concat(new object[]
					{
						selectCommand,
						" and RecordType = 1 and PrjGuid = '",
						this.ProjectCode,
						"'"
					});
				}
				else
				{
					this.LbTilteName.Text = "管理类档案记录清单";
					this.ClassTypeCode = "004";
					SqlDataSource expr_153 = this.sqlBulletin;
					expr_153.SelectCommand += " and RecordType = 2 ";
				}
			}
			this.GVSelecteFile.DataSource = this.eFileInfoDT;
			this.GVSelecteFile.DataBind();
		}
	}
	private void eFileInfo()
	{
		this.eFileInfoDT.Columns.Add(new DataColumn("RecordID", typeof(int)));
		this.eFileInfoDT.Columns.Add(new DataColumn("RecordType", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("CorpCode", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("PrjGuid", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("FileTitle", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("SubmitMan", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("SubmitDate", typeof(DateTime)));
		this.eFileInfoDT.Columns.Add(new DataColumn("Remark", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("FileCode", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("ClassID", typeof(int)));
		this.eFileInfoDT.Columns.Add(new DataColumn("UserCode", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("RecordDate", typeof(DateTime)));
		this.eFileInfoDT.Columns.Add(new DataColumn("SaveLimit", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("SecretLevel", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("FileType", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("FileCopy", typeof(string)));
		this.eFileInfoDT.Columns.Add(new DataColumn("PlanReturnDate", typeof(string)));
	}
	protected void GVeFileInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[3].Text = userManageDb.GetUserName(e.Row.Cells[3].Text);
			string text;
			if ((text = e.Row.Cells[7].Text) != null)
			{
				if (text == "1")
				{
					e.Row.Cells[7].Text = "秘密";
					return;
				}
				if (text == "2")
				{
					e.Row.Cells[7].Text = "机密";
					return;
				}
				if (!(text == "3"))
				{
					return;
				}
				e.Row.Cells[7].Text = "绝密";
			}
		}
	}
	protected void BtnSelAdd_Click(object sender, EventArgs e)
	{
		string text = "";
		for (int i = 0; i < this.GVeFileInfo.Rows.Count; i++)
		{
			CheckBox checkBox = (CheckBox)this.GVeFileInfo.Rows[i].FindControl("CBeFile");
			if (checkBox.Checked)
			{
				text = text + this.GVeFileInfo.DataKeys[i]["RecordID"].ToString() + ",";
			}
		}
		if (text == "")
		{
			return;
		}
		DataTable dataTable = (DataTable)this.Session["eFileInfoDT"];
		for (int j = 0; j < dataTable.Rows.Count; j++)
		{
			DataRow dataRow = this.eFileInfoDT.NewRow();
			dataRow["RecordID"] = Convert.ToInt32(dataTable.Rows[j]["RecordID"]);
			dataRow["RecordType"] = dataTable.Rows[j]["RecordType"].ToString();
			dataRow["CorpCode"] = dataTable.Rows[j]["CorpCode"].ToString();
			dataRow["PrjGuid"] = dataTable.Rows[j]["PrjGuid"].ToString();
			dataRow["FileTitle"] = dataTable.Rows[j]["FileTitle"].ToString();
			dataRow["SubmitMan"] = dataTable.Rows[j]["SubmitMan"].ToString();
			dataRow["SubmitDate"] = Convert.ToDateTime(dataTable.Rows[j]["SubmitDate"].ToString());
			dataRow["Remark"] = dataTable.Rows[j]["Remark"].ToString();
			dataRow["FileCode"] = dataTable.Rows[j]["FileCode"].ToString();
			dataRow["ClassID"] = Convert.ToInt32(dataTable.Rows[j]["ClassID"]);
			dataRow["UserCode"] = dataTable.Rows[j]["UserCode"].ToString();
			dataRow["RecordDate"] = Convert.ToDateTime(dataTable.Rows[j]["RecordDate"].ToString());
			dataRow["SaveLimit"] = dataTable.Rows[j]["SaveLimit"].ToString();
			dataRow["SecretLevel"] = dataTable.Rows[j]["SecretLevel"].ToString();
			dataRow["FileType"] = dataTable.Rows[j]["FileType"].ToString();
			dataRow["FileCopy"] = dataTable.Rows[j]["FileCopy"].ToString();
			for (int k = 0; k < this.GVSelecteFile.Rows.Count; k++)
			{
				if (this.GVSelecteFile.DataKeys[k]["RecordID"].ToString() == dataTable.Rows[j]["RecordID"].ToString())
				{
					TextBox textBox = (TextBox)this.GVSelecteFile.Rows[k].FindControl("DBPlanReturnDate");
					dataRow["PlanReturnDate"] = textBox.Text;
					break;
				}
			}
			this.eFileInfoDT.Rows.Add(dataRow);
		}
		text = text.Substring(0, text.Length - 1);
		DataTable list = this.efia.GetList(" RecordID in (" + text + ")");
		for (int l = 0; l < list.Rows.Count; l++)
		{
			DataRow dataRow = this.eFileInfoDT.NewRow();
			if (this.IsRecordID(list.Rows[l]["RecordID"].ToString()))
			{
				break;
			}
			dataRow["RecordID"] = Convert.ToInt32(list.Rows[l]["RecordID"]);
			dataRow["RecordType"] = list.Rows[l]["RecordType"].ToString();
			dataRow["CorpCode"] = list.Rows[l]["CorpCode"].ToString();
			dataRow["PrjGuid"] = list.Rows[l]["PrjGuid"].ToString();
			dataRow["FileTitle"] = list.Rows[l]["FileTitle"].ToString();
			dataRow["SubmitMan"] = list.Rows[l]["SubmitMan"].ToString();
			dataRow["SubmitDate"] = Convert.ToDateTime(list.Rows[l]["SubmitDate"].ToString());
			dataRow["Remark"] = list.Rows[l]["Remark"].ToString();
			dataRow["FileCode"] = list.Rows[l]["FileCode"].ToString();
			dataRow["ClassID"] = Convert.ToInt32(list.Rows[l]["ClassID"]);
			dataRow["UserCode"] = list.Rows[l]["UserCode"].ToString();
			dataRow["RecordDate"] = Convert.ToDateTime(list.Rows[l]["RecordDate"].ToString());
			dataRow["SaveLimit"] = list.Rows[l]["SaveLimit"].ToString();
			dataRow["SecretLevel"] = list.Rows[l]["SecretLevel"].ToString();
			dataRow["FileType"] = list.Rows[l]["FileType"].ToString();
			dataRow["FileCopy"] = list.Rows[l]["FileCopy"].ToString();
			dataRow["PlanReturnDate"] = DateTime.Now.ToShortDateString();
			this.eFileInfoDT.Rows.Add(dataRow);
		}
		this.Session["eFileInfoDT"] = this.eFileInfoDT;
		this.GVSelecteFile.DataSource = (DataTable)this.Session["eFileInfoDT"];
		this.GVSelecteFile.DataBind();
	}
	protected void GVSelecteFile_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			userManageDb userManageDb = new userManageDb();
			e.Row.Cells[3].Text = userManageDb.GetUserName(e.Row.Cells[3].Text);
			string text;
			if ((text = e.Row.Cells[7].Text) != null)
			{
				if (!(text == "1"))
				{
					if (!(text == "2"))
					{
						if (text == "3")
						{
							e.Row.Cells[7].Text = "绝密";
						}
					}
					else
					{
						e.Row.Cells[7].Text = "机密";
					}
				}
				else
				{
					e.Row.Cells[7].Text = "秘密";
				}
			}
			TextBox textBox = (TextBox)e.Row.Cells[8].Controls[1];
			textBox.Text = ((DataRowView)e.Row.DataItem)["PlanReturnDate"].ToString();
		}
	}
	protected void BtnConfirm_Click(object sender, EventArgs e)
	{
		eFileLend eFileLend = new eFileLend();
		DataTable dataTable = (DataTable)this.Session["eFileInfoDT"];
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			eFileLend.AuditState = -1;
			eFileLend.BorrowMan = this.Session["yhdm"].ToString();
			eFileLend.FileRecordID = Convert.ToInt32(dataTable.Rows[i]["RecordID"].ToString());
			eFileLend.LendDate = DateTime.Now;
			if (dataTable.Rows[i]["SecretLevel"].ToString() == "1")
			{
				eFileLend.LendState = "0";
			}
			else
			{
				eFileLend.LendState = "1";
			}
			eFileLend.PlanReturnDate = Convert.ToDateTime(dataTable.Rows[i]["PlanReturnDate"]);
			eFileLend.RecordID = Guid.NewGuid();
			this.efla.Add(eFileLend);
		}
		this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "returnValue=true;window.close();", true);
	}
	protected void BtnDel_Click(object sender, EventArgs e)
	{
		DataTable dataTable = (DataTable)this.Session["eFileInfoDT"];
		for (int i = 0; i < this.GVSelecteFile.Rows.Count; i++)
		{
			CheckBox checkBox = (CheckBox)this.GVSelecteFile.Rows[i].FindControl("CBRecordID");
			if (checkBox.Checked)
			{
				this.GVSelecteFile.DataKeys[i]["RecordID"].ToString();
				for (int j = 0; j < dataTable.Rows.Count; j++)
				{
					if (dataTable.Rows[j]["RecordID"].ToString() == this.GVSelecteFile.DataKeys[i]["RecordID"].ToString())
					{
						dataTable.Rows[j].Delete();
					}
				}
			}
		}
		this.Session["eFileInfoDT"] = dataTable;
		this.GVSelecteFile.DataSource = dataTable;
		this.GVSelecteFile.DataBind();
	}
	private bool IsRecordID(string recordid)
	{
		DataTable dataTable = (DataTable)this.Session["eFileInfoDT"];
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dataTable.Rows[i]["RecordID"].ToString() == recordid)
			{
				return true;
			}
		}
		return false;
	}
}


