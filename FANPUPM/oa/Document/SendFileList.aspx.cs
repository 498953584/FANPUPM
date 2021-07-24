using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SendFileList : BasePage, IRequiresSessionState
	{

		public string CorpCode
		{
			get
			{
				object obj = this.ViewState["CorpCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["CorpCode"] = value;
			}
		}
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = Convert.ToString(this.Session["yhdm"]);
				DataTable dataTable = DocumentAction.QueryCorpCode(this.UserCode);
				if (dataTable.Rows.Count > 0)
				{
					this.CorpCode = dataTable.Rows[0]["CorpCode"].ToString();
				}
				else
				{
					this.CorpCode = "";
				}
				this.btnEdit.Attributes["onclick"] = string.Concat(new string[]
				{
					"openClassEdit(2,'",
					this.CorpCode,
					"','",
					this.UserCode,
					"')"
				});
				this.btnView.Attributes["onclick"] = string.Concat(new string[]
				{
					"openClassEdit(3,'",
					this.CorpCode,
					"','",
					this.UserCode,
					"')"
				});
				this.btnGiveOut.Attributes["onclick"] = "openSelect(1);";
				this.btnSend.Attributes["onclick"] = "openSelect(2);";
				this.btnViewSign.Attributes["onclick"] = "openSelect(3);";
				this.btnOnHole.Attributes["onclick"] = " return confirm('确定归档吗？');";
				this.dgFileList_Bind();
			}
		}
		protected void dgFileList_Bind()
		{
			this.dgFileList.DataSource = DocumentAction.QuerySendFileList(this.CorpCode, 1, this.UserCode);
			this.dgFileList.DataBind();
		}
		protected void dgFileList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				DataRowView dataRowView = (DataRowView)e.Item.DataItem;
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);doClickRow('",
					dataRowView["FileID"].ToString(),
					"','",
					dataRowView["IsPigeonhole"].ToString(),
					"');"
				});
				e.Item.Attributes["onmouseout"] = "OnMouseOverRecord(this);";
				e.Item.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				if (dataRowView["IsPigeonhole"].ToString() == "1")
				{
					e.Item.Cells[9].Text = "已归档";
					return;
				}
				e.Item.Cells[9].Text = "未归档";
				return;
			}
			default:
				return;
			}
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.dgFileList_Bind();
		}
		protected void btnGiveOut_Click(object sender, EventArgs e)
		{
			int num = 0;
			string text = "";
			Guid guid = new Guid(this.hdnFileID.Value);
			string text2 = this.hdnUserCode.Value;
			if (text2 != "")
			{
				int i = text2.IndexOf(",");
				if (i > 0)
				{
					while (i > 0)
					{
						string text3 = text2.Substring(0, i);
						object obj = text;
						text = string.Concat(new object[]
						{
							obj,
							" Insert into OA_Document_SignIn values('",
							guid,
							"','1','",
							text3,
							"',getdate(),null) "
						});
						text2 = text2.Substring(i + 1, text2.Length - i - 1);
						i = text2.IndexOf(",");
						num++;
					}
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						" Insert into OA_Document_SignIn values('",
						guid,
						"','1','",
						text2,
						"',getdate(),null) "
					});
				}
				else
				{
					text = string.Concat(new object[]
					{
						" Insert into OA_Document_SignIn values('",
						guid,
						"','1','",
						text2,
						"',getdate(),null) "
					});
				}
				if (publicDbOpClass.NonQuerySqlString(text))
				{
					this.JS.Text = "alert('文件分发成功！');";
					return;
				}
				this.JS.Text = "alert('文件分发失败！');";
			}
		}
		protected void btnOnHole_Click(object sender, EventArgs e)
		{
			Guid guid = new Guid(this.hdnFileID.Value);
			string text = " Insert Into OA_eFile_Info(corpCode,FileTitle,submitMan,SubmitDate,Remark,FilePath,OriginalName,UserCode,RecordDate) ";
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				" select corpCode,Title,'",
				this.UserCode,
				"',getDate(),Remark,FilePath,OriginalName,UserCode,RecordDate from OA_Document_SendFile where fileID = '",
				guid.ToString(),
				"'"
			});
			text = text + " Update OA_Document_SendFile set IsPigeonhole='1' where fileID = '" + guid.ToString() + "'";
			if (publicDbOpClass.NonQuerySqlString(text))
			{
				this.JS.Text = "alert('文件归档成功！');";
				this.dgFileList_Bind();
				return;
			}
			this.JS.Text = "alert('文件归档失败！');";
		}
	}


