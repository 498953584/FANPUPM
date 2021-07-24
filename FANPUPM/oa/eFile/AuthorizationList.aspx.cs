using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_AuthorizationList : BasePage, IRequiresSessionState
{

	private eFileAuthorizationAction efaa
	{
		get
		{
			return new eFileAuthorizationAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid <> '00000000-0000-0000-0000-000000000000'";
			this.GVProjectFile.DataBind();
		}
	}
	protected void BtnSearch_Click(object sender, EventArgs e)
	{
		if (this.RBSelect.SelectedValue == "0")
		{
			string selectedValue;
			if ((selectedValue = this.DropDownList1.SelectedValue) != null)
			{
				if (selectedValue == "1")
				{
					this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid <> '00000000-0000-0000-0000-000000000000' and [FileTitle] like '%" + this.TxtSearchCount.Text + "%'";
					this.GVProjectFile.DataBind();
					this.GVeFile.DataBind();
					return;
				}
				if (selectedValue == "2")
				{
					this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid <> '00000000-0000-0000-0000-000000000000' ";
					this.GVProjectFile.DataBind();
					return;
				}
				if (!(selectedValue == "3"))
				{
					return;
				}
				this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid <> '00000000-0000-0000-0000-000000000000'";
				this.GVProjectFile.DataBind();
				return;
			}
		}
		else
		{
			string selectedValue2;
			if ((selectedValue2 = this.DropDownList1.SelectedValue) != null)
			{
				if (selectedValue2 == "1")
				{
					this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid = '00000000-0000-0000-0000-000000000000' and [FileTitle] like '%" + this.TxtSearchCount.Text + "%'";
					this.GVeFile.DataBind();
					return;
				}
				if (!(selectedValue2 == "2"))
				{
					return;
				}
				this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid = '00000000-0000-0000-0000-000000000000' ";
				this.GVeFile.DataBind();
			}
		}
	}
	protected void btnAuthorization_Click(object sender, EventArgs e)
	{
		eFileAuthorization eFileAuthorization = new eFileAuthorization();
		if (this.HdnYhdmList.Value != "")
		{
			if (this.RBSelect.SelectedValue == "0")
			{
				for (int i = 0; i < this.GVProjectFile.Rows.Count; i++)
				{
					CheckBox checkBox = (CheckBox)this.GVProjectFile.Rows[i].FindControl("CBRecordID");
					if (checkBox.Checked)
					{
						eFileAuthorization.FileRecordID = Convert.ToInt32(this.GVProjectFile.DataKeys[i]["RecordID"].ToString());
						eFileAuthorization.LeaderCode = this.Session["yhdm"].ToString();
						eFileAuthorization.ReaderCodes = this.HdnYhdmList.Value.ToString();
						this.efaa.Add(eFileAuthorization);
					}
				}
				return;
			}
			for (int j = 0; j < this.GVeFile.Rows.Count; j++)
			{
				CheckBox checkBox2 = (CheckBox)this.GVeFile.Rows[j].FindControl("CBRecordID");
				if (checkBox2.Checked)
				{
					eFileAuthorization.FileRecordID = Convert.ToInt32(this.GVeFile.DataKeys[j]["RecordID"].ToString());
					eFileAuthorization.LeaderCode = this.Session["yhdm"].ToString();
					eFileAuthorization.ReaderCodes = this.HdnYhdmList.Value.ToString();
					this.efaa.Add(eFileAuthorization);
				}
			}
		}
	}
	protected void GVProjectFile_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			string text;
			if ((text = e.Row.Cells[4].Text) != null)
			{
				if (text == "1")
				{
					e.Row.Cells[4].Text = "秘密";
					return;
				}
				if (text == "2")
				{
					e.Row.Cells[4].Text = "机密";
					return;
				}
				if (!(text == "3"))
				{
					return;
				}
				e.Row.Cells[4].Text = "绝密";
			}
		}
	}
	protected void RBSelect_SelectedIndexChanged(object sender, EventArgs e)
	{
		int num = Convert.ToInt32(this.RBSelect.SelectedValue);
		this.MultiView1.ActiveViewIndex = num;
		if (num == 0)
		{
			if (this.DropDownList1.Items.Count < 3)
			{
				this.DropDownList1.Items.Add(new ListItem("项目名称", "3"));
				this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid <> '00000000-0000-0000-0000-000000000000'";
				this.GVProjectFile.DataBind();
				return;
			}
		}
		else
		{
			this.DropDownList1.Items.Remove(new ListItem("项目名称", "3"));
			this.sqlProjecteFile.SelectCommand = "SELECT [RecordID], [RecordType], [CorpCode], [PrjGuid], [FileCode], [Remark], [SubmitDate], [SubmitMan], [FileTitle], [ClassID], [UserCode], [RecordDate], [SaveLimit], [SecretLevel], [OriginalName], [FilePath], [FileCopy], [FileType] FROM [OA_eFile_Info]  where PrjGuid = '00000000-0000-0000-0000-000000000000'";
			this.GVProjectFile.DataBind();
		}
	}
	protected void GVeFile_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			((DataRowView)e.Row.DataItem)["RecordID"].ToString();
			string text;
			if ((text = e.Row.Cells[3].Text) != null)
			{
				if (text == "1")
				{
					e.Row.Cells[3].Text = "秘密";
					return;
				}
				if (text == "2")
				{
					e.Row.Cells[3].Text = "机密";
					return;
				}
				if (!(text == "3"))
				{
					return;
				}
				e.Row.Cells[3].Text = "绝密";
			}
		}
	}
}


